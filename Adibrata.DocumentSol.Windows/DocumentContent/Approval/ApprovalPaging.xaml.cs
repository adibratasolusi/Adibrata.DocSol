using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Framework.Logging;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Adibrata.Windows.UserController;

namespace Adibrata.DocumentSol.Windows.DocumentContent.Approval
{
    /// <summary>
    /// Interaction logic for ApprovalPaging.xaml
    /// </summary>
    public partial class ApprovalPaging : Page
    {
        SessionEntities SessionProperty;
        public ApprovalPaging()
        {
            try
            {
                InitializeComponent();
                this.DataContext = new MainVM(new Shell());
                //SessionProperty = _session;
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.DocumentContent.Approval",
                    ClassName = "ApprovalPaging",
                    FunctionName = "ApprovalPaging",
                    ExceptionNumber = 1,
                    EventSource = "Approval",
                    ExceptionObject = _exp,
                    EventID = 200,
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

      
    }
}
