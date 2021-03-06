﻿using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Adibrata.Windows.UserController
{
    public class DataGridHelper
    {

        /*Cara pemanggilan
         *            
         *   int i = dgPaging.SelectedIndex;

            DataGridHelper oDataGrid = new DataGridHelper();
            oDataGrid.dtg = dgPaging;
            DataGridCell cell = oDataGrid.GetCell(i, 0);
            TextBlock ReffKey = oDataGrid.GetVisualChild<TextBlock>(cell); // pass the DataGridCell as a parameter to GetVisualChild
            SessionProperty.IsEdit = true;

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

        public T GetVisualChild<T>(Visual parent) where T : Visual
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
