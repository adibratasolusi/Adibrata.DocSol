﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Adibrata.Framework.Messaging.ServiceReference3 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CompositeType", Namespace="http://schemas.datacontract.org/2004/07/Adibrata.Framework.WCF.DocTransView")]
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
    [System.Runtime.Serialization.DataContractAttribute(Name="DocTrans", Namespace="http://schemas.datacontract.org/2004/07/Adibrata.Framework.WCF.DocTransView")]
    [System.SerializableAttribute()]
    public partial class DocTrans : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ComputerNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime ContenValueDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ContentNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ContentSearchTagField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ContentValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal ContentValueNumericField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DPIField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime DateCreatedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long DocTransBinaryIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DocTransCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long DocTransContentIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long DocTransIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DocTypeCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime DtmCrtField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime DtmUpdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private byte[] FileBinaryField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FileNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PixelField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal SizeFileBytesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TransIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UsrCrtField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UsrUpdField;
        
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
        public string ComputerName {
            get {
                return this.ComputerNameField;
            }
            set {
                if ((object.ReferenceEquals(this.ComputerNameField, value) != true)) {
                    this.ComputerNameField = value;
                    this.RaisePropertyChanged("ComputerName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime ContenValueDate {
            get {
                return this.ContenValueDateField;
            }
            set {
                if ((this.ContenValueDateField.Equals(value) != true)) {
                    this.ContenValueDateField = value;
                    this.RaisePropertyChanged("ContenValueDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ContentName {
            get {
                return this.ContentNameField;
            }
            set {
                if ((object.ReferenceEquals(this.ContentNameField, value) != true)) {
                    this.ContentNameField = value;
                    this.RaisePropertyChanged("ContentName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ContentSearchTag {
            get {
                return this.ContentSearchTagField;
            }
            set {
                if ((object.ReferenceEquals(this.ContentSearchTagField, value) != true)) {
                    this.ContentSearchTagField = value;
                    this.RaisePropertyChanged("ContentSearchTag");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ContentValue {
            get {
                return this.ContentValueField;
            }
            set {
                if ((object.ReferenceEquals(this.ContentValueField, value) != true)) {
                    this.ContentValueField = value;
                    this.RaisePropertyChanged("ContentValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal ContentValueNumeric {
            get {
                return this.ContentValueNumericField;
            }
            set {
                if ((this.ContentValueNumericField.Equals(value) != true)) {
                    this.ContentValueNumericField = value;
                    this.RaisePropertyChanged("ContentValueNumeric");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DPI {
            get {
                return this.DPIField;
            }
            set {
                if ((object.ReferenceEquals(this.DPIField, value) != true)) {
                    this.DPIField = value;
                    this.RaisePropertyChanged("DPI");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime DateCreated {
            get {
                return this.DateCreatedField;
            }
            set {
                if ((this.DateCreatedField.Equals(value) != true)) {
                    this.DateCreatedField = value;
                    this.RaisePropertyChanged("DateCreated");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long DocTransBinaryId {
            get {
                return this.DocTransBinaryIdField;
            }
            set {
                if ((this.DocTransBinaryIdField.Equals(value) != true)) {
                    this.DocTransBinaryIdField = value;
                    this.RaisePropertyChanged("DocTransBinaryId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DocTransCode {
            get {
                return this.DocTransCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.DocTransCodeField, value) != true)) {
                    this.DocTransCodeField = value;
                    this.RaisePropertyChanged("DocTransCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long DocTransContentId {
            get {
                return this.DocTransContentIdField;
            }
            set {
                if ((this.DocTransContentIdField.Equals(value) != true)) {
                    this.DocTransContentIdField = value;
                    this.RaisePropertyChanged("DocTransContentId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long DocTransID {
            get {
                return this.DocTransIDField;
            }
            set {
                if ((this.DocTransIDField.Equals(value) != true)) {
                    this.DocTransIDField = value;
                    this.RaisePropertyChanged("DocTransID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DocTypeCode {
            get {
                return this.DocTypeCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.DocTypeCodeField, value) != true)) {
                    this.DocTypeCodeField = value;
                    this.RaisePropertyChanged("DocTypeCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime DtmCrt {
            get {
                return this.DtmCrtField;
            }
            set {
                if ((this.DtmCrtField.Equals(value) != true)) {
                    this.DtmCrtField = value;
                    this.RaisePropertyChanged("DtmCrt");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime DtmUpd {
            get {
                return this.DtmUpdField;
            }
            set {
                if ((this.DtmUpdField.Equals(value) != true)) {
                    this.DtmUpdField = value;
                    this.RaisePropertyChanged("DtmUpd");
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
        public string Pixel {
            get {
                return this.PixelField;
            }
            set {
                if ((object.ReferenceEquals(this.PixelField, value) != true)) {
                    this.PixelField = value;
                    this.RaisePropertyChanged("Pixel");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal SizeFileBytes {
            get {
                return this.SizeFileBytesField;
            }
            set {
                if ((this.SizeFileBytesField.Equals(value) != true)) {
                    this.SizeFileBytesField = value;
                    this.RaisePropertyChanged("SizeFileBytes");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TransID {
            get {
                return this.TransIDField;
            }
            set {
                if ((object.ReferenceEquals(this.TransIDField, value) != true)) {
                    this.TransIDField = value;
                    this.RaisePropertyChanged("TransID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserName {
            get {
                return this.UserNameField;
            }
            set {
                if ((object.ReferenceEquals(this.UserNameField, value) != true)) {
                    this.UserNameField = value;
                    this.RaisePropertyChanged("UserName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UsrCrt {
            get {
                return this.UsrCrtField;
            }
            set {
                if ((object.ReferenceEquals(this.UsrCrtField, value) != true)) {
                    this.UsrCrtField = value;
                    this.RaisePropertyChanged("UsrCrt");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UsrUpd {
            get {
                return this.UsrUpdField;
            }
            set {
                if ((object.ReferenceEquals(this.UsrUpdField, value) != true)) {
                    this.UsrUpdField = value;
                    this.RaisePropertyChanged("UsrUpd");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference3.IService1")]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetData", ReplyAction="http://tempuri.org/IService1/GetDataResponse")]
        string GetData(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetData", ReplyAction="http://tempuri.org/IService1/GetDataResponse")]
        System.Threading.Tasks.Task<string> GetDataAsync(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetDataUsingDataContract", ReplyAction="http://tempuri.org/IService1/GetDataUsingDataContractResponse")]
        Adibrata.Framework.Messaging.ServiceReference3.CompositeType GetDataUsingDataContract(Adibrata.Framework.Messaging.ServiceReference3.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetDataUsingDataContract", ReplyAction="http://tempuri.org/IService1/GetDataUsingDataContractResponse")]
        System.Threading.Tasks.Task<Adibrata.Framework.Messaging.ServiceReference3.CompositeType> GetDataUsingDataContractAsync(Adibrata.Framework.Messaging.ServiceReference3.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/DocTransInquiryDetail", ReplyAction="http://tempuri.org/IService1/DocTransInquiryDetailResponse")]
        System.Data.DataTable DocTransInquiryDetail(Adibrata.Framework.Messaging.ServiceReference3.DocTrans _ent);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/DocTransInquiryDetail", ReplyAction="http://tempuri.org/IService1/DocTransInquiryDetailResponse")]
        System.Threading.Tasks.Task<System.Data.DataTable> DocTransInquiryDetailAsync(Adibrata.Framework.Messaging.ServiceReference3.DocTrans _ent);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/DocTransContentDetail", ReplyAction="http://tempuri.org/IService1/DocTransContentDetailResponse")]
        System.Data.DataTable DocTransContentDetail(Adibrata.Framework.Messaging.ServiceReference3.DocTrans _ent);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/DocTransContentDetail", ReplyAction="http://tempuri.org/IService1/DocTransContentDetailResponse")]
        System.Threading.Tasks.Task<System.Data.DataTable> DocTransContentDetailAsync(Adibrata.Framework.Messaging.ServiceReference3.DocTrans _ent);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/DocTransGetTransID", ReplyAction="http://tempuri.org/IService1/DocTransGetTransIDResponse")]
        long DocTransGetTransID(Adibrata.Framework.Messaging.ServiceReference3.DocTrans _ent);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/DocTransGetTransID", ReplyAction="http://tempuri.org/IService1/DocTransGetTransIDResponse")]
        System.Threading.Tasks.Task<long> DocTransGetTransIDAsync(Adibrata.Framework.Messaging.ServiceReference3.DocTrans _ent);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : Adibrata.Framework.Messaging.ServiceReference3.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<Adibrata.Framework.Messaging.ServiceReference3.IService1>, Adibrata.Framework.Messaging.ServiceReference3.IService1 {
        
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
        
        public Adibrata.Framework.Messaging.ServiceReference3.CompositeType GetDataUsingDataContract(Adibrata.Framework.Messaging.ServiceReference3.CompositeType composite) {
            return base.Channel.GetDataUsingDataContract(composite);
        }
        
        public System.Threading.Tasks.Task<Adibrata.Framework.Messaging.ServiceReference3.CompositeType> GetDataUsingDataContractAsync(Adibrata.Framework.Messaging.ServiceReference3.CompositeType composite) {
            return base.Channel.GetDataUsingDataContractAsync(composite);
        }
        
        public System.Data.DataTable DocTransInquiryDetail(Adibrata.Framework.Messaging.ServiceReference3.DocTrans _ent) {
            return base.Channel.DocTransInquiryDetail(_ent);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> DocTransInquiryDetailAsync(Adibrata.Framework.Messaging.ServiceReference3.DocTrans _ent) {
            return base.Channel.DocTransInquiryDetailAsync(_ent);
        }
        
        public System.Data.DataTable DocTransContentDetail(Adibrata.Framework.Messaging.ServiceReference3.DocTrans _ent) {
            return base.Channel.DocTransContentDetail(_ent);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> DocTransContentDetailAsync(Adibrata.Framework.Messaging.ServiceReference3.DocTrans _ent) {
            return base.Channel.DocTransContentDetailAsync(_ent);
        }
        
        public long DocTransGetTransID(Adibrata.Framework.Messaging.ServiceReference3.DocTrans _ent) {
            return base.Channel.DocTransGetTransID(_ent);
        }
        
        public System.Threading.Tasks.Task<long> DocTransGetTransIDAsync(Adibrata.Framework.Messaging.ServiceReference3.DocTrans _ent) {
            return base.Channel.DocTransGetTransIDAsync(_ent);
        }
    }
}
