//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17020
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Loom.Web.Configuration {
    using System;
    using System.Configuration;
    
    
    public class ExtensionsElement : System.Configuration.ConfigurationElement {
        
        [System.Configuration.ConfigurationProperty("extension", DefaultValue=".aspx", IsKey=true, IsRequired=true)]
        public string Extension {
            get {
                return ((string)(base["extension"]));
            }
            set {
                base["extension"] = value;
            }
        }
    }
    
    [System.Configuration.ConfigurationCollectionAttribute(typeof(ExtensionsElement))]
    public class ExtensionsCollection : System.Configuration.ConfigurationElementCollection {
        
        protected override System.Configuration.ConfigurationElement CreateNewElement() {
            return new ExtensionsElement();
        }
        
        protected override object GetElementKey(System.Configuration.ConfigurationElement element) {
            return ((ExtensionsElement)(element)).Extension;
        }
        
        public void Add(ExtensionsElement element) {
            this.BaseAdd(element);
        }
        
        public void Remove(string key) {
            this.BaseRemove(key);
        }
        
        public void Clear() {
            this.BaseClear();
        }
    }
    
    public class PageViewScriptBlockerSettingsSection : System.Configuration.ConfigurationSection {
        
        [System.Configuration.ConfigurationProperty("requestCycleSeconds", DefaultValue=15, IsKey=false, IsRequired=false)]
        public int RequestCycleSeconds {
            get {
                return ((int)(base["requestCycleSeconds"]));
            }
            set {
                base["requestCycleSeconds"] = value;
            }
        }
        
        [System.Configuration.ConfigurationProperty("maxRequestsPerCycle", DefaultValue=30, IsKey=false, IsRequired=false)]
        public int MaxRequestsPerCycle {
            get {
                return ((int)(base["maxRequestsPerCycle"]));
            }
            set {
                base["maxRequestsPerCycle"] = value;
            }
        }
        
        [System.Configuration.ConfigurationProperty("banMinutes", DefaultValue=60, IsKey=false, IsRequired=false)]
        public int BanMinutes {
            get {
                return ((int)(base["banMinutes"]));
            }
            set {
                base["banMinutes"] = value;
            }
        }
        
        [System.Configuration.ConfigurationProperty("extension", DefaultValue=".aspx", IsKey=false, IsRequired=false)]
        public string Extension {
            get {
                return ((string)(base["extension"]));
            }
            set {
                base["extension"] = value;
            }
        }
        
        [System.Configuration.ConfigurationProperty("extensions")]
        public ExtensionsCollection Extensions {
            get {
                return ((ExtensionsCollection)(base["extensions"]));
            }
        }
    }
}
