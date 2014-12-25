using Adibrata.BusinessProcess.Entities.Base;
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

namespace Adibrata.DocumentSol.Windows.UserManagement.UserFormRegistration
{
    /// <summary>
    /// Interaction logic for UserFormRegistration.xaml
    /// </summary>
    public partial class UserFormRegistration : Page
    {

        SessionEntities SessionProperty = new SessionEntities();
        public UserFormRegistration(SessionEntities _session)
        {
            InitializeComponent();
            this.DataContext = new MainVM(new Shell());
            SessionProperty = _session;

        }
    }
}
