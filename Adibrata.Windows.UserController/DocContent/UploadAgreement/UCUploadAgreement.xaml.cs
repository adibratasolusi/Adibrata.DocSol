using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.Configuration;
using Adibrata.Controller;
using Adibrata.Framework.ImageProcessing;
using Adibrata.Framework.Logging;
using Adibrata.Framework.Messaging;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using Saraff.Twain;
using SharpBits.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using WIATest;

namespace Adibrata.Windows.UserController.DocContent.UploadAgreement
{
    /// <summary>
    /// Interaction logic for UCUploadAgreement.xaml
    /// </summary>
    public partial class UCUploadAgreement : UserControl
    {

        #region Global Variable
        Saraff.Twain.Twain32 _twain;
        private bool _isEnable = false;

        List<string> listPathFromTwain = new List<string>();

        object jobTransferredSync = new object();
        Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
        // ServiceReference1.Service1Client objService = new ServiceReference1.Service1Client();
        Dictionary<int, string> dicFile = new Dictionary<int, string>();
        Dictionary<int, string> dicExt = new Dictionary<int, string>();
        string server = AppConfig.Config("BITSServer");
        int jumlahUploadMax; //jumlah maksimal upload, masih hardcode
        #endregion

        #region Properties
        public string DocumentTypeUpload
        {
            get;
            set;
        }
        public class DataItem
        {
            public string PathFile { get; set; }
            public string img { get; set; }
        }
        public string UserLogin { get; set; }
        public string DocumentType { get; set; }
        public long TransId
        {
            get;
            set;
        }
        public string TransCode { get; set; }
        #endregion

