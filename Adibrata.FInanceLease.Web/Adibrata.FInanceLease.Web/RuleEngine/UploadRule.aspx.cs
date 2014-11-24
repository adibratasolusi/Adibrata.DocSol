using Adibrata.Rule.Engine;
using System;
using System.Data;
using System.Web.UI;

namespace Adibrata.FinanceLease.Web
{
    public partial class UploadRule : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                fuExcel.AllowMultiple = false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (fuExcel.HasFile)
                try
                {
                    fuExcel.SaveAs("D:\\RuleEngine\\" + fuExcel.FileName);
                    
                    RuleEngineEntities _ent = new RuleEngineEntities();
                    
                    string _path;
                    _path = @"D:\RuleEngine\" + fuExcel.FileName;
                    _ent.PathFile = _path;
                    _ent = RuleEngineProcess.UploadRuleEngine(_ent);
                    //Response.Redirect("~/RuleEngine/PagingRule.aspx");
                   //Response.Write("File name: " + fuExcel.PostedFile.FileName + "<br>" + fuExcel.PostedFile.ContentLength + " kb<br>" + "Content type: " + fuExcel.PostedFile.ContentType);
                }
                catch (Exception ex)
                {
                    Response.Write("ERROR: " + ex.Message.ToString());
                }
            else
            {
                Response.Write("You have not specified a file.");
            }
     
        }

        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public static Boolean CheckFileExists(String FileName)
        {
            String ParseFileName;
            ParseFileName = FileName.Substring(FileName.LastIndexOf("\\") + 1);
            if (System.IO.File.Exists("C:\\" + ParseFileName))
                return true;
            else
                return false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PagingRule.aspx");
        }
    }
}