using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Configuration;
using Adibrata.Controller;
using Adibrata.Framework.Logging;
using Adibrata.Windows.UserController;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;






namespace Adibrata.DocumentSol.Windows.UploadInquiry
{
    /// <summary>
    /// Interaction logic for UploadDetailInquiry.xaml
    /// </summary>
    public partial class UploadDetailInquiry : Page
    {
        SessionEntities SessionProperty = new SessionEntities();
 
        #region Global Variable

        object jobTransferredSync = new object();
        Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
        // ServiceReference1.Service1Client objService = new ServiceReference1.Service1Client();
        Dictionary<int, string> dicFile = new Dictionary<int, string>();
        Dictionary<int, string> dicExt = new Dictionary<int, string>();
        string server = AppConfig.Config("BITSServer");
        int jumlahUploadMax; //jumlah maksimal upload, masih hardcode
        #endregion
        public class DataItem
        {
            public string PathFile { get; set; }
            public string img { get; set; }
        }
   
        public UploadDetailInquiry(SessionEntities _session)
        {
            try
            {
                DocSolEntities _ent = new DocSolEntities();
                InitializeComponent();
                this.DataContext = new MainVM(new Shell());
                SessionProperty = _session;
                string _doctranscode = SessionProperty.ReffKey;
                _ent.ClassName = "UploadProcess";
                _ent.MethodName = "DocTransGetTransID";
                _ent.DocTransCode = SessionProperty.ReffKey;
                _ent.UserName = SessionProperty.UserName;
                //_ent.DocTransId = Convert.ToInt64(SessionProperty.ReffKey);
                SessionProperty.ReffKey = Convert.ToString(DocumentSolutionController.DocSolProcess<Int64>(_ent));

                txtDocTransId.Text =  _ent.DocTransCode;
                    
                bindContent();
                bindBinary();
                WebBrowser browser = new WebBrowser();
                browser.NavigateToString("www.detik.com");
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.UploadInquiry",
                    ClassName = "UploadDetailInquiry",
                    FunctionName = "btnEdit_Click",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
               
            }

        }
       
        private void bindContent()
        {
            try
            {
                DocSolEntities _ent = new DocSolEntities();
                DataTable _dt = new DataTable();
                _ent.ClassName = "UploadProcess";
                _ent.MethodName = "DocTransContentDetail";
                _ent.DocTransCode = SessionProperty.ReffKey;
                _ent.UserName = SessionProperty.UserName;
                //_ent.DocTransId = Convert.ToInt64(SessionProperty.ReffKey);
                _dt = DocumentSolutionController.DocSolProcess<DataTable>(_ent);
                dtgContent.ItemsSource = _dt.DefaultView;
            }
            catch (Exception _exp)
            {

                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.UploadInquiry",
                    ClassName = "UploadDetailInquiry",
                    FunctionName = "bindContent",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }

        }

        void bindBinary()
        {
            try
            {
                DocSolEntities _ent = new DocSolEntities();
                DataTable _dt = new DataTable();
                _ent.ClassName = "UploadProcess";
                _ent.MethodName = "DocTransInquiryDetail";
                _ent.UserName = SessionProperty.UserName;
                _ent.DocTransCode = SessionProperty.ReffKey;
                _dt = DocumentSolutionController.DocSolProcess<DataTable>(_ent);
                dgPaging.ItemsSource = _dt.DefaultView;
            }
            catch (Exception _exp)
            {

                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.UploadInquiry",
                    ClassName = "UploadDetailInquiry",
                    FunctionName = "bindBinary",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }

        }

