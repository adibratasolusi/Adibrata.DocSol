using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Controller;
using Adibrata.Framework.Logging;
using Adibrata.Windows.UserController;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Adibrata.DocumentSol.Windows.Archiving
{
    /// <summary>
    /// Interaction logic for ApprovalDetail.xaml
    /// </summary>
    public partial class ApprovalDetail : Page
    {
        SessionEntities SessionProperty = new SessionEntities();
        public ApprovalDetail(SessionEntities _session)
        {

            InitializeComponent();
            this.DataContext = new MainVM(new Shell());
            SessionProperty = _session;

            ucView.Session = SessionProperty;
            ucView.DocTransCode = SessionProperty.ReffKey;
        }




        private void btnHold_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                DocSolEntities _ent = new DocSolEntities
                {
                    MethodName = "ArchieveApproval",
                    ClassName = "ArchieveProcess"
                };
                _ent.DocTransCode = SessionProperty.ReffKey;
                _ent.ApprovalStatus = "H";

                DocumentSolutionController.DocSolProcess<string>(_ent);
                MessageBox.Show("Document Approval Hold Success");
                RedirectPage redirect = new RedirectPage(this, "Archiving.Approval", SessionProperty);
            }
            catch (Exception _exp)
            {

                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.Archiving",
                    ClassName = "ApprovalDetail",
                    FunctionName = "btnHold_Click",
                    ExceptionNumber = 1,
                    EventSource = "Archieve",
                    ExceptionObject = _exp,
                    EventID = 200, // 70 Untuk User Management
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
                #endregion
            }
        }

        private void btnReject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DocSolEntities _ent = new DocSolEntities
                {
                    MethodName = "ArchieveApproval",
                    ClassName = "ArchieveProcess"
                };
                _ent.DocTransCode = SessionProperty.ReffKey;
                _ent.ApprovalStatus = "R";

                DocumentSolutionController.DocSolProcess<string>(_ent);
                MessageBox.Show("Document Approval Reject Success");
                RedirectPage redirect = new RedirectPage(this, "Archiving.Approval", SessionProperty);
            }
            catch (Exception _exp)
            {

                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.Archiving",
                    ClassName = "ApprovalDetail",
                    FunctionName = "btnReject_Click",
                    ExceptionNumber = 1,
                    EventSource = "Archieve",
                    ExceptionObject = _exp,
                    EventID = 200, // 70 Untuk User Management
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
                #endregion
            }

        }

        private void btnApproval_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DocSolEntities _ent = new DocSolEntities
                {
                    MethodName = "ArchieveApproval",
                    ClassName = "ArchieveProcess"
                };
                _ent.DocTransCode = SessionProperty.ReffKey;
                _ent.ApprovalStatus = "A";

                DocumentSolutionController.DocSolProcess<string>(_ent);
                MessageBox.Show("Document Approval Success");
                RedirectPage redirect = new RedirectPage(this, "Archiving.Approval", SessionProperty);
            }
            catch (Exception _exp)
            {

                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.Archiving",
                    ClassName = "ApprovalDetail",
                    FunctionName = "btnApproval_Click",
                    ExceptionNumber = 1,
                    EventSource = "Archieve",
                    ExceptionObject = _exp,
                    EventID = 200, // 70 Untuk User Management
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
                #endregion
            }

        }
    }
}
