//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17020
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Loom.Data.Mapping.Configuration {
    using System;
    using System.Configuration;
    
    
    public class LocalizationElement : System.Configuration.ConfigurationElement {
        
        [System.Configuration.ConfigurationProperty("defaultLocale", DefaultValue="en-US", IsKey=false, IsRequired=false)]
        public string DefaultLocale {
            get {
                return ((string)(base["defaultLocale"]));
            }
            set {
                base["defaultLocale"] = value;
            }
        }
        
        [System.Configuration.ConfigurationProperty("supportedLanguages", DefaultValue="en-US", IsKey=false, IsRequired=false)]
        public string SupportedLanguages {
            get {
                return ((string)(base["supportedLanguages"]));
            }
            set {
                base["supportedLanguages"] = value;
            }
        }
    }
    
    public class SessionProvidersElement : System.Configuration.ConfigurationElement {
        
        [System.Configuration.ConfigurationProperty("name", DefaultValue="", IsKey=true, IsRequired=true)]
        public string Name {
            get {
                return ((string)(base["name"]));
            }
            set {
                base["name"] = value;
            }
        }
        
        [System.Configuration.ConfigurationProperty("type", DefaultValue="", IsKey=false, IsRequired=false)]
        public string Type {
            get {
                return ((string)(base["type"]));
            }
            set {
                base["type"] = value;
            }
        }
        
        [System.Configuration.ConfigurationProperty("connectionStringName", DefaultValue="", IsKey=false, IsRequired=false)]
        public string ConnectionStringName {
            get {
                return ((string)(base["connectionStringName"]));
            }
            set {
                base["connectionStringName"] = value;
            }
        }
    }
    
    [System.Configuration.ConfigurationCollectionAttribute(typeof(SessionProvidersElement))]
    public class SessionProvidersCollection : System.Configuration.ConfigurationElementCollection {
        
        [System.Configuration.ConfigurationProperty("defaultProvider", DefaultValue="portal", IsKey=false, IsRequired=false)]
        public string DefaultProvider {
            get {
                return ((string)(base["defaultProvider"]));
            }
            set {
                base["defaultProvider"] = value;
            }
        }
        
        protected override System.Configuration.ConfigurationElement CreateNewElement() {
            return new SessionProvidersElement();
        }
        
        protected override object GetElementKey(System.Configuration.ConfigurationElement element) {
            return ((SessionProvidersElement)(element)).Name;
        }
        
        public void Add(SessionProvidersElement element) {
            this.BaseAdd(element);
        }
        
        public void Remove(string key) {
            this.BaseRemove(key);
        }
        
        public void Clear() {
            this.BaseClear();
        }
    }
    
    public class ActiveMapConfigurationSection : System.Configuration.ConfigurationSection {
        
        [System.Configuration.ConfigurationProperty("localization")]
        public LocalizationElement Localization {
            get {
                return ((LocalizationElement)(base["localization"]));
            }
        }
        
        [System.Configuration.ConfigurationProperty("sessionProviders")]
        public SessionProvidersCollection SessionProviders {
            get {
                return ((SessionProvidersCollection)(base["sessionProviders"]));
            }
        }
    }
}
