using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Adibrata.Framework.ObjetHelper
{
    public class DataGridHelper
    {

        /*Cara pemanggilan
         *            
         * 
           int i = ObjDatagrid.SelectedIndex;
           DataGridHelper.dtg = ObjDatagrid
           DataGridCell cell = DataGridHelper.GetCell(i, 0);
           TextBlock agrmntNo = DataGridHelper.GetVisualChild<TextBlock>(cell); // pass the DataGridCell as a parameter to GetVisualChild
           string _value = agrmntNo.Text;
         * 
         */
        public DataGrid dtg
        {
            get;
            set;
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
                    dtg.ScrollIntoView(rowContainer, dtg.Columns[column]);
                    cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                }
                return cell;
            }
            return null;
        }

        public DataGridRow GetRow(int index)
        {
            DataGridRow row = (DataGridRow)dtg.ItemContainerGenerator.ContainerFromIndex(index);
            if (row == null)
            {
                dtg.UpdateLayout();
                dtg.ScrollIntoView(dtg.Items[index]);
                row = (DataGridRow)dtg.ItemContainerGenerator.ContainerFromIndex(index);
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

    }
}