        #region InitializeComponent
        public UCUploadAgreement()
        {
            try
            {
                InitializeComponent();

                //jumlahUploadMax = 3;
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = "UCUploadAgreement",
                    NameSpace = "Adibrata.Windows.UserController.DocContent.UploadAgreement",
                    ClassName = "UCUploadAgreement",
                    FunctionName = "UCUploadAgreement",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        public void BindingValueMax()
        {
            try
            {
                dtgUpload.Items.Clear();
                DocSolEntities _ent = new DocSolEntities
                {
                    ClassName = "DocContent",
                    MethodName = "DocContentFiles",
                    UserLogin = this.UserLogin,
                    DocumentType = this.DocumentType
                };
#if DEBUG
                jumlahUploadMax = 5;
#else
                
                jumlahUploadMax = DocumentSolutionController.DocSolProcess<int>(_ent);
#endif


            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = "UCUploadAgreement",
                    NameSpace = "Adibrata.Windows.UserController.DocContent.UploadAgreement",
                    ClassName = "UCUploadAgreement",
                    FunctionName = "UCUploadAgreement",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }
        #endregion

        #region Button Event

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dtgUpload.Items.Count > jumlahUploadMax)
                {
                    MessageBox.Show("Number Of File insufficient, please check the Number of File Configuration");
                }
                else
                {
                    BrowseFile();
                }
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = "UCUploadAgreement",
                    NameSpace = "Adibrata.Windows.UserController.DocContent.UploadAgreement",
                    ClassName = "UCUploadAgreement",
                    FunctionName = "btnBrowse_Click",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
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
        private void btnScan_ClickLAMA(object sender, RoutedEventArgs e)
        {


            if (dtgUpload.Items.Count > jumlahUploadMax)
            {
                MessageBox.Show("Number Of File insufficient, please check the Number of File Configuration");
            }
            else
            {
                scanFile();
            }

        }
        private void btnScan_Click(object sender, RoutedEventArgs e)
        {
            _twain =  new Saraff.Twain.Twain32();
            try
            {
                if (Environment.OSVersion.Platform == PlatformID.Unix)
                {
                    SelectSourceForm _dlg = new SelectSourceForm { Twain = this._twain };
                    if (_dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        this._twain.SetDefaultSource(_dlg.SourceIndex);
                        this._twain.SourceIndex = _dlg.SourceIndex;
                    }
                }
                else
                {
                    this._twain.CloseDataSource();
                    this._twain.SelectSource();
                }
                listPathFromTwain.Clear();
                _twain.TwainStateChanged += _twain_TwainStateChanged;
                _twain.AcquireCompleted += _twain_AcquireCompleted;
                this._twain.OpenDSM();
                this._twain.Acquire();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0}\n\n{1}", ex.Message, ex.StackTrace), "SAMPLE1");
            }




        }
        #endregion

        Int64 docTransId;
        string docTransBinaryId;


        #region Method

        private void _twain_AcquireCompleted(object sender, EventArgs e)
        {
            try
            {

                if (this._twain.ImageCount > 0)
                {
                    for (int i = 0; i < this._twain.ImageCount; i++)
                    {
                        string path = "C:\\Temp\\Temp" + "_" + i.ToString() + DateTime.Now.ToString("yyyy-MM-dd HHmmss") + ".jpg";
                        this._twain.GetImage(i).Save(path, ImageFormat.Jpeg);
                        listPathFromTwain.Add(path);


                    }
                    for (int i = 0; i < listPathFromTwain.Count; i++)
                    {
                        dtgUpload.Items.Add(new DataItem { PathFile = listPathFromTwain[i], img = listPathFromTwain[i] });
                        dtgUpload.Items.Refresh();
                    }
                }
                this._twain.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SAMPLE1");
            }
        }

        private void _twain_TwainStateChanged(object sender, Twain32.TwainStateEventArgs e)
        {
            try
            {
                if ((e.TwainState & Twain32.TwainStateFlag.DSEnabled) == 0 && this._isEnable)
                {
                    this._isEnable = false;
                    // <<< scaning finished (or closed)
                }
                this._isEnable = (e.TwainState & Twain32.TwainStateFlag.DSEnabled) != 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SAMPLE1");
            }
        }

        public void CheckAndUpload(DocSolEntities _ent)
        {
            try
            {
                if (dtgUpload.Items.Count > jumlahUploadMax)
                {

                    MessageBox.Show("Number Of File insufficient, please check the Number of File Configuration");
                }
                else
                {
                    UploadFile(_ent);
                }
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = "UCUploadAgreement",
                    NameSpace = "Adibrata.Windows.UserController.DocContent.UploadAgreement",
                    ClassName = "UCUploadAgreement",
                    FunctionName = "CheckAndUpload",
                    ExceptionNumber = 1,
                    EventSource = "UCUploadAgreement",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        //public void CheckAndUpload(DataTable dtContent, DocSolEntities _ent)
        //{
        //    try
        //    {
        //        if (dtgUpload.Items.Count > jumlahUploadMax)
        //        {

        //            MessageBox.Show("Number Of File insufficient, please check the Number of File Configuration");
        //        }
        //        else
        //        {
        //            UploadFile(dtContent);
        //        }
        //    }
        //    catch (Exception _exp)
        //    {
        //        ErrorLogEntities _errent = new ErrorLogEntities
        //        {
        //            UserLogin = "UCUploadAgreement",
        //            NameSpace = "Adibrata.Windows.UserController.DocContent.UploadAgreement",
        //            ClassName = "UCUploadAgreement",
        //            FunctionName = "CheckAndUpload",
        //            ExceptionNumber = 1,
        //            EventSource = "UCUploadAgreement",
        //            ExceptionObject = _exp,
        //            EventID = 200, // 1 Untuk Framework 
        //            ExceptionDescription = _exp.Message
        //        };
        //        ErrorLog.WriteEventLog(_errent);
        //    }
        //}
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
                        dtgUpload.Items.Add(new DataItem { PathFile = filename, img = picture });

                    }
                    dtgUpload.Items.Refresh();
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

        List<string> fileList = new List<string>();
        private void UploadFileLAMA(DataTable dtContent)
        {

            DataGridHelper dtgHelper = new DataGridHelper();
            List<string> listPath = new List<string>();
            try
            {
                dtgHelper.dtg = dtgUpload;

                List<KeyValuePair<Int64, string>> dictDocTransBinary = new List<KeyValuePair<Int64, string>>();
                #region CEK DATAGRID
                for (int i = 0; i < dtgUpload.Items.Count; i++)
                {
                    DataGridCell cellPath = dtgHelper.GetCell(i, 1);
                    TextBlock path = (TextBlock)cellPath.Content;
                    listPath.Add(path.Text);
                }
                if (listPath.Count != 0)
                {
                    Int64 docTransId = SaveUploadToDocTrans(TransId, DocumentType);
                    //place for doc content
                    saveUploadToDocTransContent(docTransId, dtContent);
                    //

                    for (int i = 0; i < listPath.Count; i++)
                    {
                        Int64 docTransBinaryId = Convert.ToInt64(saveUploadToDocTransBinary(docTransId, listPath[i]));

                        dictDocTransBinary.Add(new KeyValuePair<Int64, string>(docTransBinaryId, listPath[i]));
                    }
                    for (int i = 0; i < dictDocTransBinary.Count; i++)
                    {
                        string a = bits(listPath[i], dictDocTransBinary[i].Key.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("File Not Found");
                }
                #endregion
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = "UCUploadAgreement",
                    NameSpace = "Adibrata.Windows.UserController.DocContent.UploadAgreement",
                    ClassName = "UCUploadAgreement",
                    FunctionName = "UploadFile",
                    ExceptionNumber = 1,
                    EventSource = "UCUploadAgreement",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }
        private void UploadFileSEBELUMREVISI(DocSolEntities _ent)
        {

            DataGridHelper dtgHelper = new DataGridHelper();
            List<string> listPath = new List<string>();
            try
            {
                dtgHelper.dtg = dtgUpload;

                List<KeyValuePair<Int64, string>> listDocTransBinary = new List<KeyValuePair<Int64, string>>();
                #region CEK DATAGRID
                for (int i = 0; i < dtgUpload.Items.Count; i++)
                {
                    DataGridCell cellPath = dtgHelper.GetCell(i, 1);
                    TextBlock path = (TextBlock)cellPath.Content;
                    listPath.Add(path.Text);
                }
                if (listPath.Count != 0)
                {

                    _ent.MethodName = "DocUpload";
                    _ent.ClassName = "UploadProcess";
                    _ent.TransCode = this.TransCode;
                    _ent.DocumentType = DocumentType;
                    _ent.ListPath = listPath;

                    listDocTransBinary = Adibrata.Controller.DocumentSolutionController.DocSolProcess<List<KeyValuePair<Int64, string>>>(_ent);

                    for (int i = 0; i < listDocTransBinary.Count; i++)
                    {
                        string a = bits(listPath[i], listDocTransBinary[i].Key.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("File Not Found");
                }
                #endregion
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = "UCUploadAgreement",
                    NameSpace = "Adibrata.Windows.UserController.DocContent.UploadAgreement",
                    ClassName = "UCUploadAgreement",
                    FunctionName = "UploadFile",
                    ExceptionNumber = 1,
                    EventSource = "UCUploadAgreement",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }
        private void UploadFile(DocSolEntities _ent)
        {

            DataGridHelper dtgHelper = new DataGridHelper();
            List<string> listPath = new List<string>();
            try
            {
                dtgHelper.dtg = dtgUpload;

                List<KeyValuePair<Int64, string>> listDocTransBinary = new List<KeyValuePair<Int64, string>>();
                #region singlefile langsung jadi image

                if (dtgUpload.Items.Count == 1)
                {
                    DataGridCell cellPath = dtgHelper.GetCell(1, 1);
                    TextBlock path = (TextBlock)cellPath.Content;
                    listPath.Add(path.Text);

                    if (listPath.Count != 0)
                    {

                        _ent.MethodName = "DocUpload";
                        _ent.ClassName = "UploadProcess";
                        _ent.TransId = TransId;
                        _ent.DocumentType = DocumentType;
                        _ent.ListPath = listPath;

                        listDocTransBinary = Adibrata.Controller.DocumentSolutionController.DocSolProcess<List<KeyValuePair<Int64, string>>>(_ent);

                        for (int i = 0; i < listDocTransBinary.Count; i++)
                        {
                            string a = bits(listPath[i], listDocTransBinary[i].Key.ToString());
                        }
                    }
                    else
                    {
                        MessageBox.Show("File Not Found");
                    }
                }

                #endregion
                #region MultiFile jadi satu file pdf
                if (dtgUpload.Items.Count > 1)
                {
                    #region CEK DATAGRID
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

                        doc.Save("C:\\Temp\\FilePDF.pdf");
                        doc.Close();

                        MessageBox.Show("Convert Succeed");



                    }

                    listPath.Add("C:\\Temp\\FilePDF.pdf");
                    if (listPath.Count != 0)
                    {

                        _ent.MethodName = "DocUpload";
                        _ent.ClassName = "UploadProcess";
                        _ent.TransId = TransId;
                        _ent.DocumentType = DocumentType;
                        _ent.ListPath = listPath;

                        listDocTransBinary = Adibrata.Controller.DocumentSolutionController.DocSolProcess<List<KeyValuePair<Int64, string>>>(_ent);

                        for (int i = 0; i < listDocTransBinary.Count; i++)
                        {
                            string a = bits(listPath[i], listDocTransBinary[i].Key.ToString());
                        }
                    }
                    else
                    {
                        MessageBox.Show("File Not Found");
                    }
                    #endregion
                }
                #endregion


            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = "UCUploadAgreement",
                    NameSpace = "Adibrata.Windows.UserController.DocContent.UploadAgreement",
                    ClassName = "UCUploadAgreement",
                    FunctionName = "UploadFile",
                    ExceptionNumber = 1,
                    EventSource = "UCUploadAgreement",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }
        private Int64 SaveUploadToDocTrans(Int64 transId, string docType)
        {

            try
            {
                DocSolEntities _ent = new Adibrata.BusinessProcess.DocumentSol.Entities.DocSolEntities
                {
                    MethodName = "DocTransInsert",
                    ClassName = "UploadProcess"
                };
                _ent.TransId = transId;
                _ent.DocumentType = docType;
                docTransId = Convert.ToInt64(Adibrata.Controller.DocumentSolutionController.DocSolProcess<string>(_ent));//get trxId hasil dari query insert

            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = "UCUploadAgreement",
                    NameSpace = "Adibrata.Windows.UserController.DocContent.UploadAgreement",
                    ClassName = "UCUploadAgreement",
                    FunctionName = "CheckAndUpload",
                    ExceptionNumber = 1,
                    EventSource = "UCUploadAgreement",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
            return docTransId;
        }

        private string saveUploadToDocTransBinary(Int64 docTransId, string path)
        {
            byte[] byteFile = File.ReadAllBytes(path);

            try
            {
                DocSolEntities _ent = new Adibrata.BusinessProcess.DocumentSol.Entities.DocSolEntities
                {
                    MethodName = "DocTransBinaryInsert",
                    ClassName = "UploadProcess"
                };


                _ent.Ext = Path.GetExtension(path).ToLower();
                _ent.DocTransId = docTransId;
                _ent.SizeFileBytes = byteFile.Length;
                _ent.ComputerName = Environment.MachineName;
                _ent.DateCreated = File.GetCreationTime(path);
                if (path == ".jpg" || path == ".jpeg" || path == ".png")
                {

                    System.Drawing.Image imgFile = ImageConverterProcess.byteArrayToImage(byteFile);

                    _ent.Pixel = imgFile.Width + "x" + imgFile.Height;
                    _ent.DPI = imgFile.HorizontalResolution.ToString();

                }
                else
                {

                    _ent.Pixel = "-";
                    _ent.DPI = "-";
                }


                _ent.FileName = Path.GetFileName(path);

                docTransBinaryId = Adibrata.Controller.DocumentSolutionController.DocSolProcess<string>(_ent);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = "UCUploadAgreement",
                    NameSpace = "Adibrata.Windows.UserController.DocContent.UploadAgreement",
                    ClassName = "UCUploadAgreement",
                    FunctionName = "saveUploadToDocTransBinary",
                    ExceptionNumber = 1,
                    EventSource = "UCUploadAgreement",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
            return docTransBinaryId;
        }

        private void saveUploadToDocTransContent(Int64 docTransId, DataTable dtContent)
        {
            for (int i = 0; i < dtContent.Rows.Count; i++)
            {

                DocSolEntities _ent = new Adibrata.BusinessProcess.DocumentSol.Entities.DocSolEntities
                {
                    MethodName = "DocTransContentInsert",
                    ClassName = "UploadProcess"
                };

                _ent.DocTypeCode = dtContent.Rows[i]["Field1"].ToString();
                _ent.DocTransId = docTransId;
                _ent.ContentName = dtContent.Rows[i]["Field2"].ToString();
                _ent.ContentValue = "";
                _ent.ContentValueNumeric = 0;
                _ent.ContentValueDate = DateTime.Now;
                if (dtContent.Rows[i]["DataType"].ToString().ToLower().Trim() == "string")
                {

                    _ent.ContentValue = dtContent.Rows[i]["EntryValue"].ToString();
                }
                if (dtContent.Rows[i]["DataType"].ToString().ToLower().Trim() == "date")
                {
                    _ent.ContentValue = dtContent.Rows[i]["EntryValue"].ToString();
                    _ent.ContentValueDate = Convert.ToDateTime(dtContent.Rows[i]["EntryValueDate"].ToString());

                }
                if (dtContent.Rows[i]["DataType"].ToString().ToLower().Trim() == "number")
                {

                    _ent.ContentValueNumeric = Convert.ToDecimal(dtContent.Rows[i]["EntryValueNumber"].ToString());
                }

                Adibrata.Controller.DocumentSolutionController.DocSolProcess<string>(_ent);
            }
        }


        private string bits(string path, string jobName)
        {
            //get file info

            string fileName = Path.GetFileNameWithoutExtension(path);
            string extension = Path.GetExtension(path);

            //BITS begin
            BitsManager manager = new BitsManager();
            manager.EnumJobs(JobOwner.CurrentUser);

            StringBuilder fileFrom = new StringBuilder();
            fileFrom.Append(server);
            fileFrom.Append(jobName);
            fileFrom.Append("_");
            fileFrom.Append(fileName);
            fileFrom.Append(extension);

            BitsJob newJob = manager.CreateJob(jobName, JobType.Upload); //upload or download
            newJob.AddFile(fileFrom.ToString(), path); //NewJob.AddFile(namafiletujuan, namafileasal)
            newJob.Description = fileFrom.ToString();
            newJob.Resume();
            manager.OnJobTransferred += manager_OnJobTransferred;
            // newJob.OnJobTransferred += new EventHandler<JobNotificationEventArgs>(newJob_OnJobTransferred); //event notification success

            return jobName;
        }

        void manager_OnJobTransferred(object sender, NotificationEventArgs e)
        {
            lock (jobTransferredSync)
            {
                BitsJob job = e.Job;
                job.Complete();
                WCFEntities oWcf = new WCFEntities();
                oWcf.DocTransBinaryID = Convert.ToInt64(job.DisplayName.Trim());
                oWcf.FileName = Path.GetFileName(job.Description);
                MessageToWCF.UpdateFilePath(oWcf);

                MessageBox.Show(job.Description + " Upload Succeed");
                //    MessageBox.Show(job.JobId.ToString() + "----------" + job.DisplayName.ToString() + "--------" + job.Description.ToString());

            }
        }

        //private void FileUpload(DataTable dtContent)
        //{

        //    //set flag for save to database
        //    try
        //    {
        //        #region "reset flag"
        //        fileCount = 0;
        //        jobCount = 0;
        //        dicFile.Clear();
        //        dicExt.Clear();
        //        #endregion
        //        DataGridHelper dtgHelper = new DataGridHelper();
        //        dtgHelper.dtg = dtgUpload;
        //        int flagContent = 0;
        //        for (int i = 0; i < dtgUpload.Items.Count; i++)
        //        {
        //            var path = new TextBlock();
        //            var docType = new TextBlock();

        //            DataGridCell cellPath = dtgHelper.GetCell(i, 1);
        //            path = (TextBlock)cellPath.Content;
        //            if (path.Text != null && path.Text != "")
        //            {
        //                string newPath = WaterMarkProcess.SetWatermark(path.Text);
        //                string extension = System.IO.Path.GetExtension(newPath);
        //                fileCount += 1; //set jumlah file
        //                saveUpload(DocumentTypeUpload, TransId, path.Text);
        //                if (flagContent != 0)
        //                {
        //                    DataTable test = dtContent;
        //                }
        //                flagContent += 1;
        //                dicExt.Add(fileCount, extension);
        //            }

        //        }
        //        for (int i = 0; i < dtgUpload.Items.Count; i++)
        //        {
        //            var path = new TextBlock();
        //            DataGridCell cellPath = dtgHelper.GetCell(i, 1);
        //            path = (TextBlock)cellPath.Content;
        //            if (path.Text != null && path.Text != "")
        //            {
        //                string filePath = path.Text;
        //                string fileName = System.IO.Path.GetFileNameWithoutExtension(filePath);
        //                string filePathOnly = System.IO.Path.GetDirectoryName(filePath);
        //                string extension = System.IO.Path.GetExtension(filePath);
        //                string newPath = filePathOnly + "//" + fileName + "_marking" + extension;
        //                bits(newPath, dicFile[i + 1]);
        //            }
        //        }
        //    }
        //    catch (Exception _exp)
        //    {
        //        ErrorLogEntities _errent = new ErrorLogEntities
        //        {
        //            UserLogin = "UCUploadAgreement",
        //            NameSpace = "Adibrata.Windows.UserController.DocContent.UploadAgreement",
        //            ClassName = "UCUploadAgreement",
        //            FunctionName = "FileUpload",
        //            ExceptionNumber = 1,
        //            EventSource = "UCUploadAgreement",
        //            ExceptionObject = _exp,
        //            EventID = 200, // 1 Untuk Framework 
        //            ExceptionDescription = _exp.Message
        //        };
        //        ErrorLog.WriteEventLog(_errent);
        //    }
        //    //MessageBox.Show("Upload");
        //}


        void scanFile()
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
                string pathFile = path + DateTime.Now.ToString("yyyy-MM-dd HHmmss") + ".jpeg";
                foreach (System.Drawing.Image img in images)
                {

                    img.Save(pathFile, ImageFormat.Jpeg);
                }

                PdfDocument doc = new PdfDocument();
                doc.Pages.Add(new PdfPage());
                XGraphics xgr = XGraphics.FromPdfPage(doc.Pages[0]);
                XImage ximg = XImage.FromFile(pathFile);

                string fileName = System.IO.Path.GetFileNameWithoutExtension(pathFile);
                xgr.DrawImage(ximg, 0, 0);

                doc.Save(pathFile + "\\" + fileName + ".pdf");
                doc.Close();
                xgr.Dispose();

                File.Delete(pathFile);

                dtgUpload.Items.Add(new DataItem { PathFile = pathFile, img = pathFile });
                dtgUpload.Items.Refresh();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void btnDS_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Environment.OSVersion.Platform == PlatformID.Unix)
                {
                    SelectSourceForm _dlg = new SelectSourceForm { Twain = this._twain };
                    if (_dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        this._twain.SetDefaultSource(_dlg.SourceIndex);
                        this._twain.SourceIndex = _dlg.SourceIndex;
                    }
                }
                else
                {
                    this._twain.CloseDataSource();
                    this._twain.SelectSource();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SAMPLE1");
            }
        }

        #region comment
        //private void saveUpload(string docType, string transId, string path)
        //{
        //    //ketika upload file akan di catat di database
        //    try
        //    {
        //       DocSolEntities _ent = new Adibrata.BusinessProcess.DocumentSol.Entities.DocSolEntities
        //        {
        //            MethodName = "PathInsert",
        //            ClassName = "UploadProcess"
        //        };
        //        _ent.TransId = transId;
        //        _ent.DocumentType = docType;
        //        int docTransId = Convert.ToInt16(Adibrata.Controller.DocumentSolutionController.DocSolProcess<string>(_ent));//get trxId hasil dari query insert
        //       //trxFileName

        //       _ent = new Adibrata.BusinessProcess.DocumentSol.Entities.DocSolEntities
        //        {
        //            MethodName = "DocTransBinaryInsert",
        //            ClassName = "UploadProcess"
        //        };

        //        byte[] imgByte = File.ReadAllBytes(path);
        //        System.Drawing.Image imgFile = ImageConverterProcess.byteArrayToImage(imgByte);
        //        _ent.DocTransId = docTransId;
        //        _ent.FileName= Path.GetFileName(path) ;
        //        _ent.DateCreated= DataImageProcess.getImageDtCreate(imgFile);
        //        _ent.SizeFileBytes= imgByte.Length;
        //        _ent.Pixel=imgFile.Width + "x"+imgFile.Height;
        //        _ent.ComputerName= Environment.MachineName;
        //        _ent.DPI=imgFile.HorizontalResolution.ToString();

        //        trxFileName = Adibrata.Controller.DocumentSolutionController.DocSolProcess<string>(_ent);
        //        dicFile.Add(fileCount, trxFileName); //file yg di upload di simpan di list dictionary
        //    }
        //    catch (Exception _exp)
        //    {
        //        ErrorLogEntities _errent = new ErrorLogEntities
        //        {
        //            UserLogin = "UCUploadAgreement",
        //            NameSpace = "Adibrata.Windows.UserController.DocContent.UploadAgreement",
        //            ClassName = "UCUploadAgreement",
        //            FunctionName = "saveUpload",
        //            ExceptionNumber = 1,
        //            EventSource = "UCUploadAgreement",
        //            ExceptionObject = _exp,
        //            EventID = 200, // 1 Untuk Framework 
        //            ExceptionDescription = _exp.Message
        //        };
        //        ErrorLog.WriteEventLog(_errent);
        //    }

        //}
        //#region "BITS"
        //private void bits1(string fileName, string trxNo)
        //{
        //    try
        //    {
        //        //get file info
        //        string safeFileName = System.IO.Path.GetFileName(fileName);
        //        string extension = System.IO.Path.GetExtension(fileName);
        //        //BITS begin
        //        //manager = new BitsManager();
        //        BitsManager manager = new BitsManager();
        //        manager.EnumJobs(JobOwner.CurrentUser);
        //        BitsJob newJob = manager.CreateJob("TestUpload1", JobType.Upload); //upload or download
        //        newJob.AddFile(server + trxNo + extension, fileName); //NewJob.AddFile(namafiletujuan, namafileasal)
        //        newJob.Resume();
        //        newJob.OnJobTransferred += new EventHandler<JobNotificationEventArgs>(newJob_OnJobTransferred); //event notification success

        //        #region "Comment, about BITS info, uncomment with your own risk"
        //        //foreach (BitsJob job in manager.Jobs.Values)
        //        //{
        //        //    job.EnumFiles();
        //        //    job.ProxySettings.ProxyUsage = ProxyUsage.AutoDetect;
        //        //    BitsError error = job.Error;

        //        //    job.NotificationFlags = NotificationFlags.JobTransferred | NotificationFlags.JobErrorOccured;
        //        //   // job.OnJobTransferred += new EventHandler<JobNotificationEventArgs>(job_OnJobTransferredEvent);
        //        //    job.OnJobModified += new EventHandler<JobNotificationEventArgs>(job_OnJobModifiedEvent); //never raised with the flags above
        //        //    //job.OnJobTransferred += new EventHandler<JobNotificationEventArgs>(job_OnJobTransferredEvent); //never raised with the flags above
        //        //    job.OnJobTransferred += new EventHandler<JobNotificationEventArgs>(newJob_OnJobTransferred);

        //        //}
        //        //newJob.OnJobTransferred += new EventHandler<JobNotificationEventArgs>(newJob_OnJobTransferred);
        //        #endregion
        //    }
        //    catch (Exception _exp)
        //    {
        //        //logging BITS here
        //        ErrorLogEntities _errent = new ErrorLogEntities
        //        {
        //            UserLogin = "UCUploadAgreement",
        //            NameSpace = "Adibrata.Windows.UserController.DocContent.UploadAgreement",
        //            ClassName = "UCUploadAgreement",
        //            FunctionName = "bits",
        //            ExceptionNumber = 1,
        //            EventSource = "UCUploadAgreement",
        //            ExceptionObject = _exp,
        //            EventID = 200, // 1 Untuk Framework 
        //            ExceptionDescription = _exp.Message
        //        };
        //        ErrorLog.WriteEventLog(_errent);
        //    }

        //}
        //void newJob_OnJobTransferred1(object sender, JobNotificationEventArgs e)
        //{
        //    //job selesai di transfer
        //    int flag = 0; //flag untuk save ke db
        //    jobCount += 1; //flag perhitungan jumlah job yang selesai
        //    try
        //    {

        //        if (jobCount == fileCount && flag == 0) //jika jumlah job yg selesai sama dengan jumlah file
        //        {
        //            //simpan file berdasarkan list dictionary
        //            for (int i = 0; i < dicFile.Count; i++)
        //            {
        //                WCFEntities oWcf = new WCFEntities();
        //                //  oWcf.DicExtString = dicExt[i + 1];// why + 1 ? karena mengambil file berdasarkan key nya, bukan dari index, (key mulai dari 1, index mulai dari 0, nilai awal i adalah 0) jadi harus + 1
        //                oWcf.DocTransID = Convert.ToInt64(dicFile[i + 1]);
        //                oWcf.FileName = dicFile[i + 1] + dicExt[i + 1];
        //                oWcf.ComputerName = Environment.MachineName;
        //                //MessageToWCF.UpdateFilePath(oWcf);
        //                flag += 1;
        //            }
        //            MessageBox.Show("Upload file Success");
        //        }
        //    }
        //    catch (Exception _exp)
        //    {
        //        ErrorLogEntities _errent = new ErrorLogEntities
        //        {
        //            UserLogin = "UCUploadAgreement",
        //            NameSpace = "Adibrata.Windows.UserController.DocContent.UploadAgreement",
        //            ClassName = "UCUploadAgreement",
        //            FunctionName = "newJob_OnJobTransferred",
        //            ExceptionNumber = 1,
        //            EventSource = "UCUploadAgreement",
        //            ExceptionObject = _exp,
        //            EventID = 200, // 1 Untuk Framework 
        //            ExceptionDescription = _exp.Message
        //        };
        //        ErrorLog.WriteEventLog(_errent);
        //    }
        //}

        //#endregion
        #endregion


        #endregion


    }
}
