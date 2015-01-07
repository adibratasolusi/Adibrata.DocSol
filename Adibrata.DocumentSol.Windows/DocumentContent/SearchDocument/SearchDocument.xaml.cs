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
using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Framework.Logging;

using Adibrata.Windows.UserController;
namespace Adibrata.DocumentSol.Windows.DocumentContent
{
    /// <summary>
    /// Interaction logic for SearchDocument.xaml
    /// </summary>
    public partial class SearchDocument : Page
    {
        SessionEntities SessionProperty;
        public SearchDocument(SessionEntities _session)
        {

            try
            {
                InitializeComponent();
                this.DataContext = new MainVM(new Shell());
                SessionProperty = _session;
                GridDataGrid.Visibility = Visibility.Hidden;
                GridPaging.Visibility = Visibility.Hidden;
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.DocumentContent",
                    ClassName = "SearchDocument",
                    FunctionName = "SearchDocument",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        public SearchDocument(SessionEntities _session, string SearchCriteria)
        {

            try
            {
                InitializeComponent();
                this.DataContext = new MainVM(new Shell());
                SessionProperty = _session;
                GridDataGrid.Visibility = Visibility.Hidden;
                GridPaging.Visibility = Visibility.Hidden;
                SearchDocumentProcess(SearchCriteria);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.DocumentContent",
                    ClassName = "SearchDocument",
                    FunctionName = "SearchDocument",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }


        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int i = dgPaging.SelectedIndex;

                DataGridHelper oDataGrid = new DataGridHelper();
                oDataGrid.dtg = dgPaging;
                DataGridCell cell = oDataGrid.GetCell(i, 1);
                TextBlock ReffKey = oDataGrid.GetVisualChild<TextBlock>(cell); // pass the DataGridCell as a parameter to GetVisualChild
                SessionProperty.IsEdit = true;
                SessionProperty.ReffKey = ReffKey.Text;
                SessionProperty.SourceForm = "DocumentContent.SearchDocument";
                RedirectPage redirect = new RedirectPage(this, "UploadInquiry.UploadDetailInquiry", SessionProperty);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.DocumentContent",
                    ClassName = "SearchDocument",
                    FunctionName = "btnView_Click",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        private void SearchDocumentProcess(string searchcriteria)
        {
            StringBuilder sb = new StringBuilder(8000);
            try
            {

                GridDataGrid.Visibility = Visibility.Visible;
                GridPaging.Visibility = Visibility.Visible;
                oPaging.ClassName = "DocumentSearch";
                oPaging.MethodName = "DocumentSearchPaging";
                oPaging.dgObj = dgPaging;

                oPaging.WhereCond = searchcriteria;
                oPaging.SortBy = " Rank Asc ";
                oPaging.UserName = SessionProperty.UserName;
                oPaging.PagingData();
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.DocumentContent.SearchDocument",
                    ClassName = "SearchDocument",
                    FunctionName = "SearchDocumentProcess",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text == "")
            {
                MessageBox.Show("Please Enter Search Criteria");
            }
            else
            {
                SearchDocumentProcess(txtSearch.Text);
            }
        }
    }
}
