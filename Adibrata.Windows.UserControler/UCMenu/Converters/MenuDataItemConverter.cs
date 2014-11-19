using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Adibrata.Windows.UserControler.UCMenu.Converters
{

    public class MenuDataItemConverter : IValueConverter
    {

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {


            if (value is int)
            {
                UCMainMenu objWindow = new UCMainMenu();

                return objWindow.GetChildItems((int)value);

            }
            else
            {
                return null;
            }

        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            throw new NotImplementedException();

        }

    }
}
