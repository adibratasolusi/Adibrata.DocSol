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
    /// Interaction logic for UCPassword.xaml
    /// </summary>
    public partial class UCPassword : UserControl
    {
        public string MessageValidator { get; set; }
        public string PasswordValue { get; set; }
        public Boolean IsValid { get; set; }

        public UCPassword()
        {
            InitializeComponent();
            this.IsValid = false;
            lblValidPassword.Text = this.MessageValidator;
        }

        private void txtPassword_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (txtPassword.Password== "")
            {
                lblValidPassword.Text = this.MessageValidator;
                this.IsValid = false;
            }
            else
            {
                lblValidPassword.Text = "";
                this.IsValid = true;
            }

        }

    }
}
