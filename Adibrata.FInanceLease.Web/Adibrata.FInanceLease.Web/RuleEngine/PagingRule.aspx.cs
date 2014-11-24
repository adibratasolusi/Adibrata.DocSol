using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Adibrata.Configuration;
using Adibrata.Rule.Engine;
using System.Data;
using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Controller.Paging;
using Adibrata.BusinessProcess.Paging.Entities;

namespace Adibrata.FinanceLease.Web
{
    public partial class PagingRule : System.Web.UI.Page
    {
        static string Connectionstring = AppConfig.Config("ConnectionString");
        static string PageSize = AppConfig.Config("PageSize");
        
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                DataTable _dt = new DataTable();
                PagingEntities _ent = new PagingEntities { ClassName = "RuleSchemePaging", MethodName = "RuleEngineList" };

                _ent.CurrentPage = 1;
                _ent.WhereCond = "";
                _ent.SortBy = "";

                _dt = PagingController.PagingData<DataTable>(_ent);

                grdRule.DataSource = _dt;
                grdRule.DataBind();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("UploadRule.aspx");
        }

        protected void grdRule_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Write(grdRule.DataKeys[grdRule.SelectedIndex].Value.ToString());
        }

        
    }
}