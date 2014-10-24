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

namespace Adibrata.Demo.FileTransfer
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void mnUpload_Click_1(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Name == "mnUpload")
            {
                mainFrm.Source = new Uri("AgrmntPaging.xaml", UriKind.Relative);
            }
            if (btn.Name == "mnContact")
            {

                mainFrm.Source = new Uri("Contact.xaml", UriKind.Relative); ;
            } 
            if (btn.Name == "mnAbout")
            {

                mainFrm.Source = new Uri("About.xaml", UriKind.Relative); ;
            }
        }
    }
}
