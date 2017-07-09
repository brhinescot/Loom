namespace Loom.Web.Localization
{
    /// <summary>
    ///     Resource Provider marker interface. Also provides for clearing resources.
    /// </summary>
    public interface IResourceProvider
    {
        /// <summary>
        ///     Interface method used to force providers to register themselves
        ///     with DbResourceConfiguration.
        /// </summary>
        void ClearResourceCache();
    }
}