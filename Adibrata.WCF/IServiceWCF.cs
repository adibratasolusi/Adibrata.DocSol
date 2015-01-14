using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Adibrata.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServiceWCF" in both code and config file together.
    [ServiceContract]
    public interface IServiceWCF
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        void ArchieveExecutionProcess(DocTrans _ent);

        [OperationContract]
        DataTable DocTransInquiryDetail(DocTrans _ent);

        [OperationContract]
        DataTable DocTransContentDetail(DocTrans _ent);

        [OperationContract]
        Int64 DocTransGetTransID(DocTrans _ent);

        [OperationContract]
        void UpdatePathDetails(PathDetails pathInfo);

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class DocTrans
    {
        Int64 docTransID;
        string docTransCode;
        string transID = string.Empty;
        string docTypeCode = string.Empty;
        string usrUpd = string.Empty;
        DateTime dtmUpd = DateTime.Now;
        string usrCrt = string.Empty;
        DateTime dtmCrt = DateTime.Now;

        Int64 docTransBinaryId;
        string fileName = string.Empty;
        DateTime dateCreated = DateTime.Now;
        decimal sizeFileBytes;
        string pixel = string.Empty;
        string computerName = string.Empty;
        string dPI = string.Empty;
        byte[] fileBinary;

        Int64 docTransContentId;
        string contentName = string.Empty;
        string contentValue = string.Empty;
        DateTime contenValueDate = DateTime.Now;
        decimal contentValueNumeric;
        string contentSearchTag = string.Empty;

        string userName;


        [DataMember]
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        [DataMember]
        public string DocTransCode
        {
            get { return docTransCode; }
            set { docTransCode = value; }
        }
        [DataMember]
        public Int64 DocTransID
        {
            get { return docTransID; }
            set { docTransID = value; }
        }
        [DataMember]
        public string TransID
        {
            get { return transID; }
            set { transID = value; }
        }
        [DataMember]
        public string DocTypeCode
        {
            get { return docTypeCode; }
            set { docTypeCode = value; }
        }
        [DataMember]
        public string UsrUpd
        {
            get { return usrUpd; }
            set { usrUpd = value; }
        }

        [DataMember]
        public string UsrCrt
        {
            get { return usrCrt; }
            set { usrCrt = value; }
        }
        [DataMember]
        public DateTime DtmCrt
        {
            get { return dtmCrt; }
            set { dtmCrt = value; }
        }
        [DataMember]
        public DateTime DtmUpd
        {
            get { return dtmUpd; }
            set { dtmUpd = value; }
        }
        [DataMember]
        public Int64 DocTransBinaryId
        {
            get { return docTransBinaryId; }
            set { docTransBinaryId = value; }
        }
        [DataMember]
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }
        [DataMember]
        public DateTime DateCreated
        {
            get { return dateCreated; }
            set { dateCreated = value; }
        }
        [DataMember]
        public decimal SizeFileBytes
        {
            get { return sizeFileBytes; }
            set { sizeFileBytes = value; }
        }
        [DataMember]
        public string Pixel
        {
            get { return pixel; }
            set { pixel = value; }
        }
        [DataMember]
        public string ComputerName
        {
            get { return computerName; }
            set { computerName = value; }
        }
        [DataMember]
        public string DPI
        {
            get { return dPI; }
            set { dPI = value; }
        }
        [DataMember]
        public byte[] FileBinary
        {
            get { return fileBinary; }
            set { fileBinary = value; }
        }
        [DataMember]
        public Int64 DocTransContentId
        {
            get { return docTransContentId; }
            set { docTransContentId = value; }
        }
        [DataMember]
        public string ContentName
        {
            get { return contentName; }
            set { contentName = value; }
        }
        [DataMember]
        public string ContentValue
        {
            get { return contentValue; }
            set { contentValue = value; }
        }
        [DataMember]
        public DateTime ContenValueDate
        {
            get { return contenValueDate; }
            set { contenValueDate = value; }
        }
        [DataMember]
        public decimal ContentValueNumeric
        {
            get { return contentValueNumeric; }
            set { contentValueNumeric = value; }
        }
        [DataMember]
        public string ContentSearchTag
        {
            get { return contentSearchTag; }
            set { contentSearchTag = value; }
        }

    }
    [DataContract]
    public class PathDetails
    {
        Int64 docTransID;
        Int64 docTransBinaryID;
        string fileName = string.Empty;
        DateTime dateCreated;
        decimal sizeFileBytes;
        string pixel = string.Empty;
        string computerName = string.Empty;
        string dPI = string.Empty;
        byte[] fileBinary;

        [DataMember]
        public Int64 DocTransID
        {
            get { return docTransID; }
            set { docTransID = value; }
        }
        [DataMember]
        public Int64 DocTransBinaryID
        {
            get { return docTransBinaryID; }
            set { docTransBinaryID = value; }
        }
        [DataMember]
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }
        [DataMember]
        public DateTime DateCreated
        {
            get { return dateCreated; }
            set { dateCreated = value; }
        }
        [DataMember]
        public decimal SizeFileBytes
        {
            get { return sizeFileBytes; }
            set { sizeFileBytes = value; }
        }
        [DataMember]
        public string Pixel
        {
            get { return pixel; }
            set { pixel = value; }
        }
        [DataMember]
        public string ComputerName
        {
            get { return computerName; }
            set { computerName = value; }
        }
        [DataMember]
        public string DPI
        {
            get { return dPI; }
            set { dPI = value; }
        }
        [DataMember]
        public byte[] FileBinary
        {
            get { return fileBinary; }
            set { fileBinary = value; }
        }
    }
}
