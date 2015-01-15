using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Controller;
using Adibrata.Framework.Logging;
using Adibrata.Windows.UserController;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Adibrata.DocumentSol.Windows.ImageProcess.Lock
{
    /// <summary>
    /// Interaction logic for ImageLockDetail.xaml
    /// </summary>
    public partial class ImageLockDetail : Page
    {

        SessionEntities SessionProperty = new SessionEntities();
        Int64 id;
        public ImageLockDetail(SessionEntities _session)
        {
            try
            {
                InitializeComponent();
                this.DataContext = new MainVM(new Shell());
                SessionProperty = _session;
                string _doctranscode = SessionProperty.ReffKey;
            
                ucView.Session = SessionProperty;
                ucView.DocTransCode = SessionProperty.ReffKey ;
                id = ucView.DocTransId;
         

            }
            catch (Exception _exp)
            {
                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.ImageProcess.Lock",
                    ClassName = "ImageLockDetail",
                    FunctionName = "ImageLockDetail",
                    ExceptionNumber = 1,
                    EventSource = "ImageDetail",
                    ExceptionObject = _exp,
                    EventID = 200, // 70 Untuk User Management
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
                #endregion
            }
        }

      

        private void btnLock_Click(object sender, RoutedEventArgs e)
        {
            DocSolEntities _ent = new DocSolEntities
            {
                MethodName = "ImageStatusLocked",
                ClassName = "ImageProcess"
            };
            _ent.Id = Convert.ToInt64(SessionProperty.ReffKey);

            DocumentSolutionController.DocSolProcess<string>(_ent);
            MessageBox.Show("Sukses");
            RedirectPage redirect = new RedirectPage(this, "ImageProcess.Lock.ImageLockPaging", SessionProperty);
        
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

            RedirectPage redirect = new RedirectPage(this, "ImageProcess.Lock.ImageLockPaging", SessionProperty);
        }
    }
}
