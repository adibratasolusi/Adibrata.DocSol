using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Framework.Logging;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;


namespace Adibrata.DocumentSol.Windows.Customer
{
    /// <summary>
    /// Interaction logic for CustomerPaging.xaml
    /// </summary>
    public partial class CustomerPaging : Page
    {
        SessionEntities SessionProperty;
        public CustomerPaging(SessionEntities _session)
        {
            InitializeComponent();
            SessionProperty = _session;

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder(8000);
            try
            {
                oPaging.ClassName = "CustomerRegistrasi";
                oPaging.MethodName = "CustomerPaging";
                oPaging.dgObj = dgPaging;
                if (txtCustName.Text != "")
                {
                    sb.Append(" Where ");
                    sb.Append (" CustName = '");
                    sb.Append(txtCustName.Text);
                    sb.Append("'");
                }
                else
                {
                    sb.Append("");
                }
                oPaging.WhereCond = sb.ToString();
                oPaging.SortBy = " CustName Asc ";
                oPaging.UserName = SessionProperty.UserName;
                oPaging.PagingData();
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.Customer",
                    ClassName = "CustomerPaging",
                    FunctionName = "btnSearch_Click",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.NavigationService.Navigate(new CustomerAddEdit(SessionProperty));
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.Customer",
                    ClassName = "CustomerPaging",
                    FunctionName = "btnAdd_Click",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.Customer",
                    ClassName = "CustomerPaging",
                    FunctionName = "btnEdit_Click",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
            //dgPaging.
        }

     
    }
}
