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

namespace Adibrata.Windows.UserController
{
    /// <summary>
    /// Interaction logic for UCTextBox.xaml
    /// </summary>
    public partial class UCTextBox : UserControl
    {
        public string MessageValidator { get; set; }
        public string InputValue
        {
            get { return txtInput.Text; }
            set { txtInput.Text = value; }
        }
        public Boolean IsMandatory { get; set; }
        public Boolean IsValid { get; set; }
        public int MaxLength { get; set; }
        public TextBox textInput
        {
            get { return txtInput; }
            set { txtInput = value; }
        }
        public UCTextBox()
        {
            InitializeComponent();
            txtInput.Text= this.InputValue;

            txtInput.MaxLength = this.MaxLength;
            this.IsValid = false;
            lblValidInput.Text = this.MessageValidator;
        }

        public void CheckValue()
        {
            if (this.IsMandatory)
            {
                if (txtInput.Text == "")
                {
                    lblValidInput.Text = this.MessageValidator;
                    this.IsValid = false;
                }
                else
                {
                    lblValidInput.Text = "";
                    this.IsValid = true;
                }
            }
        }
        
        private void txtInput_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            CheckValue();
        }
    }
}
