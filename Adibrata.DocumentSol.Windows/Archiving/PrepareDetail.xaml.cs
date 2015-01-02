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
    /// Interaction logic for PrepareDetail.xaml
    /// </summary>
    public partial class PrepareDetail : Page
    {

        SessionEntities SessionProperty = new SessionEntities();
        public PrepareDetail(SessionEntities _session)
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
                    ClassName = "PrepareDetail",
                    FunctionName = "PrepareDetail",
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

        private void btnPrepare_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DocSolEntities _ent = new DocSolEntities
                {
                    MethodName = "ArchievePrepare",
                    ClassName = "ArchieveProcess"
                };
                _ent.DocTransCode = SessionProperty.ReffKey;

                DocumentSolutionController.DocSolProcess<string>(_ent);
                MessageBox.Show("Document Archieve Prepare Success");
                RedirectPage redirect = new RedirectPage(this, "Archiving.Prepare", SessionProperty);
            }
            catch (Exception _exp)
            {

                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.Archiving",
                    ClassName = "PrepareDetail",
                    FunctionName = "btnPrepare_Click",
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

            RedirectPage redirect = new RedirectPage(this, "Archiving.Prepare", SessionProperty);
        }
    }
}
