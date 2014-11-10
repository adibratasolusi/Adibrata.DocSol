using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adibrata.Windows.UserControler.UCMenu.Project_Business_Layer.Menu
{
    public class MenuDataItem
    {

        #region " Declarations "

        private bool _bolIsSeparator = false;
        private int _intMenuItemID;
        private int _intMenuItemParentID;
        private int _intSortOrder;
        private string _strIcon = string.Empty;

        private string _strMenuText = string.Empty;
        #endregion

        #region " Properties "

        public string Icon
        {
            get { return _strIcon; }
            set { _strIcon = value; }
        }

        public bool IsSeparator
        {
            get { return _bolIsSeparator; }
            set { _bolIsSeparator = value; }
        }

        public int MenuItemID
        {
            get { return _intMenuItemID; }
            set { _intMenuItemID = value; }
        }

        public int MenuItemParentID
        {
            get { return _intMenuItemParentID; }
            set { _intMenuItemParentID = value; }
        }

        public string MenuText
        {
            get { return _strMenuText; }
            set { _strMenuText = value; }
        }

        public int SortOrder
        {
            get { return _intSortOrder; }
            set { _intSortOrder = value; }
        }

        #endregion

        #region " Constructors "


        public  MenuDataItem(int intMenuItemID, int intMenuItemParentID, int intSortOrder, string strMenuText, bool bolIsSeparator, string strIcon)
        {
            _intMenuItemID = intMenuItemID;
            _intMenuItemParentID = intMenuItemParentID;
            _intSortOrder = intSortOrder;
            _strMenuText = strMenuText;
            _bolIsSeparator = bolIsSeparator;
            _strIcon = strIcon;

        }

        #endregion

    }
}
