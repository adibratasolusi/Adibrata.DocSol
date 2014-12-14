using Adibrata.Framework.Logging;
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
    /// Interaction logic for UCUploadAgreementAllDoc.xaml
    /// </summary>
    public partial class UCUploadAgreementAllDoc : UserControl
    {

        Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
        public UCUploadAgreementAllDoc()
        {
            InitializeComponent();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                dtgUpload.Items.RemoveAt(dtgUpload.SelectedIndex);
                dtgUpload.Items.Refresh();
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = "UCUploadAgreement",
                    NameSpace = "Adibrata.Windows.UserController.DocContent.UploadAgreement",
                    ClassName = "UCUploadAgreement",
                    FunctionName = "btnDelete_Click",
                    ExceptionNumber = 1,
                    EventSource = "UCUploadAgreement",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        private void btnBrows_Click(object sender, RoutedEventArgs e)
        {
            BrowseFile();
        }
        private void BrowseFile()
        {
            // Create OpenFileDialog
            try
            {
                // Set filter for file extension and default file extension
                dlg.Title = "Select a picture";
                dlg.DefaultExt = ".jpg";
                dlg.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                "Portable Network Graphic (*.png)|*.png";


                // Display OpenFileDialog by calling ShowDialog method
                Nullable<bool> result = dlg.ShowDialog();

                // Get the selected file name and display in a DataGrid
                if (result == true)
                {

                    string filename = dlg.FileName;

                }
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = "UCUploadAgreement",
                    NameSpace = "Adibrata.Windows.UserController.DocContent.UploadAgreement",
                    ClassName = "UCUploadAgreement",
                    FunctionName = "BrowseFile",
                    ExceptionNumber = 1,
                    EventSource = "UCUploadAgreement",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

    }
}
