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

namespace Adibrata.Windows.UserController.DocContent.UploadAgreement
{
    /// <summary>
    /// Interaction logic for UCUploadAgreement.xaml
    /// </summary>
    public partial class UCUploadAgreement : UserControl
    {

        Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
        public UCUploadAgreement()
        {
            InitializeComponent();
        }
        public class DataItem
        {
            public string PathFile { get; set; }
            public string img { get; set; }
        }
        public string AgreementNo
        {
            get;
            set;
        }
        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {

            // Create OpenFileDialog

            // Set filter for file extension and default file extension
            dlg.Title = "Select a picture";
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
            "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
            "Portable Network Graphic (*.png)|*.png";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {

                string filename = dlg.FileName;
                dtgUpload.Items.Add(new DataItem { PathFile = filename, img = filename });

            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
