﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Adibrata.Framework.Messaging.AdibrataWF {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="EntitiesBase", Namespace="http://schemas.datacontract.org/2004/07/AdibrataWF.WCF")]
    [System.SerializableAttribute()]
    public partial class EntitiesBase : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long BranchIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Data.DataTable DataCustField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long RoleIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StatusField;
        
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
        public long BranchId {
            get {
                return this.BranchIdField;
            }
            set {
                if ((this.BranchIdField.Equals(value) != true)) {
                    this.BranchIdField = value;
                    this.RaisePropertyChanged("BranchId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Data.DataTable DataCust {
            get {
                return this.DataCustField;
            }
            set {
                if ((object.ReferenceEquals(this.DataCustField, value) != true)) {
                    this.DataCustField = value;
                    this.RaisePropertyChanged("DataCust");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long RoleId {
            get {
                return this.RoleIdField;
            }
            set {
                if ((this.RoleIdField.Equals(value) != true)) {
                    this.RoleIdField = value;
                    this.RaisePropertyChanged("RoleId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Status {
            get {
                return this.StatusField;
            }
            set {
                if ((object.ReferenceEquals(this.StatusField, value) != true)) {
                    this.StatusField = value;
                    this.RaisePropertyChanged("Status");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="CompositeType", Namespace="http://schemas.datacontract.org/2004/07/AdibrataWF.WCF")]
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="AdibrataWF.IService1")]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetData", ReplyAction="http://tempuri.org/IService1/GetDataResponse")]
        string GetData(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetData", ReplyAction="http://tempuri.org/IService1/GetDataResponse")]
        System.Threading.Tasks.Task<string> GetDataAsync(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetDataCust", ReplyAction="http://tempuri.org/IService1/GetDataCustResponse")]
        System.Data.DataTable GetDataCust(Adibrata.Framework.Messaging.AdibrataWF.EntitiesBase ent);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetDataCust", ReplyAction="http://tempuri.org/IService1/GetDataCustResponse")]
        System.Threading.Tasks.Task<System.Data.DataTable> GetDataCustAsync(Adibrata.Framework.Messaging.AdibrataWF.EntitiesBase ent);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetDataUsingDataContract", ReplyAction="http://tempuri.org/IService1/GetDataUsingDataContractResponse")]
        Adibrata.Framework.Messaging.AdibrataWF.CompositeType GetDataUsingDataContract(Adibrata.Framework.Messaging.AdibrataWF.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetDataUsingDataContract", ReplyAction="http://tempuri.org/IService1/GetDataUsingDataContractResponse")]
        System.Threading.Tasks.Task<Adibrata.Framework.Messaging.AdibrataWF.CompositeType> GetDataUsingDataContractAsync(Adibrata.Framework.Messaging.AdibrataWF.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetNextStep", ReplyAction="http://tempuri.org/IService1/GetNextStepResponse")]
        string GetNextStep();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetNextStep", ReplyAction="http://tempuri.org/IService1/GetNextStepResponse")]
        System.Threading.Tasks.Task<string> GetNextStepAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : Adibrata.Framework.Messaging.AdibrataWF.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<Adibrata.Framework.Messaging.AdibrataWF.IService1>, Adibrata.Framework.Messaging.AdibrataWF.IService1 {
        
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
        
        public System.Data.DataTable GetDataCust(Adibrata.Framework.Messaging.AdibrataWF.EntitiesBase ent) {
            return base.Channel.GetDataCust(ent);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> GetDataCustAsync(Adibrata.Framework.Messaging.AdibrataWF.EntitiesBase ent) {
            return base.Channel.GetDataCustAsync(ent);
        }
        
        public Adibrata.Framework.Messaging.AdibrataWF.CompositeType GetDataUsingDataContract(Adibrata.Framework.Messaging.AdibrataWF.CompositeType composite) {
            return base.Channel.GetDataUsingDataContract(composite);
        }
        
        public System.Threading.Tasks.Task<Adibrata.Framework.Messaging.AdibrataWF.CompositeType> GetDataUsingDataContractAsync(Adibrata.Framework.Messaging.AdibrataWF.CompositeType composite) {
            return base.Channel.GetDataUsingDataContractAsync(composite);
        }
        
        public string GetNextStep() {
            return base.Channel.GetNextStep();
        }
        
        public System.Threading.Tasks.Task<string> GetNextStepAsync() {
            return base.Channel.GetNextStepAsync();
        }
    }
}