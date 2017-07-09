namespace Loom.Net.Mail.Pop3
{
    /// <summary>
    /// </summary>
    internal enum Pop3SessionState
    {
        /// <summary>
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// </summary>
        Authorization = 1,

        /// <summary>
        /// </summary>
        Transaction = 2,

        /// <summary>
        /// </summary>
        Update = 3
    }
}