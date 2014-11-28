using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.BusinessProcess.UserManagement.Entities;
using Adibrata.Controller.UserManagement;
using Adibrata.Windows.UserController;
using System;
using System.Windows;
using System.Windows.Controls;

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
            this.DataContext = new MainVM(new Shell());
            txtUserName.MessageValidator = "Please Enter User Name";
            txtUserName.IsMandatory = true;
            txtPassword.MessageValidator = "Please Enter Password";
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
            SessionEntities _session = new SessionEntities();

            if (txtUserName.IsValid && txtPassword.IsValid && UserManagementController.UserManagement<Boolean>(_ent) != true)
            {
                lblMessage.Text = "Please Verify User Name And Password";
            }
            else {
                _session.UserName = txtUserName.InputValue;
                _session.BusinessDate = DateTime.Now;
                this.NavigationService.Navigate(new Customer.CustomerPaging(_session));
                
            }
        }

    }
}
