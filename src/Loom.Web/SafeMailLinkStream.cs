#region Using Directives

using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

#endregion

namespace Loom.Web
{
    /// <summary>
    ///     The Filter that actually performs the e-mail encoding
    /// </summary>
    public class SafeMailLinkStream : Stream
    {
        private const string KEY = "JiDGHhydy8wY";

        private static readonly Regex Validator = new
            Regex(@"(?<email>mailto:\w+([-+.]\w+)*?@\w+([-.]\w+)*\.\w+([-.]\w+)*?(\?.*?)?)(?<restOfTag>"".*?>)(?<text>(.|\s)*?)</a>",
                RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private readonly Stream baseStream;
        private readonly StringBuilder htmlBuffer = new StringBuilder();
        private bool done;

        /// <summary>
        ///     Creates a new <see cref="SafeMailLinkStream" /> instance.
        /// </summary>
        /// <param name="baseStream">Base stream.</param>
        public SafeMailLinkStream(Stream baseStream)
        {
            this.baseStream = baseStream;
        }

        /// <summary>
        ///     Gets a value indicating whether this instance can read.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance can read; otherwise, <c>false</c>.
        /// </value>
        public override bool CanRead => false;

        /// <summary>
        ///     Gets a value indicating whether this instance can write.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance can write; otherwise, <c>false</c>.
        /// </value>
        public override bool CanWrite => true;

        /// <summary>
        ///     Gets a value indicating whether this instance can seek.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance can seek; otherwise, <c>false</c>.
        /// </value>
        public override bool CanSeek => false;

        /// <summary>
        ///     This property is not supported.
        /// </summary>
        public override long Position
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }

        /// <summary>
        ///     This property is not supported.
        /// </summary>
        /// <value></value>
        public override long Length => throw new NotSupportedException();

        /// <summary>
        ///     This method is not supported.
        /// </summary>
        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        ///     This property is not supported.
        /// </summary>
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        ///     This method is not supported.
        /// </summary>
        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        ///     Closes this instance.
        /// </summary>
        public override void Close()
        {
            baseStream?.Close();
        }

        /// <summary>
        ///     Flushes this instance.
        /// </summary>
        public override void Flush()
        {
            baseStream?.Flush();
        }

        /// <summary>
        ///     Writes to the specified buffer.
        /// </summary>
        /// <param name="buffer">Buffer.</param>
        /// <param name="offset">Offset.</param>
        /// <param name="count">Count.</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            #region Fast Exit

            if (done)
            {
                baseStream.Write(buffer, offset, count);
                return;
            }

            #endregion

            string internalBuffer = Encoding.UTF8.GetString(buffer, offset, count);
            htmlBuffer.Append(internalBuffer);

            if (!Regex.IsMatch(internalBuffer, "</html>", RegexOptions.IgnoreCase))
                return;

            // if the </html> closing tag was found, this is the end of the file, so let's do the replace work
            string html = htmlBuffer.ToString();
            StringBuilder result = new StringBuilder();

            // search for e-mail links
            int index = 0;
            MatchCollection emailMatches = Validator.Matches(html);
            foreach (Match match in emailMatches)
            {
                // add to the output text the substring before this match
                result.Append(html.Substring(index, match.Index - index));

                // add to the output string the text resulting from the encoding of the mail address
                string email = BitConverter.ToString(Encoding.ASCII.GetBytes(KEY + match.Groups["email"].Value)).Replace("-", string.Empty);
                string text = match.Groups["text"].Value;

                text = text.Replace("@", "<!--w-->@<!--x-->");
                result.Append("javascript:safeMessageSend('").Append(email).Append("');")
                    .Append(match.Groups["restOfTag"].Value).Append(text).Append("</a>");

                // increment the index so that it starts after this processed match
                index = match.Index + match.Length;
            }

            // append the rest of the input string until </body>
            Match bodyTagMatch = Regex.Match(html, "</body>", RegexOptions.IgnoreCase);
            result.Append(html.Substring(index, bodyTagMatch.Index - index));

            // Append the client-side javascript that decodes the e-mail address and 
            // launches a new instance of the default e-mail client
            if (emailMatches.Count > 0)
                result.Append(SR.ScriptSafeMailLink(KEY.Length));

            // append the rest of the original html source
            result.Append(html.Substring(bodyTagMatch.Index));

            // write the resulting string to the response stream
            byte[] data = Encoding.UTF8.GetBytes(result.ToString());
            baseStream.Write(data, 0, data.Length);
            done = true;
        }
    }
}