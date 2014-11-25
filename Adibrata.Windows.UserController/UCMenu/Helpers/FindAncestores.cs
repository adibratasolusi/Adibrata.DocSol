using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Adibrata.Windows.UserController.UCMenu.Helpers
{
    public class FindAncestores
    {
        public static MenuItem FindAncestorMenuItem(DependencyObject objDependencyObject)
        {

            while ((objDependencyObject) != null)
            {
                objDependencyObject = VisualTreeHelper.GetParent(objDependencyObject);

                if (objDependencyObject is MenuItem)
                {
                    break; // TODO: might not be correct. Was : Exit While
                }

            }

            return objDependencyObject as MenuItem;

        }

    }
}
