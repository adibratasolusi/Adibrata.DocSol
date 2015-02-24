using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Framework.Messaging;
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

namespace Adibrata.DocumentSol.Windows.WorkflowApproval
{
    /// <summary>
    /// Interaction logic for WorkflowApprovalPaging.xaml
    /// </summary>
    public partial class WorkflowApprovalPaging : Page
    {
        SessionEntities SessionProperty = new SessionEntities();
        String nextstep;
        public WorkflowApprovalPaging(SessionEntities _session)
        {
            InitializeComponent();
            this.DataContext = new MainVM(new Shell());
            SessionProperty = _session;

        }

        private void btnApprove_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            WCFEntities oEnt = new WCFEntities();
            oEnt.BranchId =Convert.ToInt64(txtBranch.Text);
            oEnt.RoleId = Convert.ToInt64(txtRole.Text);
            oEnt.Status = txtStatus.Text;
            dgPaging.ItemsSource = MessageToWCF.DocTransApproval(oEnt).DefaultView;
            nextstep = MessageToWCF.DocTransApprovalGetNextStep();
        }
    }
}
