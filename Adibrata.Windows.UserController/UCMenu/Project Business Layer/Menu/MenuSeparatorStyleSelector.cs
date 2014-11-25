using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Adibrata.Windows.UserController.UCMenu.Project_Business_Layer.Menu
{
    public class MenuSeparatorStyleSelector : StyleSelector
    {

        public override System.Windows.Style SelectStyle(object item, System.Windows.DependencyObject container)
        {

            if (((MenuDataItem)item).IsSeparator)
            {
                return (Style)((FrameworkElement)container).FindResource("menuSeparatorStyle");

            }
            else
            {
                return base.SelectStyle(item, container);
            }

        }

    }
}
