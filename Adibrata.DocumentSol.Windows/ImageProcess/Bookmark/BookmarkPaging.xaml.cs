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

namespace Adibrata.DocumentSol.Windows.ImageProcess.Bookmark
{
    /// <summary>
    /// Interaction logic for BookmarkPaging.xaml
    /// </summary>
    public partial class BookmarkPaging : Page
    {
        SessionEntities SessionProperty;
        public BookmarkPaging(SessionEntities _session)
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
                    NameSpace = "Adibrata.DocumentSol.Windows.ImageProcess.Unlock",
                    ClassName = " WaterMarkPaging",
                    FunctionName = "WaterMarkPaging",
                    ExceptionNumber = 1,
                    EventSource = "ImageWaterMark",
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
                oPaging.ClassName = "ImageProcessPaging";
                oPaging.MethodName = "UnlockPaging";
                //"DeleteDocumentPaging"
                oPaging.dgObj = dgPaging;
                if (txtTransId.Text != "")
                {
                    sb.Append(" And ");
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
                if (txtDocType.Text != "")
                {
                    sb.Append(" And ");
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
                    NameSpace = "Adibrata.DocumentSol.Windows.ImageProcess.Unlock",
                    ClassName = "UnlockImagePaging",
                    FunctionName = "UnlockImagePaging",
                    ExceptionNumber = 1,
                    EventSource = "UnlockImage",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        private void btnBookmark_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}