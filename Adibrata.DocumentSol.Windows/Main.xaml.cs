using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Framework.Logging;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Adibrata.Windows.UserController;
using Adibrata.Controller.UserManagement;
using System.Data;
using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.BusinessProcess.UserManagement.Entities;
using System.Windows.Media;
using System.Windows.Controls.Primitives;


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
            InitializeComponent();
            SessionProperty = _session;
            lblLoginName.Text = SessionProperty.UserName.ToUpper();
            lblBusinessDate.Text = DateTime.Now.ToString("dd/MMMM/yyyy");
            //RedirectPage redirect = new RedirectPage(frmWorksheet, "Form.FormRegistrasiPaging", SessionProperty);
        }

        private void TreeMenuGenerate()
        {

        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
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

        private void hpMenu_Click(object sender, RoutedEventArgs e)
        {
            //int i = dtgMenu.SelectedIndex;
            //DataGridHelper dtgHelper = new DataGridHelper();
            //dtgHelper.dtg = dtgMenu;
            //DataGridCell cell = dtgHelper.GetCell(i, 1);
            //var asd = cell.Content;
            //TextBlock formUrl = dtgHelper.GetVisualChild<TextBlock>(cell); // pass the DataGridCell as a parameter to GetVisualChild

            //string _formUrl = formUrl.Text;

            DataRowView Grdrow = ((FrameworkElement)sender).DataContext as DataRowView;



            //Fidn the DataGrid row index

            int rowIndex = dtgMenu.Items.IndexOf(Grdrow);



            //Find the DataGridCell

            DataGridCell cell = GetCell(rowIndex, 1); //Pass the row and column



            //Find the "lblVehicleName" lable.

            Label lblsource_address = GetVisualChild<Label>(cell); // pass the DataGridCell as a parameter to GetVisualChild



            string _value = lblsource_address.Content.ToString();


            RedirectPage redirect = new RedirectPage(frmWorksheet, _value, SessionProperty);

            //RedirectPage(frmWorksheet, _formUrl, SessionProperty);
            //MessageBox.Show(_value);
           // frmWorksheet.NavigationService.Navigate( new Uri( "pack://application:,,,/AssemblyName;component/Resources/logo.png"+ _formUrl),UriKind.Relative);
            //this.NavigationService.Navigate(new AgrmntUploadProc(_value));
        }
        public DataGridCell GetCell(int row, int column)
        {

            DataGridRow rowContainer = GetRow(row);

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

            return null;

        }



        public DataGridRow GetRow(int index)
        {

            DataGridRow row = (DataGridRow)dtgMenu.ItemContainerGenerator.ContainerFromIndex(index);

            if (row == null)
            {

                dtgMenu.UpdateLayout();

                dtgMenu.ScrollIntoView(dtgMenu.Items[index]);

                row = (DataGridRow)dtgMenu.ItemContainerGenerator.ContainerFromIndex(index);

            }

            return row;

        }



        public static T GetVisualChild<T>(Visual parent) where T : Visual
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

            }
            return child;

        }

    }
}
