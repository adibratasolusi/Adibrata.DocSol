using Adibrata.BusinessProcess.Paging.Entities;
using Adibrata.Configuration;
using Adibrata.Controller.Paging;
using Adibrata.Framework.Logging;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Adibrata.Windows.UserController
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
        public UCDataGrid dgObj { get; set; }
        public string UserName { get; set; }
        private static int _pageSize = Convert.ToInt32(AppConfig.Config("PageSize"));
        public UCPaging()
        {
            InitializeComponent();
            txtPageNumber.Text = "1";
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            DataTable _dt = new DataTable();
            try
            {
                _dt = PagingData();
                dgObj.DataGridProperty.ItemsSource = _dt.DefaultView;
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = this.UserName,
                    
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
                    UserName = this.UserName,
                    
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
                dgObj.DataGridProperty.ItemsSource = _dt.DefaultView;
                
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = this.UserName,
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
            int _startrecord=1;
                int _endrecord = 1;
                int _currentpage;
            try
            {
                if (int.TryParse(txtPageNumber.Text.Replace(",", ""), out _currentpage))
                {
                    _startrecord = (Convert.ToInt32(txtPageNumber.Text) - 1) * _pageSize + 1;
                    _endrecord = (Convert.ToInt32(txtPageNumber.Text) * _pageSize + 1) - 1;
                    PagingEntities _ent = new PagingEntities { MethodName = this.MethodName, ClassName = this.ClassName, SortBy = this.SortBy, WhereCond = this.WhereCond, StartRecord = _startrecord.ToString(), EndRecord = _endrecord.ToString() };

                    _dt = (DataTable)PagingController.PagingData<DataTable>(_ent);
                }
              

           
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = this.UserName,
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
