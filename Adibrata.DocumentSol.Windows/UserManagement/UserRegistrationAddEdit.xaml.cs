using Adibrata.BusinessProcess.UserManagement.Entities;
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

using Adibrata.Controller.UserManagement;
using System.Data;
using Adibrata.Framework.Security;
using Adibrata.Configuration;
using Adibrata.Windows.UserController;
using Adibrata.Framework.Logging;


namespace Adibrata.DocumentSol.Windows.UserManagement
{
    /// <summary>
    /// Interaction logic for UserRegistrationAddEdit.xaml
    /// </summary>
    public partial class UserRegistrationAddEdit : Page
    {

        string currentUserName;
        
        static Boolean _isedit;
        public UserRegistrationAddEdit(string UserName, Boolean isEdit)
        {
            InitializeComponent();
            this.DataContext = new MainVM(new Shell());
            _isedit = isEdit;
            currentUserName = UserName;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UserManagementEntities _ent = new UserManagementEntities { MethodName = "UserRegisterAddEdit", ClassName = "UserRegister" };
                _ent.UserName = txtUserName.Text;
                _ent.Password = txtPassword.Text;
                _ent.FullName = txtFullname.Text;

                if (isActive.IsChecked == true)
                {
                    _ent.IsActive = 1;
                }
                else
                {
                    _ent.IsActive = 0;
                }
                if (_isedit)
                {
                    _ent.UsrUpd = currentUserName;
                }
                else
                {
                    _ent.UsrCrt = currentUserName;
                }
                _ent.IsEdit = _isedit;

                UserManagementController.UserManagement<string>(_ent);
            }
            catch (Exception _exp)
            {
                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = currentUserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.UserManagement",
                    ClassName = "UserRegistrationAddEdit",
                    FunctionName = "btnSave_Click",
                    ExceptionNumber = 1,
                    EventSource = "UserRegistration",
                    ExceptionObject = _exp,
                    EventID = 70, // 70 Untuk User Managemetn
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
                #endregion
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
