using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.BusinessProcess.UserManagement.Entities;
using Adibrata.Controller.UserManagement;
using Adibrata.Framework.Logging;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
namespace Adibrata.DocumentSol.Windows
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class MenuTree : Page
    {
        SessionEntities SessionProperty = new SessionEntities();
        UserManagementEntities _ent = new UserManagementEntities();
        String _url;
        Boolean _isrefreshfav = false;
        Boolean _isSearchMenu = false;
        Frame _frmwork = new Frame();
        public MenuTree(SessionEntities _session)
        {
            try
            {
                InitializeComponent();
                this.DataContext = new Adibrata.Windows.UserController.MainVM(new Shell());
                SessionProperty = _session;
                BindMenuRoot();
                BindFavoriteMenu();
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows",
                    ClassName = "Home",
                    FunctionName = "Home",
                    ExceptionNumber = 1,
                    EventSource = "Home",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }
        public MenuTree(SessionEntities _session, Frame _frmworksheet)
        {
            try
            {
                InitializeComponent();
                this.DataContext = new Adibrata.Windows.UserController.MainVM(new Shell());
                SessionProperty = _session;
                BindMenuRoot();
                _frmwork = _frmworksheet;
                BindFavoriteMenu();
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows",
                    ClassName = "MenuTree",
                    FunctionName = "Home",
                    ExceptionNumber = 1,
                    EventSource = "Home",
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
                    ClassName = "MenuTree",
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
        private void TreeViewItem_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = e.Source as TreeViewItem;
            DataTable _dt = new DataTable();
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
                    ClassName = "MenuTree",
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
            String _url;
            if (temp == null)
                return;
            UserManagementEntities _ent = new UserManagementEntities();
            try
            {
                _ent.ClassName = "MainMenu";
                _ent.MethodName = "MenuTreeGetURL";
                _ent.MenuName = temp.Header.ToString();
                _url = UserManagementController.UserManagement<String>(_ent);
                if (_url != "")
                {
                    RedirectPage redirect = new RedirectPage(_frmwork, _url, SessionProperty);
                }
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows",
                    ClassName = "MenuTree",
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

        private void BindFavoriteMenu()
        {
               UserManagementEntities _ent = new UserManagementEntities();
               DataTable _dt = new DataTable();
               try
               {
                   _ent.ClassName = "FavoriteMenu";
                   _ent.MethodName = "FavoriteMenuList";
                   _ent.UserLogin = SessionProperty.UserName;
                   _dt = UserManagementController.UserManagement<DataTable>(_ent);
                   lstFavorite.ItemsSource = _dt.DefaultView;
               }

               catch (Exception _exp)
               {
                   ErrorLogEntities _errent = new ErrorLogEntities
                   {
                       UserLogin = SessionProperty.UserName,
                       NameSpace = "Adibrata.DocumentSol.Windows",
                       ClassName = "MenuTree",
                       FunctionName = "BindFavoriteMenu",
                       ExceptionNumber = 1,
                       EventSource = "Main",
                       ExceptionObject = _exp,
                       EventID = 200, // 1 Untuk Framework 
                       ExceptionDescription = _exp.Message
                   };
                   ErrorLog.WriteEventLog(_errent);
               }

        }
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
            try
            {
                if (_isrefreshfav != true)
                {
                    _ent.ClassName = "MainMenu";
                    _ent.MethodName = "MenuTreeGetURL";
                    _ent.MenuName = ((DataRowView)lstFavorite.SelectedItem)["FormName"].ToString();
                    _url = ((DataRowView)lstFavorite.SelectedItem)["FormUrl"].ToString();
                    RedirectPage redirect = new RedirectPage(_frmwork, _url, SessionProperty);
                }
                //_ent.MenuName = lstFavorite.SelectedValue.ToString();
                //_url = UserManagementController.UserManagement<String>(_ent);
                //if (_url != "")
                //{
                //    RedirectPage redirect = new RedirectPage(_frmwork, _url, SessionProperty);
                //}
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows",
                    ClassName = "MenuTree",
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

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            _isrefreshfav = true;
            BindFavoriteMenu();
            _isrefreshfav = false;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtSearch.Text != "")
                {
                    lstSearchMenu.ItemsSource = MenuDataRetrieve().DefaultView;
                }
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows",
                    ClassName = "MenuTree",
                    FunctionName = "btnFind_Click",
                    ExceptionNumber = 1,
                    EventSource = "Main",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }
        private DataTable MenuDataRetrieve()
        {
            DataTable dt = new DataTable();
            try
            {
                UserManagementEntities _ent = new UserManagementEntities
                {
                    MethodName = "SearchEngineMenu",
                    ClassName = "MainMenu"
                };
                _ent.Form = txtSearch.Text;
                dt = UserManagementController.UserManagement<DataTable>(_ent);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows",
                    ClassName = "MenuTree",
                    FunctionName = "MenuDataRetrieve",
                    ExceptionNumber = 1,
                    EventSource = "Main",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
            return dt;
        }

        private void lstSearchMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (_isSearchMenu != true)
                {
                    _ent.ClassName = "MainMenu";
                    _ent.MethodName = "MenuTreeGetURL";
                    _ent.MenuName = ((DataRowView)lstSearchMenu.SelectedItem)["FormName"].ToString();
                    _url = ((DataRowView)lstSearchMenu.SelectedItem)["FormUrl"].ToString();
                    RedirectPage redirect = new RedirectPage(_frmwork, _url, SessionProperty);
                }
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows",
                    ClassName = "MenuTree",
                    FunctionName = "lstSearchMenu_SelectionChanged",
                    ExceptionNumber = 1,
                    EventSource = "Main",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

    }
}


//using System;
//using System.IO;
//using System.Windows;
//using System.Windows.Controls;

//namespace WpfTutorialSamples.TreeView_control
//{
//        public partial class LazyLoadingSample : Window
//        {
//                public LazyLoadingSample()
//                {
//                        InitializeComponent();
//                        DriveInfo[] drives = DriveInfo.GetDrives();
//                        foreach(DriveInfo driveInfo in drives)
//                                trvStructure.Items.Add(CreateTreeItem(driveInfo));
//                }

//                public void TreeViewItem_Expanded(object sender, RoutedEventArgs e)
//                {
//                        TreeViewItem item = e.Source as TreeViewItem;
//                        if((item.Items.Count == 1) && (item.Items[0] is string))
//                        {
//                                item.Items.Clear();

//                                DirectoryInfo expandedDir = null;
//                                if(item.Tag is DriveInfo)
//                                        expandedDir = (item.Tag as DriveInfo).RootDirectory;
//                                if(item.Tag is DirectoryInfo)
//                                        expandedDir = (item.Tag as DirectoryInfo);
//                                try
//                                {
//                                        foreach(DirectoryInfo subDir in expandedDir.GetDirectories())
//                                                item.Items.Add(CreateTreeItem(subDir));
//                                }
//                                catch { }
//                        }
//                }

//                private TreeViewItem CreateTreeItem(object o)
//                {
//                        TreeViewItem item = new TreeViewItem();
//                        item.Header = o.ToString();
//                        item.Tag = o;
//                        item.Items.Add("Loading...");
//                        return item;
//                }
//        }
//}