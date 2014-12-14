using Adibrata.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using Adibrata.Framework.Logging;
using Adibrata.BusinessProcess.Entities.Base;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Globalization;

namespace Adibrata.Framework.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        static string Connectionstring = AppConfig.Config("ConnectionString");
        SessionEntities SessionProperty = new SessionEntities();
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }
        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
        public DateTime getImageDtCreate(Image img)
        {
            PropertyItem propItem = img.GetPropertyItem(0x132);
            DateTime dt = new DateTime();
            if (propItem != null)
            {
                System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                string text = encoding.GetString(propItem.Value, 0, propItem.Len - 1);

                CultureInfo provider = CultureInfo.InvariantCulture;
                 dt = DateTime.ParseExact(text, "yyyy:MM:d H:m:s", provider);

                
            }
            else 
            {
                dt = DateTime.Now;
            }
            return dt;
        }

        public void UpdatePathDetails(PathDetails pathInfo)
        {


            string bitsServer = AppConfig.Config("BITSServer");
            var webClient = new WebClient();
            byte[] fileBytes = webClient.DownloadData(bitsServer + pathInfo.FileName);
            pathInfo.FileName = pathInfo.DocTransBinaryID+pathInfo.FileName;
            string strMessage = string.Empty;
            SqlConnection con = new SqlConnection(Connectionstring);
            int result = 0;
            try
            {

                SqlCommand command = new SqlCommand("spDocTransBinaryUpdate", con);
                command.CommandType = CommandType.StoredProcedure;
                
                command.Parameters.Add("@DocTransBinaryID", SqlDbType.BigInt).Value = pathInfo.DocTransBinaryID;
                command.Parameters.Add("@FileName", SqlDbType.VarChar).Value = pathInfo.FileName;
                command.Parameters.Add("@FileBinary", SqlDbType.VarBinary).Value = fileBytes;
                con.Open();
                result = command.ExecuteNonQuery();
                con.Close();

                if (result == 1)
                {

                    //logging success here
                }
                else
                {

                    //logging error db here
                }
            }
            catch (Exception _exp)
            {
                //logging app here
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.Framework.WCF",
                    ClassName = "Service1",
                    FunctionName = "UpdatePathDetails",
                    ExceptionNumber = 1,
                    EventSource = "UploadServices",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }

        }
    }
}
