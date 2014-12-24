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
    /// Interaction logic for ApprovalProcessScreen.xaml
    /// </summary>
    public partial class ApprovalProcessScreen : Page
    {
        SessionEntities SessionProperty;
        DocSolEntities _ent = new DocSolEntities();
        public ApprovalProcessScreen(SessionEntities _session)
        {
            try
            {
                InitializeComponent();
                this.DataContext = new MainVM(new Shell());
                SessionProperty = _session;
                _ent.ClassName = "ApprovalProcess";
                _ent.MethodName = "ApprovalTransView";
                _ent.ApprovalTransCode = SessionProperty.ReffKey;
                _ent = DocumentSolutionController.DocSolProcess<DocSolEntities>(_ent);
                lblCustomerCode.Text = _ent.CustomerCode;
                lblCustomerName.Text = _ent.CompanyName;
                lblProjectCode.Text = _ent.ProjectCode;
                lblProjectName.Text = _ent.ProjectName;
                lblProjectType.Text = _ent.ProjectType;
                lblDocumentType.Text = _ent.DocumentType;
                  #region "List Approval Status"
                

                List<string> statusApproval = new List<string>();
                statusApproval.Add("Approve");
                statusApproval.Add("Reject");
                cboApprovalStatus.ItemsSource = statusApproval;
                #endregion 
                oDocContentView.DocTransId = _ent.DocumentTransID;
               
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.DocumentContent.Approval",
                    ClassName = "ApprovalProcessScreen",
                    FunctionName = "btnSave_Click",
                    ExceptionNumber = 1,
                    EventSource = "ApprovalDocContent",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _ent.ClassName = "ApprovalProcess";
                _ent.MethodName = "ApprovalDocContentSave";
                _ent.ApprovalStatus = cboApprovalStatus.SelectedValue.ToString();
                _ent.ApprovalNotes = txtNotes.Text;
                DocumentSolutionController.DocSolProcess<DataTable>(_ent);
                RedirectPage redirect = new RedirectPage(this, "Approval.ApprovalPaging", SessionProperty);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.DocumentContent.Approval",
                    ClassName = "ApprovalProcessScreen",
                    FunctionName = "btnSave_Click",
                    ExceptionNumber = 1,
                    EventSource = "ApprovalDocContent",
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

                RedirectPage redirect = new RedirectPage(this, "Approval.ApprovalPaging", SessionProperty);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.DocumentContent.Approval",
                    ClassName = "ApprovalProcessScreen",
                    FunctionName = "btnBack_Click",
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
