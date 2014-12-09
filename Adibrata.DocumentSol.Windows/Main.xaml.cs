using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Framework.Logging;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Adibrata.Windows.UserController;


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
            InitializeComponent();
            SessionProperty = _session;
            lblLoginName.Text = SessionProperty.UserName.ToUpper();
            lblBusinessDate.Text = DateTime.Now.ToString("dd/MMMM/yyyy");
            RedirectPage redirect = new RedirectPage(frmWorksheet, "Form.FormRegistrasiPaging", SessionProperty);
        }

        private void TreeMenuGenerate()
        {

        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {

        }

        private void hpMenu_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
