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
            try
            {
                InitializeComponent();
                SessionProperty = _session;
                this.DataContext = new MainVM(new Shell());
                if (_session.IsEdit)
                {
                    DocSolEntities _ent = new DocSolEntities
                    {
                        ClassName = "CustomerRegistrasi",
                        MethodName = "CustomerCompanyRegistrasiView",
                        CustomerCode = _session.ReffKey
                    };
                    _ent = DocumentSolutionController.DocSolProcess<DocSolEntities>(_ent);
                    txtCompanyName.Text = _ent.CompanyName;

                    oAddress.Address.Text = _ent.CompanyAddress.ToString();
                    oAddress.RT.Text = _ent.CompanyRT.ToString();
                    oAddress.RW.Text = _ent.CompanyRW.ToString();
                    oAddress.Kelurahan.Text = _ent.CompanyKelurahan.ToString();
                    oAddress.Kecamatan.Text = _ent.CompanyKecamatan.ToString();
                    oAddress.City.Text = _ent.CompanyCity.ToString();
                    oAddress.ZipCode.Text = _ent.CompanyZipCode.ToString();
                    txtNPWPNumber.Text = _ent.CompanyNPWP.ToString();
                    txtSIUPNo.Text = _ent.CompanySiup.ToString();
                    txtTDPNumber.Text = _ent.CompanyTDP.ToString();
                    txtNotaryNumber.Text = _ent.CompanyNotary.ToString();
                }
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.Customer",
                    ClassName = "CustomerAddEdit",
                    FunctionName = "CustomerAddEdit",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
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
                if (SessionProperty.IsEdit)
                {
                    _ent.IsEdit = SessionProperty.IsEdit;
                    _ent.CustomerCode = SessionProperty.ReffKey;
                }
                DocumentSolutionController.DocSolProcess<string>(_ent);
                RedirectPage redirect = new RedirectPage(this, "Customer.CustomerPaging", SessionProperty);

            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
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
