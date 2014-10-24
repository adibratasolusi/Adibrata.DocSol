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
using System.Data;
using System.Data.SqlClient;
using Adibrata.Rule.Engine;
using System.Configuration; 

namespace Adibrata.FinanceLease.Windows
{
    /// <summary>
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        public Page2(string str)
        {
            InitializeComponent();
            lblName.Content = str;

            DataTable _dt = new DataTable();
            RuleEngineEntities _ent = new RuleEngineEntities();
            _ent.ConnectionString = ConfigurationManager.AppSettings["ConnnectionStringRule"].ToString();
            _ent.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);

            _ent.CurrentPage = 1;
            _ent.WhereCond = "";
            _ent.SortBy = "";

            _dt = RuleEngineProcess.ListRuleEngine(_ent);


            dgTest.DataContext = _dt.DefaultView;
              
         
        }

     

    }
}
