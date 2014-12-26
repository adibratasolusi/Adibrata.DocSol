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

namespace Adibrata.DocumentSol.Windows.Archiving
{
    /// <summary>
    /// Interaction logic for InquiryFinishDetail.xaml
    /// </summary>
    public partial class InquiryFinishDetail : Page
    {
        SessionEntities SessionProperty = new SessionEntities();
        public InquiryFinishDetail(SessionEntities _session)
        {
            //Lampar ke detail
            try
            {
                InitializeComponent();
                this.DataContext = new MainVM(new Shell());
                SessionProperty = _session;
                ucView.Session = SessionProperty;
                ucView.DocTransId = Convert.ToInt64(SessionProperty.ReffKey);


            }
            catch (Exception _exp)
            {
                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.Archiving",
                    ClassName = "InquiryFinishDetail",
                    FunctionName = "InquiryFinishDetail",
                    ExceptionNumber = 1,
                    EventSource = "Archieve",
                    ExceptionObject = _exp,
                    EventID = 200, // 70 Untuk User Management
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
                #endregion
            }
        }


        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

            RedirectPage redirect = new RedirectPage(this, "Archiving.InquiryFinish", SessionProperty);
        }

    }
}
