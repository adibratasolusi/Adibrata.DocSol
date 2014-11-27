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
using Adibrata.BusinessProcess.UserManagement.Extend;
using Adibrata.Controller.UserManagement;
using Adibrata.BusinessProcess.UserManagement.Entities;
using Adibrata.Windows.UserController;

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
         
            if (txtUserName.IsValid && txtPassword.IsValid && UserManagementController.UserManagement<Boolean>(_ent) != true)
            {
                lblMessage.Text = "Please Verify User Name And Password";
            }
            else {
                _ent.UserLogin = txtUserName.InputValue;
                _ent.Password = txtPassword.PasswordValue;
            }
        }

    }
}
