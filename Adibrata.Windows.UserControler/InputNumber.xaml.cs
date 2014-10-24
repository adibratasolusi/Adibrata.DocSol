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

namespace Adibrata.Windows.UserControler
{
    /// <summary>
    /// Interaction logic for InputNumber.xaml
    /// </summary>
    public partial class InputNumber : UserControl
    {
        public string TxtInputNumber
        {
            get { return txtNumber.Text ; }
            set 
            { 
                txtNumber.Text = value; 
            }
        }

        public InputNumber()
        {
            InitializeComponent();
        }

        private void txtNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
    }
}
