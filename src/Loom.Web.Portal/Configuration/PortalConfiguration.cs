//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Loom.Web.Portal.Configuration {
    using System;
    using System.Configuration;
    
    
    public class ImageCacheElement : System.Configuration.ConfigurationElement {
        
        [System.Configuration.ConfigurationProperty("quality", DefaultValue=90, IsKey=false, IsRequired=false)]
        public int Quality {
            get {
                return ((int)(base["quality"]));
            }
            set {
                base["quality"] = value;
            }
        }
        
        [System.Configuration.ConfigurationProperty("cacheTime", DefaultValue=60, IsKey=false, IsRequired=false)]
        public int CacheTime {
            get {
                return ((int)(base["cacheTime"]));
            }
            set {
                base["cacheTime"] = value;
            }
        }
    }
    
    public class JQueryElement : System.Configuration.ConfigurationElement {
        
        [System.Configuration.ConfigurationProperty("version", DefaultValue="1.6.2", IsKey=false, IsRequired=false)]
        public string Version {
            get {
                return ((string)(base["version"]));
            }
            set {
                base["version"] = value;
            }
        }
        
        [System.Configuration.ConfigurationProperty("debug", DefaultValue=false, IsKey=false, IsRequired=false)]
        public bool Debug {
            get {
                return ((bool)(base["debug"]));
            }
            set {
                base["debug"] = value;
            }
        }
        
        [System.Configuration.ConfigurationProperty("cdn", DefaultValue="Google", IsKey=false, IsRequired=false)]
        public string Cdn {
            get {
                return ((string)(base["cdn"]));
            }
            set {
                base["cdn"] = value;
            }
        }
        
        [System.Configuration.ConfigurationProperty("disableFallback", DefaultValue=false, IsKey=false, IsRequired=false)]
        public bool DisableFallback {
            get {
                return ((bool)(base["disableFallback"]));
            }
            set {
                base["disableFallback"] = value;
            }
        }
    }
    
    public class ModernizerElement : System.Configuration.ConfigurationElement {
        
        [System.Configuration.ConfigurationProperty("version", DefaultValue="2.0.6", IsKey=false, IsRequired=false)]
        public string Version {
            get {
                return ((string)(base["version"]));
            }
            set {
                base["version"] = value;
            }
        }
        
        [System.Configuration.ConfigurationProperty("debug", DefaultValue=false, IsKey=false, IsRequired=false)]
        public bool Debug {
            get {
                return ((bool)(base["debug"]));
            }
            set {
                base["debug"] = value;
            }
        }
    }
    
    public class AssetsElement : System.Configuration.ConfigurationElement {
        
        [System.Configuration.ConfigurationProperty("name", DefaultValue="images", IsKey=true, IsRequired=true)]
        public string Name {
            get {
                return ((string)(base["name"]));
            }
            set {
                base["name"] = value;
            }
        }
        
        [System.Configuration.ConfigurationProperty("value", DefaultValue="/content/images", IsKey=false, IsRequired=false)]
        public string Value {
            get {
                return ((string)(base["value"]));
            }
            set {
                base["value"] = value;
            }
        }
    }
    
    [System.Configuration.ConfigurationCollectionAttribute(typeof(AssetsElement))]
    public class AssetsCollection : System.Configuration.ConfigurationElementCollection {
        
        [System.Configuration.ConfigurationProperty("base", DefaultValue="/", IsKey=false, IsRequired=false)]
        public string Base {
            get {
                return ((string)(base["base"]));
            }
            set {
                base["base"] = value;
            }
        }
        
        protected override System.Configuration.ConfigurationElement CreateNewElement() {
            return new AssetsElement();
        }
        
        protected override object GetElementKey(System.Configuration.ConfigurationElement element) {
            return ((AssetsElement)(element)).Name;
        }
        
        public void Add(AssetsElement element) {
            this.BaseAdd(element);
        }
        
        public void Remove(string key) {
            this.BaseRemove(key);
        }
        
        public void Clear() {
            this.BaseClear();
        }
    }
    
    public class ThumbnailsElement : System.Configuration.ConfigurationElement {
        
        [System.Configuration.ConfigurationProperty("name", DefaultValue="icon", IsKey=true, IsRequired=true)]
        public string Name {
            get {
                return ((string)(base["name"]));
            }
            set {
                base["name"] = value;
            }
        }
        
        [System.Configuration.ConfigurationProperty("suffix", DefaultValue="i", IsKey=false, IsRequired=false)]
        public string Suffix {
            get {
                return ((string)(base["suffix"]));
            }
            set {
                base["suffix"] = value;
            }
        }
        
        [System.Configuration.ConfigurationProperty("maxSize", DefaultValue=32, IsKey=false, IsRequired=false)]
        public int MaxSize {
            get {
                return ((int)(base["maxSize"]));
            }
            set {
                base["maxSize"] = value;
            }
        }
    }
    
    [System.Configuration.ConfigurationCollectionAttribute(typeof(ThumbnailsElement))]
    public class ThumbnailsCollection : System.Configuration.ConfigurationElementCollection {
        
        [System.Configuration.ConfigurationProperty("quality", DefaultValue=90, IsKey=false, IsRequired=false)]
        public int Quality {
            get {
                return ((int)(base["quality"]));
            }
            set {
                base["quality"] = value;
            }
        }
        
        [System.Configuration.ConfigurationProperty("cacheTime", DefaultValue=60, IsKey=false, IsRequired=false)]
        public int CacheTime {
            get {
                return ((int)(base["cacheTime"]));
            }
            set {
                base["cacheTime"] = value;
            }
        }
        
        protected override System.Configuration.ConfigurationElement CreateNewElement() {
            return new ThumbnailsElement();
        }
        
        protected override object GetElementKey(System.Configuration.ConfigurationElement element) {
            return ((ThumbnailsElement)(element)).Name;
        }
        
        public void Add(ThumbnailsElement element) {
            this.BaseAdd(element);
        }
        
        public void Remove(string key) {
            this.BaseRemove(key);
        }
        
        public void Clear() {
            this.BaseClear();
        }
    }
    
    public class RoutesElement : System.Configuration.ConfigurationElement {
        
        [System.Configuration.ConfigurationProperty("name", DefaultValue="", IsKey=true, IsRequired=true)]
        public string Name {
            get {
                return ((string)(base["name"]));
            }
            set {
                base["name"] = value;
            }
        }
        
        [System.Configuration.ConfigurationProperty("expression", DefaultValue="", IsKey=false, IsRequired=false)]
        public string Expression {
            get {
                return ((string)(base["expression"]));
            }
            set {
                base["expression"] = value;
            }
        }
        
        [System.Configuration.ConfigurationProperty("controller", DefaultValue="", IsKey=false, IsRequired=false)]
        public string Controller {
            get {
                return ((string)(base["controller"]));
            }
            set {
                base["controller"] = value;
            }
        }
    }
    
    [System.Configuration.ConfigurationCollectionAttribute(typeof(RoutesElement))]
    public class RoutesCollection : System.Configuration.ConfigurationElementCollection {
        
        [System.Configuration.ConfigurationProperty("allowDatabaseRoutes", DefaultValue=true, IsKey=false, IsRequired=false)]
        public bool AllowDatabaseRoutes {
            get {
                return ((bool)(base["allowDatabaseRoutes"]));
            }
            set {
                base["allowDatabaseRoutes"] = value;
            }
        }
        
        [System.Configuration.ConfigurationProperty("allowPhysicalPages", DefaultValue=false, IsKey=false, IsRequired=false)]
        public bool AllowPhysicalPages {
            get {
                return ((bool)(base["allowPhysicalPages"]));
            }
            set {
                base["allowPhysicalPages"] = value;
            }
        }
        
        protected override System.Configuration.ConfigurationElement CreateNewElement() {
            return new RoutesElement();
        }
        
        protected override object GetElementKey(System.Configuration.ConfigurationElement element) {
            return ((RoutesElement)(element)).Name;
        }
        
        public void Add(RoutesElement element) {
            this.BaseAdd(element);
        }
        
        public void Remove(string key) {
            this.BaseRemove(key);
        }
        
        public void Clear() {
            this.BaseClear();
        }
    }
    
    public class LanguagesElement : System.Configuration.ConfigurationElement {
        
        [System.Configuration.ConfigurationProperty("name", DefaultValue="English", IsKey=true, IsRequired=true)]
        public string Name {
            get {
                return ((string)(base["name"]));
            }
            set {
                base["name"] = value;
            }
        }
        
        [System.Configuration.ConfigurationProperty("value", DefaultValue="en-US", IsKey=false, IsRequired=false)]
        public string Value {
            get {
                return ((string)(base["value"]));
            }
            set {
                base["value"] = value;
            }
        }
    }
    
    [System.Configuration.ConfigurationCollectionAttribute(typeof(LanguagesElement))]
    public class LanguagesCollection : System.Configuration.ConfigurationElementCollection {
        
        [System.Configuration.ConfigurationProperty("default", DefaultValue="English", IsKey=false, IsRequired=false)]
        public string Default {
            get {
                return ((string)(base["default"]));
            }
            set {
                base["default"] = value;
            }
        }
        
        protected override System.Configuration.ConfigurationElement CreateNewElement() {
            return new LanguagesElement();
        }
        
        protected override object GetElementKey(System.Configuration.ConfigurationElement element) {
            return ((LanguagesElement)(element)).Name;
        }
        
        public void Add(LanguagesElement element) {
            this.BaseAdd(element);
        }
        
        public void Remove(string key) {
            this.BaseRemove(key);
        }
        
        public void Clear() {
            this.BaseClear();
        }
    }
    
    public class VirtualResourcesElement : System.Configuration.ConfigurationElement {
        
        [System.Configuration.ConfigurationProperty("name", DefaultValue="", IsKey=true, IsRequired=true)]
        public string Name {
            get {
                return ((string)(base["name"]));
            }
            set {
                base["name"] = value;
            }
        }
        
        [System.Configuration.ConfigurationProperty("namespace", DefaultValue="", IsKey=false, IsRequired=false)]
        public string Namespace {
            get {
                return ((string)(base["namespace"]));
            }
            set {
                base["namespace"] = value;
            }
        }
        
        [System.Configuration.ConfigurationProperty("assembly", DefaultValue="", IsKey=false, IsRequired=false)]
        public string Assembly {
            get {
                return ((string)(base["assembly"]));
            }
            set {
                base["assembly"] = value;
            }
        }
    }
    
    [System.Configuration.ConfigurationCollectionAttribute(typeof(VirtualResourcesElement))]
    public class VirtualResourcesCollection : System.Configuration.ConfigurationElementCollection {
        
        protected override System.Configuration.ConfigurationElement CreateNewElement() {
            return new VirtualResourcesElement();
        }
        
        protected override object GetElementKey(System.Configuration.ConfigurationElement element) {
            return ((VirtualResourcesElement)(element)).Name;
        }
        
        public void Add(VirtualResourcesElement element) {
            this.BaseAdd(element);
        }
        
        public void Remove(string key) {
            this.BaseRemove(key);
        }
        
        public void Clear() {
            this.BaseClear();
        }
    }
    
    public class TenantsElement : System.Configuration.ConfigurationElement {
        
        [System.Configuration.ConfigurationProperty("domain", DefaultValue="", IsKey=true, IsRequired=true)]
        public string Domain {
            get {
                return ((string)(base["domain"]));
            }
            set {
                base["domain"] = value;
            }
        }
        
        [System.Configuration.ConfigurationProperty("tenant", DefaultValue="", IsKey=false, IsRequired=false)]
        public string Tenant {
            get {
                return ((string)(base["tenant"]));
            }
            set {
                base["tenant"] = value;
            }
        }
    }
    
    [System.Configuration.ConfigurationCollectionAttribute(typeof(TenantsElement))]
    public class TenantsCollection : System.Configuration.ConfigurationElementCollection {
        
        protected override System.Configuration.ConfigurationElement CreateNewElement() {
            return new TenantsElement();
        }
        
        protected override object GetElementKey(System.Configuration.ConfigurationElement element) {
            return ((TenantsElement)(element)).Domain;
        }
        
        public void Add(TenantsElement element) {
            this.BaseAdd(element);
        }
        
        public void Remove(string key) {
            this.BaseRemove(key);
        }
        
        public void Clear() {
            this.BaseClear();
        }
    }
    
    public class PortalSettingsSection : System.Configuration.ConfigurationSection {
        
        [System.Configuration.ConfigurationProperty("setup", DefaultValue=false, IsKey=false, IsRequired=false)]
        public bool Setup {
            get {
                return ((bool)(base["setup"]));
            }
            set {
                base["setup"] = value;
            }
        }
        
        [System.Configuration.ConfigurationProperty("enabledTenants", DefaultValue=false, IsKey=false, IsRequired=false)]
        public bool EnabledTenants {
            get {
                return ((bool)(base["enabledTenants"]));
            }
            set {
                base["enabledTenants"] = value;
            }
        }
        
        [System.Configuration.ConfigurationProperty("imageCache")]
        public ImageCacheElement ImageCache {
            get {
                return ((ImageCacheElement)(base["imageCache"]));
            }
        }
        
        [System.Configuration.ConfigurationProperty("jQuery")]
        public JQueryElement JQuery {
            get {
                return ((JQueryElement)(base["jQuery"]));
            }
        }
        
        [System.Configuration.ConfigurationProperty("modernizer")]
        public ModernizerElement Modernizer {
            get {
                return ((ModernizerElement)(base["modernizer"]));
            }
        }
        
        [System.Configuration.ConfigurationProperty("assets")]
        public AssetsCollection Assets {
            get {
                return ((AssetsCollection)(base["assets"]));
            }
        }
        
        [System.Configuration.ConfigurationProperty("thumbnails")]
        public ThumbnailsCollection Thumbnails {
            get {
                return ((ThumbnailsCollection)(base["thumbnails"]));
            }
        }
        
        [System.Configuration.ConfigurationProperty("routes")]
        public RoutesCollection Routes {
            get {
                return ((RoutesCollection)(base["routes"]));
            }
        }
        
        [System.Configuration.ConfigurationProperty("languages")]
        public LanguagesCollection Languages {
            get {
                return ((LanguagesCollection)(base["languages"]));
            }
        }
        
        [System.Configuration.ConfigurationProperty("virtualResources")]
        public VirtualResourcesCollection VirtualResources {
            get {
                return ((VirtualResourcesCollection)(base["virtualResources"]));
            }
        }
        
        [System.Configuration.ConfigurationProperty("tenants")]
        public TenantsCollection Tenants {
            get {
                return ((TenantsCollection)(base["tenants"]));
            }
        }
    }
}