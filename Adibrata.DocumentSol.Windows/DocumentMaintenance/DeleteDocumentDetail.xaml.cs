using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.BusinessProcess.UserManagement.Entities;
using Adibrata.Controller;
using Adibrata.Controller.UserManagement;
using Adibrata.Framework.Logging;
using Adibrata.Windows.UserController;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
                ucView.DocTransId = Convert.ToInt64(SessionProperty.ReffKey);
                
           
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
            _ent.Id = Convert.ToInt64(SessionProperty.ReffKey);

            DocumentSolutionController.DocSolProcess<string>(_ent);
            MessageBox.Show("Sukses");
            RedirectPage redirect = new RedirectPage(this, "DocumentMaintenance.DeleteDocumentPaging", SessionProperty);
        
       

        }


    }
}
