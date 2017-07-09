#region License information

/******************************************************************
 * Copyright © 2004 Brian Scott (DevInterop)
 * All Rights Reserved
 * 
 * Unauthorized reproduction or distribution in source or compiled
 * form is strictly prohibited.
 * 
 * http://www.devinterop.com
 * 
 * ****************************************************************/

#endregion

#region Using Directives 

using System;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using DevInterop.Framework.Net.Servers;

#endregion

namespace DevInterop.Framework.Net.Sockets
{
    /// <summary>
    /// 
    /// </summary>
    public class Socket2 : IDisposable
    {
        private delegate void BufferDataBlockCompleted(Exception x, object tag);

        private readonly Socket socket;
        private readonly NetworkStream socketStream;
        private SslStream sslStream;
        private bool usingSsl;
        private byte[] buffer;
        private int bufferOffset;
        private int bufferAvailable;
        private readonly Encoding encoding;
        //private string host;
        private DateTime lastActivity;
        private long readCount;
        private long writeCount;

        /// <summary>
        /// Gets the last activity.
        /// </summary>
        /// <value>The last activity.</value>
        public DateTime LastActivity
        {
            get { return lastActivity; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DevInterop.Framework.Net.Sockets.Socket2"/> class.
        /// </summary>
        public Socket2() : this(new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="DevInterop.Framework.Net.Sockets.Socket2"/> class.
        /// </summary>
        /// <param name="socket">The socket.</param>
        public Socket2(Socket socket)
        {
            Argument.AssertNotNull(socket, "socket");

            buffer = new byte[8000];
            encoding = Encoding.UTF8;
            lastActivity = DateTime.Now;

            this.socket = socket;
            this.socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.NoDelay, 1);

            if (socket.Connected)
                socketStream = new NetworkStream(socket, false);
        }

        /// <summary>
        /// Switches to SSL.
        /// </summary>
        /// <param name="certificate">The certificate.</param>
        public void SwitchToSsl(X509Certificate certificate)
        {
            Argument.AssertNotNull(certificate, "certificate");

            //TODO: Error when socket already in SSL mode.
            //if(usingSsl)

            sslStream = new SslStream(socketStream);
            sslStream.AuthenticateAsServer(certificate);
            usingSsl = true;
        }

        /// <summary>
        /// Disconnects this instance.
        /// </summary>
        public void Disconnect()
        {
            Dispose();
        }

        /// <summary>
        /// Shutdowns the specified how.
        /// </summary>
        /// <param name="how">The how.</param>
        public void Shutdown(SocketShutdown how)
        {
            socket.Shutdown(how);
        }

        ///<summary>
        ///Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        ///</summary>
        ///<filterpriority>2</filterpriority>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (socket != null)
                    socket.Close();
                if (socketStream != null)
                    socketStream.Dispose();
                if (sslStream != null)
                    sslStream.Dispose();

                usingSsl = false;
                bufferOffset = 0;
                bufferAvailable = 0;
                readCount = 0;
                writeCount = 0;
//                host = string.Empty;
            }
        }

        /// <summary>
        /// Writes the line.
        /// </summary>
        /// <param name="line">The line.</param>
        public void WriteLine(string line)
        {
            WriteLine(encoding.GetBytes(line));
        }

        /// <summary>
        /// Writes the line.
        /// </summary>
        /// <param name="line">The line.</param>
        public void WriteLine(byte[] line)
        {
            if (usingSsl)
            {
                sslStream.Write(line);
                sslStream.Flush();
            }
            else
            {
                socket.Send(line);
            }

            writeCount += line.Length;
            lastActivity = DateTime.Now;
        }

        /// <summary>
        /// Reads byte from socket. Returns readed byte or -1 if socket is shutdown and tehre is no more data available.
        /// </summary>
        /// <returns>Returns readed byte or -1 if socket is shutdown and tehre is no more data available.</returns>
        public int ReadByte()
        {
            BufferDataBlock();
            // Socket is shutdown
            if (bufferAvailable == 0)
            {
                bufferOffset = 0;
                bufferAvailable = 0;
                return -1;
            }

            bufferOffset++;
            bufferAvailable--;

            return buffer[bufferOffset - 1];
        }

        /// <summary>
        /// Buffers data from socket if needed. If there is data in buffer, no buffering is done.
        /// </summary>
        private void BufferDataBlock()
        {
            lock (this)
            {
                // There is no data in buffer, buffer next data block
                if (bufferAvailable == 0)
                {
                    bufferOffset = 0;

                    if (usingSsl)
                    {
                        bufferAvailable = sslStream.Read(buffer, 0, buffer.Length);
                    }
                    else
                    {
                        bufferAvailable = socket.Receive(buffer);
                    }
                    readCount += bufferAvailable;
                    lastActivity = DateTime.Now;
                }
            }
        }

        /// <summary>
        /// Begins the read line.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="maxLength">Length of the max.</param>
        /// <param name="callBack">The call back.</param>
        public void BeginReadLine(MemoryStream stream, int maxLength, SocketCallBack callBack)
        {
            TryToReadLine(callBack, null, stream, maxLength, -1, 0);
        }

