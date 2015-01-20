using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Framework.Logging;
using Adibrata.Windows.UserController;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Adibrata.DocumentSol.Windows.UploadInquiry
{
    /// <summary>
    /// Interaction logic for UploadInquiry.xaml
    /// </summary>
    public partial class UploadInquiry : Page
    {
        SessionEntities SessionProperty;
        DocSolEntities _ent = new DocSolEntities();
        public UploadInquiry(SessionEntities _session)
        {
            try
            {
                InitializeComponent();
                this.DataContext = new MainVM(new Shell());
                SessionProperty = _session;
                oFavorite.UserLogin = SessionProperty.UserName;
                oFavorite.FormUrl = "UploadInquiry.UploadInquiry";
                oFavorite.DisableFavorit();
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.UploadInquiry",
                    ClassName = "UploadInquiry",
                    FunctionName = "UploadInquiry",
                    ExceptionNumber = 1,
                    EventSource = "UploadInquiry",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder(8000);
            try
            {
                oPaging.ClassName = "UploadProcessPaging";
                oPaging.MethodName = "UploadInquiry";
                oPaging.dgObj = dgPaging;
                if (txtCustCode.Text != "" || txtCustName.Text != "" || txtProjCode.Text != "" || txtProjName.Text != "" || txtDocType.Text != "")
                {
                    sb.Append(" Where ");
                    if (txtCustCode.Text != "")
                    {

                        if (txtCustCode.Text.Contains("%"))
                        {
                            sb.Append(" Cust.CustCode LIKE '");
                        }
                        else
                        {
                            sb.Append(" Cust.CustCode = '");
                        }
                        sb.Append(txtCustCode.Text);
                        sb.Append("'");
                    }

                    if (txtCustName.Text != "")
                    {

                        if (txtCustName.Text.Contains("%"))
                        {
                            sb.Append("  Cust.CustName LIKE '");
                        }
                        else
                        {
                            sb.Append("  Cust.CustName = '");
                        }
                        sb.Append(txtCustName.Text);
                        sb.Append("'");
                    }
                    if (txtProjCode.Text != "")
                    {

                        if (txtProjCode.Text.Contains("%"))
                        {
                            sb.Append(" Proj.ProjCode LIKE '");
                        }
                        else
                        {
                            sb.Append(" Proj.ProjCode = '");
                        }
                        sb.Append(txtProjCode.Text);
                        sb.Append("'");
                    }
                    if (txtProjName.Text != "")
                    {

                        if (txtProjName.Text.Contains("%"))
                        {
                            sb.Append(" Proj.ProjName LIKE '");
                        }
                        else
                        {
                            sb.Append(" Proj.ProjName = '");
                        }
                        sb.Append(txtProjName.Text);
                        sb.Append("'");
                    }
                    if (txtDocType.Text != "")
                    {

                        if (txtDocType.Text.Contains("%"))
                        {
                            sb.Append(" DocTypeCode LIKE '");
                        }
                        else
                        {
                            sb.Append(" DocTypeCode = '");
                        }
                        sb.Append(txtDocType.Text);
                        sb.Append("'");
                    }
                }
                oPaging.WhereCond = sb.ToString();
                oPaging.SortBy = " Proj.ProjName Asc ";
                oPaging.UserName = SessionProperty.UserName;
                oPaging.PagingData();
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.UploadInquiry",
                    ClassName = "UploadInquiry",
                    FunctionName = "btnSearch_Click",
                    ExceptionNumber = 1,
                    EventSource = "UploadInquiry",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int i = dgPaging.SelectedIndex;

                DataGridHelper oDataGrid = new DataGridHelper();
                oDataGrid.dtg = dgPaging;
                DataGridCell cell = oDataGrid.GetCell(i, 1);
                TextBlock ReffKey = oDataGrid.GetVisualChild<TextBlock>(cell); // pass the DataGridCell as a parameter to GetVisualChild
                SessionProperty.IsEdit = true;
                SessionProperty.ReffKey = ReffKey.Text;
                SessionProperty.SourceForm= "UploadInquiry.UploadInquiry";
                
                RedirectPage redirect = new RedirectPage(this, "UploadInquiry.UploadDetailInquiry", SessionProperty);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.UploadInquiry",
                    ClassName = "DocumentUploadPaging",
                    FunctionName = "btnUpload_Click",
                    ExceptionNumber = 1,
                    EventSource = "UploadInquiry",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }
    }
}
