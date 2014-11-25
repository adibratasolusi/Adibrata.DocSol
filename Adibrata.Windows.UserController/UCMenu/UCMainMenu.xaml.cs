using Adibrata.Windows.UserController.UCMenu.Helpers;
using Adibrata.Windows.UserController.UCMenu.Project_Business_Layer.Menu;
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

namespace Adibrata.Windows.UserController.UCMenu
{
    /// <summary>
    /// Interaction logic for UCMainMenu.xaml
    /// </summary>
    public partial class UCMainMenu : UserControl
    {
        public Frame mainFrame
        { get; set; }
        public UCMainMenu()
        {
            InitializeComponent();

            _objMenuDataItems = MenuItems.Load();
            this.mnuMainMenu.DataContext = GetChildItems(0);
        }
        #region " Declarations "


        private List<MenuDataItem> _objMenuDataItems;
        #endregion

        #region " Properties "

        public List<MenuDataItem> MenuDataItems
        {
            get { return _objMenuDataItems; }
        }

        #endregion

        #region " Constructors & Loaded "



        #endregion

        #region " Menu "

        int flag = 1;
        private void OnMenuDataItemLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            MenuItem objMenuItem = FindAncestores.FindAncestorMenuItem((DependencyObject)sender);


            if (objMenuItem != null)
            {
                MenuDataItem objMenuDataItem = objMenuItem.DataContext as MenuDataItem;


                if (objMenuDataItem != null)
                {

                    if (objMenuDataItem.Icon.Length > 0)
                    {
                        //You have a number of options here.
                        //  1.  you could assign each menu icon a resource name
                        //  2.  or you could switch the directory based on the skin.
                        //  3.  or ...
                        //I have hard coded the menu icon directory for this short demo
                        Image img = default(Image);
                        img = new Image();
                        img.Source = new BitmapImage(new Uri(string.Concat("Images\\", objMenuDataItem.Icon), UriKind.Relative));
                        objMenuItem.Icon = img;
                        objMenuItem.Margin = new Thickness(0, 8, 0, 0);
                    }
                    if (objMenuDataItem.Form.Length > 0)
                    {
                        flag = 1;
                        objMenuItem.Click += objMenuItem_Click;


                    }
                    //TODO developers this is where you assign the 
                    //   MenuItem Command and CommandParameter
                    //
                    // Depending on your application architecture, you'll need to 
                    //  expand the MenuDataItem class to support your menu commands.
                }

            }

        }

        void objMenuItem_Click(object sender, RoutedEventArgs e)
        {

            if (flag == 1)
            {
                MenuItem objMenuItem = (MenuItem)sender;

                MenuDataItem objMenuDataItem = objMenuItem.DataContext as MenuDataItem;
                mainFrame.Source = new Uri("pack://application:,,,/" + objMenuDataItem.Form, UriKind.Absolute);

                flag++;
            }

        }









        //scope must be Friend so that the MenuDataItemConverter 
        //  can use this function
        internal List<MenuDataItem> GetChildItems(int intParentID)
        {

            List<MenuDataItem> obj = new List<MenuDataItem>();


            foreach (MenuDataItem i in this.MenuDataItems)
            {
                if (i.MenuItemParentID == intParentID)
                {
                    obj.Add(i);
                }

            }

            return obj;

        }

        #endregion

    }
}
