using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.Controller;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Adibrata.DocumentSol.Windows.AgrmntUpload
{
    /// <summary>
    /// Interaction logic for AgrmntUploadPaging.xaml
    /// </summary>
    public partial class AgrmntUploadPaging : Page
    {
        public AgrmntUploadPaging()
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
            DocSolEntities _ent = new DocSolEntities
            {
                MethodName = "PagingAgreement",
                ClassName = "UploadProcess"
            };
            _ent.By = by;
            _ent.Crit = crit;
            DataTable dt = new DataTable();
            dt = DocumentSolutionController.DocSolProcess<DataTable>(_ent);

            dgPaging.ItemsSource = dt.DefaultView;
            dgPaging.CanUserSortColumns = false;


        }
        private void btnUpload_Click_1(object sender, RoutedEventArgs e)
        {

            int i = dgPaging.SelectedIndex;
            DataGridCell cell = GetCell(i, 0);
            //var asd = cell.Content;
            TextBlock agrmntNo = GetVisualChild<TextBlock>(cell); // pass the DataGridCell as a parameter to GetVisualChild

            string _value = agrmntNo.Text;

            //MessageBox.Show(_value);
            this.NavigationService.Navigate(new AgrmntUploadProc(_value));
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
            this.NavigationService.Navigate(new AgrmntUploadView(_value));
        }
    }

}
