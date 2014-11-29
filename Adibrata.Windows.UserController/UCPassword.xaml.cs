using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace Adibrata.Windows.UserController
{
    /// <summary>
    /// Interaction logic for UCPassword.xaml
    /// </summary>
    public partial class UCPassword : UserControl
    {
        public string MessageValidator { get; set; }
        public string PasswordValue
        {
            get { return txtPassword.Password; }
            set { txtPassword.Password = value; }
        }
        public Boolean IsValid { get; set; }

        public UCPassword()
        {
            InitializeComponent();
            this.IsValid = false;
            lblValidPassword.Text = this.MessageValidator;
        }

        private void txtPassword_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            CheckValue();

        }
        public void CheckValue()
        {
          
                if (txtPassword.Password == "")
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
