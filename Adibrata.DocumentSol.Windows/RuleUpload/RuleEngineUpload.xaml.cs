using Adibrata.Framework.Rule;
using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Controller;
using Adibrata.Framework.Logging;
using Adibrata.Windows.UserController;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Controls;

namespace Adibrata.DocumentSol.Windows.RuleUpload
{
    /// <summary>
    /// Interaction logic for RuleEngineUpload.xaml
    /// </summary>
    public partial class RuleEngineUpload : Page
    {
        RuleEngineEntities _ent = new RuleEngineEntities();
        SessionEntities SessionProperty = new SessionEntities();
        public RuleEngineUpload(SessionEntities _session)
        {
            InitializeComponent();
        }
        private void btnBack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                RedirectPage redirect = new RedirectPage(this, "RuleUpload.RuleEngineUpload", SessionProperty);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.Customer",
                    ClassName = "CustomerAddEdit",
                    FunctionName = "btnBack_Click",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        private void btnSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {

                RedirectPage redirect = new RedirectPage(this, "Customer.CustomerPaging", SessionProperty);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.Customer",
                    ClassName = "CustomerAddEdit",
                    FunctionName = "btnBack_Click",
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
