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

namespace Adibrata.DocumentSol.Windows.DocumentMaintenance
{
    /// <summary>
    /// Interaction logic for DeleteDocumentInquiryPaging.xaml
    /// </summary>
    public partial class DeleteDocumentInquiryPaging : Page
    {
        SessionEntities SessionProperty;
        public DeleteDocumentInquiryPaging(SessionEntities _session)
        {
            
          
            try
            {
                InitializeComponent();
                this.DataContext = new MainVM(new Shell());
                SessionProperty = _session;
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.DocumentContent",
                    ClassName = "DeleteDocumentInquiryPaging",
                    FunctionName = "DeleteDocumentInquiryPaging",
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

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

              StringBuilder sb = new StringBuilder(8000);
            try
            {
                oPaging.ClassName = "DeleteDocument";
                oPaging.MethodName = "DeleteDocumentPaging";
                //"DeleteDocumentPaging"
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
                    NameSpace = "Adibrata.DocumentSol.Windows.DocumentContent",
                    ClassName = "DocumentUploadPaging",
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

        private void btnDetail_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
