using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.BusinessProcess.UserManagement.Entities;
using Adibrata.Controller.UserManagement;
using Adibrata.Framework.Logging;
using Adibrata.Windows.UserController;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Adibrata.DocumentSol.Windows.Form
{
    /// <summary>
    /// Interaction logic for FormRegistrasiSave.xaml
    /// </summary>
    public partial class FormRegistrasiSave : Page
    {
        SessionEntities SessionProperty = new SessionEntities();
        public FormRegistrasiSave(SessionEntities _session)
        {
            try
            {
                InitializeComponent();
                this.DataContext = new MainVM(new Shell());
                SessionProperty = _session;

                if (SessionProperty.IsEdit)
                {
                    UserManagementEntities _ent = new UserManagementEntities { MethodName = "FormRegisterAddEdit", ClassName = "UserRegister", UserLogin = SessionProperty.UserName };
                    _ent.UserID = Convert.ToInt64(SessionProperty.ReffKey);
                    _ent = UserManagementController.UserManagement<UserManagementEntities>(_ent);
                    txtFormCode.Text = _ent.FormCode;
                    txtFormName.Text= _ent.FormName;
                    txtFormClass.Text = _ent.FormURL;
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

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RedirectPage redirect = new RedirectPage(this, "Form.FormRegistrasiPaging", SessionProperty);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.Form",
                    ClassName = "FormRegistrasiSave",
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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                UserManagementEntities _ent = new UserManagementEntities { MethodName = "FormRegisterAddEdit", ClassName = "FormRegistrasi", UserLogin = SessionProperty.UserName };
                _ent.FormCode= txtFormCode.Text;
                _ent.FormName = txtFormName.Text;
                _ent.FormURL = txtFormClass.Text;
                _ent.UserLogin = SessionProperty.UserName;
                _ent.IsEdit = SessionProperty.IsEdit;
                _ent.UserID = Convert.ToInt64(SessionProperty.ReffKey);

                UserManagementController.UserManagement<string>(_ent);
                
                RedirectPage redirect = new RedirectPage(this, "Form.FormRegistrasiPaging", SessionProperty);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.Form",
                    ClassName = "FormRegistrasiSave",
                    FunctionName = "btnSave_Click",
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
