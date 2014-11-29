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
        SessionEntities SessionProperty;
        public Main(SessionEntities _session)
        {
            InitializeComponent();
            RedirectPage redirect = new RedirectPage(frmWorksheet, "Customer.CustomerPaging", _session);

        }
    }
}
