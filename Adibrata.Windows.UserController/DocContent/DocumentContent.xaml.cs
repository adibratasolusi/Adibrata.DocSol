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

namespace Adibrata.Windows.UserController.DocContent
{
    /// <summary>
    /// Interaction logic for DocumentContent.xaml
    /// </summary>
    public partial class DocumentContent : UserControl
    {
        
        public DocumentContent()
        {
            InitializeComponent();
            GenerateControls();
        }
        public void GenerateControls()
        {
            Button btnClickMe = new Button();
            btnClickMe.Content = "Click Me";
            btnClickMe.Name = "btnClickMe";
            btnClickMe.Click += new RoutedEventHandler(this.CallMeClick);
            oStackPanel.Children.Add(btnClickMe);
            TextBox txtNumber = new TextBox();
            txtNumber.Name = "txtNumber";
            txtNumber.Text = "1776";
            oStackPanel.Children.Add(txtNumber);
            oStackPanel.RegisterName(txtNumber.Name, txtNumber);
        }
        protected void CallMeClick(object sender, RoutedEventArgs e)
        {
            TextBox txtNumber = (TextBox)this.oStackPanel.FindName("txtNumber");
            string message = string.Format("The number is {0}", txtNumber.Text);
            MessageBox.Show(message);
        }
    }
}
