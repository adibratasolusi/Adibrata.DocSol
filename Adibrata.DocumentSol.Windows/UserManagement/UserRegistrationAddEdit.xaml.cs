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
        long _userid;
        public UserRegistrationAddEdit(SessionEntities _session)
        {
            InitializeComponent();
            this.DataContext = new MainVM(new Shell());
            SessionProperty = _session;
            _userid = SessionProperty.ReffKey;
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
                UserManagementEntities _ent = new UserManagementEntities { MethodName = "UserRegisterAddEdit", ClassName = "UserRegister" };
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
                if (SessionProperty.IsEdit)
                {
                    _ent.UsrUpd = SessionProperty.UserName;
                }
                else
                {
                    _ent.UsrCrt = SessionProperty.UserName;
                }
                _ent.IsEdit = SessionProperty.IsEdit;

                UserManagementController.UserManagement<string>(_ent);
                this.NavigationService.Navigate(new UserRegistrationPaging(SessionProperty));
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
                this.NavigationService.Navigate(new UserRegistrationPaging(SessionProperty));
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
