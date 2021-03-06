﻿using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Controller;
using Adibrata.Framework.Logging;
using Adibrata.Framework.Messaging;
using Adibrata.Windows.UserController;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Adibrata.DocumentSol.Windows.Archiving
{
    /// <summary>
    /// Interaction logic for Execution.xaml
    /// </summary>
    public partial class Execution : Page
    {
        List<string> listCode = new List<string>();
        public class DataItem
        {
            public string DocTransCode { get; set; }
            public string DocTypeCode { get; set; }
            public string TransID { get; set; }
            public string Id { get; set; }
        }
        SessionEntities SessionProperty = new SessionEntities();
        public Execution(SessionEntities _session)
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
                    ClassName = "Execution",
                    FunctionName = "Execution",
                    ExceptionNumber = 1,
                    EventSource = "Execution",
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

            //DataGridCell cellId = oDataGrid.GetCell(i, 2);
            //TextBlock tbId = oDataGrid.GetVisualChild<TextBlock>(cellId);

            DataGridCell cellDocTransCode = oDataGrid.GetCell(i, 2);
            TextBlock tbDocTransCode = oDataGrid.GetVisualChild<TextBlock>(cellDocTransCode);

            DataGridCell cellDocTypeCode = oDataGrid.GetCell(i, 3);
            TextBlock tbDocTypeCode = oDataGrid.GetVisualChild<TextBlock>(cellDocTypeCode);
            if (!listCode.Contains(tbDocTransCode.Text))
            {
                if (listCode.Count == 0)
                {
                    gbQueue.Visibility = Visibility.Visible;
                }

                listCode.Add(tbDocTransCode.Text);
                dgQueue.Items.Add(new DataItem { DocTransCode = tbDocTransCode.Text, DocTypeCode = tbDocTypeCode.Text,  });
                dgQueue.Items.Refresh();
            }
            else
            {
                MessageBox.Show(tbDocTransCode.Text + "-" + tbDocTypeCode.Text + " already in queue");
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
                RedirectPage redirect = new RedirectPage(this, "Archiving.ExecutionDetail", SessionProperty);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.Archiving",
                    ClassName = "Execution",
                    FunctionName = "btnDetail_Click",
                    ExceptionNumber = 1,
                    EventSource = "Archiving",
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
                oPaging.MethodName = "ArchievingExecution";
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
                    ClassName = "Execution",
                    FunctionName = "btnSearch_Click",
                    ExceptionNumber = 1,
                    EventSource = "Archieve",
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

            DataGridCell cellCode = oDataGrid.GetCell(i, 1);
            TextBlock tbCode = oDataGrid.GetVisualChild<TextBlock>(cellCode);
            listCode.Remove(tbCode.Text);
            dgQueue.Items.RemoveAt(dgQueue.SelectedIndex);
            dgQueue.Items.Refresh();
            gbQueueVisibleCheck();
        }


        private void btnExec_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                for (int i = 0; i < listCode.Count; i++)
                {
                    DocSolEntities _ent = new DocSolEntities();
                    _ent.ClassName = "UploadProcess";
                    _ent.MethodName = "DocTransGetTransID";
                    _ent.DocTransCode = listCode[i];
                    _ent.UserLogin = SessionProperty.UserName;
                    Int64 _transid = DocumentSolutionController.DocSolProcess<Int64>(_ent);


                    WCFEntities oWcf = new WCFEntities();
                    oWcf.DocTransID = _transid;
                    oWcf.UserName = SessionProperty.UserName;
                    MessageToWCF.ArchieveExecProcess(oWcf);

                }
                
                MessageBox.Show("Document Archieve Execution Success");
                RedirectPage redirect = new RedirectPage(this, "Archiving.Execution", SessionProperty);
            }
            catch (Exception _exp)
            {

                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.Archiving",
                    ClassName = "Execution",
                    FunctionName = "btnExec_Click",
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
