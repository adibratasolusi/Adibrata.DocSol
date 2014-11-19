using Adibrata.BusinessProcess.UserManagement.Entities;
using Adibrata.Controller.UserManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adibrata.Windows.UserControler.UCMenu.Project_Business_Layer.Menu
{
    public class MenuItems
    {
        public static List<MenuDataItem> Load()
        {

            UserManagementEntities _ent = new UserManagementEntities
            {
                MethodName = "MainMenuGetActive",
                ClassName = "UserManagement"
            };
            List<MenuDataItem> objMenuDataItems = new List<MenuDataItem>();
            DataTable dt = new DataTable();
            dt = UserManagementController.UserManagement<DataTable>(_ent);

            //TODO devlopers load from a data base
            //You will probably want to return rows based on the user.

            foreach (DataRow row in dt.Rows)
            {
                int menuItemId = Convert.ToInt16(row["MENU_ITEM_ID"].ToString());
                int menuParentId = Convert.ToInt16(row["MENU_PARENT_ID"].ToString());
                int shortOrder = Convert.ToInt16(row["SHORT_ORDER"].ToString());
                string menuTxt = row["MENU_TXT"].ToString();
                bool isSeparator = false;
                if (row["IS_SEPARATOR"].ToString() == "1")
                {
                    isSeparator = true;
                }
                string icon = row["ICON"].ToString();
                string form = row["FORM"].ToString();
                objMenuDataItems.Add(new MenuDataItem(menuItemId, menuParentId, shortOrder, menuTxt, isSeparator, icon,form));
            }
            objMenuDataItems.Sort(new MenuDataItemComparer());
            return objMenuDataItems;

        }

    }

}
