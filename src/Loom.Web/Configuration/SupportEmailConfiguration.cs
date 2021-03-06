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
    
    
    public class SenderElement : System.Configuration.ConfigurationElement {
        
        [System.Configuration.ConfigurationProperty("name", DefaultValue="", IsKey=false, IsRequired=false)]
        public string Name {
            get {
                return ((string)(base["name"]));
            }
            set {
                base["name"] = value;
            }
        }
        
        [System.Configuration.ConfigurationProperty("address", DefaultValue="", IsKey=false, IsRequired=false)]
        public string Address {
            get {
                return ((string)(base["address"]));
            }
            set {
                base["address"] = value;
            }
        }
    }
    
    public class MessageElement : System.Configuration.ConfigurationElement {
        
        [System.Configuration.ConfigurationProperty("subject", DefaultValue="", IsKey=false, IsRequired=false)]
        public string Subject {
            get {
                return ((string)(base["subject"]));
            }
            set {
                base["subject"] = value;
            }
        }
        
        [System.Configuration.ConfigurationProperty("header", DefaultValue="", IsKey=false, IsRequired=false)]
        public string Header {
            get {
                return ((string)(base["header"]));
            }
            set {
                base["header"] = value;
            }
        }
    }
    
    public class RecipientsElement : System.Configuration.ConfigurationElement {
        
        [System.Configuration.ConfigurationProperty("name", DefaultValue="", IsKey=true, IsRequired=true)]
        public string Name {
            get {
                return ((string)(base["name"]));
            }
            set {
                base["name"] = value;
            }
        }
        
        [System.Configuration.ConfigurationProperty("address", DefaultValue="", IsKey=false, IsRequired=false)]
        public string Address {
            get {
                return ((string)(base["address"]));
            }
            set {
                base["address"] = value;
            }
        }
    }
    
    [System.Configuration.ConfigurationCollectionAttribute(typeof(RecipientsElement))]
    public class RecipientsCollection : System.Configuration.ConfigurationElementCollection {
        
        protected override System.Configuration.ConfigurationElement CreateNewElement() {
            return new RecipientsElement();
        }
        
        protected override object GetElementKey(System.Configuration.ConfigurationElement element) {
            return ((RecipientsElement)(element)).Name;
        }
        
        public void Add(RecipientsElement element) {
            this.BaseAdd(element);
        }
        
        public void Remove(string key) {
            this.BaseRemove(key);
        }
        
        public void Clear() {
            this.BaseClear();
        }
    }
    
    public class SupportEmailSettingsSection : System.Configuration.ConfigurationSection {
        
        [System.Configuration.ConfigurationProperty("applicationName", DefaultValue="", IsKey=false, IsRequired=false)]
        public string ApplicationName {
            get {
                return ((string)(base["applicationName"]));
            }
            set {
                base["applicationName"] = value;
            }
        }
        
        [System.Configuration.ConfigurationProperty("server", DefaultValue="", IsKey=false, IsRequired=false)]
        public string Server {
            get {
                return ((string)(base["server"]));
            }
            set {
                base["server"] = value;
            }
        }
        
        [System.Configuration.ConfigurationProperty("port", DefaultValue=0, IsKey=false, IsRequired=false)]
        public int Port {
            get {
                return ((int)(base["port"]));
            }
            set {
                base["port"] = value;
            }
        }
        
        [System.Configuration.ConfigurationProperty("sender")]
        public SenderElement Sender {
            get {
                return ((SenderElement)(base["sender"]));
            }
        }
        
        [System.Configuration.ConfigurationProperty("message")]
        public MessageElement Message {
            get {
                return ((MessageElement)(base["message"]));
            }
        }
        
        [System.Configuration.ConfigurationProperty("recipients")]
        public RecipientsCollection Recipients {
            get {
                return ((RecipientsCollection)(base["recipients"]));
            }
        }
    }
}
