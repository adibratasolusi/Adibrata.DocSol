using Adibrata.Framework.Logging;
using Adibrata.Framework.Messaging;
using System;
using System.Data;

namespace Adibrata.Web
{
    public partial class DocTransView : System.Web.UI.Page
    {
        string docTransCode = "JKTPROJ20141203";
        string userName = "Fredy";
        Int64 docTransId;
        public void Page_Load(object sender, EventArgs e)
        {
            #region MyRegion
            //WCFEntities _ent = new WCFEntities();
            //DataTable _dt = new DataTable();
            ////_ent.DocTransCode = Request.QueryString["DocTransCode"];
            ////_ent.UserName = Request.QueryString["UserName"];
            //_dt = MessageToWCF.DocTransInquiryDetail(_ent);

            //_ent.DocTransCode = "JKTPROJ201412067";
            //_ent.UserName = "fredy";

            ////_ent.FileName = "88_Surat Penawaran harga - Rabobank Indonesia.pdf";
            ////_ent.UserName = "fredy";


            ////DataTable _dtc = new DataTable();
            ////_dtc = MessageToWCF.DocTransContentDetail(_ent);
            #endregion
     
     
            bindContent();

            bindBinary();
           
        }



        private void bindContent()
        {
            try
            {
                WCFEntities _ent = new WCFEntities();
                DataTable _dt = new DataTable();
                _ent.DocTransCode = docTransCode;
                _ent.UserName = userName;
                _dt = MessageToWCF.DocTransContentDetail(_ent); 
                gvContent.DataSource = _dt;
                gvContent.DataBind();
            }
            catch (Exception _exp)
            {

                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = "UserControl",
                    NameSpace = "Adibrata.Windows.UserController",
                    ClassName = "UCDocTransBinaryContentView",
                    FunctionName = "bindContent",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }
        private void bindBinary()
        {
            try
            {
                WCFEntities _ent = new WCFEntities();
                DataTable _dt = new DataTable();
                _ent.DocTransCode =docTransCode;
                _ent.UserName = userName;
                _dt = MessageToWCF.DocTransInquiryDetail(_ent);
         
                gvBinary.DataSource = _dt;
                gvBinary.DataBind();
                
            }
            catch (Exception _exp)
            {

                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = "UserControl",
                    NameSpace = "Adibrata.Windows.UserController",
                    ClassName = "UCDocTransBinaryContentView",
                    FunctionName = "bindBinary",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }

        }

        protected void gvBinary_SelectedIndexChanged(object sender, EventArgs e)
        {


        }
     
 

   
      

        
    }
}