using Adibrata.Framework.Logging;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using WIATest;

namespace Adibrata.Windows.UserController
{
    /// <summary>
    /// Interaction logic for UCScanToPdf.xaml
    /// </summary>
    public partial class UCScanToPdf : UserControl
    {
        Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
        System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();


        int flag = 0;
        public class DataItem
        {
            public string PathFile { get; set; }
            public string img { get; set; }
        }
        public UCScanToPdf()
        {
            InitializeComponent();
        }


        #region BY SCAN

        private void rdbGrp_Checked(object sender, RoutedEventArgs e)
        {
            txtDir.Text = "";
        }

        private void rdbSingle_Checked(object sender, RoutedEventArgs e)
        {
            txtDir.Text = "";
        }
        private void scanFile()
        {
            if (txtDir.Text != "")
            {
                DataGridHelper dtgHelper = new DataGridHelper();
                dtgHelper.dtg = dtgUpload;
                if (rdbGrp.IsChecked == true)
                {
                    try
                    {


                        PdfDocument doc = new PdfDocument();
                        for (int i = 0; i < dtgUpload.Items.Count; i++)
                        {
                            DataGridCell cellPath = dtgHelper.GetCell(i, 1);
                            TextBlock path = (TextBlock)cellPath.Content;
                            doc.Pages.Add(new PdfPage());
                            XGraphics xgr = XGraphics.FromPdfPage(doc.Pages[i]);
                            XImage img = XImage.FromFile(path.Text);
                            xgr.DrawImage(img, 0, 0);
                            xgr.Dispose();
                        }
                        doc.Save(txtDir.Text);
                        doc.Close();
                        for (int i = 0; i < dtgUpload.Items.Count; i++)
                        {
                            
                            DataGridCell cellPath = dtgHelper.GetCell(i, 1);
                            TextBlock path = (TextBlock)cellPath.Content;
                            File.Delete(path.Text);
                        }

                        MessageBox.Show("Convert Succeed");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                    }
                }
                if (rdbSingle.IsChecked == true)
                {
                    try
                    {


                        for (int i = 0; i < dtgUpload.Items.Count; i++)
                        {

                            PdfDocument doc = new PdfDocument();
                            DataGridCell cellPath = dtgHelper.GetCell(i, 1);
                            TextBlock path = (TextBlock)cellPath.Content;
                            doc.Pages.Add(new PdfPage());
                            XGraphics xgr = XGraphics.FromPdfPage(doc.Pages[0]);
                            XImage img = XImage.FromFile(path.Text);
                            string fileName = System.IO.Path.GetFileNameWithoutExtension(path.Text);
                            xgr.DrawImage(img, 0, 0);


                            doc.Save(txtDir.Text + "\\" + fileName + ".pdf");
                            doc.Close();
                            xgr.Dispose();
                            File.Delete(path.Text);
                        }

                        MessageBox.Show("Convert Succeed");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                    }
                }
            }
            else
            {
                MessageBox.Show("Directory save null");
            }


        }
        private void btnScan_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                //get list of devices available
                List<string> devices = WIAScanner.GetDevices();

                foreach (string device in devices)
                {
                    lbDevices.Items.Add(device);
                }
                //check if device is not available
                if (lbDevices.Items.Count == 0)
                {
                    MessageBox.Show("You do not have any WIA devices.");
                }
                else
                {
                    lbDevices.SelectedIndex = 0;
                }
                //get images from scanner
                string path = "C:\\Temp\\"; // your code goes here

                bool exists = System.IO.Directory.Exists(path);

                if (!exists)
                    System.IO.Directory.CreateDirectory(path);
                List<System.Drawing.Image> images = WIAScanner.Scan((string)lbDevices.SelectedItem);
                string pathFile = path + DateTime.Now.ToString("yyyy-MM-dd HHmmss") + ".jpg";
                foreach (System.Drawing.Image img in images)
                {

                    img.Save(pathFile,ImageFormat.Jpeg);
                }
                dtgUpload.Items.Add(new DataItem { PathFile = pathFile, img = pathFile });
                dtgUpload.Items.Refresh();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void btnSaveTo_Click(object sender, RoutedEventArgs e)
        {
            if (rdbGrp.IsChecked == true)
            {

                sfd.Title = "Save PDF to...";
                sfd.DefaultExt = ".pdf";
                sfd.Filter = "Portable Document Format (*.pdf)|*.pdf|All files (*.*)|*.*";
                Nullable<bool> result = sfd.ShowDialog();
                if (result == true)
                {
                    txtDir.Text = sfd.FileName;
                    sfd.FileName = "";
                }
            }
            if (rdbSingle.IsChecked == true)
            {
                fbd.ShowDialog();
                txtDir.Text = fbd.SelectedPath;
                fbd.SelectedPath = "";
            }


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
                    UserLogin = "UCScanToPdf",
                    NameSpace = "Adibrata.Windows.UserController",
                    ClassName = "UCScanToPdf",
                    FunctionName = "btnDelete_Click",
                    ExceptionNumber = 1,
                    EventSource = "UCScanToPdf",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }
        #endregion

