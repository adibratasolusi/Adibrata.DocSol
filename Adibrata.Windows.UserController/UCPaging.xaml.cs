using Adibrata.BusinessProcess.Paging.Entities;
using Adibrata.Configuration;
using Adibrata.Controller.Paging;
using Adibrata.Framework.Logging;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Text;
namespace Adibrata.Windows.UserController
{
    /// <summary>
    /// Interaction logic for UCPaging.xaml
    /// </summary>
    public partial class UCPaging : UserControl
    {
        public string ClassName { get; set; }
        public string MethodName { get; set; }

        public string MethodNameTotalRec { get; set; }

        public Boolean IsNeedTotalRec { get; set; }
        public string DataGrid { get; set; }
        public string WhereCond { get; set; }
        public string SortBy { get; set; }
        public DataGrid dgObj { get; set; }
        public string UserName { get; set; }
        private int MaxPage = 999;
        private static int _pageSize = Convert.ToInt32(AppConfig.Config("PageSize"));
        public UCPaging()
        {
            InitializeComponent();
            txtPageNumber.Text = "1";
            MaxPage = 999;
            if (this.IsNeedTotalRec)
            {
                btnTotRec.Visibility = Visibility.Hidden;
            }
            

        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            DataTable _dt = new DataTable();
            int _currentpage;
            try
            {
                
                _currentpage = Convert.ToInt32(txtPageNumber.Text);
                if (_currentpage >= MaxPage)
                {
                    _currentpage = MaxPage;
                }
                else
                {
                    _currentpage += 1;
                }
                txtPageNumber.Text = _currentpage.ToString();
                PagingData();
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = this.UserName,
                    
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
                    UserLogin = this.UserName,
                    
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
            int _currentpage;
            try
            {
                _currentpage = Convert.ToInt32(txtPageNumber.Text);
                if (_currentpage == 1)
                {
                    txtPageNumber.Text = "1";
                }
                else
                {
                    _currentpage -= 1;
                    txtPageNumber.Text = _currentpage.ToString();
                }
                PagingData();
                
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = this.UserName,
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
                    _currentpage = Convert.ToInt32(txtPageNumber.Text);
                    PagingEntities _ent = new PagingEntities { MethodName = this.MethodName, ClassName = this.ClassName, SortBy = this.SortBy, WhereCond = this.WhereCond, StartRecord = _startrecord.ToString(), EndRecord = _endrecord.ToString(), UserLogin= this.UserName };

                    _dt = (DataTable)PagingController.PagingData<DataTable>(_ent);
                    if (_dt.Rows.Count == 0)
                    {
                        MaxPage = _currentpage - 1;
                        dgObj.ItemsSource = _dt.DefaultView;
                    }
                    else
                    {
                        dgObj.ItemsSource = _dt.DefaultView;
                    }
                }
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = this.UserName,
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

        private void btnTotRec_Click(object sender, RoutedEventArgs e)
        {
            Int64 _totrec = 0;
            StringBuilder sb = new StringBuilder(8000);
            try
            {
                if (this.MethodNameTotalRec == null)
                {
                    sb.Append(this.MethodName.Trim());
                    sb.Append("TotRec");
                }
                PagingEntities _ent = new PagingEntities {  SortBy = this.SortBy, WhereCond = this.WhereCond, UserLogin = this.UserName };
                _ent.MethodName = sb.ToString();
                _ent.ClassName = this.ClassName;
                _totrec = PagingController.PagingData<Int64>(_ent);
                btnTotRec.ToolTip = _totrec.ToString("###,###");
                lblTotalRec.Text = _totrec.ToString("###,###");

            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = this.UserName,
                    NameSpace = "Adibrata.Windows.UserControler",
                    ClassName = "UCPaging",
                    FunctionName = "btnTotRec_Click",
                    ExceptionNumber = 1,
                    EventSource = "UserController",
                    ExceptionObject = _exp,
                    EventID = 1, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }
    }
}
