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

namespace Adibrata.DocumentSol.Windows.StorageMonitoring.FileStorageSize
{
    /// <summary>
    /// Interaction logic for FileStoragePaging.xaml
    /// </summary>
    public partial class FileStoragePaging : Page
    {

        SessionEntities SessionProperty = new SessionEntities();
        public FileStoragePaging(SessionEntities _session)
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
                oPaging.ClassName = "FileStorage";
                oPaging.MethodName = "FileStoragePaging";
                //"DeleteDocumentPaging"
                oPaging.dgObj = dgPaging;
                if (txtDocTransCode.Text != "" || txtDocType.Text != "" || txtFilename.Text != "")
                {
                    if (txtDocTransCode.Text != "")
                    {

                        sb.Append(" and ");
                        if (txtDocTransCode.Text.Contains("%"))
                        {
                            sb.Append(" DocTransCode LIKE '");
                        }
                        else
                        {
                            sb.Append(" DocTransCode = '");
                        }
                        sb.Append(txtDocTransCode.Text);
                        sb.Append("'");
                    }

                    if (txtDocType.Text != "")
                    {
                        sb.Append(" and ");
                        if (txtDocType.Text.Contains("%"))
                        {
                            sb.Append(" DocTypeCode LIKE '");
                        }
                        else
                        {
                            sb.Append(" DocTypeCode = '");
                        }
                        sb.Append(txtDocType.Text);
                        sb.Append("'");
                    }

                    if (txtFilename.Text != "")
                    {
                        sb.Append(" and ");
                        if (txtFilename.Text.Contains("%"))
                        {
                            sb.Append(" FileName LIKE '");
                        }
                        else
                        {
                            sb.Append(" FileName = '");
                        }
                        sb.Append(txtFilename.Text);
                        sb.Append("'");
                    }

                }
                
                else
                {
                    sb.Append("");
                }
                oPaging.WhereCond = sb.ToString();
                oPaging.SortBy = " DocTransCode Asc ";
                oPaging.UserName = SessionProperty.UserName;
                oPaging.PagingData();
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.StorageMonitoring.FileStorageSize",
                    ClassName = " FileStoragePaging",
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
