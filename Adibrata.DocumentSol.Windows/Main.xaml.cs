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
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        SessionEntities SessionProperty = new SessionEntities();
        public Main(SessionEntities _session)
        {
            try
            {
                InitializeComponent();
                SessionProperty = _session;
                lblLoginName.Text = SessionProperty.UserName.ToUpper();
                lblBusinessDate.Text = DateTime.Now.ToString("dd/MMMM/yyyy");
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows",
                    ClassName = "Main",
                    FunctionName = "Main",
                    ExceptionNumber = 1,
                    EventSource = "Main",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
            //RedirectPage redirect = new RedirectPage(frmWorksheet, "Form.FormRegistrasiPaging", SessionProperty);
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UserManagementEntities _ent = new UserManagementEntities
                {
                    MethodName = "SearchEngineMenu",
                    ClassName = "MainMenu"
                };

                DataTable dt = new DataTable();
                _ent.Form = searchTextBox.Text;
                dt = UserManagementController.UserManagement<DataTable>(_ent);

                dtgMenu.ItemsSource = dt.DefaultView;
                dtgMenu.CanUserSortColumns = true;
                dtgMenu.HeadersVisibility = DataGridHeadersVisibility.None;
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows",
                    ClassName = "Main",
                    FunctionName = "Main",
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


                RedirectPage redirect = new RedirectPage(frmWorksheet, _value, SessionProperty);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows",
                    ClassName = "Main",
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
                    ClassName = "Main",
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
                    ClassName = "Main",
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
                    ClassName = "Main",
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

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                this.NavigationService.Navigate(new Login.Login()); 
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows",
                    ClassName = "Main",
                    FunctionName = "btnLogout_Click",
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
