using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Configuration;
using System.Windows.Controls.Primitives;

namespace Adibrata.Demo.FileTransfer
{
    /// <summary>
    /// Interaction logic for AgrmntPaging.xaml
    /// </summary>
    public partial class AgrmntPaging : Page
    {
        public AgrmntPaging()
        {
            InitializeComponent();

        }

        private void cmbType_Loaded_1(object sender, RoutedEventArgs e)
        {
            // ... A List.
            List<string> data = new List<string>();
            data.Add("Agreement No");
            data.Add("Customer Name");

            // ... Assign the ItemsSource to the List.
            cmbType.ItemsSource = data;

            // ... Make the first item selected.
            cmbType.SelectedIndex = 0;
        }

        private void btnSearch_Click_1(object sender, RoutedEventArgs e)
        {
            string ConString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string CmdString = string.Empty;
            string by;
            if (cmbType.SelectedIndex == 0)
            {
                by = "AGREEMENT_NO";

            }
            else
            {
                by = "CUST_NAME";
            }
            string crit = txtCrit.Text;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                    CmdString = "select AGREEMENT_NO as [AgreementNo], CUST_NAME as [CustName] from AGREEMENT where " + by + " like '%" + crit + "%'";
                    SqlCommand cmd = new SqlCommand(CmdString, con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable("AGREEMENT");
                    sda.Fill(dt);
                    dgPaging.ItemsSource = dt.DefaultView;
                    dgPaging.CanUserSortColumns = false;


            }
        }

        private void btnUpload_Click_1(object sender, RoutedEventArgs e)
        {

            int i = dgPaging.SelectedIndex;
            DataGridCell cell = GetCell(i, 0);
            //var asd = cell.Content;
            TextBlock agrmntNo = GetVisualChild<TextBlock>(cell); // pass the DataGridCell as a parameter to GetVisualChild

            string _value = agrmntNo.Text;

            //MessageBox.Show(_value);
            this.NavigationService.Navigate(new AgrmntUpload(_value));
        }

        #region "GET GRID"
        public DataGridCell GetCell(int row, int column)
        {
            DataGridRow rowContainer = GetRow(row);

            if (rowContainer != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(rowContainer);

                DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                if (cell == null)
                {
                    dgPaging.ScrollIntoView(rowContainer, dgPaging.Columns[column]);
                    cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                }
                return cell;
            }
            return null;
        }

        public DataGridRow GetRow(int index)
        {
            DataGridRow row = (DataGridRow)dgPaging.ItemContainerGenerator.ContainerFromIndex(index);
            if (row == null)
            {
                dgPaging.UpdateLayout();
                dgPaging.ScrollIntoView(dgPaging.Items[index]);
                row = (DataGridRow)dgPaging.ItemContainerGenerator.ContainerFromIndex(index);
            }
            return row;
        }

        public static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }
        #endregion

        private void btnDocument_Click(object sender, RoutedEventArgs e)
        {
            int i = dgPaging.SelectedIndex;
            DataGridCell cell = GetCell(i, 0);
            //var asd = cell.Content;
            TextBlock agrmntNo = GetVisualChild<TextBlock>(cell); // pass the DataGridCell as a parameter to GetVisualChild

            string _value = agrmntNo.Text;

            //MessageBox.Show(_value);
            this.NavigationService.Navigate(new AgrmntView(_value));
        }
    }
}
