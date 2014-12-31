using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Framework.Logging;
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

namespace Adibrata.DocumentSol.Windows.StorageMonitoring.FileNumber
{
    /// <summary>
    /// Interaction logic for FileNumberPaging.xaml
    /// </summary>
    public partial class FileNumberPaging : Page
    {

        SessionEntities SessionProperty = new SessionEntities();
        public FileNumberPaging(SessionEntities _session)
        {
            InitializeComponent();
            this.DataContext = new MainVM(new Shell());
            SessionProperty = _session;
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
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
                RedirectPage redirect = new RedirectPage(this, "StorageMonitoring.FileNumber.FileNumberDetail", SessionProperty);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.StorageMonitoring.FileNumber",
                    ClassName = "ImageLockPaging",
                    FunctionName = "btnDetail_Click",
                    ExceptionNumber = 1,
                    EventSource = "Detail",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder(8000);
            try
            {
                oPaging.ClassName  = "FileNumberPaging";
                oPaging.MethodName = "StoragePaging";
                //"DeleteDocumentPaging"
                oPaging.dgObj = dgPaging;
                if (txtExtension.Text != "")
                {
                    sb.Append(" Where ");
                    if (txtExtension.Text.Contains("%"))
                    {
                        sb.Append(" (dbo.GetColumnValue(DocTransBinary.FileName,'.',2)) LIKE '");
                    }
                    else
                    {
                        sb.Append(" (dbo.GetColumnValue(DocTransBinary.FileName,'.',2)) = '");
                    }
                    sb.Append(txtExtension.Text);
                    sb.Append("'");
                }
                
                else
                {
                    sb.Append("");
                }
                oPaging.WhereCond = sb.ToString();
                oPaging.SortBy = " (dbo.GetColumnValue(DocTransBinary.FileName,'.',2)) Asc ";
                oPaging.UserName = SessionProperty.UserName;
                oPaging.PagingData();
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.ImageProcess.Lock",
                    ClassName = "ImageLockPaging",
                    FunctionName = "ImageLockPaging",
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
