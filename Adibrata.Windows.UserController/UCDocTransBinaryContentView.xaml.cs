using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.BusinessProcess.Entities.Base;
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

            DocSolEntities _ent = new DocSolEntities();
            _ent.ClassName = "UploadProcess";
            _ent.MethodName = "DocTransGetTransID";
            _ent.DocTransCode = DocTransCode;

            Int64 _transid = DocumentSolutionController.DocSolProcess<Int64>(_ent);
            Session.ReffKey = _transid.ToString();
            DocTransId = _transid;
            txtDocTransId.Text = _ent.DocTransCode;
            bindContent(_transid);
            bindBinary(_transid);
            // txtDocTransId.Text = this.DocTransId.ToString();
        }

        private void bindContent(Int64 _transid)
        {
            try
            {
                DocSolEntities _ent = new DocSolEntities();
                DataTable _dt = new DataTable();
                _ent.ClassName = "UploadProcess";
                _ent.MethodName = "DocTransContentDetail";
                _ent.DocTransId = this.DocTransId;
                _ent.UserName = this.Session.UserName;
                _dt = DocumentSolutionController.DocSolProcess<DataTable>(_ent);
                dtgContent.ItemsSource = _dt.DefaultView;
            }
            catch (Exception _exp)
            {

                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = "UserControl",
                    NameSpace = "Adibrata.Windows.UserController",
                    ClassName = "UCDocTransBinaryContentView",
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
    }
}
