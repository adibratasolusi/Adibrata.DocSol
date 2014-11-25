using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.Controller;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Adibrata.Windows.UserControler
{
    /// <summary>
    /// Interaction logic for UCDataGidBitsView.xaml
    /// </summary>
    public partial class UCDataGidBitsView : Page
    {
        public string currentAgrmntNo
        {
            get;
            set;
        }
        public UCDataGidBitsView()
        {
            InitializeComponent();
        }

        public void SetScreen()
        {
            FillDataGrid();
            txtaAgrmntNo.Text = currentAgrmntNo;
        }
        private void FillDataGrid()
        {
            DocSolEntities _ent = new DocSolEntities
            {
                MethodName = "PathGetView",
                ClassName = "UploadProcess"
            };
            _ent.AgrmntNo = currentAgrmntNo;
            DataTable dt = new DataTable();
            dt = DocumentSolutionController.DocSolProcess<DataTable>(_ent);

            dgPaging.ItemsSource = dt.DefaultView;
            dgPaging.CanUserSortColumns = false;


        }
    }
}
