﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Adibrata.Framework.Messaging.ServiceReference1 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CompositeType", Namespace="http://schemas.datacontract.org/2004/07/Adibrata.Demo.WCF.FileTransfer")]
    [System.SerializableAttribute()]
    public partial class CompositeType : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool BoolValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StringValueField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool BoolValue {
            get {
                return this.BoolValueField;
            }
            set {
                if ((this.BoolValueField.Equals(value) != true)) {
                    this.BoolValueField = value;
                    this.RaisePropertyChanged("BoolValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StringValue {
            get {
                return this.StringValueField;
            }
            set {
                if ((object.ReferenceEquals(this.StringValueField, value) != true)) {
                    this.StringValueField = value;
                    this.RaisePropertyChanged("StringValue");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PathDetails", Namespace="http://schemas.datacontract.org/2004/07/Adibrata.Demo.WCF.FileTransfer")]
    [System.SerializableAttribute()]
    public partial class PathDetails : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AgrmntNoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DocTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ExtField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private byte[] FileBinaryField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FileNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long PathIdField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string AgrmntNo {
            get {
                return this.AgrmntNoField;
            }
            set {
                if ((object.ReferenceEquals(this.AgrmntNoField, value) != true)) {
                    this.AgrmntNoField = value;
                    this.RaisePropertyChanged("AgrmntNo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DocType {
            get {
                return this.DocTypeField;
            }
            set {
                if ((object.ReferenceEquals(this.DocTypeField, value) != true)) {
                    this.DocTypeField = value;
                    this.RaisePropertyChanged("DocType");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Ext {
            get {
                return this.ExtField;
            }
            set {
                if ((object.ReferenceEquals(this.ExtField, value) != true)) {
                    this.ExtField = value;
                    this.RaisePropertyChanged("Ext");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte[] FileBinary {
            get {
                return this.FileBinaryField;
            }
            set {
                if ((object.ReferenceEquals(this.FileBinaryField, value) != true)) {
                    this.FileBinaryField = value;
                    this.RaisePropertyChanged("FileBinary");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FileName {
            get {
                return this.FileNameField;
            }
            set {
                if ((object.ReferenceEquals(this.FileNameField, value) != true)) {
                    this.FileNameField = value;
                    this.RaisePropertyChanged("FileName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long PathId {
            get {
                return this.PathIdField;
            }
            set {
                if ((this.PathIdField.Equals(value) != true)) {
                    this.PathIdField = value;
                    this.RaisePropertyChanged("PathId");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IService1")]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetData", ReplyAction="http://tempuri.org/IService1/GetDataResponse")]
        string GetData(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetData", ReplyAction="http://tempuri.org/IService1/GetDataResponse")]
        System.Threading.Tasks.Task<string> GetDataAsync(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetDataUsingDataContract", ReplyAction="http://tempuri.org/IService1/GetDataUsingDataContractResponse")]
        Adibrata.Framework.Messaging.ServiceReference1.CompositeType GetDataUsingDataContract(Adibrata.Framework.Messaging.ServiceReference1.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetDataUsingDataContract", ReplyAction="http://tempuri.org/IService1/GetDataUsingDataContractResponse")]
        System.Threading.Tasks.Task<Adibrata.Framework.Messaging.ServiceReference1.CompositeType> GetDataUsingDataContractAsync(Adibrata.Framework.Messaging.ServiceReference1.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/UpdatePathDetails", ReplyAction="http://tempuri.org/IService1/UpdatePathDetailsResponse")]
        void UpdatePathDetails(Adibrata.Framework.Messaging.ServiceReference1.PathDetails pathInfo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/UpdatePathDetails", ReplyAction="http://tempuri.org/IService1/UpdatePathDetailsResponse")]
        System.Threading.Tasks.Task UpdatePathDetailsAsync(Adibrata.Framework.Messaging.ServiceReference1.PathDetails pathInfo);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : Adibrata.Framework.Messaging.ServiceReference1.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<Adibrata.Framework.Messaging.ServiceReference1.IService1>, Adibrata.Framework.Messaging.ServiceReference1.IService1 {
        
        public Service1Client() {
        }
        
        public Service1Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Service1Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetData(int value) {
            return base.Channel.GetData(value);
        }
        
        public System.Threading.Tasks.Task<string> GetDataAsync(int value) {
            return base.Channel.GetDataAsync(value);
        }
        
        public Adibrata.Framework.Messaging.ServiceReference1.CompositeType GetDataUsingDataContract(Adibrata.Framework.Messaging.ServiceReference1.CompositeType composite) {
            return base.Channel.GetDataUsingDataContract(composite);
        }
        
        public System.Threading.Tasks.Task<Adibrata.Framework.Messaging.ServiceReference1.CompositeType> GetDataUsingDataContractAsync(Adibrata.Framework.Messaging.ServiceReference1.CompositeType composite) {
            return base.Channel.GetDataUsingDataContractAsync(composite);
        }
        
        public void UpdatePathDetails(Adibrata.Framework.Messaging.ServiceReference1.PathDetails pathInfo) {
            base.Channel.UpdatePathDetails(pathInfo);
        }
        
        public System.Threading.Tasks.Task UpdatePathDetailsAsync(Adibrata.Framework.Messaging.ServiceReference1.PathDetails pathInfo) {
            return base.Channel.UpdatePathDetailsAsync(pathInfo);
        }
    }
}