        #region BY FOLDER

        private void btnSaveToFile_Click(object sender, RoutedEventArgs e)
        {
            if (rdbGrpFile.IsChecked == true)
            {

                sfd.Title = "Save PDF to...";
                sfd.DefaultExt = ".pdf";
                sfd.Filter = "Portable Document Format (*.pdf)|*.pdf|All files (*.*)|*.*";
                Nullable<bool> result = sfd.ShowDialog();
                if (result == true)
                {

                    txtSaveFile.Text = sfd.FileName;
                    sfd.FileName = "";
                }

            }
            if (rdbSingleFile.IsChecked == true)
            {
                fbd.ShowDialog();
                txtSaveFile.Text = fbd.SelectedPath;
                fbd.SelectedPath = "";
            }
        }

        private void btnBrowseToFile_Click(object sender, RoutedEventArgs e)
        {
            fbd.ShowDialog();
            txtDirFile.Text = fbd.SelectedPath;
            fbd.SelectedPath = "";
        }

        private void rdbGrpFile_Checked(object sender, RoutedEventArgs e)
        {
            txtSaveFile.Text = "";
        }

        private void rdbSingleFile_Checked(object sender, RoutedEventArgs e)
        {

            txtSaveFile.Text = "";
        }
        private void dirFile()
        {
            if (txtDirFile.Text != "" && txtSaveFile.Text != "")
            {
                if (rdbGrpFile.IsChecked == true)
                {
                    try
                    {
                        PdfDocument doc = new PdfDocument();
                        int index = 0;
                        string[] fileDir = System.IO.Directory.GetFiles(txtDirFile.Text);

                        foreach (string FilePath in fileDir)
                        {
                            doc.Pages.Add(new PdfPage());
                            XGraphics xgr = XGraphics.FromPdfPage(doc.Pages[index]);
                            XImage img = XImage.FromFile(FilePath);
                            xgr.DrawImage(img, 0, 0);
                            xgr.Dispose();
                            index++;
                        }
                        doc.Save(txtSaveFile.Text);
                        doc.Close();

                        MessageBox.Show("Convert Succeed");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                    }
                }

                if (rdbSingleFile.IsChecked == true)
                {
                    try
                    {
                        string[] fileDir = System.IO.Directory.GetFiles(txtDirFile.Text);

                        foreach (string FilePath in fileDir)
                        {

                            PdfDocument doc = new PdfDocument();
                            doc.Pages.Add(new PdfPage());
                            XGraphics xgr = XGraphics.FromPdfPage(doc.Pages[0]);
                            XImage img = XImage.FromFile(FilePath);
                            xgr.DrawImage(img, 0, 0);

                            string fileName = System.IO.Path.GetFileNameWithoutExtension(FilePath);
                            doc.Save(txtSaveFile.Text + "\\" + fileName + ".pdf");
                            doc.Close();

                            xgr.Dispose();
                        }

                        MessageBox.Show("Convert Succeed");
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message, "Error");
                    }

                }
            }
            else
            {
                MessageBox.Show("Source or Destination null");
            }


        }
        #endregion



        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            progBar.IsIndeterminate = true;
            if (flag == 0)
            {
                scanFile();
            }
            else
            {
                dirFile();
            }

            progBar.IsIndeterminate = false;
            txtDir.Text = "";
            txtDirFile.Text = "";
            txtSaveFile.Text = "";
            dtgUpload.Items.Clear();
            dtgUpload.Items.Refresh();
        }

        private void tabScan_GotFocus(object sender, RoutedEventArgs e)
        {
            flag = 0;
        }

        private void tabDir_GotFocus(object sender, RoutedEventArgs e)
        {
            flag = 1;

        }


    }
}
