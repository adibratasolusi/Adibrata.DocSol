using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Framework.Logging;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Adibrata.Windows.UserController;
using Adibrata.BusinessProcess.DocumentSol.Entities;
using System.Data;
using System.Collections.Generic;
using Adibrata.Controller;

namespace Adibrata.DocumentSol.Windows.DocumentContent.Approval
{
    /// <summary>
    /// Interaction logic for ApprovalPaging.xaml
    /// </summary>
    public partial class ApprovalPaging : Page
    {
        SessionEntities SessionProperty;
        DocSolEntities _ent = new DocSolEntities();
        public ApprovalPaging(SessionEntities _session)
        {
            List<string> data = new List<string>();
            List<string> statusApproval = new List<string>();
            try
            {
                InitializeComponent();
                this.DataContext = new MainVM(new Shell());
                SessionProperty = _session;

                DataTable _dt = new DataTable();
                _ent.ClassName = "ProjectRegistrasi";
                _ent.MethodName = "ProjectTypeReceive";
                _dt = DocumentSolutionController.DocSolProcess<DataTable>(_ent);
                
                if (_dt.Rows.Count > 0)
                {
                    foreach (DataRow _row in _dt.Rows)
                    {
                        data.Add(_row["Result"].ToString());
                    }
                }

                cboProjectCode.ItemsSource = data;
                #region "List Approval Status"
                

                statusApproval.Add("New");
                statusApproval.Add("In Progress");
                statusApproval.Add("Final");
                cboApprStatus.ItemsSource = statusApproval;
                #endregion 
                lblUserName.Text = SessionProperty.UserName;


            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.DocumentContent.Approval",
                    ClassName = "ApprovalPaging",
                    FunctionName = "ApprovalPaging",
                    ExceptionNumber = 1,
                    EventSource = "ApprovalDocContent",
                    ExceptionObject = _exp,
                    EventID = 200,
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
                #region "Getting path Approval from Rule Configuration"
                DocSolEntities _ent = new DocSolEntities
                {
                    ClassName = "ApprovalProcess",
                    MethodName = "ApprovalPathRetrieve",
                    DocumentType = cboDocumentType.SelectedItem.ToString(),
                    UserLogin = SessionProperty.UserName
                };
                _ent = DocumentSolutionController.DocSolProcess<DocSolEntities>(_ent);
                #endregion 

                oPaging.ClassName = "DocContentApproval";
                oPaging.MethodName = "ApprovalTaskPaging";
                oPaging.dgObj = dgPaging;
                if (txtCustName.Text != "")
                {
                    sb.Append(" Where ");
                    sb.Append(" DocTypeCode = '");
                    sb.Append(cboDocumentType.SelectedItem.ToString());
                    sb.Append("'");
                    sb.Append(" AND ");
                    sb.Append(" NextLevelAppr = '");
                    sb.Append(_ent.UserApprovalPath);
                    sb.Append("'");
                }
                else
                {
                    sb.Append("");
                }
                oPaging.WhereCond = sb.ToString();
                oPaging.SortBy = " DocTransReqDate Asc ";
                oPaging.UserName = SessionProperty.UserName;
                oPaging.PagingData();
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.DocumentContent.Approval",
                    ClassName = "ApprovalPaging",
                    FunctionName = "btnSearch_Click",
                    ExceptionNumber = 1,
                    EventSource = "ApprovalDocContent",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        private void btnApproval_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                RedirectPage redirect = new RedirectPage(this, "Approval.ApprovalProcessScreen", SessionProperty);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.DocumentContent.Approval",
                    ClassName = "ApprovalPaging",
                    FunctionName = "btnApproval_Click",
                    ExceptionNumber = 1,
                    EventSource = "ApprovalDocContent",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        private void rdoApproval_Checked(object sender, RoutedEventArgs e)
        {

            try
            {
                int i = dgPaging.SelectedIndex;

                DataGridHelper oDataGrid = new DataGridHelper();
                oDataGrid.dtg = dgPaging;
                DataGridCell cell = oDataGrid.GetCell(i, 2);
                TextBlock ReffKey = oDataGrid.GetVisualChild<TextBlock>(cell); // pass the DataGridCell as a parameter to GetVisualChild
                SessionProperty.IsEdit = true;
                SessionProperty.ReffKey = ReffKey.Text;
                //RedirectPage redirect = new RedirectPage(this, "Customer.CustomerAddEdit", SessionProperty);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.DocumentContent.Approval",
                    ClassName = "ApprovalPaging",
                    FunctionName = "rdoApproval_Checked",
                    ExceptionNumber = 1,
                    EventSource = "ApprovalDocContent",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        private void cboProjectCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                #region "Get Document Type"

                DataTable _dt = new DataTable();
                _ent.ClassName = "DocType";
                _ent.MethodName = "DocTypeRetrieve";
                _ent.LineOfBusiness = cboProjectCode.SelectedValue.ToString();
                _dt = DocumentSolutionController.DocSolProcess<DataTable>(_ent);
                List<string> data = new List<string>();
                if (_dt.Rows.Count > 0)
                {
                    foreach (DataRow _row in _dt.Rows)
                    {
                        data.Add(_row["Result"].ToString());
                    }
                }
                cboDocumentType.ItemsSource = data;
                #endregion 
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.DocumentContent.Approval",
                    ClassName = "ApprovalPaging",
                    FunctionName = "rdoApproval_Checked",
                    ExceptionNumber = 1,
                    EventSource = "ApprovalDocContent",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

      
    }
}
