using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Framework.Logging;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Adibrata.Windows.UserController;

namespace Adibrata.DocumentSol.Windows.DocumentContent
{
    /// <summary>
    /// Interaction logic for DocumentUploadPaging.xaml
    /// </summary>
    public partial class DocumentUploadPaging : Page
    {
        SessionEntities SessionProperty;
        public DocumentUploadPaging(SessionEntities _session)
        {
            try
            {
                InitializeComponent();
                this.DataContext = new MainVM(new Shell());
                SessionProperty = _session;
                oFavorite.UserLogin = SessionProperty.UserName;
                oFavorite.FormUrl = "DocumentContent.DocumentUploadPaging";
                oFavorite.DisableFavorit();

            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.DocumentContent",
                    ClassName = "DocumentUploadPaging",
                    FunctionName = "DocumentUploadPaging",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
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
            StringBuilder sbquery = new StringBuilder(8000);
            try
            {
                oPaging.ClassName = "ProjectRegistrasi";
                oPaging.MethodName = "ProjectRegisterPaging";
                oPaging.dgObj = dgPaging;
                sb.Append("");
                if (txtCustCode.Text != "" || txtCustName.Text != "" || txtProjectCode.Text != "" || txtProjectName.Text != "")
                {
                    sb.Append(" Where ");
                    
                    if (txtCustCode.Text != "")
                    {
                        if (sbquery.ToString() != "")
                        {
                            sbquery.Append(" AND ");
                        }
                        else
                        {
                            sbquery.Append(" ");
                        }
    
                        if (txtCustCode.Text.Contains("%"))
                        {
                            sbquery.Append(" CustCode LIKE '");
                        }
                        else
                        {
                            sbquery.Append(" CustCode = '");
                        }
                        sbquery.Append(txtCustCode.Text);
                        sbquery.Append("' ");
                    }


                    if (txtCustName.Text != "")
                    {
                        if (sbquery.ToString() != "")
                        {
                            sbquery.Append(" AND ");
                        }
                        else
                        {
                            sbquery.Append(" ");
                        }


                        if (txtCustName.Text.Contains("%"))
                        {
                            sbquery.Append(" CustName LIKE '");
                        }
                        else
                        {
                            sbquery.Append(" CustName = '");
                        }
                        sbquery.Append(txtCustName.Text);
                        sbquery.Append("' ");
                    }


                    if (txtProjectName.Text != "")
                    {
                        if (sbquery.ToString() != "")
                        {
                            sbquery.Append(" AND ");
                        }
                        else
                        {
                            sbquery.Append(" ");
                        }

                        if (txtProjectName.Text.Contains("%"))
                        {
                            sbquery.Append(" ProjName LIKE '");
                        }
                        else
                        {
                            sbquery.Append(" ProjName = '");
                        }
                        sbquery.Append(txtProjectName.Text);
                        sbquery.Append("' ");
                    }

                    if (txtProjectCode.Text != "")
                    {
                        if (sbquery.ToString() != "")
                        {
                            sbquery.Append(" AND ");
                        }
                        else
                        {
                            sbquery.Append(" ");
                        }

                        if (txtProjectCode.Text.Contains("%"))
                        {
                            sbquery.Append(" ProjCode LIKE '");
                        }
                        else
                        {
                            sbquery.Append(" ProjCode = '");
                        }
                        sbquery.Append(txtProjectCode.Text);
                        sbquery.Append("' ");
                    }
                }
                if (sbquery.ToString() != "")
                {
                    sb.Append(sbquery.ToString());
                }
                oPaging.WhereCond = sb.ToString();
                oPaging.SortBy = " ProjName Asc ";
                oPaging.UserName = SessionProperty.UserName;
                oPaging.PagingData();
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.Project",
                    ClassName = "DocumentUploadPaging",
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

        private void btnUpload_Click(object sender, RoutedEventArgs e)
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
                RedirectPage redirect = new RedirectPage(this, "DocumentContent.DocumentContentEntry", SessionProperty);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.DocumentContent",
                    ClassName = "DocumentUploadPaging",
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

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
