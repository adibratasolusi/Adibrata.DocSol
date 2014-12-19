using Adibrata.BusinessProcess.DocumentSol.Entities;
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

namespace Adibrata.DocumentSol.Windows.UploadInquiry
{
    /// <summary>
    /// Interaction logic for UploadInquiry.xaml
    /// </summary>
    public partial class UploadInquiry : Page
    {
        SessionEntities SessionProperty;
        DocSolEntities _ent = new DocSolEntities();
        public UploadInquiry(SessionEntities _session)
        {
            InitializeComponent();
            this.DataContext = new MainVM(new Shell());
            SessionProperty = _session;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder(8000);
            try
            {
                oPaging.ClassName = "UploadProcessPaging";
                oPaging.MethodName = "UploadInquiry";
                oPaging.dgObj = dgPaging;
                if (txtTransId.Text != "")
                {
                    sb.Append(" Where ");
                    if (txtTransId.Text.Contains("%"))
                    {
                        sb.Append(" TransId LIKE '");
                    }
                    else
                    {
                        sb.Append(" TransId = '");
                    }
                    sb.Append(txtTransId.Text);
                    sb.Append("'");
                }
                else
                {
                    sb.Append("");
                }
                oPaging.WhereCond = sb.ToString();
                oPaging.SortBy = " TransId Asc ";
                oPaging.UserName = SessionProperty.UserName;
                oPaging.PagingData();
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.UploadInquiry",
                    ClassName = "UploadInquiry",
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
                RedirectPage redirect = new RedirectPage(this, "UploadInquiry.UploadDetailInquiry", SessionProperty);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.UploadInquiry",
                    ClassName = "DocumentUploadPaging",
                    FunctionName = "btnUpload_Click",
                    ExceptionNumber = 1,
                    EventSource = "UploadInquiry",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }
    }
}
