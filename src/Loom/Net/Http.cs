#region Using Directives

using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

#endregion

namespace Loom.Net
{
    /// <summary>
    ///     An HTTP wrapper class that abstracts away the common needs for adding post keys
    ///     and firing update events as data is received. This class is real easy to use
    ///     with many operations requiring single method calls.
    /// </summary>
    internal class Http
    {
        #region Delegates

        public delegate void ReceiveDataDelegate(object sender, ReceiveDataEventArgs e);

        #endregion

        private readonly string multiPartBoundary = "-----------------------------" + DateTime.Now.Ticks.ToString("x");

        private CookieCollection cookies;

        private BinaryWriter postData;

        private MemoryStream postStream;

        public Http()
        {
            Username = string.Empty;
            UserAgent = "Colossus HTTP .NET Client";
            Timeout = 30;
            ProxyUsername = string.Empty;
            ProxyPassword = string.Empty;
            ProxyBypass = string.Empty;
            ProxyAddress = string.Empty;
            PostMode = HttpPostMode.UrlEncoded;
            Password = string.Empty;
            ErrorMessage = string.Empty;
            BufferSize = 100;
        }

        /// <summary>
        ///     The buffersize used for the Send and Receive operations
        /// </summary>
        public int BufferSize { get; set; }

        /// <summary>
        ///     Returns whether the last request was cancelled through one of the
        ///     events.
        /// </summary>
        public bool Cancelled { get; set; }

        /// <summary>
        ///     Holds the internal Cookie collection before or after a request. This
        ///     collection is used only if HandleCookies is set to .t. which also causes it
        ///     to capture cookies and repost them on the next request.
        /// </summary>
        public CookieCollection Cookies
        {
            get
            {
                if (cookies == null)
                    Cookies = new CookieCollection();

                return cookies;
            }
            set => cookies = value;
        }

        /// <summary>
        ///     Error flag if an error occurred.
        /// </summary>
        public bool Error { get; set; }

        /// <summary>
        ///     Error Message if the Error Flag is set or an error value is returned from a method.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        ///     If set to a non-zero value will automatically track cookies.
        /// </summary>
        public bool HandleCookies { get; set; }

        /// <summary>
        ///     Password for Authentication.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///     Determines how data is POSTed when when using AddPostKey() and other methods
        ///     of posting data to the server. Support UrlEncoded, Multi-Part, XML and Raw modes.
        /// </summary>
        public HttpPostMode PostMode { get; set; }

        /// <summary>
        ///     Address of the Proxy Server to be used.
        ///     Use optional DEFAULTPROXY value to specify that you want to IE's Proxy Settings
        /// </summary>
        public string ProxyAddress { get; set; }

        /// <summary>
        ///     Semicolon separated Address list of the servers the proxy is not used for.
        /// </summary>
        public string ProxyBypass { get; set; }

        /// <summary>
        ///     Password for a password validating Proxy. Only used if the proxy info is set.
        /// </summary>
        public string ProxyPassword { get; set; }

        /// <summary>
        ///     Username for a password validating Proxy. Only used if the proxy info is set.
        /// </summary>
        public string ProxyUsername { get; set; }

        /// <summary>
        ///     Determines whether errors cause exceptions to be thrown. By default errors
        ///     are handled in the class and the Error property is set for error conditions.
        ///     (not implemented at this time).
        /// </summary>
        public bool ThrowExceptions { get; set; }

        /// <summary>
        ///     Timeout for the Web request in seconds. Times out on connection, read and send operations.
        ///     Default is 30 seconds.
        /// </summary>
        public int Timeout { get; set; }

        /// <summary>
        ///     Lets you specify the User Agent  browser string that is sent to the server.
        ///     This allows you to simulate a specific browser if necessary.
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        ///     User name used for Authentication.
        ///     To use the currently logged in user when accessing an NTLM resource you can use "AUTOLOGIN".
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     WebRequest object that can be manipulated and set up for the request if you
        ///     called .
        ///     Note: This object must be recreated and reset for each request, since a
        ///     request's life time is tied to a single request. This object is not used if
        ///     you specify a URL on any of the GetUrl methods since this causes a default
        ///     WebRequest to be created.
        /// </summary>
        public HttpWebRequest WebRequest { get; set; }

