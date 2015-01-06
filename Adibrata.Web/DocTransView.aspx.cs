using Adibrata.Framework.Messaging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Adibrata.Web
{
    public partial class DocTransView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WCFEntities _ent = new WCFEntities();
            DataTable _dt = new DataTable();
            //_ent.DocTransCode = Request.QueryString["DocTransCode"];
            //_ent.UserName = Request.QueryString["UserName"];

            _ent.DocTransCode = "JKTPROJ201412067";
            _ent.UserName = "fredy";

            _dt = MessageToWCF.DocTransInquiryDetail(_ent);

            DataTable _dtc = new DataTable();
            _dtc = MessageToWCF.DocTransContentDetail(_ent);

            GridView1.DataSource = _dt;
            GridView1.DataBind();
               

          

        }
    }
}