using Adibrata.Windows.UserController;
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

namespace Adibrata.DocumentSol.Windows
{
    /// <summary>
    /// Interaction logic for FredyTestPage.xaml
    /// </summary>
    public partial class FredyTestPage : Page
    {
        public FredyTestPage()
        {
            InitializeComponent();
            this.DataContext = new MainVM(new Shell());
            string colProperty = "Name";

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
        void addButton()
        {
            /*
             * Type --> Button
             * Header
             * Binding
             * */

        }
    }
}
