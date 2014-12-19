using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.BusinessProcess.UserManagement.Entities;
using Adibrata.Controller.UserManagement;
using Adibrata.Framework.Logging;
using Adibrata.Windows.UserController;
using System;
using System.Collections.Generic;
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

                if (SessionProperty.IsEdit)
                {
                    UserManagementEntities _ent = new UserManagementEntities
                    {
                        MethodName = "DeleteDocumentPaging",
                        ClassName  = "DeleteDocument",
                        UserLogin  = SessionProperty.UserName
                    };
                    _ent.UserID = Convert.ToInt64(SessionProperty.ReffKey);
                    _ent = UserManagementController.UserManagement<UserManagementEntities>(_ent);
                   // txtTransId.Text = _ent.;
                }
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

        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
