#region Using Directives

using System.Runtime.InteropServices;
using System.Xml.Serialization;

#endregion

namespace Loom.Web.Reporting
{
    /// <summary>
    ///     Summary description for EntryCount.
    /// </summary>
    [StructLayout(LayoutKind.Auto)]
    public class UniqueEntry
    {
        /// <summary>
        ///     Creates a new <see cref="UniqueEntry" /> instance.
        /// </summary>
        /// <param name="hits">Hits.</param>
        /// <param name="value">Value.</param>
        public UniqueEntry(int hits, string value)
        {
            Value = value;
            Hits = hits;
        }

        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        /// <value></value>
        [XmlText]
        public string Value { get; set; }

        /// <summary>
        ///     Gets or sets the hits.
        /// </summary>
        /// <value></value>
        [XmlAttribute("numberOfHits")]
        public int Hits { get; set; }
    }
}