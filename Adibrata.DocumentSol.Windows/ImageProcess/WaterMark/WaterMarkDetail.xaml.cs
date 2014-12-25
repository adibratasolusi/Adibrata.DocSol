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

namespace Adibrata.DocumentSol.Windows.ImageProcess.WaterMark
{
    /// <summary>
    /// Interaction logic for WaterMarkDetail.xaml
    /// </summary>
    public partial class WaterMarkDetail : Page
    {
        SessionEntities SessionProperty = new SessionEntities();
        public WaterMarkDetail(SessionEntities _session)
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
                    NameSpace = "Adibrata.DocumentSol.Windows.ImageProcess.WaterMark",
                    ClassName = "WaterMarkDetail",
                    FunctionName = "WaterMarkDetail",
                    ExceptionNumber = 1,
                    EventSource = "WaterMarkDetail",
                    ExceptionObject = _exp,
                    EventID = 200, // 70 Untuk User Management
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
                #endregion
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

       
    }
}
