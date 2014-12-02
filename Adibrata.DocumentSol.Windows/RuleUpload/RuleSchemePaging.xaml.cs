using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Framework.Logging;
using Adibrata.Windows.UserController;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Adibrata.DocumentSol.Windows.RuleUpload
{
    /// <summary>
    /// Interaction logic for RuleSchemePaging.xaml
    /// </summary>
    /// 

    public partial class RuleSchemePaging : Page
    {
        SessionEntities SessionProperty;
        public RuleSchemePaging(SessionEntities _session)
        {
            InitializeComponent();
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder(8000);
            try
            {
                oPaging.ClassName = "RuleScheme";
                oPaging.MethodName = "RuleSchemePaging";
                oPaging.dgObj = dgPaging;
                if (txtRule.Text != "")
                {
                    sb.Append(" Where ");
                    sb.Append(" RuleSchmDesc = '");
                    sb.Append(txtRule.Text);
                    sb.Append("'");
                }
                else
                {
                    sb.Append("");
                }
                oPaging.WhereCond = sb.ToString();
                oPaging.SortBy = " CustName Asc ";
                oPaging.UserName = SessionProperty.UserName;
                oPaging.PagingData();
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.RuleUpload",
                    ClassName = "RuleSchemePaging",
                    FunctionName = "btnSearch_Click",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RedirectPage redirect = new RedirectPage(this, "RuleUpload.RuleEngineUpload", SessionProperty);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.RuleUpload",
                    ClassName = "RuleSchemePaging",
                    FunctionName = "btnAdd_Click",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int i = dgPaging.SelectedIndex;
                DataGridHelper oDataGrid = new DataGridHelper();
                oDataGrid.dtg = dgPaging;
                DataGridCell cell = oDataGrid.GetCell(i, 2);
                TextBlock ReffKey = oDataGrid.GetVisualChild<TextBlock>(cell); // pass the DataGridCell as a parameter to GetVisualChild
                SessionProperty.IsEdit = true;
                SessionProperty.ReffKey = Convert.ToInt64(ReffKey.Text);
                RedirectPage redirect = new RedirectPage(this, "RuleUpload.RuleEngineUpload", SessionProperty);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.RuleUpload",
                    ClassName = "RuleSchemePaging",
                    FunctionName = "btnUpload_Click",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

     
    }
}
