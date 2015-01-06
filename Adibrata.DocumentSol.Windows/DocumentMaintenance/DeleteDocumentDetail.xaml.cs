using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Controller;
using Adibrata.Framework.Logging;
using Adibrata.Windows.UserController;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Adibrata.DocumentSol.Windows.DocumentMaintenance
{
    /// <summary>
    /// Interaction logic for DeleteDocumentDetail.xaml
    /// </summary>
    public partial class DeleteDocumentDetail : Page

    {
        SessionEntities SessionProperty = new SessionEntities();
       
        public DeleteDocumentDetail(SessionEntities _session)
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
                    NameSpace = "Adibrata.DocumentSol.Windows.DocumentMaintenance",
                    ClassName = "DeleteDocumentDetail",
                    FunctionName = "DeleteDocumentDetail",
                    ExceptionNumber = 1,
                    EventSource = "UserRegistration",
                    ExceptionObject = _exp,
                    EventID = 200, // 70 Untuk User Management
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
                #endregion
            }
        }


        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            DocSolEntities _ent = new DocSolEntities
            {
                MethodName = "DeleteDocumentStatus",
                ClassName = "DeleteDocument"
            };
            _ent.Id = ucView.DocTransId;

            DocumentSolutionController.DocSolProcess<string>(_ent);
            MessageBox.Show("Delete Succes");
            RedirectPage redirect = new RedirectPage(this, "DocumentMaintenance.DeleteDocumentPaging", SessionProperty);
        
       

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RedirectPage redirect = new RedirectPage(this, "DocumentMaintenance.DeleteDocumentPaging", SessionProperty);
            }
            catch (Exception _exp)
            {
                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = " Adibrata.DocumentSol.Windows.DocumentMaintenance",
                    ClassName = "DeleteDocumentInquiryDetail",
                    FunctionName = "btnBack_Click",
                    ExceptionNumber = 1,
                    EventSource = "DeleteDocumentInquiryDetail",
                    ExceptionObject = _exp,
                    EventID = 200, // 70 Untuk User Managemetn
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
                #endregion
            }
        }


    }
}
