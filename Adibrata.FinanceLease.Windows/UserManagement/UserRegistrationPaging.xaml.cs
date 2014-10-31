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
using System.Data; 
using Adibrata.Framework.Logging;

namespace Adibrata.FinanceLease.Windows.UserManagement
{
    /// <summary>
    /// Interaction logic for UserRegistrationPaging.xaml
    /// </summary>
    public partial class UserRegistrationPaging : Page
    {
        public UserRegistrationPaging()
        {
            try
            {
                DataTable dt = new DataTable();
                InitializeComponent();
                PagingControl.ClassName = "Adibrata.BusinessProcess.Paging.Extend.UserManagement.UserRegisterPaging";
                PagingControl.MethodName = "UserRegister";
                PagingControl.dgObj = dgListUser;
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = "EMAIL",
                    NameSpace = "Adibrata.FinanceLease.Windows.UserManagement",
                    ClassName = "UserRegistrationPaging",
                    FunctionName = "UserRegistrationPaging / Constructor",
                    ExceptionNumber = 1,
                    EventSource = "Pages",
                    ExceptionObject = _exp,
                    EventID = 1, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
     
        }
    }
}
