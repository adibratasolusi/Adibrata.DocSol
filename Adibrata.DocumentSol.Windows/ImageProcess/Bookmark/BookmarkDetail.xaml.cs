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
    /// Interaction logic for BookmarkDetail.xaml
    /// </summary>
    public partial class BookmarkDetail : Page
    {

        SessionEntities SessionProperty;
        public BookmarkDetail(SessionEntities _session)
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
                    NameSpace = "Adibrata.DocumentSol.Windows.ImageProcess.Bookmark",
                    ClassName = " BookmarkDetail",
                    FunctionName = "BookmarkDetail",
                    ExceptionNumber = 1,
                    EventSource = "BookmarkDetail",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        private void btnBookmark_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                RedirectPage redirect = new RedirectPage(this, "ImageProcess.Bookmark.BookmarkPaging", SessionProperty);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.ImageProcess.Bookmark",
                    ClassName = " BookmarkDetail",
                    FunctionName = "btnBookmark_Click",
                    ExceptionNumber = 1,
                    EventSource = "BookmarkDetail",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

            RedirectPage redirect = new RedirectPage(this, "ImageProcess.Bookmark.BookmarkPaging", SessionProperty);
        }
    }
}
