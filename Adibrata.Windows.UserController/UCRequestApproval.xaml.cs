using System;
using System.Collections.Generic;
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
using Adibrata.Framework.Logging;
using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Controller;
using System.Data;


namespace Adibrata.Windows.UserController
{
    /// <summary>
    /// Interaction logic for UCRequestApproval.xaml
    /// </summary>
    public partial class UCRequestApproval : UserControl
    {
        public string RequestTo { get; set;}
         
        public string UserName { get; set; }
        public string DocumentType
        {
            set { RequestToBind(value); }
        }

        public string Notes
        {
            get { return txtNotes.Text; }
            set { txtNotes.Text = value; }
        }
        public UCRequestApproval()
        {
            InitializeComponent();
        }

        public void RequestToBind(string _doctype)
        {
            try
            {
                DocSolEntities _ent = new DocSolEntities();
                DataTable _dt = new DataTable();

                _ent.ClassName = "ApprovalProcess";
                _ent.MethodName = "ApprovalRequestTo";
                _ent.DocumentType = _doctype;

                _dt = DocumentSolutionController.DocSolProcess<DataTable>(_ent);
                List<string> data = new List<string>();
                if (_dt.Rows.Count > 0)
                {
                    foreach (DataRow _row in _dt.Rows)
                    {
                        data.Add(_row["Result"].ToString());
                    }
                }

                cboRequestTo.ItemsSource = data;
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = this.UserName,
                    NameSpace = "Adibrata.Windows.UserController",
                    ClassName = "UCRequestApproval",
                    FunctionName = "cboRequestTo_SelectionChanged",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }
        private void cboRequestTo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                this.RequestTo = cboRequestTo.SelectedValue.ToString();
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = this.UserName,
                    NameSpace = "Adibrata.Windows.UserController",
                    ClassName = "UCRequestApproval",
                    FunctionName = "cboRequestTo_SelectionChanged",
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
