using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using SharpBits.Base;
using System.Configuration;
using System.Windows.Controls.Primitives;
//using Adibrata.Demo.FileTransfer.ServiceReference1;
using System.IO;
using Adibrata.Framework.Messaging;

namespace Adibrata.Demo.FileTransfer
{
    /// <summary>
    /// Interaction logic for AgrmntUpload.xaml
    /// </summary>
    public partial class AgrmntUpload : Page
    {
        string currentAgrmntNo;

        string trxFileName;
        int jobCount = 0;
        int fileCount = 0;
       // ServiceReference1.Service1Client objService = new ServiceReference1.Service1Client();
        string ConString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        Dictionary<int, string> dicFile = new Dictionary<int, string>();
        Dictionary<int, string> dicExt = new Dictionary<int, string>();
        string server;
        public AgrmntUpload( string agrmntNo = "0")
        {

            currentAgrmntNo = agrmntNo;
            InitializeComponent();
            FillDataGrid();
            txtSvr.Text = "file://PC195/bits/";
            server = "file://PC195/bits/";
        }
        Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            
            //get server and agreement no
            string svr = txtSvr.Text;
            string agrmntNo = txtAgrmntNo.Text;

            //set flag for save to database
            #region "reset flag"
            fileCount = 0;
            jobCount = 0;
            dicFile.Clear();
            dicExt.Clear();
            #endregion

            for (int i = 0; i < dgUpload.Items.Count; i++)
            {
                var path = new TextBox();
                var docType = new TextBlock();
                DataGridCell cellDocType = GetCell(i, 1);
                DataGridCell cell = GetCell(i, 3);
                path = (TextBox)cell.Content;
                docType = (TextBlock)cellDocType.Content;
                if (path.Text != null && path.Text != "")
                {
                    string extension = System.IO.Path.GetExtension(path.Text);
                    fileCount += 1; //set jumlah file
                    saveUpload(docType.Text);
                    dicExt.Add(fileCount, extension);
                }

            }
            for (int i = 0; i < dgUpload.Items.Count; i++)
            {
                var path = new TextBox();
                DataGridCell cell = GetCell(i, 3);
                path = (TextBox)cell.Content;
                if (path.Text != null && path.Text != "")
                {
                    string filePath = path.Text;
                    bits(filePath, dicFile[i+1]);
                }
            }

        }

        void newJob_OnJobTransferred(object sender, JobNotificationEventArgs e)
        {
            //job selesai di transfer
            int flag = 0; //flag untuk save ke db
            jobCount+=1; //flag perhitungan jumlah job yang selesai
            if (jobCount == fileCount && flag == 0) //jika jumlah job yg selesai sama dengan jumlah file
            {
                //simpan file berdasarkan list dictionary
                for (int i = 0; i < dicFile.Count; i++) 
                {
                    WCFEntities oWcf = new WCFEntities();
                    oWcf.DicExtString = dicExt[i + 1];// why + 1 ? karena mengambil file berdasarkan key nya, bukan dari indeks, (key mulai dari 1, index mulai dari 0, nilai awal i adalah 0) jadi harus + 1
                    oWcf.DicFileString = dicFile[i + 1];
                    MessageToWCF.UpdateFilePath(oWcf);
                    flag += 1;
                    //MessageBox.Show("panggil service disini" + dicFile[i+1].ToString());
                }
                MessageBox.Show("Upload file Success");
            }
        }

        //void updateFile(string dicFileString,string dicExtString)
        //{
        //    PathDetails pathInfo = new PathDetails();
        //    pathInfo.Ext=dicExtString;
        //    pathInfo.FileName = dicFileString;
        //    objService.UpdatePathDetails(pathInfo);
        //}
        private void FillDataGrid()
        {


            string CmdString = string.Empty;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                CmdString = "SELECT DOC_MASTER_ID as No, DOC_TYPE as DocumentType FROM DOC_MASTER with (Nolock)";
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("DOC_MASTER");
                sda.Fill(dt);

                dgUpload.ItemsSource = dt.DefaultView;
                dgUpload.CanUserSortColumns = false;
            }


            using (SqlConnection con = new SqlConnection(ConString))
            {
                CmdString = "SELECT CUST_NAME as CustName FROM AGREEMENT  with (Nolock) where AGREEMENT_NO = '" + currentAgrmntNo + "'";
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Agrmnt");
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txtCustName.Text = dt.Rows[0]["CustName"].ToString();
                }
            }

            txtAgrmntNo.Text = currentAgrmntNo;
        }

        private void saveUpload(string docType)
        {
            //ketika upload file akan di catat di database
            
            string CmdString = string.Empty;

            using (SqlConnection con = new SqlConnection(ConString))
            {
                CmdString = "INSERT INTO PATH(AGREEMENT_NO,DOC_TYPE) output inserted.PATH_ID VALUES ('"+currentAgrmntNo+"','"+docType+"')";
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Path");
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    trxFileName = dt.Rows[0]["PATH_ID"].ToString(); //get trxId hasil dari query insert
                    dicFile.Add(fileCount, trxFileName); //file yg di upload di simpan di list dictionary
                    
                }
            }

        }


        void Browse(object sender, RoutedEventArgs e)
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
                int i = dgUpload.SelectedIndex;
                var txtBox = new TextBox();

                txtBox.Text = filename;

                // Open document
                string asd = dgUpload.CurrentItem.ToString();
                DataGridCell cell = GetCell(i, 3);
                cell.Content = txtBox;
                //MessageBox.Show(tb.Text);
                var img = new Image { Width = 100, Height = 100 };
                var bitmapImage = new BitmapImage(new Uri(filename));

                img.Source = bitmapImage;
                DataGridCell image = GetCell(i, 4);
                image.Content = img;

            }

        }

        #region "GET GRID"
        public DataGridCell GetCell(int row, int column)
        {
            DataGridRow rowContainer = GetRow(row);

            if (rowContainer != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(rowContainer);

                DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                if (cell == null)
                {
                    dgUpload.ScrollIntoView(rowContainer, dgUpload.Columns[column]);
                    cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                }
                return cell;
            }
            return null;
        }

        public DataGridRow GetRow(int index)
        {
            DataGridRow row = (DataGridRow)dgUpload.ItemContainerGenerator.ContainerFromIndex(index);
            if (row == null)
            {
                dgUpload.UpdateLayout();
                dgUpload.ScrollIntoView(dgUpload.Items[index]);
                row = (DataGridRow)dgUpload.ItemContainerGenerator.ContainerFromIndex(index);
            }
            return row;
        }

        public static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }
        #endregion
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
                BitsJob newJob = manager.CreateJob("TestUpload1",JobType.Upload); //upload or download
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

        #endregion
    }
}
