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
using Adibrata.BusinessProcess.Paging.Entities;

using Adibrata.Framework.Logging;


namespace Adibrata.DocumentSol.Windows.Customer
{
    /// <summary>
    /// Interaction logic for CustomerPaging.xaml
    /// </summary>
    public partial class CustomerPaging : Page
    {
        public CustomerPaging()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            PagingEntities _ent = new PagingEntities { ClassName = "CustomerPaging", MethodName = "CustomerPaging" };
            oPaging.ClassName = "CustomerRegistrasi";
            oPaging.MethodName = "CustomerPaging";
            oPaging.dgObj = dgPaging;
            oPaging.WhereCond = "";
            oPaging.SortBy = "";
            oPaging.PagingData();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CustomerAddEdit("Henry"));
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

     
    }
}
