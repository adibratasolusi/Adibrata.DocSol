﻿using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Configuration;
using Adibrata.Controller;
using Adibrata.Framework.Logging;
using Adibrata.Windows.UserController;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Adibrata.DocumentSol.Windows.ImageProcess.Checkout
{
    /// <summary>
    /// Interaction logic for CheckoutDetail.xaml
    /// </summary>
    public partial class CheckoutDetail : Page
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
        string _doctranscode;
        public CheckoutDetail(SessionEntities _session)
        {
            try
            {
                DocSolEntities _ent = new DocSolEntities();

                InitializeComponent();
                this.DataContext = new MainVM(new Shell());
                SessionProperty = _session;
                _doctranscode = SessionProperty.ReffKey;
                _ent.ClassName = "UploadProcess";
                _ent.MethodName = "DocTransGetTransID";
                _ent.DocTransCode = SessionProperty.ReffKey;
                _ent.UserName = SessionProperty.UserName;
                SessionProperty.ReffKey = Convert.ToString(DocumentSolutionController.DocSolProcess<Int64>(_ent));

                txtDocTransId.Text = _ent.DocTransCode;

                BindBinary();
                BindContent();
                //WebBrowser browser = new WebBrowser();
                //browser.NavigateToString("www.detik.com");


            }
            catch (Exception _exp)
            {
                #region "Write to Event Viewer"
                //ErrorLogEntities _errent = new ErrorLogEntities
                //{
                //    UserLogin = SessionProperty.UserName,
                //    NameSpace = "Adibrata.DocumentSol.Windows.ImageProcess.Checkout",
                //    ClassName = "CheckoutDetail",
                //    FunctionName = "CheckoutDetail",
                //    ExceptionNumber = 1,
                //    EventSource = "CheckoutDetail",
                //    ExceptionObject = _exp,
                //    EventID = 200, // 70 Untuk User Management
                //    ExceptionDescription = _exp.Message
                //};
                //ErrorLog.WriteEventLog(_errent);
                #endregion

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
                    ClassName = "CheckoutDetail",
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

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                RedirectPage redirect = new RedirectPage(this, "ImageProcess.Checkout.CheckoutPaging", SessionProperty);
            }
            catch (Exception _exp)
            {
                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.ImageProcess.Checkout",
                    ClassName = "CheckoutDetail",
                    FunctionName = "btnBack_Click",
                    ExceptionNumber = 1,
                    EventSource = "CheckoutDetail",
                    ExceptionObject = _exp,
                    EventID = 200, // 70 Untuk User Managemetn
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
                #endregion
            }
        }

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


                }
            }




            System.IO.FileStream _FileStream = new System.IO.FileStream(_filename, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);


            System.IO.Path.GetDirectoryName(_filename);

            _FileStream.Write(_imgbin, 0, _imgbin.Length);
            string _url;

            _url = @_filename;
            _FileStream.Close();

            DocSolEntities _ent = new DocSolEntities
            {
                MethodName = "DocTransCheckOut",
                ClassName = "ImageProcess"
            };
            _ent.Id = Convert.ToInt64(SessionProperty.ReffKey);
            _ent.UserName = SessionProperty.UserName;

            DocumentSolutionController.DocSolProcess<string>(_ent);
            MessageBox.Show("Check Out Succes");
   
            RedirectPage redirect = new RedirectPage(this, "ImageProcess.Checkout.CheckoutPaging", SessionProperty);
            WebBrowser wb = new WebBrowser();

          
        }
    }
}
