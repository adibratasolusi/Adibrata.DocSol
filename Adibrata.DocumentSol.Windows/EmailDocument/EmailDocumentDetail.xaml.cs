using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Controller;
using Adibrata.Framework.Logging;
using Adibrata.Windows.UserController;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Adibrata.DocumentSol.Windows.EmailDocument
{
    /// <summary>
    /// Interaction logic for EmailDocumentDetail.xaml
    /// </summary>
    public partial class EmailDocumentDetail : Page
    {
        SessionEntities SessionProperty = new SessionEntities();

        #region Global Variable

        object jobTransferredSync = new object();
        Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();

        // ServiceReference1.Service1Client objService = new ServiceReference1.Service1Client();
        Dictionary<int, string> dicFile = new Dictionary<int, string>();
        Dictionary<int, string> dicExt = new Dictionary<int, string>();
       
        Byte[] _imgbin;
        String _filename = "";

        //int jumlahUploadMax; //jumlah maksimal upload, masih hardcode
        #endregion
        public class DataItem
        {
            public string PathFile { get; set; }
            public string img { get; set; }
            public bool save { get; set; }
        }
        public bool RestoreDirectory { get; set; }

        string tmpPath = Adibrata.Configuration.AppConfig.Config("ReportOutputPath");
        public EmailDocumentDetail(SessionEntities _session)
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

                txtDocTransId.Text = _ent.DocTransCode;

                BindContent();
                BindBinary();

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
                    EventSource = "UploadInquiry",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);

            }
        }

        private void BindContent()
        {
            try
            {
                DocSolEntities _ent = new DocSolEntities();
                DataTable _dt = new DataTable();
                _ent.ClassName = "UploadProcess";
                _ent.MethodName = "DocTransContentDetail";
                //_ent.DocTransId = SessionProperty.ReffKey;
                _ent.UserName = SessionProperty.UserName;
                _ent.DocTransId = Convert.ToInt64(SessionProperty.ReffKey);
                _dt = DocumentSolutionController.DocSolProcess<DataTable>(_ent);

                if (_dt.Rows.Count > 0)
                {

                    foreach (DataRow _row in _dt.Rows)
                    {
                        TextBlock DocContentDescription = new TextBlock();
                        DocContentDescription.Name = "lbl" + _row["ContentName"].ToString().Replace(" ", "");
                        DocContentDescription.Text = _row["ContentName"].ToString();
                        DocContentDescription.HorizontalAlignment = HorizontalAlignment.Stretch;

                        DocContentDescription.Width = 200;

                        DocContentDescription.SetResourceReference(TextBlock.StyleProperty, "TextBlockStyle");
                        spContent.Children.Remove(DocContentDescription);
                        spContent.Children.Add(DocContentDescription);

                        spContent.RegisterName(DocContentDescription.Name, DocContentDescription);
                        string _datatype = _row["DataType"].ToString().ToUpper();

                        switch (_row["DataType"].ToString().ToUpper())
                        {

                            case "DATE":
                                {
                                    TextBlock txtInput = new TextBlock();
                                    txtInput.Name = "txt" + _row["ContentName"].ToString().Replace(" ", "");
                                    txtInput.Text = _row["value"].ToString().Trim();
                                    txtInput.Width = 400;
                                    txtInput.SetResourceReference(TextBlock.StyleProperty, "TextBlockStyle");
                                    spValue.Children.Add(txtInput);
                                    spValue.RegisterName(txtInput.Name, txtInput);
                                }
                                break;
                            case "NUMBER":
                                {
                                    TextBlock txtInput = new TextBlock();
                                    txtInput.Name = "txt" + _row["ContentName"].ToString().Replace(" ", "");
                                    txtInput.Text = Format.NumberFormatting(_row["value"].ToString().Trim());
                                    //txtInput.Text = "0";
                                    txtInput.Width = 400;
                                    txtInput.SetResourceReference(TextBlock.StyleProperty, "TextBlockStyle");
                                    spValue.Children.Add(txtInput);
                                    spValue.RegisterName(txtInput.Name, txtInput);
                                }
                                break;
                            default:
                                {
                                    TextBlock txtInput = new TextBlock();
                                    txtInput.Name = "txt" + _row["ContentName"].ToString().Replace(" ", "");
                                    txtInput.Text = _row["value"].ToString().Trim();
                                    //txtInput.Text = "0";
                                    txtInput.Width = 400;
                                    txtInput.SetResourceReference(TextBlock.StyleProperty, "TextBlockStyle");
                                    //txtInput.SetResourceReference(TextBlock.StyleProperty, "textStyle");
                                    spValue.Children.Add(txtInput);

                                    spValue.RegisterName(txtInput.Name, txtInput);
                                }
                                break;
                        }
                    }
                }
                //dtgContent.ItemsSource = _dt.DefaultView;
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
                    EventSource = "UploadInquiry",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        private void BindBinary()
        {
            try
            {
                DocSolEntities _ent = new DocSolEntities();
                DataTable _dt = new DataTable();
                _ent.ClassName = "UploadProcess";
                _ent.MethodName = "DocTransInquiryDetail";
                _ent.UserName = SessionProperty.UserName;
                _ent.DocTransId = Convert.ToInt64(SessionProperty.ReffKey);
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
                    EventSource = "UploadInquiry",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RedirectPage redirect = new RedirectPage(this, "EmailDocument.EmailDocument", SessionProperty);
            }
            catch (Exception _exp)
            {
                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.ImageProcess",
                    ClassName = "ImageMaintenanceDetail",
                    FunctionName = "btnBack_Click",
                    ExceptionNumber = 1,
                    EventSource = "ImageMaintenanceDetail",
                    ExceptionObject = _exp,
                    EventID = 200, // 70 Untuk User Managemetn
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
                #endregion
            }
        }

        private void btnEmail_Click(object sender, RoutedEventArgs e)
        {
     

            String _filename;
            String _extention;
           

            if ((Byte[])((DataRowView)dgPaging.SelectedItem)["FileBinary"] != null)
            {

                _imgbin = (Byte[])((DataRowView)dgPaging.SelectedItem)["FileBinary"];
                _filename = (String)((DataRowView)dgPaging.SelectedItem)["FileName"];

                _extention = System.IO.Path.GetExtension(_filename);
                //if (_extention == ".jpg" || _extention == ".png")
                //{
               
                if (!Directory.Exists(tmpPath))
                {
                    Directory.CreateDirectory(tmpPath);
                }
              
                StringBuilder sbFileName = new StringBuilder(8000);
                sbFileName.Append(tmpPath);
                sbFileName.Append("\\");
                sbFileName.Append("filemail");
                sbFileName.Replace(".", "");
                sbFileName.Append(_extention);
                string fullPath = sbFileName.ToString();
                //_filename = tmpPath + (string)((DataRowView)dgPaging.SelectedItem)["FileName"];

                if (File.Exists(fullPath))
                {
               
                    File.Delete(fullPath);
                 
                    System.IO.FileStream _FileStream = new System.IO.FileStream(fullPath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                    _FileStream.Write(_imgbin, 0, _imgbin.Length);

                    _FileStream.Close();

                }
                else
                {
                    System.IO.FileStream _FileStream = new System.IO.FileStream(fullPath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                    _FileStream.Write(_imgbin, 0, _imgbin.Length);

                    _FileStream.Close();
                }




                //        string myTempFile = System.IO.Path.Combine(System.IO.Path.GetTempPath(),_filename, tmpPath);
                //using (StreamWriter sw = new StreamWriter(myTempFile))
                //{
                //    sw.Write(myTempFile);
                //}


                Email_Client.EmailClient emaildoc = new Email_Client.EmailClient();

                emaildoc.DocTransBinaryId = Convert.ToInt64(((DataRowView)dgPaging.SelectedItem)["Id"].ToString());
                //emaildoc._imgbin = Convert.ToByte(_imgbin);
                emaildoc._filename = _filename;
                emaildoc._extention = _extention;
                    emaildoc.showDlg();


            }
           
        }

        private void dgPaging_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
