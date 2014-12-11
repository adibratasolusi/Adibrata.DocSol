using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Adibrata.Framework.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here

        [OperationContract]
        void UpdatePathDetails(PathDetails pathInfo);

        [OperationContract]
        byte[] imageToByteArray(System.Drawing.Image imageIn);

        [OperationContract]
        Image byteArrayToImage(byte[] byteArrayIn);

        [OperationContract]
        DateTime getImageDtCreate(Image img);
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
        Int64 docTransID;
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
