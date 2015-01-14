using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Controller;
using Adibrata.Framework.Logging;
using Adibrata.Windows.UserController;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Adibrata.DocumentSol.Windows.Archiving
{
    /// <summary>
    /// Interaction logic for Approval.xaml
    /// </summary>
    public partial class Approval : Page
    {
        List<string> listId = new List<string>();
        public class DataItem
        {
            public string DocTransCode { get; set; }
            public string DocTypeCode { get; set; }
            public string TransID { get; set; }
            public string Id { get; set; }
        }

        SessionEntities SessionProperty = new SessionEntities();
        public Approval(SessionEntities _session)
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
                    ClassName = "Approval",
                    FunctionName = "Approval",
                    ExceptionNumber = 1,
                    EventSource = "Approval",
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

        private void btnQueue_Click(object sender, RoutedEventArgs e)
        {
            int i = dgPaging.SelectedIndex;
            DataGridHelper oDataGrid = new DataGridHelper();
            oDataGrid.dtg = dgPaging;

            DataGridCell cellId = oDataGrid.GetCell(i, 2);
            TextBlock tbId = oDataGrid.GetVisualChild<TextBlock>(cellId);

            //DataGridCell cellDocTransCode = oDataGrid.GetCell(i, 2);
            //TextBlock tbDocTransCode = oDataGrid.GetVisualChild<TextBlock>(cellDocTransCode);

            DataGridCell cellDocTypeCode = oDataGrid.GetCell(i, 3);
            TextBlock tbDocTypeCode = oDataGrid.GetVisualChild<TextBlock>(cellDocTypeCode);
            if (!listId.Contains(tbId.Text))
            {
                if (listId.Count == 0)
                {
                    gbQueue.Visibility = Visibility.Visible;
                }

                listId.Add(tbId.Text);
                dgQueue.Items.Add(new DataItem { DocTransCode = tbId.Text, DocTypeCode = tbDocTypeCode.Text, });
                dgQueue.Items.Refresh();
            }
            else
            {
                MessageBox.Show(tbId.Text + "-" + tbDocTypeCode.Text + " already in queue");
            }

        }
        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int i = dgPaging.SelectedIndex;

                DataGridHelper oDataGrid = new DataGridHelper();
                oDataGrid.dtg = dgPaging;
                DataGridCell cell = oDataGrid.GetCell(i, 2);
                TextBlock ReffKey = oDataGrid.GetVisualChild<TextBlock>(cell); // pass the DataGridCell as a parameter to GetVisualChild
                SessionProperty.IsEdit = true;
                SessionProperty.ReffKey = ReffKey.Text;
                RedirectPage redirect = new RedirectPage(this, "Archiving.ApprovalDetail", SessionProperty);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.Archiving",
                    ClassName = "UnlockImagePaging",
                    FunctionName = "btnUnlock_Click",
                    ExceptionNumber = 1,
                    EventSource = "ImageUnlock",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }


        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder(8000);
            try
            {
                oPaging.ClassName = "ArchivingPaging";
                oPaging.MethodName = "ArchievingApproval";
                //"DeleteDocumentPaging"
                oPaging.dgObj = dgPaging;
                if (txtDocTransCode.Text != "")
                {
                    sb.Append(" And ");
                    if (txtDocTransCode.Text.Contains("%"))
                    {
                        sb.Append(" DocTransCode LIKE '");
                    }
                    else
                    {
                        sb.Append(" DocTransCode = '");
                    }
                    sb.Append(txtDocTransCode.Text);
                    sb.Append("'");
                }
                if (txtDocType.Text != "")
                {
                    sb.Append(" And ");
                    if (txtDocType.Text.Contains("%"))
                    {
                        sb.Append(" DocTypeCode LIKE '");
                    }
                    else
                    {
                        sb.Append(" DocTypeCode = '");
                    }
                    sb.Append(txtDocType.Text);
                    sb.Append("'");
                }
                else
                {
                    sb.Append("");
                }
                oPaging.WhereCond = sb.ToString();
                oPaging.SortBy = " DocTransCode Asc ";
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


        private void btnApprove_Click(object sender, RoutedEventArgs e)
        {
            approvalProcess("A"); 
        }

        private void btnReject_Click(object sender, RoutedEventArgs e)
        {

            approvalProcess("R");
        }

        private void btnHold_Click(object sender, RoutedEventArgs e)
        {

            approvalProcess("H");
        }

        private void approvalProcess(string statusApproval)
        {
            try
            {

                DocSolEntities _ent = new DocSolEntities
                {
                    MethodName = "ArchieveApprovalQueueProcess",
                    ClassName = "ArchieveProcess"
                };
                _ent.ListArchieve = listId;
                _ent.ApprovalStatus = statusApproval; //1 = di approve, 2 = Hold, 3 = reject
                DocumentSolutionController.DocSolProcess<string>(_ent);
                MessageBox.Show("Document Approval Archieve Success");
                RedirectPage redirect = new RedirectPage(this, "Archiving.Approval", SessionProperty);
            }
            catch (Exception _exp)
            {

                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.Archiving",
                    ClassName = "Approval",
                    FunctionName = "approvalProcess",
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
    }
}