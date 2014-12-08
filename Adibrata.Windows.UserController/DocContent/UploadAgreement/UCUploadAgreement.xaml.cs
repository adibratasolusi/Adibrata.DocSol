using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.Configuration;
using Adibrata.Framework.ImageProcessing;
using Adibrata.Framework.Messaging;
using SharpBits.Base;
using System;
using System.Collections.Generic;
using System.Data;
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
using Adibrata.Controller;
namespace Adibrata.Windows.UserController.DocContent.UploadAgreement
{
    /// <summary>
    /// Interaction logic for UCUploadAgreement.xaml
    /// </summary>
    public partial class UCUploadAgreement : UserControl
    {

        #region Global Variable
        Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
        string trxFileName;
        int jobCount = 0;
        int fileCount = 0;
        // ServiceReference1.Service1Client objService = new ServiceReference1.Service1Client();
        Dictionary<int, string> dicFile = new Dictionary<int, string>();
        Dictionary<int, string> dicExt = new Dictionary<int, string>();
        string server = AppConfig.Config("BITSServer");
        int jumlahUploadMax; //jumlah maksimal upload, masih hardcode
        #endregion

        #region Properties
        public class DataItem
        {
            public string PathFile { get; set; }
            public string img { get; set; }
        }
        public string UserLogin { get; set; }
        public string DocumentType { get; set; }
        public Int64 TransId
        {
            get;
            set;
        }
        #endregion

        #region InitializeComponent
        public UCUploadAgreement()
        {
            InitializeComponent();
            //jumlahUploadMax = 3;
        }
        public void BindingValueMax()
        {
            dtgUpload.Items.Clear();
            DocSolEntities _ent = new DocSolEntities { ClassName = "DocContent", MethodName = "DocContentFiles", UserLogin = this.UserLogin, DocumentType = this.DocumentType };
            jumlahUploadMax = DocumentSolutionController.DocSolProcess<int>(_ent);
        }
        #endregion

        #region Button Event

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            if (dtgUpload.Items.Count >= jumlahUploadMax)
            {
                MessageBox.Show("Number Of File insufficient, please check the Number of File Configuration");
            }
            else
            {
                BrowseFile();
            }

        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

            dtgUpload.Items.RemoveAt(dtgUpload.SelectedIndex);
            dtgUpload.Items.Refresh();
        }
        #endregion

        #region Method
        public void CheckAndUpload(DataTable dtContent)
        {
            
            if (dtgUpload.Items.Count >= jumlahUploadMax)
            {

                MessageBox.Show("Number Of File insufficient, please check the Number of File Configuration");
            }
            else
            {
                FileUpload(dtContent);
            }
        }
        private void BrowseFile()
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

