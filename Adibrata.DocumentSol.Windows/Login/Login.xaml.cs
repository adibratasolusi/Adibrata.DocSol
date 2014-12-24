using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.BusinessProcess.UserManagement.Entities;
using Adibrata.Controller.UserManagement;
using Adibrata.Windows.UserController;
using System;
using System.Windows;
using System.Windows.Controls;
using Adibrata.Configuration;

using Adibrata.Framework.Logging;


namespace Adibrata.DocumentSol.Windows.Login
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
          
                InitializeComponent();
                if (License.ActiveDate() == true)
                {
                    this.DataContext = new MainVM(new Shell());
                    txtUserName.MessageValidator = "Please Enter User Name";
                    txtUserName.IsMandatory = true;
                    txtPassword.MessageValidator = "Please Enter Password";

                }
                else
                {
                    btnLogin.Visibility = Visibility.Hidden;
                    lblMessage.Text = "Your Application has been disabled";
                    MessageBox.Show("Your Application has been disabled");
                }
        }

        private void CheckMandatory()
        {
            if (!txtUserName.IsValid)
            {
                txtUserName.CheckValue();
            }
            if (!txtPassword.IsValid)
            {
                txtPassword.CheckValue();
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

            UserManagementEntities _ent = new UserManagementEntities { ClassName = "UserManagement", MethodName = "UserNamePasswordVerification" };
            SessionEntities SessionProperty = new SessionEntities();
            try
            {
                if (License.UserLicense() == true)
                {
                    _ent.UserName = txtUserName.InputValue;
                    _ent.Password = txtPassword.PasswordValue;
                    if (txtUserName.IsValid && txtPassword.IsValid && UserManagementController.UserManagement<Boolean>(_ent) != true)
                    {
                        lblMessage.Text = "Please Verify User Name And Password";
                    }
                    else
                    {
                        if (_ent.UserName == "")
                        { lblMessage.Text = "Pleasse Enter Your User Name"; }
                        else
                        {
                            if (_ent.Password == "") { lblMessage.Text = "Please Enter Your Password"; }

                            else
                            {
                                SessionProperty.UserName = txtUserName.InputValue;
                                SessionProperty.BusinessDate = DateTime.Now;
                                SessionProperty.StartLoginTime = DateTime.Now;
                                RedirectPage redirect = new RedirectPage(this, "MainForm", SessionProperty);
                            }

                            //_obj = Adibrata.Controller.PageRedirect.PageRedirectController.RedirectPage("CustomerPaging",_session);
                            //Customer.CustomerPaging(_session));
                        }

                    }
                }
                else
                {
                    MessageBox.Show("User License has Reach Out");
                }
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = "Login",
                    NameSpace = "Adibrata.DocumentSol.Windows.Login",
                    ClassName = "Login",
                    FunctionName = "btnLogin_Click",
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
