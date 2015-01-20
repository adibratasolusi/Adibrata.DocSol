using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Configuration;
using Adibrata.Controller;
using Adibrata.Framework.Logging;
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

namespace Adibrata.Windows.UserController
{
    /// <summary>
    /// Interaction logic for UCDocTransBinaryContentView.xaml
    /// </summary>
    public partial class UCDocTransBinaryContentView : UserControl
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


        public SessionEntities Session
        { get; set; }

        public string DocTransCode
        {
            get
            {
                return _doctranscode;
            }
            set
            {
                _doctranscode = value;
                BindingData(value);
            }
        }

        public Int64 DocTransId
        { get; set; }


        public UCDocTransBinaryContentView()
        {
            InitializeComponent();

            //_ent.DocTransId = Convert.ToInt64(SessionProperty.ReffKey);



        }
        private string _doctranscode;
        public void BindingData(string _doctranscode)
        {
            SessionProperty = Session;
            DocSolEntities _ent = new DocSolEntities();
            _ent.ClassName = "UploadProcess";
            _ent.MethodName = "DocTransGetTransID";
            _ent.DocTransCode = DocTransCode;
            _ent.UserLogin = this.Session.UserName;
            Int64 _transid = DocumentSolutionController.DocSolProcess<Int64>(_ent);
            Session.ReffKey = _transid.ToString();
            DocTransId = _transid;
            txtDocTransId.Text = _ent.DocTransCode;
            BindBinary();
            BindContent();
            //bindContent(_transid);
            bindBinary(_transid);
            // txtDocTransId.Text = this.DocTransId.ToString();
        }

        //private void bindContent(Int64 _transid)
        //{
        //    try
        //    {
        //        DocSolEntities _ent = new DocSolEntities();
        //        DataTable _dt = new DataTable();
        //        _ent.ClassName = "UploadProcess";
        //        _ent.MethodName = "DocTransContentDetail";
        //        _ent.DocTransId = this.DocTransId;
        //        _ent.UserName = this.Session.UserName;
        //        _dt = DocumentSolutionController.DocSolProcess<DataTable>(_ent);
        //        dtgContent.ItemsSource = _dt.DefaultView;
        //    }
        //    catch (Exception _exp)
        //    {

        //        ErrorLogEntities _errent = new ErrorLogEntities
        //        {
        //            UserLogin = "UserControl",
        //            NameSpace = "Adibrata.Windows.UserController",
        //            ClassName = "UCDocTransBinaryContentView",
        //            FunctionName = "bindContent",
        //            ExceptionNumber = 1,
        //            EventSource = "Customer",
        //            ExceptionObject = _exp,
        //            EventID = 200, // 1 Untuk Framework 
        //            ExceptionDescription = _exp.Message
        //        };
        //        ErrorLog.WriteEventLog(_errent);
        //    }
        //}
        private void bindBinary(Int64 _transid)
        {
            try
            {
                DocSolEntities _ent = new DocSolEntities();
                DataTable _dt = new DataTable();
                _ent.ClassName = "UploadProcess";
                _ent.MethodName = "DocTransInquiryDetail";
                _ent.DocTransId = this.DocTransId;
                _ent.UserName = this.Session.UserName;
                _dt = DocumentSolutionController.DocSolProcess<DataTable>(_ent);
                dgPaging.ItemsSource = _dt.DefaultView;
            }
            catch (Exception _exp)
            {

                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = "UserControl",
                    NameSpace = "Adibrata.Windows.UserController",
                    ClassName = "UCDocTransBinaryContentView",
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

        private void BindBinary()
        {
            try
            {
                DocSolEntities _ent = new DocSolEntities();
                DataTable _dt = new DataTable();
                _ent.ClassName = "UploadProcess";
                _ent.MethodName = "DocTransInquiryDetail";
                _ent.UserName = SessionProperty.UserName;
                _ent.DocTransId = DocTransId;
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
                _ent.DocTransId = DocTransId;
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
                                    //txtInput.Text = "-";
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
                    NameSpace = "Adibrata.Windows.UserController",
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

       
    }
}
