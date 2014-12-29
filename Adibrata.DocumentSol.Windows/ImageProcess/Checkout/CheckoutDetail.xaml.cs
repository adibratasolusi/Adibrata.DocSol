using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Controller;
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

namespace Adibrata.DocumentSol.Windows.ImageProcess.Checkout
{
    /// <summary>
    /// Interaction logic for CheckoutDetail.xaml
    /// </summary>
    public partial class CheckoutDetail : Page
    {
        SessionEntities SessionProperty = new SessionEntities();
        public CheckoutDetail(SessionEntities _session)
        {
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
                    NameSpace = "Adibrata.DocumentSol.Windows.ImageProcess.Checkout",
                    ClassName = "CheckoutDetail",
                    FunctionName = "CheckoutDetail",
                    ExceptionNumber = 1,
                    EventSource = "CheckoutDetail",
                    ExceptionObject = _exp,
                    EventID = 200, // 70 Untuk User Management
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
                #endregion
            }
        }

        private void btnCheckout_Click(object sender, RoutedEventArgs e)
        {
            DocSolEntities _ent = new DocSolEntities
            {
                MethodName = "DocTransCheckOut",
                ClassName = "ImageProcess"
            };
            _ent.Id = Convert.ToInt64(SessionProperty.ReffKey);
            _ent.UserName = SessionProperty.UserName;

            DocumentSolutionController.DocSolProcess<string>(_ent);
            MessageBox.Show("Check Out Succes");
            RedirectPage redirect = new RedirectPage(this, "ImageProcess.Checkout.CheckoutPaging", SessionProperty);
        
       
        }
    }
}
