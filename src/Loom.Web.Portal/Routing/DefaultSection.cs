#region Using Directives

#endregion

namespace Loom.Web.Portal.Routing
{
    internal sealed class DefaultSection : SectionData
    {
        internal const string DefaultName = "Home";
        internal const string DefaultNamespace = "Default";

        /// <summary>
        ///     Initializes a new instance of the <see cref="SectionData" /> class.
        /// </summary>
        public DefaultSection() : base(DefaultName, DefaultNamespace) { }
    }
}