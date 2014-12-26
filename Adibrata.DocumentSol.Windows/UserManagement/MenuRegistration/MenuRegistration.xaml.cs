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
                txtFormName.Text = _ent.MenuName;
                txtFormOrder.Text = (_ent.MenuLevel + 1).ToString();
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

        private void TreeViewItem_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = e.Source as TreeViewItem;
            UserManagementEntities _menutag;
            try
            {
                if ((item.Items.Count == 1) && (item.Items[0] is string))
                {
                    item.Items.Clear();

                    _menutag = (UserManagementEntities)item.Tag;
                    //DirectoryInfo expandedDir = null;
                    //if (item.Tag is DriveInfo)
                    //    expandedDir = (item.Tag as DriveInfo).RootDirectory;
                    //if (item.Tag is DirectoryInfo)
                    //    expandedDir = (item.Tag as DirectoryInfo);

                    //    foreach (DirectoryInfo subDir in expandedDir.GetDirectories())
                    //item.Items.Add(CreateTreeItem(_ent));
                    //}

                    lblParentLevel.Text = _menutag.MenuLevel.ToString();
                    lblParentName.Text = _menutag.MenuName;

                    _ent.ClassName = "MainMenu";
                    _ent.MethodName = "MenuTreeRetrieve";
                    _ent.MenuLevel = _menutag.MenuLevel;

                    _dt = UserManagementController.UserManagement<DataTable>(_ent);
                    if (_dt.Rows.Count > 0)
                    {
                        foreach (DataRow _row in _dt.Rows)
                        {
                            _ent.MenuName = _row["MenuName"].ToString().Trim();
                            _ent.MenuLevel = (long)_row["MenuLevel"];
                            _ent.FormURL = (string)_row["FormUrl"];
                            item.Items.Add(CreateTreeItem(_ent));
                            //                                trvStructure.Items.Add(CreateTreeItem(_ent));
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
                    ClassName = "MenuRegistration",
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
                _ent.ClassName = "MainMenu";
                _ent.MethodName = "MenuTreeRetrieve";
                _ent.MenuLevel = 0;
                _dt = UserManagementController.UserManagement<DataTable>(_ent);
                if (_dt.Rows.Count > 0)
                {
                    foreach (DataRow _row in _dt.Rows)
                    {
                        _ent.MenuName = _row["MenuName"].ToString().Trim();
                        _ent.MenuLevel = (long)_row["MenuLevel"];
                        _ent.FormURL = (string)_row["FormUrl"];
                        trvStructure.Items.Add(CreateTreeItem(_ent));
                    }
                }
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows",
                    ClassName = "MenuRegistration",
                    FunctionName = "BindMenuRoot",
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
            UserManagementEntities _ent;
            _ent = (UserManagementEntities)o;
            item.Header = _ent.MenuName;
            item.Tag = _ent;
            item.Items.Add("Loading...");
            
            return item;
        }

       
        private void cboYesNo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboYesNo.SelectedValue != null)
            {
                if (cboYesNo.SelectedValue.ToString() == "Yes")
                {
                    grdForm.Visibility = Visibility.Visible;
                    grdMenuName.Visibility = Visibility.Hidden;
                }
                else
                {
                    grdForm.Visibility = Visibility.Hidden;
                    grdMenuName.Visibility = Visibility.Visible;
                }
            }
        }

      
    }
}
