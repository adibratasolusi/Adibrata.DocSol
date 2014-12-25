using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adibrata.DocumentSol.Windows.CommonClass
{
    public class Format
    {
        public string NumberFormatting(string num)
        {
           return Convert.ToDecimal(num).ToString("#,##0.00");
 
        }
        public string NumberFormatting(int num)
        {

           return Convert.ToDecimal(num).ToString("#,##0.00");
        }
        public string NumberFormatting(decimal num)
        {

           return  num.ToString("#,##0.00");
        }
    }
}
