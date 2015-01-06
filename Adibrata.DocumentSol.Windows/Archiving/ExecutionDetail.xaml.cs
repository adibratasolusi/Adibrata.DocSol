using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Controller;
using Adibrata.Framework.Logging;
using Adibrata.Framework.Messaging;
using Adibrata.Windows.UserController;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Adibrata.DocumentSol.Windows.Archiving
{
    /// <summary>
    /// Interaction logic for ExecutionDetail.xaml
    /// </summary>
    public partial class ExecutionDetail : Page
    {

        SessionEntities SessionProperty = new SessionEntities();
        public ExecutionDetail(SessionEntities _session)
        {
            //Lampar ke detail
            try
            {
                InitializeComponent();
                this.DataContext = new MainVM(new Shell());
                SessionProperty = _session;
                ucView.Session = SessionProperty;
                ucView.DocTransCode = SessionProperty.ReffKey;


            }
            catch (Exception _exp)
            {
                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.Archiving",
                    ClassName = "ExecutionDetail",
                    FunctionName = "ExecutionDetail",
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

        
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

            RedirectPage redirect = new RedirectPage(this, "Archiving.Execution", SessionProperty);
        }

        private void btnExec_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WCFEntities oWcf = new WCFEntities();
                oWcf.DocTransID = ucView.DocTransId;
                oWcf.UserName = SessionProperty.UserName;
                MessageToWCF.ArchieveExecProcess(oWcf);
                MessageBox.Show("Document Archieve Execution Success");
                RedirectPage redirect = new RedirectPage(this, "Archiving.Execution", SessionProperty);
            }
            catch (Exception _exp)
            {

                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.Archiving",
                    ClassName = "ExecutionDetail",
                    FunctionName = "btnExec_Click",
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
