using Adibrata.Windows.UserController;
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
using Adibrata.Controller;
using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.BusinessProcess.Entities.Base;

namespace Adibrata.DocumentSol.Windows.Customer
{
    /// <summary>
    /// Interaction logic for CustomerAddEdit.xaml
    /// </summary>
    public partial class CustomerAddEdit : Page
    {
        string _username;
        public CustomerAddEdit(string UserName) 
        {
            InitializeComponent();
            _username = UserName;
            this.DataContext = new MainVM(new Shell());

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
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
                UserLogin = ""
            };
            DocumentSolutionController.DocSolProcess<string>(_ent);

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

        }

   
    }
}