        /// <summary>
        ///     WebResponse object that is accessible after the request is complete and
        ///     allows you to retrieve additional information about the completed request.
        ///     The Response Stream is already closed after the GetUrl methods complete
        ///     (except GetUrlResponse()) but you can access the Response object members
        ///     and collections to retrieve more detailed information about the current
        ///     request that completed.
        /// </summary>
        public HttpWebResponse WebResponse { get; set; }

        public static string UrlEncode(string inputString)
        {
            StringReader sr = new StringReader(inputString);
            StringBuilder sb = new StringBuilder(inputString.Length);

            while (true)
            {
                int lnVal = sr.Read();
                if (lnVal == -1)
                    break;
                char lcChar = (char) lnVal;

                if (lcChar >= 'a' && lcChar < 'z' ||
                    lcChar >= 'A' && lcChar < 'Z' ||
                    lcChar >= '0' && lcChar < '9')
                    sb.Append(lcChar);
                else if (lcChar == ' ')
                    sb.Append("+");
                else
                    sb.AppendFormat("%{0:X2}", lnVal);
            }

            return sb.ToString();
        }

        /// <summary>
        ///     Fires progress events when receiving data from the server
        /// </summary>
        public event ReceiveDataDelegate ReceiveData;

        /// <summary>
        ///     Fires progress events when using GetUrlEvents() to retrieve a URL.
        /// </summary>
        public event ReceiveDataDelegate SendData;

