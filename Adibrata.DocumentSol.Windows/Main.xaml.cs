using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Framework.Logging;
using System;
using System.Windows;
using System.Windows.Controls;


namespace Adibrata.DocumentSol.Windows
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        SessionEntities SessionProperty = new SessionEntities();
        public Main(SessionEntities _session)
        {
            try
            {
                InitializeComponent();
                this.DataContext = new Adibrata.Windows.UserController.MainVM(new Shell());
                SessionProperty = _session;
                lblLoginName.Text = SessionProperty.UserName.ToUpper();
                lblBusinessDate.Text = DateTime.Now.ToString("dd/MMMM/yyyy");
                frmWorksheet.NavigationService.Navigate(new  DocumentContent.SearchDocument (SessionProperty));
                frmMenu.NavigationService.Navigate(new MenuTree(_session,frmWorksheet));
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows",
                    ClassName = "Main",
                    FunctionName = "Main",
                    ExceptionNumber = 1,
                    EventSource = "Main",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
            //RedirectPage redirect = new RedirectPage(frmWorksheet, "Form.FormRegistrasiPaging", SessionProperty);
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                this.NavigationService.Navigate(new LogOutScreen(SessionProperty)); 
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows",
                    ClassName = "Main",
                    FunctionName = "btnLogout_Click",
                    ExceptionNumber = 1,
                    EventSource = "Main",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

    }
}
