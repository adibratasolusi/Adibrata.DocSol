using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adibrata.Windows.UserControler.UCMenu.Project_Business_Layer.Menu
{
    public class MenuDataItemComparer : IComparer<MenuDataItem>
    {

        public int Compare(MenuDataItem x, MenuDataItem y)
        {


            if (x == null)
            {
                if (y == null)
                {
                    return 0;

                }
                else
                {
                    return -1;
                }


            }
            else
            {
                int retValue = x.MenuItemParentID.CompareTo(y.MenuItemParentID);

                if (retValue != 0)
                {
                    return retValue;

                }
                else
                {
                    return x.SortOrder.CompareTo(y.SortOrder);
                }

            }

        }

    }

}
