using Adibrata.BusinessProcess.UserManagement.Entities;
using Adibrata.Controller.UserManagement;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Adibrata.Windows.UserController
{
    /// <summary>
    /// Interaction logic for UCMenuSearch.xaml
    /// </summary>
    public partial class UCMenuSearch : UserControl
    {
        public UCMenuSearch()
        {
            InitializeComponent();
        }

        private void hypMenu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            UserManagementEntities _ent = new UserManagementEntities
            {
                MethodName = "MainMenuGetActive",
                ClassName = "UserManagement",
                Form = "'"+txtSearch.Text+"'"
            };
            DataTable dt = UserManagementController.UserManagement<DataTable>(_ent);
            dtgMenu.ItemsSource = dt.DefaultView;

        }
    }
}