        private void TryToReadLine(SocketCallBack callback, object tag, MemoryStream stream, int maxLineLength, int lastByte, int readedCount)
        {
            // There is no data in buffer, buffer next block asynchronously.
            if (bufferAvailable == 0)
            {
                BeginBufferDataBlock(OnBeginReadLineBufferingCompleted, new object[] {callback, tag, stream, maxLineLength, lastByte, readedCount});
                return;
            }

            // Delay last byte writing, this is because CR, if next is LF, then skip CRLF and terminate reading.

            // This is first method call, buffer 1 byte
            if (lastByte == -1)
            {
                lastByte = ReadByte();
                readedCount++;

                // We use last byte, buffer next block asynchronously.
                if (bufferAvailable == 0)
                {
                    BeginBufferDataBlock(OnBeginReadLineBufferingCompleted, new object[] {callback, tag, stream, maxLineLength, lastByte, readedCount});
                    return;
                }
            }

            int currentByte = ReadByte();
            readedCount++;
            while (currentByte > -1)
            {
                // We got line
                if (lastByte == (byte) '\r' && currentByte == (byte) '\n')
                {
                    // Maximum allowed length exceeded
                    if (readedCount > maxLineLength)
                    {
                        if (callback != null)
                        {
                            callback(SocketCallBackResult.LengthExceeded, new InvalidOperationException("Maximum allowed data length exceeded !"), stream, tag);
//                            callback(SocketCallBackResult.LengthExceeded, 0, new ReadException(ReadReplyCode.LengthExceeded, "Maximum allowed data length exceeded !"), tag);
                        }
                    }

                    // Line read ok, call callback.
                    if (callback != null)
                    {
                        callback(SocketCallBackResult.Ok, null, stream, tag);
//                        callback(SocketCallBackResult.Ok, readedCount, null, tag);
                    }

                    return;
                }
                else
                {
                    // Maximum allowed length exceeded, just don't store data.
                    if (readedCount < maxLineLength && stream.CanWrite)
                    {
                        stream.WriteByte((byte) lastByte);
                    }
                }

                // Read next byte
                lastByte = currentByte;
                if (bufferAvailable > 0)
                {
                    currentByte = ReadByte();
                    readedCount++;
                }
                    // We have use all data in the buffer, buffer next block asynchronously.
                else
                {
                    BeginBufferDataBlock(OnBeginReadLineBufferingCompleted, new object[] {callback, tag, stream, maxLineLength, lastByte, readedCount});
                    return;
                }
            }

            // We should never reach here.
            if (callback != null)
            {
                callback(SocketCallBackResult.Exception, new Exception("Never should reach there ! method TryToReadLine out of while loop."), stream, tag);
//                callback(SocketCallBackResult.Exception, 0, new Exception("Never should reach there ! method TryToReadLine out of while loop."), tag);
            }
        }

        /// <summary>
        /// This method is called after asynchronous data buffering is completed.
        /// </summary>
        /// <param name="x">Exception what happened on method execution or null, if operation completed sucessfully.</param>
        /// <param name="tag">User data.</param>
        private void OnBeginReadLineBufferingCompleted(Exception x, object tag)
        {
            object[] param = (object[]) tag;
            SocketCallBack callback = (SocketCallBack) param[0];
            object callbackTag = param[1];
            MemoryStream stream = (MemoryStream)param[2];
            int maxLineLength = (int) param[3];
            int lastByte = (int) param[4];
            int readedCount = (int) param[5];

            if (x == null)
            {
                // We didn't get data, this can only happen if socket closed.
                if (bufferAvailable == 0 || socket == null || !socket.Connected)
                {
                    callback(SocketCallBackResult.SocketClosed, null, stream, callbackTag);
//                    callback(SocketCallBackResult.SocketClosed, 0, null, callbackTag);
                }
                else
                {
                    TryToReadLine(callback, callbackTag, stream, maxLineLength, lastByte, readedCount);
                }
            }
            else
            {
                callback(SocketCallBackResult.Exception, x, stream, callbackTag);
//                callback(SocketCallBackResult.Exception, 0, x, callbackTag);
            }
        }

        /// <summary>
        /// Start buffering data from socket asynchronously.
        /// </summary>
        /// <param name="callback">The method to be called when the asynchronous data buffering operation is completed.</param>
        /// <param name="tag">User data.</param>
        private void BeginBufferDataBlock(BufferDataBlockCompleted callback, object tag)
        {
            if (bufferAvailable == 0)
            {
                bufferOffset = 0;

                if (usingSsl)
                {
                    sslStream.BeginRead(buffer, 0, buffer.Length, OnBeginBufferDataBlockCallback, new object[] {callback, tag});
                }
                else
                {
                    socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, OnBeginBufferDataBlockCallback, new object[] {callback, tag});
                }
            }
        }

        /// <summary>
        /// This method is called after asynchronous BeginBufferDataBlock is completed.
        /// </summary>
        /// <param name="ar"></param>
        private void OnBeginBufferDataBlockCallback(IAsyncResult ar)
        {
            object[] param = (object[]) ar.AsyncState;
            BufferDataBlockCompleted callback = (BufferDataBlockCompleted) param[0];
            object tag = param[1];

            try
            {
                // Socket closed by this.Disconnect() or closed by remote host.
                if (socket == null || !socket.Connected)
                {
                    bufferAvailable = 0;
                }
                else
                {
                    if (usingSsl)
                    {
                        bufferAvailable = sslStream.EndRead(ar);
                    }
                    else
                    {
                        bufferAvailable = socket.EndReceive(ar);
                    }
                }
                readCount += bufferAvailable;
                lastActivity = DateTime.Now;

                if (callback != null)
                {
                    callback(null, tag);
                }
            }
            catch (Exception x)
            {
                if (callback != null)
                {
                    callback(x, tag);
                }
            }
        }

        /// <summary>
        /// Writes the file.
        /// </summary>
        /// <param name="path">The path.</param>
        public void WriteFile(string path)
        {
            using (FileStream fileStream = File.OpenRead(path))
            {
                // PERF: Add bandwith limiting to Socket2.WriteFile(string)
                int bytesRead = 1;
                while (bytesRead > 0)
                {
                    byte[] data = new byte[10000];
                    bytesRead = fileStream.Read(data, 0, data.Length);
                    socket.Send(data);
                }
            }
        }
    }
}