        /// <summary>
        ///     Creates a new WebRequest instance that can be set prior to calling the
        ///     various Get methods. You can then manipulate the WebRequest property, to
        ///     custom configure the request.
        ///     Instead of passing a URL you  can then pass null.
        ///     Note - You need a new Web Request for each and every request so you need to
        ///     set this object for every call if you manually customize it.
        /// </summary>
        /// <param name="url">
        ///     The url to access with this WebRequest
        /// </param>
        /// <returns>Boolean</returns>
        public bool CreateWebRequestObject(string url)
        {
            try
            {
                WebRequest = (HttpWebRequest) System.Net.WebRequest.Create(url);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Adds POST form variables to the request buffer.
        ///     PostMode determines how parms are handled.
        /// </summary>
        /// <param name="key">Key value or raw buffer depending on post type</param>
        /// <param name="value">Value to store. Used only in key/value pair modes</param>
        public void AddPostKey(string key, byte[] value)
        {
            if (postData == null)
            {
                postStream = new MemoryStream();
                postData = new BinaryWriter(postStream);
            }

            if (key == "RESET")
            {
                postStream = new MemoryStream();
                postData = new BinaryWriter(postStream);
            }

            switch (PostMode)
            {
                case HttpPostMode.UrlEncoded:
                    postData.Write(Encoding.GetEncoding(1252).GetBytes(key + "=" +
                                                                       UrlEncode(Encoding.GetEncoding(1252).GetString(value)) +
                                                                       "&"));
                    break;
                case HttpPostMode.MultiPart:
                    postData.Write(Encoding.GetEncoding(1252).GetBytes(
                        "--" + multiPartBoundary + "\r\n" +
                        "Content-Disposition: form-data; name=\"" + key + "\"\r\n\r\n"));

                    postData.Write(value);

                    postData.Write(Encoding.GetEncoding(1252).GetBytes("\r\n"));
                    break;
                default: // Raw or Xml modes
                    postData.Write(value);
                    break;
            }
        }

        /// <summary>
        ///     Adds POST form variables to the request buffer.
        ///     PostMode determines how parms are handled.
        /// </summary>
        /// <param name="key">Key value or raw buffer depending on post type</param>
        /// <param name="value">Value to store. Used only in key/value pair modes</param>
        public void AddPostKey(string key, string value)
        {
            AddPostKey(key, Encoding.GetEncoding(1252).GetBytes(value));
        }

        /// <summary>
        ///     Adds a fully self contained POST buffer to the request.
        ///     Works for XML or previously encoded content.
        /// </summary>
        /// <param name="fullPostBuffer">String based full POST buffer</param>
        public void AddPostKey(string fullPostBuffer)
        {
            AddPostKey(null, fullPostBuffer);
        }

        /// <summary>
        ///     Adds a fully self contained POST buffer to the request.
        ///     Works for XML or previously encoded content.
        /// </summary>
        /// <param name="fullPostBuffer">Byte array of a full POST buffer</param>
        public void AddPostKey(byte[] fullPostBuffer)
        {
            AddPostKey(null, fullPostBuffer);
        }

        /// <summary>
        ///     Allows posting a file to the Web Server. Make sure that you
        ///     set PostMode
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool AddPostFile(string key, string fileName)
        {
            byte[] lcFile;

            if (PostMode != HttpPostMode.MultiPart)
            {
                ErrorMessage = "File upload allowed only with Multi-part forms";
                Error = true;
                return false;
            }

            try
            {
                FileStream loFile = new FileStream(fileName, FileMode.Open, FileAccess.Read);

                lcFile = new byte[loFile.Length];
                loFile.Read(lcFile, 0, (int) loFile.Length);
                loFile.Close();
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
                Error = true;
                return false;
            }

            if (postData == null)
            {
                postStream = new MemoryStream();
                postData = new BinaryWriter(postStream);
            }

            postData.Write(Encoding.GetEncoding(1252).GetBytes(
                "--" + multiPartBoundary + "\r\n" +
                "Content-Disposition: form-data; name=\"" + key + "\"; filename=\"" +
                new FileInfo(fileName).Name + "\"\r\n\r\n"));

            postData.Write(lcFile);

            postData.Write(Encoding.GetEncoding(1252).GetBytes("\r\n"));

            return true;
        }

        /// <summary>
        ///     Return a the result from an HTTP url into a StreamReader.
        ///     Client code should call Close() on the returned object when done reading.
        /// </summary>
        /// <param name="url">Url to retrieve.</param>
        /// <returns></returns>
        public StreamReader GetUrlStream(string url)
        {
            Encoding enc;

            HttpWebResponse response = GetUrlResponse(url);
            if (response == null)
                return null;

            try
            {
                enc = !string.IsNullOrEmpty(response.CharacterSet)
                    ? Encoding.GetEncoding(response.CharacterSet)
                    : Encoding.GetEncoding(1252);
            }
            catch
            {
                // *** Invalid encoding passed
                enc = Encoding.GetEncoding(1252);
            }

            // *** drag to a stream
            StreamReader strResponse = new StreamReader(response.GetResponseStream(), enc);
            return strResponse;
        }

        /// <summary>
        ///     Return an HttpWebResponse object for a request. You can use the Response to
        ///     read the result as needed. This is a low level method. Most of the other 'Get'
        ///     methods call this method and process the results further.
        /// </summary>
        /// <remarks>Important: The Response object's Close() method must be called when you are done with the object.</remarks>
        /// <param name="url">Url to retrieve.</param>
        /// <returns>An HttpWebResponse Object</returns>
        public HttpWebResponse GetUrlResponse(string url)
        {
            Cancelled = false;

            try
            {
                Error = false;
                ErrorMessage = string.Empty;
                Cancelled = false;

                if (WebRequest == null)
                {
                    WebRequest = (HttpWebRequest) System.Net.WebRequest.Create(url);
                    WebRequest.Headers.Add("Cache", "no-cache");
                }

                WebRequest.UserAgent = UserAgent;
                WebRequest.Timeout = Timeout * 1000;

                // *** Handle Security for the request
                if (Username.Length > 0)
                    WebRequest.Credentials = Username == "AUTOLOGIN"
                        ? CredentialCache.DefaultCredentials
                        : new NetworkCredential(Username, Password);

                // *** Handle Proxy Server configuration
                if (ProxyAddress.Length > 0)
                    if (ProxyAddress == "DEFAULTPROXY")
                    {
                        WebRequest.Proxy = System.Net.WebRequest.DefaultWebProxy;
                    }
                    else
                    {
                        WebProxy proxy = new WebProxy(ProxyAddress, true);

                        if (ProxyBypass.Length > 0)
                            proxy.BypassList = ProxyBypass.Split(';');

                        if (ProxyUsername.Length > 0)
                            proxy.Credentials = new NetworkCredential(ProxyUsername, ProxyPassword);

                        WebRequest.Proxy = proxy;
                    }

                // *** Handle cookies - automatically re-assign 
                if (HandleCookies || cookies != null && cookies.Count > 0)
                {
                    WebRequest.CookieContainer = new CookieContainer();
                    if (cookies != null && cookies.Count > 0)
                        WebRequest.CookieContainer.Add(cookies);
                }

                // *** Deal with the POST buffer if any
                if (postData != null)
                {
                    WebRequest.Method = "POST";
                    switch (PostMode)
                    {
                        case HttpPostMode.UrlEncoded:
                            WebRequest.ContentType = "application/x-www-form-urlencoded";
                            break;
                        case HttpPostMode.MultiPart:
                            WebRequest.ContentType = "multipart/form-data; boundary=" + multiPartBoundary;
                            postData.Write(Encoding.GetEncoding(1252).GetBytes("--" + multiPartBoundary + "--\r\n"));
                            break;
                        case HttpPostMode.Xml:
                            WebRequest.ContentType = "text/xml";
                            break;
                        case HttpPostMode.Raw:
                            WebRequest.ContentType = "application/octet-stream";
                            break;
                        default:
                            goto case HttpPostMode.UrlEncoded;
                    }

                    Stream loPostData = WebRequest.GetRequestStream();

                    if (SendData == null)
                        postStream.WriteTo(loPostData); // Simplest version - no events
                    else
                        StreamPostBuffer(loPostData); // Send in chunks and fire events

                    //*** Close the memory stream
                    postStream.Close();
                    postStream = null;

                    //*** Close the Binary Writer
                    postData.Close();
                    postData = null;

                    //*** Close Request Stream
                    loPostData.Close();

                    // *** If user cancelled the 'upload' exit
                    if (Cancelled)
                    {
                        ErrorMessage = "HTTP Request was cancelled.";
                        Error = true;
                        return null;
                    }
                }

                // *** Retrieve the response headers 
                HttpWebResponse response;
                try
                {
                    response = (HttpWebResponse) WebRequest.GetResponse();
                }
                catch (WebException ex)
                {
                    // *** Check for 500 error return - if so we still want to return a response
                    // *** Client can check oHttp.WebResponse.StatusCode
                    if (ex.Status == WebExceptionStatus.ProtocolError)
                        response = (HttpWebResponse) ex.Response;
                    else
                        throw;
                }

                WebResponse = response;

                // *** Close out the request - it cannot be reused
                WebRequest = null;

                // ** Save cookies the server sends
                if (HandleCookies)
                    if (response.Cookies.Count > 0)
                        if (cookies == null)
                            cookies = response.Cookies;
                        else
                            foreach (Cookie oRespCookie in response.Cookies)
                            {
                                bool bMatch = false;
                                foreach (Cookie oReqCookie in cookies)
                                    if (oReqCookie.Name == oRespCookie.Name)
                                    {
                                        oReqCookie.Value = oRespCookie.Value;
                                        bMatch = true;
                                        break; // 
                                    }
                                if (!bMatch)
                                    cookies.Add(oRespCookie);
                            } // for each Response.Cookies

                return response;
            }
            catch (Exception e)
            {
                if (ThrowExceptions)
                    throw;

                ErrorMessage = e.Message;
                Error = true;
                return null;
            }
        }

        /// <summary>
        ///     Retrieves the content of a url into a string.
        /// </summary>
        /// <remarks>Fires the ReceiveData event</remarks>
        /// <param name="url">Url to retrieve</param>
        /// <param name="bufferSize">Optional ReadBuffer Size</param>
        /// <returns></returns>
        public string GetUrl(string url, long bufferSize = 8192)
        {
            StreamReader httpResponse = GetUrlStream(url);
            if (httpResponse == null)
                return "";

            long size = WebResponse.ContentLength > 0 ? WebResponse.ContentLength : 0;

            StringBuilder writer = new StringBuilder((int) size);

            char[] lcTemp = new char[bufferSize];

            ReceiveDataEventArgs args = new ReceiveDataEventArgs();
            args.TotalBytes = size;

            size = 1;
            int lnCount = 0;
            long lnTotalBytes = 0;

            while (size > 0)
            {
                Thread.Sleep(1); // Give up a timeslice

                size = httpResponse.Read(lcTemp, 0, (int) bufferSize);
                if (size <= 0)
                    continue;

                writer.Append(lcTemp, 0, (int) size);
                lnCount++;
                lnTotalBytes += size;

                // *** Raise an event if hooked up
                if (ReceiveData == null)
                    continue;

                // *** Update the event handler
                args.CurrentByteCount = lnTotalBytes;
                args.NumberOfReads = lnCount;
                args.CurrentChunk = lcTemp;
                ReceiveData(this, args);

                // *** Check for cancelled flag
                if (!args.Cancel)
                    continue;

                Cancelled = true;
                break;
            }

            httpResponse.Close();

            // *** Send Done notification
            if (ReceiveData != null && !args.Cancel)
            {
                // *** Update the event handler
                args.Done = true;
                ReceiveData(this, args);
            }

            return writer.ToString();
        }

        /// <summary>
        ///     Returns a partial response from the URL by specifying only
        ///     given number of bytes to retrieve. This can reduce network
        ///     traffic and keep string formatting down if you are only
        ///     interested a small port at the top of the page. Also
        ///     returns full headers.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public string GetUrlPartial(string url, int size)
        {
            StreamReader reader = GetUrlStream(url);
            if (reader == null)
                return null;

            char[] inBuffer = new char[size];

            reader.Read(inBuffer, 0, size);
            reader.Close();

            return new string(inBuffer);
        }

        /// <summary>
        ///     Retrieves URL into an Byte Array.
        /// </summary>
        /// <remarks>Fires the ReceiveData Event</remarks>
        /// <param name="url">Url to read</param>
        /// <param name="bufferSize">Size of the buffer for each read. 0 = 8192</param>
        /// <returns></returns>
        public byte[] GetUrlBytes(string url, long bufferSize)
        {
            HttpWebResponse webResponse = GetUrlResponse(url);
            if (webResponse == null)
                return null;

            BinaryReader responseStream =
                new BinaryReader(webResponse.GetResponseStream());

            if (bufferSize < 1)
                bufferSize = 8192;

            long size = webResponse.ContentLength > 0 ? WebResponse.ContentLength : 0;

            byte[] result = new byte[size];

            ReceiveDataEventArgs args = new ReceiveDataEventArgs();
            args.TotalBytes = size;

            size = 1;
            int count = 0;
            long totalBytes = 0;

            while (size > 0)
            {
                if (totalBytes + bufferSize > WebResponse.ContentLength)
                    bufferSize = WebResponse.ContentLength - totalBytes;

                size = responseStream.Read(result, (int) totalBytes, (int) bufferSize);
                if (size > 0)
                {
                    count++;
                    totalBytes += size;

                    // *** Raise an event if hooked up
                    if (ReceiveData != null)
                    {
                        // *** Update the event handler
                        args.CurrentByteCount = totalBytes;
                        args.NumberOfReads = count;
                        args.CurrentChunk = null; // don't send anything here
                        ReceiveData(this, args);

                        // *** Check for cancelled flag
                        if (args.Cancel)
                        {
                            Cancelled = true;
                            goto CloseDown;
                        }
                    }
                }
            } // while

            CloseDown:
            responseStream.Dispose();

            // *** Send Done notification
            if (ReceiveData != null && !args.Cancel)
            {
                // *** Update the event handler
                args.Done = true;
                ReceiveData(this, args);
            }

            return result;
        }

        /// <summary>
        ///     Writes the output from the URL request to a file firing events.
        /// </summary>
        /// <param name="url">Url to fire</param>
        /// <param name="bufferSize">Buffersize - how often to fire events</param>
        /// <param name="outputFile">File to write response to</param>
        /// <returns>true or false</returns>
        public bool GetUrlFile(string url, long bufferSize, string outputFile)
        {
            byte[] result = GetUrlBytes(url, bufferSize);
            if (result == null)
                return false;

            FileStream file = new FileStream(outputFile, FileMode.OpenOrCreate, FileAccess.Write);
            file.Write(result, 0, (int) WebResponse.ContentLength);
            file.Close();

            return true;
        }

        /// <summary>
        ///     Sends the Postbuffer to the server
        /// </summary>
        /// <param name="stream"></param>
        protected void StreamPostBuffer(Stream stream)
        {
            if (postStream.Length < BufferSize)
            {
                postStream.WriteTo(stream);

                // *** Handle Send Data Even
                // *** Here just let it know we're done
                if (SendData != null)
                {
                    ReceiveDataEventArgs args = new ReceiveDataEventArgs();
                    args.CurrentByteCount = postStream.Length;
                    args.Done = true;
                    SendData(this, args);
                }
            }
            else
            {
                // *** Send data up in 8k blocks
                byte[] buffer = postStream.GetBuffer();
                int sent = 0;
                int toSend = (int) postStream.Length;
                int current = 1;
                while (true)
                {
                    if (toSend < 1 || current < 1)
                    {
                        if (SendData != null)
                        {
                            ReceiveDataEventArgs receiveDataEventArgs = new ReceiveDataEventArgs();
                            receiveDataEventArgs.CurrentByteCount = sent;
                            receiveDataEventArgs.TotalBytes = buffer.Length;
                            receiveDataEventArgs.Done = true;
                            SendData(this, receiveDataEventArgs);
                        }
                        break;
                    }

                    current = toSend;

                    if (current > BufferSize)
                    {
                        current = BufferSize;
                        toSend = toSend - current;
                    }
                    else
                    {
                        toSend = toSend - current;
                    }

                    stream.Write(buffer, sent, current);

                    sent = sent + current;

                    if (SendData == null)
                        continue;

                    ReceiveDataEventArgs args = new ReceiveDataEventArgs();
                    args.CurrentByteCount = sent;
                    args.TotalBytes = buffer.Length;
                    if (buffer.Length == sent)
                    {
                        args.Done = true;
                        SendData(this, args);
                        break;
                    }
                    SendData(this, args);

                    if (!args.Cancel)
                        continue;

                    Cancelled = true;
                    break;
                }
            }
        }

        #region Nested type: ReceiveDataEventArgs

        /// <summary>
        ///     Event arguments passed to the ReceiveData event handler on each block of data sent
        /// </summary>
        public class ReceiveDataEventArgs : EventArgs
        {
            /// <summary>
            ///     Flag to specify that you want the current request to cancel. This is a write-only flag
            /// </summary>
            public bool Cancel { get; set; }

            /// <summary>
            ///     Size of the cumulative bytes read in this request
            /// </summary>
            public long CurrentByteCount { get; set; }

            /// <summary>
            ///     The current chunk of data being read
            /// </summary>
            public char[] CurrentChunk { get; set; }

            /// <summary>
            ///     Flag set if the request is currently done.
            /// </summary>
            public bool Done { get; set; }

            /// <summary>
            ///     The number of reads that have occurred - how often has this event been called.
            /// </summary>
            public int NumberOfReads { get; set; }

            /// <summary>
            ///     The number of total bytes of this request
            /// </summary>
            public long TotalBytes { get; set; }
        }

        #endregion
    }

    /// <summary>
    ///     Enumeration of the various HTTP POST modes supported by wwHttp
    /// </summary>
    public enum HttpPostMode
    {
        UrlEncoded,
        MultiPart,
        Xml,
        Raw
    }
}