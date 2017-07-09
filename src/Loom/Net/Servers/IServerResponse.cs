namespace Loom.Net.Servers
{
    public interface IServerResponse
    {
        /// <summary>
        ///     Gets or sets a value indicating whether [end connection].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [end connection]; otherwise, <c>false</c>.
        /// </value>
        bool EndConnection { get; set; }

        /// <summary>
        ///     Buffers the specified response.
        /// </summary>
        /// <param name="response">The response.</param>
        void Buffer(byte[] response);

        /// <summary>
        ///     Buffers the specified response.
        /// </summary>
        /// <param name="response">The response.</param>
        void Buffer(string response);

        /// <summary>
        ///     Writes any buffered data to the output stream.
        /// </summary>
        void Flush();

        /// <summary>
        ///     Writes the specified response.
        /// </summary>
        /// <param name="response">The response.</param>
        void Write(byte[] response);

        /// <summary>
        ///     Writes the specified text response.
        /// </summary>
        /// <param name="response">The text response.</param>
        void Write(string response);

        /// <summary>
        ///     Writes the specified file to the output stream.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        void WriteFile(string path);
    }
}