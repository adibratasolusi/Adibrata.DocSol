using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Framework.Logging;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Adibrata.Windows.UserController;
using Adibrata.BusinessProcess.UserManagement.Entities;
using System.Data;
using System.Collections.Generic;
using Adibrata.Controller;

namespace Adibrata.DocumentSol.Windows
{
    /// <summary>
    /// Interaction logic for LogOutScreen.xaml
    /// </summary>
    public partial class LogOutScreen : Page
    {
        SessionEntities SessionProperty = new SessionEntities();
        
        public LogOutScreen(SessionEntities _session)
        {

            InitializeComponent();
            
            StringBuilder sb = new StringBuilder(8000);
            try
            {
                SessionProperty = _session;
                oPaging.ClassName = "Logout";
                oPaging.MethodName = "UserListActivity";
                oPaging.dgObj = dgPaging;
                sb.Append(" Where ");
                sb.Append(" DateTimeAccess Between '");
                sb.Append(_session.StartLoginTime.ToString("MM/dd/yyyy HH:mm:ss"));
                sb.Append("' AND '");
                sb.Append(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                sb.Append("' ");
                sb.Append(" AND UserLogin = '");
                sb.Append(_session.UserName);
                sb.Append("' ");
                     
                oPaging.WhereCond = sb.ToString();
                oPaging.SortBy = " DateTimeAccess Asc ";
                oPaging.UserName = SessionProperty.UserName;
                oPaging.PagingData();

            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.DocumentContent.Approval",
                    ClassName = "ApprovalPaging",
                    FunctionName = "btnSearch_Click",
                    ExceptionNumber = 1,
                    EventSource = "ApprovalDocContent",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               this.NavigationService.Navigate(new StartPage());
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows",
                    ClassName = "LogOutScreen",
                    FunctionName = "btnLogin_Click",
                    ExceptionNumber = 1,
                    EventSource = "LogOut",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }
    }
}
