using System.Windows;
using System.Windows.Controls;
using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.BusinessProcess.UserManagement.Entities;
using Adibrata.Controller.UserManagement;
using Adibrata.Framework.Logging;
using System;
using System.Data;
using System.Collections.Generic;

namespace Adibrata.DocumentSol.Windows.Menu
{
    /// <summary>
    /// Interaction logic for MenuRegistration.xaml
    /// </summary>
    public partial class MenuRegistration : Page
    {
        SessionEntities SessionProperty = new SessionEntities();
        UserManagementEntities _ent = new UserManagementEntities();
        DataTable _dt = new DataTable();
        public MenuRegistration(SessionEntities _session)
        {
            try
            {
                InitializeComponent();
                this.DataContext = new Adibrata.Windows.UserController.MainVM(new Shell());
                SessionProperty = _session;
                BindMenuRoot();
                grdAdd.Visibility = Visibility.Visible;
                grdAddEdit.Visibility = Visibility.Hidden;
                grdButtonSave.Visibility = Visibility.Hidden;
                List<string> lstYesNo = new List<string>();
                lstYesNo.Add("Yes");
                lstYesNo.Add("No");
                cboYesNo.ItemsSource = lstYesNo;
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows",
                    ClassName = "MenuRegistration",
                    FunctionName = "MenuRegistration",
                    ExceptionNumber = 1,
                    EventSource = "Main",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            try {
                grdAdd.Visibility = Visibility.Visible;
                grdAddEdit.Visibility = Visibility.Hidden;
                grdButtonSave.Visibility = Visibility.Hidden;
            
            }

            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows",
                    ClassName = "MenuRegistration",
                    FunctionName = "btnBack_Click",
                    ExceptionNumber = 1,
                    EventSource = "Main",
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
                _ent.ClassName = "MainMenu";
                _ent.MethodName = "MenuTreeSave";
                if (cboYesNo.SelectedValue.ToString() == "Yes" && cboYesNo.SelectedValue != null)
                {
                    if (cboFormName.SelectedValue != null)
                    {
                        _ent.MenuName = null;
                        _ent.FormName = cboFormName.SelectedValue.ToString();
                    }
                }
                else
                {
                    _ent.MenuName = txtMenuName.Text;
                }
                try
                {
                    _ent.MenuOrder = Convert.ToInt16(txtFormOrder.Text);
                }
                catch 
                {
                    MessageBox.Show ("Please Enter Order Menu By Numeric");
                }
                _ent.UserLogin = SessionProperty.UserName;
                _ent.ParentLevelMenu = Convert.ToInt64(lblParentLevel.Text);
                UserManagementController.UserManagement<string>(_ent);

                grdAdd.Visibility = Visibility.Visible;
                grdAddEdit.Visibility = Visibility.Hidden;
                grdButtonSave.Visibility = Visibility.Hidden;

            }

            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows",
                    ClassName = "MenuRegistration",
                    FunctionName = "btnSave_Click",
                    ExceptionNumber = 1,
                    EventSource = "Main",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try {
                
                grdAdd.Visibility = Visibility.Hidden;
                grdAddEdit.Visibility = Visibility.Visible;
                grdMenuName.Visibility = Visibility.Hidden;
                grdForm.Visibility = Visibility.Hidden;
                grdButtonSave.Visibility = Visibility.Visible;
             
            }

            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows",
                    ClassName = "MenuRegistration",
                    FunctionName = "btnAdd_Click",
                    ExceptionNumber = 1,
                    EventSource = "Main",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }
        #region "Tree Process"
        private void TreeViewItem_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = e.Source as TreeViewItem;
            UserManagementEntities _menutag = new UserManagementEntities();
            try
            {
                if ((item.Items.Count == 1) && (item.Items[0] is string))
                {
                    item.Items.Clear();

                    _menutag = (UserManagementEntities)item.Tag;

                    _ent.ClassName = "MainMenu";
                    _ent.MethodName = "MenuTreeRetrieve";
                    _ent.MenuLevel = _menutag.MenuLevel;
                    _ent.MenuName = item.Header.ToString();
                    _dt = UserManagementController.UserManagement<DataTable>(_ent);
                    if (_dt.Rows.Count > 0)
                    {
                        foreach (DataRow _row in _dt.Rows)
                        {
                            _ent.MenuName = _row["MenuName"].ToString().Trim();
                            _ent.MenuLevel = (long)_row["MenuLevel"];
                            _ent.FormURL = (string)_row["FormUrl"];
                            item.Items.Add(CreateTreeItem(_ent));
                        }
                    }
                }
            }

            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows",
                    ClassName = "Home",
                    FunctionName = "TreeViewItem_Expanded",
                    ExceptionNumber = 1,
                    EventSource = "Main",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        private TreeViewItem CreateTreeItem(object o)
        {
            TreeViewItem item = new TreeViewItem();
            UserManagementEntities _enttree = new UserManagementEntities();
            _enttree = (UserManagementEntities)o;
            item.Header = _enttree.MenuName;
            item.Tag = _enttree;
            item.Items.Add("Loading...");
            return item;
        }

        private void trvStructure_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeView tree = (TreeView)sender;
            TreeViewItem temp = ((TreeViewItem)tree.SelectedItem);
            grdAdd.Visibility = Visibility.Visible;
            grdAddEdit.Visibility = Visibility.Hidden;
            grdButtonSave.Visibility = Visibility.Hidden;
            if (temp == null)
                return;
            DataTable _dt = new DataTable();
            UserManagementEntities _ent = new UserManagementEntities();
            try
            {
                _ent.ClassName = "MainMenu";
                _ent.MethodName = "MenuTreeGetDetail";
                _ent.MenuName = temp.Header.ToString();
                _dt = UserManagementController.UserManagement<DataTable>(_ent);
                if (_dt.Rows.Count > 0)
                {
                    foreach (DataRow _row in _dt.Rows)
                    {
                        lblParentName.Text = _row["MenuName"].ToString().Trim();
                        lblParentLevel.Text = _row["MenuLevel"].ToString().Trim();
                    }
                }
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows",
                    ClassName = "Home",
                    FunctionName = "TreeViewItem_Expanded",
                    ExceptionNumber = 1,
                    EventSource = "Main",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        private void BindMenuRoot()
        {
            try
            {
                _ent.MenuName = "Menu";
                trvStructure.Items.Add(CreateTreeItem(_ent));
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows",
                    ClassName = "Home",
                    FunctionName = "BindMenuRoot",
                    ExceptionNumber = 1,
                    EventSource = "Home",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }

        }
        #endregion 

        private void cboYesNo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboYesNo.SelectedValue.ToString() == "Yes")
            {
                _ent.ClassName = "MainMenu";
                _ent.MethodName = "MenuTreeGetFormList";
                _dt = UserManagementController.UserManagement<DataTable>(_ent);
                List<string> lstForm = new List<string>();
                if (_dt.Rows.Count > 0)
                {
                    foreach (DataRow _row in _dt.Rows)
                    {
                        lstForm.Add(_row["FormName"].ToString().Trim());
                    }
                }
                lstForm.Add("No");
                cboFormName.ItemsSource = lstForm;
                grdMenuName.Visibility = Visibility.Hidden;
                grdForm.Visibility = Visibility.Visible;
            }
            else
            {
                grdMenuName.Visibility = Visibility.Visible;
                grdForm.Visibility = Visibility.Hidden;
            }
        }

        private void cboFormName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
      
      
    }
}