        private void btnBack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {

                RedirectPage redirect = new RedirectPage(this, SessionProperty.SourceForm, SessionProperty);
            }
            catch (Exception _exp)
            {

                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.UploadInquiry",
                    ClassName = "UploadDetailInquiry",
                    FunctionName = "bindBinary",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        //private void Hide_Click(object sender, System.Windows.RoutedEventArgs e)
        //{
          
        //}

        private void dgPaging_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
            Byte[] _imgbin;
            string _filename;
            WebBrowser _brow = new WebBrowser();
            _imgbin = (Byte[])((DataRowView)dgPaging.SelectedItem)["FileBin"];
            
            _filename = @"C:\" + (string)((DataRowView)dgPaging.SelectedItem)["FileName"];

            System.IO.FileStream _FileStream = new System.IO.FileStream(_filename, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            _FileStream.Write(_imgbin, 0, _imgbin.Length);
            _FileStream.Close();
             //brow.Navigate(_filename);
            //WebBrowser brow = new WebBrowser();
            //brow.Navigate("http://www.detik.com");

            if ((DataRowView)dgPaging.SelectedItem != null)
            {
                string msg = "Ready to Save";
                MessageBoxResult result =
                  MessageBox.Show(_filename + "-" +
                    msg, 
                    "Save Your File", 
                    MessageBoxButton.OKCancel, 
                    MessageBoxImage.Question);
                if (result == MessageBoxResult.OK)
                {
                
                    BrowseFile();
                }
            }
             else 
                {
                   MessageBox.Show( "Lost File Check The Other File");
                   
                }
        }
        
        // Masih belum bisa untuk di save

      
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
                "Portable Network Graphic (*.png)|*.png|" +
                "Portable Document Format (*.pdf)|*.pdf|" +
                "Word Document (*.doc;*.docx)|*.doc;*.docx|" +
                "All files (*.*)|*.*";
                dlg.Multiselect = true;

                // Display OpenFileDialog by calling ShowDialog method

                Nullable<bool> result = dlg.ShowDialog();
              

                // Get the selected file name and display in a DataGrid
                if (result == true)
                {
                    foreach (String file in dlg.FileNames)
                    {
                        string filename = file;
                        string path = Path.GetExtension(filename).ToLower();
                        string picture = "";
                        if (path == ".jpg" || path == ".jpeg" || path == ".png")
                        {
                            picture = filename;
                        }
                        else
                        {
                            picture = "";
                        }
                        for (int i = 0; i < dgPaging.Items.Count; i++)
                        {
                            filename += dgPaging.SelectedItems[i] + ":" + dgPaging.SelectedItems[i];
                            filename += Environment.NewLine;
                        }
                        Stream myStream;
                   

                        if (dlg.ShowDialog() == result.Value)
                        {
                            if ((myStream = dlg.OpenFile()) != null)
                            {
                                StreamWriter wText = new StreamWriter(myStream);
                                wText.Write(filename); myStream.Close();
                            }
                        }
                     dgPaging.Items.Add(new DataItem { PathFile = filename, img = picture });
                    }
                    dgPaging.Items.Refresh();
                }
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = "UCUploadAgreement",
                    NameSpace = "Adibrata.DocumentSol.Windows.UploadInquiry",
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

        private void SaveFile()
        {

            #region hard code save
            //                         if(saveFileDialog1.FileName != "")
            //   {
            //      // Saves the Image via a FileStream created by the OpenFile method.
            //      System.IO.FileStream fs = 
            //         (System.IO.FileStream)dgPaging.OpenFile();
            //      // Saves the Image in the appropriate ImageFormat based upon the
            //      // File type selected in the dialog box.
            //      // NOTE that the FilterIndex property is one-based.
            //      switch(dgPaging.FilterIndex)
            //      {
            //         case 1 : 
            //         this.button2.Image.Save(fs, 
            //            System.Drawing.Imaging.ImageFormat.Jpeg);
            //         break;

            //         case 2 : 
            //         this.button2.Image.Save(fs, 
            //            System.Drawing.Imaging.ImageFormat.Bmp);
            //         break;

            //         case 3 : 
            //         this.button2.Image.Save(fs, 
            //            System.Drawing.Imaging.ImageFormat.Gif);
            //         break;
            //      }

            //   fs.Close();
            //   }
            //
            #endregion

            #region  hard code save
            //SaveFileDialog dialog = new SaveFileDialog();
            //if (dialog.ShowDialog() ==  dgPaging.SelectedItem())
            //{

            //   bmp.Save(dialog.FileName, ImageFormat.JPEG);
            //}
            #endregion
                    
        }

    }
}
