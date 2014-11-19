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

namespace Adibrata.DocumentSol.Windows.UserManagement
{
    /// <summary>
    /// Interaction logic for UserRegistrationAddEdit.xaml
    /// </summary>
    public partial class UserRegistrationAddEdit : Page
    {

        string currentUserName;
        static string coyName = AppConfig.Config("CoyName");

        public UserRegistrationAddEdit(string username)
        {
            InitializeComponent();

            dpExp.DisplayDateStart = DateTime.Now;
            currentUserName = username;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string password = Encryption.EncryptToSHA3(txtPassword.Text) + Encryption.EncryptToSHA3(coyName);
            UserManagementEntities _ent = new UserManagementEntities { MethodName = "UserMangementAddEdit", ClassName = "Adibrata.BusinessProcess.UserManagement.Extend.UserManagement" };
            _ent.UserName = txtUserName.Text;
            _ent.Password = password;
            _ent.FullName = txtFullname.Text;
            if (isActive.IsChecked == true)
            {
                _ent.IsActive = 1;
            }
            else
            {
                _ent.IsActive = 0;
            }
            _ent.MaxWrong = Convert.ToInt32(txtMax.Text);


            UserManagementController.UserManagement<string>(_ent);
        }
    }
}
