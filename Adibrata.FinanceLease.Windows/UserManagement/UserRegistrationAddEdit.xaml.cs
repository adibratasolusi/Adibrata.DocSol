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
using Adibrata.BusinessProcess.UserManagement.Entities;
using Adibrata.Controller.UserManagement;
using System.Data;
namespace Adibrata.FinanceLease.Windows.UserManagement
{
    /// <summary>
    /// Interaction logic for UserRegistrationAddEdit.xaml
    /// </summary>
    public partial class UserRegistrationAddEdit : Page
    {
        string currentUserName;
        UserManagementController _obj = new UserManagementController();


        public UserRegistrationAddEdit(string username = "0")
        {


            InitializeComponent();
            dpExp.DisplayDateStart = DateTime.Now;
            currentUserName = username;
            
        }
        private void updateUser
        { 
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            UserManagementEntities _ent = new UserManagementEntities
            {
                MethodName = "UserMangementAddEdit",
                ClassName = "Adibrata.BusinessProcess.UserManagement.Extend.UserManagement"
            };
            string active = "0";
            if (isActive.IsChecked == true)
            {
                active = "1";
            }
            _ent.UserName = txtUserName.Text.Trim();
            _ent.Pass = txtPassword.Text;
            _ent.IsActive = active;
            _ent.seqWrongPwd = Convert.ToInt64(txtMax.Text.Trim());
            _ent.ExpiredDt = dpExp.SelectedDate.Value;
            _obj.UserManagement<string>(_ent);
        }
    }
}
