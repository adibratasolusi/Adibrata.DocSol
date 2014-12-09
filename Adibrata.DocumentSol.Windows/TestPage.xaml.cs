using Adibrata.BusinessProcess.UserManagement.Entities;
using Adibrata.Controller.UserManagement;
using System.Data;
using System.Windows;
using System.Windows.Controls;


namespace Adibrata.DocumentSol.Windows
{
    /// <summary>
    /// Interaction logic for TestPage.xaml
    /// </summary>
    public partial class TestPage : Page
    {
        public TestPage()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //DataTable dtReport;
            //UserManagementEntities _ent = new UserManagementEntities { ClassName = "UserRegister", MethodName = "UserRegisterListReportData" };
            //dtReport = UserManagementController.UserManagement<DataTable>(_ent);

            //rptViewer.FileNameDocument = "UserList.pdf";
            //rptViewer.DataReport = dtReport;
            //rptViewer.DataSetName = "dsUserRegisterList";
            //rptViewer.ReportTemplate = @"UserManagement\UserRegisterReport.rdlc";
            //rptViewer.ReportBind();

        }


    }
}