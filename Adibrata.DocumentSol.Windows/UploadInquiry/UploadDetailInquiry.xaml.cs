using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Configuration;
using Adibrata.Controller;
using Adibrata.Framework.Logging;
using Adibrata.Windows.UserController;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Drawing;






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
        Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
        // ServiceReference1.Service1Client objService = new ServiceReference1.Service1Client();
        Dictionary<int, string> dicFile = new Dictionary<int, string>();
        Dictionary<int, string> dicExt = new Dictionary<int, string>();
        string server = AppConfig.Config("BITSServer");
        Byte[] _imgbin;
        String _filename = "";

        int jumlahUploadMax; //jumlah maksimal upload, masih hardcode
        #endregion
        public class DataItem
        {
            public string PathFile { get; set; }
            public string img { get; set; }
            public bool save { get; set; }
        }
        public bool RestoreDirectory { get; set; }



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

                txtDocTransId.Text = _ent.DocTransCode;

                BindContent();
                BindBinary();
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

        private void BindContent()
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
                                    txtInput.Text = _row["value"].ToString().Trim();
                                    txtInput.Text = "0";
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
                                    txtInput.Text = "-";
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
                    EventSource = "Customer",
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

          

            _imgbin = (Byte[])((DataRowView)dgPaging.SelectedItem)["FileBin"];

            //_filename = @"C:\" + (string)((DataRowView)dgPaging.SelectedItem)["FileName"];

            dlg.Title = "Select a picture";
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
            "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
            "Portable Network Graphic (*.png)|*.png|" +
            "Portable Document Format (*.pdf)|*.pdf|" +
            "Word Document (*.doc;*.docx)|*.doc;*.docx|" +
            "All files (*.*)|*.*";
            dlg.AddExtension = true;
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                foreach (String file in dlg.FileNames)
                {

                    //_filename = @"C:\" + (string)((DataRowView)dgPaging.SelectedItem)["FileName"];
                
                    _filename = @file;
                    System.IO.FileStream _FileStream = new System.IO.FileStream(_filename, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);


                    System.IO.Path.GetDirectoryName(_filename);

                    _FileStream.Write(_imgbin, 0, _imgbin.Length);
                    _FileStream.Close();

                }
            }
        }

        private void btnRotate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCheckOut_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnWaterMark_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dgPaging_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnGreyScale_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLock_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}