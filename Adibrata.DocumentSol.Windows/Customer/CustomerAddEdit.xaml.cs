using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Controller;
using Adibrata.Framework.Logging;
using Adibrata.Windows.UserController;
using System;
using System.Windows;
using System.Windows.Controls;


namespace Adibrata.DocumentSol.Windows.Customer
{
    /// <summary>
    /// Interaction logic for CustomerAddEdit.xaml
    /// </summary>
    public partial class CustomerAddEdit : Page
    {
        SessionEntities SessionProperty;
        public CustomerAddEdit(SessionEntities _session) 
        {
            InitializeComponent();
            SessionProperty = _session;
            this.DataContext = new MainVM(new Shell());
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DocSolEntities _ent = new DocSolEntities
                {
                    ClassName = "CustomerRegistrasi",
                    MethodName = "CustomerCompanyRegistrasiAdd",
                    CompanyName = txtCompanyName.Text,
                    CompanyAddress = oAddress.Address.Text,
                    CompanyRT = oAddress.RT.Text,
                    CompanyRW = oAddress.RW.Text,
                    CompanyKelurahan = oAddress.Kelurahan.Text,
                    CompanyKecamatan = oAddress.Kecamatan.Text,
                    CompanyCity = oAddress.City.Text,
                    CompanyZipCode = oAddress.ZipCode.Text,
                    CompanyNPWP = txtNPWPNumber.Text,
                    CompanySiup = txtSIUPNo.Text,
                    CompanyTDP = txtTDPNumber.Text,
                    CompanyNotary = txtNotaryNumber.Text,
                    UserLogin = SessionProperty.UserName
                };
                DocumentSolutionController.DocSolProcess<string>(_ent);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.Customer",
                    ClassName = "CustomerAddEdit",
                    FunctionName = "btnSave_Click",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.NavigationService.Navigate(new CustomerPaging(SessionProperty));
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = SessionProperty.UserName,
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
