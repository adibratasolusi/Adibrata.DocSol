using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Framework.Logging;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Adibrata.DocumentSol.Windows.UserManagement
{
    /// <summary>
    /// Interaction logic for UserRegistrationPaging.xaml
    /// </summary>
    public partial class UserRegistrationPaging : Page
    {
        SessionEntities SessionProperty;
        public UserRegistrationPaging(SessionEntities _session)
        {
            InitializeComponent();
            SessionProperty = _session;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.NavigationService.Navigate(new UserRegistrationAddEdit(SessionProperty));
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.UserManagement",
                    ClassName = "UserRegistrationPaging",
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

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder(8000);
            try
            {
                oPaging.ClassName = "UserRegister";
                oPaging.MethodName = "UserRegisterPaging";
                oPaging.dgObj = dgPaging;
                if (txtUserName.Text != "")
                {
                    sb.Append(" Where ");
                    sb.Append(" UserName = '");
                    sb.Append(txtUserName.Text);
                    sb.Append("'");
                }
                else
                {
                    sb.Append("");
                }
                oPaging.WhereCond = sb.ToString();
                oPaging.SortBy = " UserName Asc ";
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
    }
}
