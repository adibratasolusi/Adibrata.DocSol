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
    public partial class Home : Page
    {
        SessionEntities SessionProperty = new SessionEntities();
        UserManagementEntities _ent = new UserManagementEntities();
        DataTable _dt = new DataTable();
        Frame _frmwork = new Frame();
        public Home(SessionEntities _session)
        {
            try
            {
                InitializeComponent();
                this.DataContext = new Adibrata.Windows.UserController.MainVM(new Shell());
                SessionProperty = _session;
                BindMenuRoot();
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
        public Home(SessionEntities _session, Frame _frmworksheet)
        {
            try
            {
                InitializeComponent();
                this.DataContext = new Adibrata.Windows.UserController.MainVM(new Shell());
                SessionProperty = _session;
                BindMenuRoot();
                _frmwork = _frmworksheet;
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
        private void TreeViewItem_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = e.Source as TreeViewItem;
            UserManagementEntities _menutag = new UserManagementEntities();
            try
            {
                //if ((item.Items.Count == 1) && (item.Items[0] is string))
                //{
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
                    //MessageBox.Show(_menutag.MenuName);
                    if (_menutag.FormURL != null && _menutag.FormURL != "" && item.IsSelected)
                    {
                        RedirectPage redirect = new RedirectPage(_frmwork, _menutag.FormURL, SessionProperty);
                        _menutag.FormURL = "";
                    }
                    else
                    {
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
            //}
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