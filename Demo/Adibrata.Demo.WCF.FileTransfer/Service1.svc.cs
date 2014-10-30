using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Adibrata.Demo.WCF.FileTransfer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
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
        public void UpdatePathDetails(PathDetails pathInfo)
        {


            var webClient = new WebClient();
            byte[] fileBytes = webClient.DownloadData("file://PC195/BITS/" + pathInfo.FileName + pathInfo.Ext);
            string strMessage = string.Empty;
            SqlConnection con = new SqlConnection(conString);
            int result = 0;
            try
            {
                SqlCommand command = new SqlCommand("spUpdatePath", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@file", SqlDbType.VarChar).Value = pathInfo.FileName;
                command.Parameters.Add("@bin", SqlDbType.VarBinary).Value = fileBytes;
                command.Parameters.Add("@ext", SqlDbType.VarChar).Value = pathInfo.Ext;
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
            catch (Exception ex)
            {
                //logging app here
            }

        }
    }
}
