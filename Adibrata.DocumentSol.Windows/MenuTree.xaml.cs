using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.BusinessProcess.UserManagement.Entities;
using Adibrata.Controller.UserManagement;
using Adibrata.Framework.Logging;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
namespace Adibrata.DocumentSol.Windows
{
    /// <summary>
    /// Interaction logic for MenuTree.xaml
    /// </summary>
    public partial class MenuTree : Page
    {
        SessionEntities SessionProperty = new SessionEntities();
        Frame _mainframe = new Frame();
        public MenuTree(SessionEntities _session, Frame _frmworksheet)
        {
            try
            {
                InitializeComponent();
                _mainframe = _frmworksheet;
                SessionProperty = _session;
                this.DataContext = new Adibrata.Windows.UserController.MainVM(new Shell());

                dtgMenu.ItemsSource = MenuDataRetrieve().DefaultView;
                dtgMenu.CanUserSortColumns = true;
                dtgMenu.HeadersVisibility = DataGridHeadersVisibility.None;
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows",
                    ClassName = "MenuTree",
                    FunctionName = "MenuTree",
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

                
                _ent.Form = searchTextBox.Text;
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

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (searchTextBox.Text != "")
                {

                    dtgMenu.ItemsSource = MenuDataRetrieve().DefaultView;
                    dtgMenu.CanUserSortColumns = true;
                    dtgMenu.HeadersVisibility = DataGridHeadersVisibility.None;
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

        private void hpMenu_Click(object sender, RoutedEventArgs e)
        {
            //int i = dtgMenu.SelectedIndex;
            //DataGridHelper dtgHelper = new DataGridHelper();
            //dtgHelper.dtg = dtgMenu;
            //DataGridCell cell = dtgHelper.GetCell(i, 1);
            //var asd = cell.Content;
            //TextBlock formUrl = dtgHelper.GetVisualChild<TextBlock>(cell); // pass the DataGridCell as a parameter to GetVisualChild

            //string _formUrl = formUrl.Text;
            try
            {
                DataRowView Grdrow = ((FrameworkElement)sender).DataContext as DataRowView;



                //Fidn the DataGrid row index

                int rowIndex = dtgMenu.Items.IndexOf(Grdrow);



                //Find the DataGridCell

                DataGridCell cell = GetCell(rowIndex, 1); //Pass the row and column



                //Find the "lblVehicleName" lable.

                Label lblsource_address = GetVisualChild<Label>(cell); // pass the DataGridCell as a parameter to GetVisualChild



                string _value = lblsource_address.Content.ToString();


                RedirectPage redirect = new RedirectPage(_mainframe, _value, SessionProperty);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows",
                    ClassName = "MenuTree",
                    FunctionName = "hpMenu_Click",
                    ExceptionNumber = 1,
                    EventSource = "Main",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
            //RedirectPage(frmWorksheet, _formUrl, SessionProperty);
            //MessageBox.Show(_value);
            // frmWorksheet.NavigationService.Navigate( new Uri( "pack://application:,,,/AssemblyName;component/Resources/logo.png"+ _formUrl),UriKind.Relative);
            //this.NavigationService.Navigate(new AgrmntUploadProc(_value));
        }
        public DataGridCell GetCell(int row, int column)
        {

            DataGridRow rowContainer = GetRow(row);
            try
            {
                if (rowContainer != null)
                {

                    DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(rowContainer);

                    if (presenter == null)
                    {

                        dtgMenu.ScrollIntoView(rowContainer, dtgMenu.Columns[column]);

                        presenter = GetVisualChild<DataGridCellsPresenter>(rowContainer);

                    }

                    DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);

                    return cell;

                }
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows",
                    ClassName = "MenuTree",
                    FunctionName = "GetCell",
                    ExceptionNumber = 1,
                    EventSource = "Main",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
            return null;

        }



        public DataGridRow GetRow(int index)
        {
            DataGridRow row = new DataGridRow();
            try
            {
                row = (DataGridRow)dtgMenu.ItemContainerGenerator.ContainerFromIndex(index);

                if (row == null)
                {

                    dtgMenu.UpdateLayout();

                    dtgMenu.ScrollIntoView(dtgMenu.Items[index]);

                    row = (DataGridRow)dtgMenu.ItemContainerGenerator.ContainerFromIndex(index);

                }
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows",
                    ClassName = "MenuTree",
                    FunctionName = "GetRow",
                    ExceptionNumber = 1,
                    EventSource = "Main",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
            return row;

        }



        public T GetVisualChild<T>(Visual parent) where T : Visual
        {

            T child = default(T);
            try
            {
                int numVisuals = VisualTreeHelper.GetChildrenCount(parent);

                for (int i = 0; i < numVisuals; i++)
                {

                    Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);

                    child = v as T;

                    if (child == null)
                    {

                        child = GetVisualChild<T>

                        (v);

                    }

                    if (child != null)
                    {

                        break;

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
                    FunctionName = "GetVisualChild",
                    ExceptionNumber = 1,
                    EventSource = "Main",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
            return child;

        }

    }
}
