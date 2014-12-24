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
using Adibrata.Framework.Security;
namespace Adibrata.DocumentSol.Windows
{
    /// <summary>
    /// Interaction logic for EncryptString.xaml
    /// </summary>
    public partial class EncryptString : Page
    {
        public EncryptString()
        {
            InitializeComponent();
        }

        private void btnEncrypt_Click(object sender, RoutedEventArgs e)
        {
            txtResult.Text = Encryption.EncryptToRSA (txtString.Text);
        }

        private void btnDecrypt_Click(object sender, RoutedEventArgs e)
        {
            txtString.Text = Encryption.DecryptFromRSA (txtResult.Text);
        }
    }
}
