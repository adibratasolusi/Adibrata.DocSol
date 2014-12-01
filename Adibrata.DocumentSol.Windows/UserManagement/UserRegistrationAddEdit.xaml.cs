using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.BusinessProcess.UserManagement.Entities;
using Adibrata.Controller.UserManagement;
using Adibrata.Framework.Logging;
using Adibrata.Windows.UserController;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Adibrata.DocumentSol.Windows.UserManagement
{
    /// <summary>
    /// Interaction logic for UserRegistrationAddEdit.xaml
    /// </summary>
    public partial class UserRegistrationAddEdit : Page
    {
        SessionEntities SessionProperty = new SessionEntities();
        
        public UserRegistrationAddEdit(SessionEntities _session)
        {
            try
            {
                InitializeComponent();
                this.DataContext = new MainVM(new Shell());
                SessionProperty = _session;
                
                if (SessionProperty.IsEdit)
                {
                    UserManagementEntities _ent = new UserManagementEntities { MethodName = "UserRegisterView", ClassName = "UserRegister", UserLogin = SessionProperty.UserName };
                    _ent.UserID = SessionProperty.ReffKey;
                    _ent = UserManagementController.UserManagement<UserManagementEntities>(_ent);
                    txtUserName.Text = _ent.UserName;
                    txtPassword.Password = _ent.Password;
                    txtFullname.Text = _ent.FullName;
                }
            }
            catch (Exception _exp)
            {
                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.UserManagement",
                    ClassName = "UserRegistrationAddEdit",
                    FunctionName = "UserRegistrationAddEdit",
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

        public UserRegistrationAddEdit()
        {
            InitializeComponent();
            this.DataContext = new MainVM(new Shell());
            SessionProperty.UserName = "test";
            
            
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UserManagementEntities _ent = new UserManagementEntities { MethodName = "UserRegisterAddEdit", ClassName = "UserRegister", UserLogin = SessionProperty.UserName };
                _ent.UserName = txtUserName.Text;
                _ent.Password = txtPassword.Password;
                _ent.FullName = txtFullname.Text;
                _ent.UserLogin = SessionProperty.UserName;

                if (isActive.IsChecked == true)
                {
                    _ent.IsActive = 1;
                }
                else
                {
                    _ent.IsActive = 0;
                }
                _ent.UserLogin = SessionProperty.UserName;
                _ent.IsEdit = SessionProperty.IsEdit;
                _ent.UserID = SessionProperty.ReffKey;

                UserManagementController.UserManagement<string>(_ent);
                RedirectPage redirect = new RedirectPage(this, "UserManagement.UserRegistrationPaging", SessionProperty);
            }
            catch (Exception _exp)
            {
                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.UserManagement",
                    ClassName = "UserRegistrationAddEdit",
                    FunctionName = "btnSave_Click",
                    ExceptionNumber = 1,
                    EventSource = "UserRegistration",
                    ExceptionObject = _exp,
                    EventID = 200, // 70 Untuk User Managemetn
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
                #endregion
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RedirectPage redirect = new RedirectPage(this, "UserManagement.UserRegistrationPaging", SessionProperty);
            }
            catch (Exception _exp)
            {
                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.UserManagement",
                    ClassName = "UserRegistrationAddEdit",
                    FunctionName = "btnSave_Click",
                    ExceptionNumber = 1,
                    EventSource = "UserRegistration",
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
