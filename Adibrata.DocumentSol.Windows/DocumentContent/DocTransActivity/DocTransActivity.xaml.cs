using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Framework.Logging;
using Adibrata.Windows.UserController;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Adibrata.DocumentSol.Windows.DocumentContent
{
    /// <summary>
    /// Interaction logic for DocTransActivity.xaml
    /// </summary>
    public partial class DocTransActivity : Page
    {
        SessionEntities SessionProperty;
        DocSolEntities _ent = new DocSolEntities();
        public DocTransActivity(SessionEntities _session)
        {
            try
            {
                InitializeComponent();
                this.DataContext = new MainVM(new Shell());
                SessionProperty = _session;
                oFavorite.UserLogin = SessionProperty.UserName;
                oFavorite.FormUrl = "DocumentContent.DocTransActivity";
                oFavorite.DisableFavorit();
                txtUserName.Text = SessionProperty.UserName;
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.DocumentContent",
                    ClassName = "DocTransActivity",
                    FunctionName = "DocTransActivity",
                    ExceptionNumber = 1,
                    EventSource = "DocTransActivity",
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
                if (txtUserName.Text == "")
                {
                    MessageBox.Show("Please Enter User Name Search Criteria");
                }
                else
                {
                    oPaging.ClassName = "DocTransActivity";
                    oPaging.MethodName = "DocTransActivityPaging";
                    oPaging.dgObj = dgPaging;
                    if (txtCustCode.Text != "" || txtCustName.Text != "" || txtProjCode.Text != "" || txtProjName.Text != "" || txtDocType.Text != "")
                    {
                        sb.Append(" Where ");
                        sb.Append(" A.UserName = '");
                        sb.Append(txtUserName.Text.Trim());
                        sb.Append("'");


                        if (txtCustCode.Text != "")
                        {
                            if (txtCustCode.Text.Contains("%"))
                            {
                                sb.Append(" AND D.CustCode LIKE '");
                            }
                            else
                            {
                                sb.Append(" AND D.CustCode = '");
                            }
                            sb.Append(txtCustCode.Text);
                            sb.Append("'");
                        }

                        if (txtCustName.Text != "")
                        {

                            if (txtCustName.Text.Contains("%"))
                            {
                                sb.Append(" AND D.CustName LIKE '");
                            }
                            else
                            {
                                sb.Append(" AND  D.CustName = '");
                            }
                            sb.Append(txtCustName.Text);
                            sb.Append("'");
                        }
                        if (txtProjCode.Text != "")
                        {

                            if (txtProjCode.Text.Contains("%"))
                            {
                                sb.Append(" AND C.ProjCode LIKE '");
                            }
                            else
                            {
                                sb.Append(" AND C.ProjCode = '");
                            }
                            sb.Append(txtProjCode.Text);
                            sb.Append("'");
                        }
                        if (txtProjName.Text != "")
                        {

                            if (txtProjName.Text.Contains("%"))
                            {
                                sb.Append(" AND C.ProjName LIKE '");
                            }
                            else
                            {
                                sb.Append(" AND C.ProjName = '");
                            }
                            sb.Append(txtProjName.Text);
                            sb.Append("'");
                        }
                        if (txtDocType.Text != "")
                        {

                            if (txtDocType.Text.Contains("%"))
                            {
                                sb.Append(" AND B.DocTypeCode LIKE '");
                            }
                            else
                            {
                                sb.Append(" AND B.DocTypeCode = '");
                            }
                            sb.Append(txtDocType.Text);
                            sb.Append("'");
                        }
                    }
                    oPaging.WhereCond = sb.ToString();
                    oPaging.SortBy = " C.ProjName Asc ";
                    oPaging.UserName = SessionProperty.UserName;
                    oPaging.PagingData();
                }
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.DocumentContent",
                    ClassName = "DocTransActivity",
                    FunctionName = "btnSearch_Click",
                    ExceptionNumber = 1,
                    EventSource = "DocTransActivity",
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
                SessionProperty.SourceForm = "DocumentContent.DocTransActivity";
                RedirectPage redirect = new RedirectPage(this, "UploadInquiry.UploadDetailInquiry", SessionProperty);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.DocumentContent",
                    ClassName = "DocTransActivity",
                    FunctionName = "btnDetail_Click",
                    ExceptionNumber = 1,
                    EventSource = "DocTransActivity",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }
    }
}
