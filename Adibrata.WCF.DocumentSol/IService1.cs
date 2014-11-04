using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Adibrata.WCF.DocumentSol
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

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
    public class PathDetails
    {
        string agrmntNo = string.Empty;
        string docType = string.Empty;
        string fileName = string.Empty;
        string ext = string.Empty;
        Int64 pathId;
        byte[] fileBinary;

        [DataMember]
        public string AgrmntNo
        {
            get { return agrmntNo; }
            set { agrmntNo = value; }
        }
        [DataMember]
        public string Ext
        {
            get { return ext; }
            set { ext = value; }
        }
        [DataMember]
        public string DocType
        {
            get { return docType; }
            set { docType = value; }
        }
        [DataMember]
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }
        [DataMember]
        public byte[] FileBinary
        {
            get { return fileBinary; }
            set { fileBinary = value; }
        }
        [DataMember]
        public Int64 PathId
        {
            get { return pathId; }
            set { pathId = value; }
        }
    }
}
