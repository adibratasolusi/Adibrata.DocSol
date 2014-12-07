using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Framework.Logging;
using Adibrata.Framework.Rule;
using Adibrata.Windows.UserController;
using System;
using System.Windows.Controls;
using System.IO;
using System.Text;

namespace Adibrata.DocumentSol.Windows.RuleUpload
{
    /// <summary>
    /// Interaction logic for RuleEngineUpload.xaml
    /// </summary>
    public partial class RuleEngineUpload : Page
    {
        Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
        RuleEngineEntities _ent = new RuleEngineEntities();
        SessionEntities SessionProperty = new SessionEntities();
        public RuleEngineUpload(SessionEntities _session)
        {
            try
            {
                InitializeComponent();
                SessionProperty = _session;
                this.DataContext = new MainVM(new Shell());
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.RuleUpload",
                    ClassName = "RuleEngineUpload",
                    FunctionName = "RuleEngineUpload",
                    ExceptionNumber = 1,
                    EventSource = "RuleEngine",
                    ExceptionObject = _exp,
                    EventID = 300, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }
        private void btnBack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                RedirectPage redirect = new RedirectPage(this, "RuleUpload.RuleSchemePaging", SessionProperty);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.RuleUpload",
                    ClassName = "RuleEngineUpload",
                    FunctionName = "btnBack_Click",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
                    ExceptionObject = _exp,
                    EventID = 300, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        private void btnSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                RuleEngineEntities _ent = new RuleEngineEntities();
                _ent.PathFile = txtRuleFile.Text;
                _ent.RuleCode = txtRuleCode.Text;
                _ent.RuleName = txtRuleName.Text;
                _ent.UserLogin = SessionProperty.UserName;
                _ent = RuleEngineProcess.UploadRuleEngine(_ent);
                RedirectPage redirect = new RedirectPage(this, "RuleUpload.RuleSchemePaging", SessionProperty);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.RuleUpload",
                    ClassName = "RuleEngineUpload",
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

        private void btnbrowse_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                StringBuilder _filter = new StringBuilder();
                // Set filter for file extension and default file extension
                dlg.Title = "Select a picture";
                dlg.DefaultExt = ".xlsx";
                _filter.Append("All supported spreadsheet|*.xlsx;*.xls|");
                _filter.Append("Excel Files(*.xlsx;*.xls)|*.xlsx;*.xls");
                
                dlg.Filter = _filter.ToString();
                // Display OpenFileDialog by calling ShowDialog method
                Nullable<bool> result = dlg.ShowDialog();
                // Get the selected file name and display in a DataGrid
                if (result == true)
                {
                    txtRuleFile.Text = dlg.FileName;
                }
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = " Adibrata.DocumentSol.Windows.RuleUpload",
                    ClassName = "RuleEngineUpload",
                    FunctionName = "btnbrowse_Click",
                    ExceptionNumber = 1,
                    EventSource = "UCUploadAgreement",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

    }
}
