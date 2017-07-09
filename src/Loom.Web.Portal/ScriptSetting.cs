namespace Loom.Web.Portal
{
    public sealed class ScriptSetting
    {
        public ScriptSetting(string version, string cdn = null, bool localFallback = false)
        {
            Version = version;
            CDN = cdn;
            LocalFallback = localFallback;
        }

        public string Version { get; }
        public string CDN { get; }
        public bool LocalFallback { get; set; }
    }
}