            // Get the selected file name and display in a DataGrid
            if (result == true)
            {

                string filename = dlg.FileName;
                dtgUpload.Items.Add(new DataItem { PathFile = filename, img = filename });

            }
        }

        private void FileUpload(DataTable dtContent)
        {

            //set flag for save to database
            #region "reset flag"
            fileCount = 0;
            jobCount = 0;
            dicFile.Clear();
            dicExt.Clear();
            #endregion
            DataGridHelper dtgHelper = new DataGridHelper();
            dtgHelper.dtg = dtgUpload;
            int flagContent = 0;
            for (int i = 0; i < dtgUpload.Items.Count; i++)
            {
                var path = new TextBlock();
                var docType = new TextBlock();

                DataGridCell cellPath = dtgHelper.GetCell(i, 1);
                path = (TextBlock)cellPath.Content;
                if (path.Text != null && path.Text != "")
                {
                    string newPath = WaterMarkProcess.SetWatermark(path.Text);
                    string extension = System.IO.Path.GetExtension(newPath);
                    fileCount += 1; //set jumlah file
                    saveUpload(docType.Text, TransId);
                    if (flagContent !=0)
                    {
                        DataTable test = dtContent;
                    }
                    flagContent += 1;
                    dicExt.Add(fileCount, extension);
                }

            }
            for (int i = 0; i < dtgUpload.Items.Count; i++)
            {
                var path = new TextBox();
                DataGridCell cellPath = dtgHelper.GetCell(i, 3);
                path = (TextBox)cellPath.Content;
                if (path.Text != null && path.Text != "")
                {
                    string filePath = path.Text;
                    string fileName = System.IO.Path.GetFileNameWithoutExtension(filePath);
                    string filePathOnly = System.IO.Path.GetDirectoryName(filePath);
                    string extension = System.IO.Path.GetExtension(filePath);
                    string newPath = filePathOnly + "//" + fileName + "_marking" + extension;
                    bits(newPath, dicFile[i + 1]);
                }
            }
            //MessageBox.Show("Upload");
        }

        private void saveUpload(string docType, Int64 transId)
        {
            //ketika upload file akan di catat di database
            DocSolEntities _ent = new Adibrata.BusinessProcess.DocumentSol.Entities.DocSolEntities
            {
                MethodName = "PathInsert",
                ClassName = "UploadProcess"
            };
            _ent.TransId = transId;
            _ent.DocumentType = docType;
            trxFileName = Adibrata.Controller.DocumentSolutionController.DocSolProcess<string>(_ent);//get trxId hasil dari query insert
            dicFile.Add(fileCount, trxFileName); //file yg di upload di simpan di list dictionary


        }
        #region "BITS"
        void bits(string fileName, string trxNo)
        {
            try
            {
                //get file info
                string safeFileName = System.IO.Path.GetFileName(fileName);
                string extension = System.IO.Path.GetExtension(fileName);
                //BITS begin
                //manager = new BitsManager();
                BitsManager manager = new BitsManager();
                manager.EnumJobs(JobOwner.CurrentUser);
                BitsJob newJob = manager.CreateJob("TestUpload1", JobType.Upload); //upload or download
                newJob.AddFile(server + trxNo + extension, fileName); //NewJob.AddFile(namafiletujuan, namafileasal)
                newJob.Resume();
                newJob.OnJobTransferred += new EventHandler<JobNotificationEventArgs>(newJob_OnJobTransferred); //event notification success

                #region "Comment, about BITS info, uncomment with your own risk"
                //foreach (BitsJob job in manager.Jobs.Values)
                //{
                //    job.EnumFiles();
                //    job.ProxySettings.ProxyUsage = ProxyUsage.AutoDetect;
                //    BitsError error = job.Error;

                //    job.NotificationFlags = NotificationFlags.JobTransferred | NotificationFlags.JobErrorOccured;
                //   // job.OnJobTransferred += new EventHandler<JobNotificationEventArgs>(job_OnJobTransferredEvent);
                //    job.OnJobModified += new EventHandler<JobNotificationEventArgs>(job_OnJobModifiedEvent); //never raised with the flags above
                //    //job.OnJobTransferred += new EventHandler<JobNotificationEventArgs>(job_OnJobTransferredEvent); //never raised with the flags above
                //    job.OnJobTransferred += new EventHandler<JobNotificationEventArgs>(newJob_OnJobTransferred);

                //}
                //newJob.OnJobTransferred += new EventHandler<JobNotificationEventArgs>(newJob_OnJobTransferred);
                #endregion
            }
            catch (Exception ex)
            {
                //logging BITS here
                MessageBox.Show(ex.ToString());
            }

        }
        void newJob_OnJobTransferred(object sender, JobNotificationEventArgs e)
        {
            //job selesai di transfer
            int flag = 0; //flag untuk save ke db
            jobCount += 1; //flag perhitungan jumlah job yang selesai
            if (jobCount == fileCount && flag == 0) //jika jumlah job yg selesai sama dengan jumlah file
            {
                //simpan file berdasarkan list dictionary
                for (int i = 0; i < dicFile.Count; i++)
                {
                    WCFEntities oWcf = new WCFEntities();
                  //  oWcf.DicExtString = dicExt[i + 1];// why + 1 ? karena mengambil file berdasarkan key nya, bukan dari index, (key mulai dari 1, index mulai dari 0, nilai awal i adalah 0) jadi harus + 1
                    oWcf.DocTransID = Convert.ToInt64(dicFile[i + 1]);
                    oWcf.FileName = dicFile[i + 1]+ dicExt[i + 1];
                    oWcf.ComputerName = Environment.MachineName;
                    MessageToWCF.UpdateFilePath(oWcf);
                    flag += 1;
                }
                MessageBox.Show("Upload file Success");
            }
        }


        #endregion
        #endregion


    }
}
