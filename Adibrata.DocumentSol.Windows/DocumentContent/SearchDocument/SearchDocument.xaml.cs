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
                    ClassName = "DocumentUploadPaging",
                    FunctionName = "DocumentUploadPaging",
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

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder(8000);
            try
            {
                if (txtSearch.Text == "")
                {
                    MessageBox.Show("Please Enter Search Criteria");
                }
                else
                {
                    GridDataGrid.Visibility = Visibility.Visible;
                    GridPaging.Visibility = Visibility.Visible;
                    oPaging.ClassName = "DocumentSearch";
                    oPaging.MethodName = "DocumentSearchPaging";
                    oPaging.dgObj = dgPaging;

                    oPaging.WhereCond = txtSearch.Text;
                    oPaging.SortBy = " Rank Asc ";
                    oPaging.UserName = SessionProperty.UserName;
                    oPaging.PagingData();
                }
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.DocumentContent.SearchDocument",
                    ClassName = "SearchDocument",
                    FunctionName = "btnSearch_Click",
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
