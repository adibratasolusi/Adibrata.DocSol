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

namespace Adibrata.DocumentSol.Windows.Menu
{
    /// <summary>
    /// Interaction logic for MenuRegistration.xaml
    /// </summary>
    public partial class MenuRegistration : Page
    {
        public MenuRegistration()
        {
            InitializeComponent();
            rbMenu.IsChecked = true;
            setCmbBoxParentId();
        }

        void setCmbBoxParentId()
        {
            UserManagementEntities _ent = new UserManagementEntities
            {
                MethodName = "MainMenuGetActiveItemId",
                ClassName = "MainMenu"
            };
            DataTable dt = new DataTable();
            dt = UserManagementController.UserManagement<DataTable>(_ent);
            cmbParent.ItemsSource = dt.DefaultView;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UserManagementEntities _ent = new UserManagementEntities
                {
                    MethodName = "MainMenuInsertUpdate",
                    ClassName = "MainMenu"
                };

                if (rbSeparator.IsChecked == true)
                {
                    _ent.IsSeparator = '1';
                    if (chkItemRoot.IsChecked == true)
                    {
                        _ent.MenuParentId = 0;
                    }
                    else
                    {
                        _ent.MenuParentId = Convert.ToInt16(cmbParent.SelectedValue);
                    }
                    if (isActive.IsChecked == true)
                    {
                        _ent.IsActive = 1;
                    }
                    else
                    {
                        _ent.IsActive = 0;
                    }
                    _ent.ShortOrder = Convert.ToInt16(txtOrder.InputValue.ToString());
                    _ent.MenuTxt = "";
                    _ent.Icon = "";
                    _ent.Form = "";
                }
                else
                {
                    _ent.IsSeparator = '0';
                    _ent.MenuTxt = txtMenu.InputValue.ToString();
                    _ent.Form = txtForm.InputValue.ToString();
                    _ent.Icon = txtIcon.InputValue.ToString();
                    if (chkItemRoot.IsChecked == true)
                    {
                        _ent.MenuParentId = 0;
                    }
                    else
                    {
                        _ent.MenuParentId = Convert.ToInt16(cmbParent.SelectedValue);
                    }
                    if (isActive.IsChecked == true)
                    {
                        _ent.IsActive = 1;
                    }
                    else
                    {
                        _ent.IsActive = 0;
                    }
                    _ent.ShortOrder = Convert.ToInt16(txtOrder.InputValue.ToString());
                }

                _ent.FlagInsert = true;
                UserManagementController.UserManagement<string>(_ent);

                MessageBox.Show("Input sukses");
                this.NavigationService.Navigate(new MenuPaging());
            }
            catch (Exception _exp)
            {

                MessageBox.Show(_exp.ToString());
            }
        }

        private void rbSeparator_Checked(object sender, RoutedEventArgs e)
        {
            txtIcon.IsEnabled = false;
            txtMenu.IsEnabled = false;
            txtForm.IsEnabled = false;
        }

        private void rbMenu_Checked(object sender, RoutedEventArgs e)
        {
            txtIcon.IsEnabled = true;
            txtMenu.IsEnabled = true;
            txtForm.IsEnabled = true;
        }



        private void cmbParent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           // setCmbOrder();
        }
        void setCmbOrder()
        {
            //UserManagementEntities _ent = new UserManagementEntities
            //{
            //    MethodName = "MainMenuGetActiveShortOrder",
            //    ClassName = "MainMenu"
            //};
            //if (chkItemRoot.IsChecked == true)
            //{
            //    _ent.MenuItemId = 0;
            //}
            //else
            //{
            //    _ent.MenuItemId = Convert.ToInt16(cmbParent.SelectedValue);
            //}
            //DataTable dt = new DataTable();
            //dt = UserManagementController.UserManagement<DataTable>(_ent);
            //if (dt.Rows.Count > 0)
            //{
            //    cmbOrder.IsEnabled = true;
            //    cmbOrder.ItemsSource = dt.DefaultView;
            //}
        }
        private void chkItemRoot_Checked(object sender, RoutedEventArgs e)
        {
            cmbParent.IsEnabled = false;
            //chkDefaultOrder.IsEnabled = true;
            //chkDefaultOrder.IsChecked = false;
            //cmbOrder.IsEnabled = true;
            //setCmbOrder();
        }

        //private void chkDefaultOrder_Checked(object sender, RoutedEventArgs e)
        //{
        //    cmbOrder.IsEnabled = false;
        //    setCmbOrder();
        //}

        //private void chkDefaultOrder_Unchecked(object sender, RoutedEventArgs e)
        //{
        //    cmbOrder.IsEnabled = true;
        //    setCmbOrder();
        //}

        private void chkItemRoot_Unchecked(object sender, RoutedEventArgs e)
        {
            cmbParent.IsEnabled = true;
            //chkDefaultOrder.IsChecked = false;
            //cmbOrder.IsEnabled = false;
            //chkDefaultOrder.IsEnabled = false;
            //setCmbOrder();
            setCmbBoxParentId();
        }
    }
}
