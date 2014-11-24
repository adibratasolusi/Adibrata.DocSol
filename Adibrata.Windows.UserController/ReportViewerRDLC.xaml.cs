using Adibrata.Framework.Logging;
using Adibrata.Framework.ReportDocument;
using System;
using System.Data;
using System.Windows.Controls;
using Adibrata.Configuration;

namespace Adibrata.Windows.UserControler
{
    /// <summary>
    /// Interaction logic for ReportViewerRDLC.xaml
    ///           DataTable dtReport;
    ///        UserManagementEntities _ent = new UserManagementEntities { ClassName = "UserRegister", MethodName = "UserRegisterListReportData" };
    ///        dtReport = UserManagementController.UserManagement<DataTable>(_ent);

    ///        rptViewer.FileNameDocument = "UserList.pdf";
     ///       rptViewer.DataReport = dtReport;
    ///        rptViewer.DataSetName = "dsUserRegisterList";
     ///       rptViewer.ReportTemplate = @"UserManagement\UserRegisterReport.rdlc";
    ///       rptViewer.ReportBind();
    /// </summary>
    public partial class ReportViewerRDLC : UserControl
    {
        public DataTable DataReport { get; set;}
        public string FileNameDocument { get; set;}
        public string DataSetName { get; set; }
        public string ReportTemplate { get; set; }
        static string OutputPath = AppConfig.Config("ReportOutputPath");

        public ReportViewerRDLC()
        {
            InitializeComponent();
        }
        
        public void ReportBind()
        {
            string _outputdoc;
            try
            {
                ReportingEntities _rptent = new ReportingEntities
                {
                    DataSetName = this.DataSetName,
                    ReportPath = this.ReportTemplate,
                    ReportData = this.DataReport,
                    FileNameDocument = this.FileNameDocument
                };

                ReportServerRDLC _objreport = new ReportServerRDLC(_rptent);
                _rptent = _objreport.ReportOutput(_rptent, ReportServerRDLC.DocumentType.PDF);
                
                _outputdoc = OutputPath + _rptent.FileNameDocument;
                WebBrowser wb = new WebBrowser();
                wb.Navigate(@_outputdoc, _rptent.FileNameDocument, _rptent.ReportResult, _rptent.MimeDocument);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = "REPORT",
                    NameSpace = "Adibrata.Framework.ReportDocument",
                    ClassName = "ReportServerRDLC",
                    FunctionName = "ReportServerRDLC / Construtor",
                    ExceptionNumber = 1,
                    EventSource = "Report",
                    ExceptionObject = _exp,
                    EventID = 1, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }

        }
    }
}
