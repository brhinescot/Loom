//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Loom.Configuration {
    using System;
    using System.Configuration;
    
    
    public class IgnoreTypeElement : System.Configuration.ConfigurationElement {
        
        [System.Configuration.ConfigurationProperty("name", DefaultValue="Cpp", IsKey=true, IsRequired=true)]
        public string Name {
            get {
                return ((string)(base["name"]));
            }
            set {
                base["name"] = value;
            }
        }
    }
    
    [System.Configuration.ConfigurationCollectionAttribute(typeof(IgnoreTypeElement))]
    public class IgnoreTypeCollection : System.Configuration.ConfigurationElementCollection {
        
        protected override System.Configuration.ConfigurationElement CreateNewElement() {
            return new IgnoreTypeElement();
        }
        
        protected override object GetElementKey(System.Configuration.ConfigurationElement element) {
            return ((IgnoreTypeElement)(element)).Name;
        }
        
        public void Add(IgnoreTypeElement element) {
            this.BaseAdd(element);
        }
        
        public void Remove(string key) {
            this.BaseRemove(key);
        }
        
        public void Clear() {
            this.BaseClear();
        }
    }
    
    public class TypeLoaderSettingsSection : System.Configuration.ConfigurationSection {
        
        [System.Configuration.ConfigurationProperty("ignoreType")]
        public IgnoreTypeCollection IgnoreType {
            get {
                return ((IgnoreTypeCollection)(base["ignoreType"]));
            }
        }
    }
}