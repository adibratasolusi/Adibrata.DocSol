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
using Adibrata.BusinessProcess.Paging.Entities;
using Adibrata.Controller.Paging;
using System.Data;
using Adibrata.Framework.Logging;

namespace Adibrata.Windows.UserControler
{
    /// <summary>
    /// Interaction logic for UCPaging.xaml
    /// </summary>
    public partial class UCPaging : UserControl
    {
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public string DataGrid { get; set; }
        public string WhereCond { get; set; }
        public string SortBy { get; set; }
        public DataGrid dgObj { get; set; }
        public string UserName { get; set; }

        public UCPaging()
        {
            InitializeComponent();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            DataTable _dt = new DataTable();
            try
            {
                _dt = PagingData();
                dgObj.ItemsSource = _dt.DefaultView;
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = "",
                    
                    NameSpace = "Adibrata.Windows.UserControler",
                    ClassName = "UCPaging",
                    FunctionName = "btnNext_Click",
                    ExceptionNumber = 1,
                    EventSource = "UserController",
                    ExceptionObject = null,
                    EventID = 1, // 1 Untuk Framework 
                    ExceptionDescription = null 
                };
                ErrorLog.WriteEventLog(true, _errent);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = "",
                    
                    NameSpace = "Adibrata.Windows.UserControler",
                    ClassName = "UCPaging",
                    FunctionName = "btnNext_Click",
                    ExceptionNumber = 1,
                    EventSource = "UserController",
                    ExceptionObject = _exp,
                    EventID = 1, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            DataTable _dt = new DataTable();
            try
            {
                _dt = PagingData();
                dgObj.ItemsSource = _dt.DefaultView;
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = "EMAIL",
                    NameSpace = "Adibrata.Windows.UserControler",
                    ClassName = "UCPaging",
                    FunctionName = "btnPrev_Click",
                    ExceptionNumber = 1,
                    EventSource = "UserController",
                    ExceptionObject = _exp,
                    EventID = 1, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        public DataTable PagingData()
        {
            DataTable _dt = new DataTable();
            
            try
            {
                PagingEntities _ent = new PagingEntities { MethodName = this.MethodName, ClassName = this.ClassName, SortBy = this.SortBy, WhereCond = this.SortBy, CurrentPage = 1 };

                PagingController.PagingData(_ent);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = "EMAIL",
                    NameSpace = "Adibrata.Windows.UserControler",
                    ClassName = "UCPaging",
                    FunctionName = "PagingData",
                    ExceptionNumber = 1,
                    EventSource = "UserController",
                    ExceptionObject = _exp,
                    EventID = 1, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
            return _dt;
        }
    }
}
