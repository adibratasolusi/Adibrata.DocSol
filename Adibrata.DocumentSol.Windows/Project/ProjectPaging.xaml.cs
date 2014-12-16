using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Framework.Logging;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Adibrata.Windows.UserController;
using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.Controller;

namespace Adibrata.DocumentSol.Windows.Project
{
    /// <summary>
    /// Interaction logic for ProjectPaging.xaml
    /// </summary>
    public partial class ProjectPaging : Page
    {
        SessionEntities SessionProperty;
        DocSolEntities _ent = new DocSolEntities();
        public ProjectPaging(SessionEntities _session)
        {
            try
            {
                InitializeComponent();
                this.DataContext = new MainVM(new Shell());
                SessionProperty = _session;
                
                DocSolEntities _ent = new DocSolEntities
                {
                    ClassName = "CustomerRegistrasi",
                    MethodName = "CustomerCompanyRegistrasiView",
                    CustomerCode = _session.ReffKey
                };
                _ent = DocumentSolutionController.DocSolProcess<DocSolEntities>(_ent);
                lblCustCode.Text = _session.ReffKey;
                lblCustName.Text = _ent.CompanyName;
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.Project",
                    ClassName = "ProjectPaging",
                    FunctionName = "ProjectPaging",
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
            try
            {
                oPaging.ClassName = "ProjectRegistrasi";
                oPaging.MethodName = "ProjectRegisterPaging";
                oPaging.dgObj = dgPaging;
                sb.Append(" Where ");
                sb.Append(" CustCode = '");
                sb.Append(SessionProperty.ReffKey);
                sb.Append(" ' ");

                if (txtProjectName.Text != "")
                {
                    sb.Append(" AND ");
                    if (txtProjectName.Text.Contains("%"))
                    {
                        sb.Append(" ProjName LIKE '");
                    }
                    else
                    {
                        sb.Append(" ProjName = '");
                    }
                    sb.Append(txtProjectName.Text);
                    sb.Append("' ");
                }
                if (txtProjectCode.Text != "")
                {
                    sb.Append(" AND ");
                    if (txtProjectCode.Text.Contains("%"))
                    {
                        sb.Append(" ProjCode LIKE '");
                    }
                    else
                    {
                        sb.Append(" ProjCode = '");
                    }
                    sb.Append(txtProjectCode.Text);
                    sb.Append("' ");
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

        private void btnEdit_Click(object sender, RoutedEventArgs e)
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
                
                RedirectPage redirect = new RedirectPage(this, "Project.ProjectAddEdit", SessionProperty);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.Project",
                    ClassName = "ProjectPaging",
                    FunctionName = "btnEdit_Click",
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
            try
            {

                RedirectPage redirect = new RedirectPage(this, "Project.CustomerProjectPaging", SessionProperty);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.Project",
                    ClassName = "ProjectPaging",
                    FunctionName = "btnEdit_Click",
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
        {  try
            {
                SessionProperty.IsEdit = false;

                RedirectPage redirect = new RedirectPage(this, "Project.ProjectAddEdit", SessionProperty);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.Project",
                    ClassName = "ProjectPaging",
                    FunctionName = "btnEdit_Click",
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
