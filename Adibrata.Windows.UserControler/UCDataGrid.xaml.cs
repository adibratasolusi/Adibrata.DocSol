using System;
using System.Collections.Generic;
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
using System.Windows.Controls.Primitives;
using System.Data;

namespace Adibrata.Windows.UserControler
{
    /// <summary>
    /// Interaction logic for UCDataGrid.xaml
    /// </summary>
    public partial class UCDataGrid : UserControl
    {
        public UCDataGrid()
        {
            InitializeComponent();
        }
        public int Row
        {
            get;
            set;
        }
        public int Column
        {
            get;
            set;
        }

        public DataGridCell GetCell
        {
            get { return _getCell(Row, Column); }
        }

        
        public void AddColumn(string header, string binding, object asd)
        {
         
            DataGridTemplateColumn textColumn = new DataGridTemplateColumn();
            textColumn.Header = header;
            textColumn.CellTemplate = (DataTemplate)Resources[asd];
            dtGrid.Columns.Add(textColumn);
            //DataGridTemplateColumn templaterColumn = new DataGridTemplateColumn();
            //templaterColumn.CellTemplate
        }
        public void Resourse(DataTable dt)
        {
            dtGrid.ItemsSource = dt.DefaultView;

        }
        /*
         *                    <DataGridTemplateColumn Header="Upload">
                        <DataGridTemplateColumn.CellTemplate>
                            <DataTemplate>
                                <Button Name="btnUpload" Content="Upload" Click="btnUpload_Click_1"></Button>
                            </DataTemplate>
                        </DataGridTemplateColumn.CellTemplate>

                    </DataGridTemplateColumn> 
         */

        #region "GET GRID"
        private DataGridCell _getCell(int row, int column)
        {
            DataGridRow rowContainer = GetRow(row);

            if (rowContainer != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(rowContainer);

                DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                if (cell == null)
                {
                    dtGrid.ScrollIntoView(rowContainer, dtGrid.Columns[column]);
                    cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                }
                return cell;
            }
            return null;
        }

        private DataGridRow GetRow(int index)
        {
            DataGridRow row = (DataGridRow)dtGrid.ItemContainerGenerator.ContainerFromIndex(index);
            if (row == null)
            {
                dtGrid.UpdateLayout();
                dtGrid.ScrollIntoView(dtGrid.Items[index]);
                row = (DataGridRow)dtGrid.ItemContainerGenerator.ContainerFromIndex(index);
            }
            return row;
        }

        private static T GetVisualChild<T>(Visual parent) where T : Visual
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
    }
}
