using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Controller;
using Adibrata.Windows.UserController;
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

namespace Adibrata.DocumentSol.Windows.StorageMonitoring
{
    /// <summary>
    /// Interaction logic for SummarySize.xaml
    /// </summary>
    public partial class SummarySize : Page
    {
        SessionEntities SessionProperty = new SessionEntities();
        Int64 MaxId, MinId;
        public SummarySize(SessionEntities _session)
        {
            InitializeComponent();
            this.DataContext = new MainVM(new Shell());
            SessionProperty = _session;
            summarysize();
            summarysizedatagrid();
        }

        void summarysize()
        {
            DocSolEntities _ent = new DocSolEntities
            {
                MethodName = "SummarySize",
                ClassName = "StorageMonitoring"
            };

            DataTable dt = new DataTable();
            dt = DocumentSolutionController.DocSolProcess<DataTable>(_ent);
            MaxId = (Int64)dt.Rows[0]["MaxFileID"];
            MinId = (Int64)dt.Rows[0]["MinFileID"];
            txtAverageSize.Text = Format.NumberFormatting(dt.Rows[0]["Average"].ToString());
            txtTotalFile.Text = dt.Rows[0]["totalfile"].ToString();
            txtMaxSize.Text = Format.NumberFormatting(dt.Rows[0]["Maximum"].ToString());
            txtMinSize.Text = Format.NumberFormatting(dt.Rows[0]["Minimum"].ToString());
            txtTotalSize.Text = Format.NumberFormatting(dt.Rows[0]["Summary"].ToString());


        }
        void summarysizedatagrid()
        {

            DocSolEntities _ent = new DocSolEntities
            {
                MethodName = "SummarySizeDetail",
                ClassName = "StorageMonitoring"
            };
            _ent.Id = MaxId;

            DataTable dt = new DataTable();
            dt = DocumentSolutionController.DocSolProcess<DataTable>(_ent);
            dgBigFile.ItemsSource = dt.DefaultView;


            _ent = new DocSolEntities
            {
                MethodName = "SummarySizeDetail",
                ClassName = "StorageMonitoring"
            };
            _ent.Id = MinId;

            dt = new DataTable();
            dt = DocumentSolutionController.DocSolProcess<DataTable>(_ent);
            dgSmallFile.ItemsSource = dt.DefaultView;
        }

    }
}
