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
    /// Interaction logic for Address.xaml
    /// </summary>
    public partial class UCAddress : UserControl
    {
    //    public string Address
    //    {
    //        get { return }
    //        set; }
        public TextBox Address
        {
            get { return txtAddress;}
            set { txtAddress = value; }
        }
        public TextBox RT
        {
            get { return txtRT; }
            set { txtRT = value; }
        }
        public TextBox RW
        {
            get { return txtRW; }
            set { txtRW = value; }
        }
        public TextBox Kelurahan
        {
            get { return txtKelurahan; }
            set { txtKelurahan = value; }
        }
        public TextBox Kecamatan
        {
            get { return txtKecamatan; }
            set { txtKecamatan = value; }
        }
        public TextBox City
        {
            get { return txtCity; }
            set { txtCity = value; }
        }
        public TextBox ZipCode
        {
            get { return txtZipCode; }
            set { txtZipCode = value; }
        }


        public UCAddress()
        {
            InitializeComponent();
        }

     
    }
}
