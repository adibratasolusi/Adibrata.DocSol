using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;

namespace Adibrata.Framework.ImageProcessing
{
    public class DataImageProcess
    {
        public static DateTime getImageDtCreate(Image img)
        {
            PropertyItem propItem = img.GetPropertyItem(0x132);
            DateTime dt = new DateTime();
            if (propItem != null)
            {
                System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                string text = encoding.GetString(propItem.Value, 0, propItem.Len - 1);

                CultureInfo provider = CultureInfo.InvariantCulture;
                dt = DateTime.ParseExact(text, "yyyy:MM:d H:m:s", provider);


            }
            else
            {
                dt = DateTime.Now;
            }
            return dt;
        }


    }
}
