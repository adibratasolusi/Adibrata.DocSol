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
        public Boolean IsMandatory { get; set;}

        public string TxtInputNumber
        {
            get { return txtNumber.Text.Replace(",",""); }
            set { txtNumber.Text = value; }
        }

        public decimal NumberValue { get; set; }

        public InputNumber()
        {
            InitializeComponent();
            txtNumber.Text = "0.00";
        }

        private void txtNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
           CheckValue();

        }
        public void CheckValue()
        {
            decimal _verify;
            if (this.IsMandatory && txtNumber.Text == "")
            {
                lblValidInput.Text = "Please Input With Decimal";
            }
            else
            {
                //txtNumber.Text = Convert.ToDecimal(txtNumber.Text).ToString("#.##");
                lblValidInput.Text = "0.00";
            }
            if (!decimal.TryParse(txtNumber.Text.Replace(",", ""), out _verify))
            {
                lblValidInput.Text = "Please Input With Decimal";
                txtNumber.Text = "0.00";
            }
            else
            {

                //txtNumber.Text = Convert.ToDecimal(txtNumber.Text).ToString("#0.00");
                lblValidInput.Text = "";
            }
        }

        private void txtNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            txtNumber.Text = Convert.ToDecimal(txtNumber.Text).ToString("#,###.##");
        }

    }
}
