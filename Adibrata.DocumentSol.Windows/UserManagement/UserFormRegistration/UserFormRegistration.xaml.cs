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

namespace Adibrata.DocumentSol.Windows.UserManagement.UserFormRegistration
{
    /// <summary>
    /// Interaction logic for UserFormRegistration.xaml
    /// </summary>
    public partial class UserFormRegistration : Page
    {
        List<string> listId = new List<string>();
        public class DataItem
        {
            public string ID { get; set; }
            public string FormName { get; set; }

        }
        SessionEntities SessionProperty = new SessionEntities();
        public UserFormRegistration(SessionEntities _session)
        {

            try
            {
                InitializeComponent();
                this.DataContext = new MainVM(new Shell());
                SessionProperty = _session;
                gbQueue.Visibility = Visibility.Hidden;
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.Archiving",
                    ClassName = "Prepare",
                    FunctionName = "Prepare",
                    ExceptionNumber = 1,
                    EventSource = "Prepare",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }

        }
        void gbQueueVisibleCheck()
        {
            if (dgQueue.Items.Count == 0)
            {
                gbQueue.Visibility = Visibility.Hidden;
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder(8000);
            try
            {
                oPaging.ClassName = "UserFormRegistration";
                oPaging.MethodName = "UserFormRegistrationPaging";
                //"DeleteDocumentPaging"
                oPaging.dgObj = dgPaging;
                if (txtKeyword.Text != "")
                {
                    sb.Append(" And ");
                    if (txtKeyword.Text.Contains("%"))
                    {
                        sb.Append(" FormName LIKE '");
                    }
                    else
                    {
                        sb.Append(" FormName = '");
                    }
                    sb.Append(txtKeyword.Text);
                    sb.Append("'");
                }

                else
                {
                    sb.Append("");
                }
                oPaging.WhereCond = sb.ToString();
                oPaging.SortBy = " FormName Asc ";
                oPaging.UserName = SessionProperty.UserName;
                oPaging.PagingData();
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.Archiving",
                    ClassName = "Prepare",
                    FunctionName = "btnSearch_Click",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int i = dgQueue.SelectedIndex;
            DataGridHelper oDataGrid = new DataGridHelper();
            oDataGrid.dtg = dgQueue;

            DataGridCell cellId = oDataGrid.GetCell(i, 1);
            TextBlock tbId = oDataGrid.GetVisualChild<TextBlock>(cellId);
            listId.Remove(tbId.Text);
            dgQueue.Items.RemoveAt(dgQueue.SelectedIndex);
            dgQueue.Items.Refresh();
            gbQueueVisibleCheck();
        }



        private void btnAddAcces_Click(object sender, RoutedEventArgs e)
        {
            int i = dgPaging.SelectedIndex;
            DataGridHelper oDataGrid = new DataGridHelper();
            oDataGrid.dtg = dgPaging;

            DataGridCell cellId = oDataGrid.GetCell(i, 1);
            TextBlock tbId = oDataGrid.GetVisualChild<TextBlock>(cellId);

            DataGridCell cellFormName = oDataGrid.GetCell(i, 2);
            TextBlock tbFormName = oDataGrid.GetVisualChild<TextBlock>(cellFormName);

           if (!listId.Contains(tbId.Text))
            {
                if (listId.Count == 0)
                {
                    gbQueue.Visibility = Visibility.Visible;
                }

                listId.Add(tbId.Text);
                dgQueue.Items.Add(new DataItem { ID = tbId.Text,FormName = tbFormName.Text });
                dgQueue.Items.Refresh();
            }
            else
            {
                MessageBox.Show(tbId.Text + "-" + tbFormName.Text + " already in queue");
            }

        }

   

        private void btnGrantAccess_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
