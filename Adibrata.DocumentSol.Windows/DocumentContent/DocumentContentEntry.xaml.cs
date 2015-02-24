using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Controller;
using Adibrata.Framework.Logging;
using Adibrata.Windows.UserController;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Controls;
using System.Windows;

namespace Adibrata.DocumentSol.Windows.DocumentContent
{
    /// <summary>
    /// Interaction logic for DocumentContentEntry.xaml
    /// </summary>
    public partial class DocumentContentEntry : Page
    {
        DocSolEntities _ent = new DocSolEntities();
        SessionEntities SessionProperty = new SessionEntities();
        string _custcode;
        public DocumentContentEntry(SessionEntities _session)
        {
            try
            {
                InitializeComponent();
                this.DataContext = new MainVM(new Shell());
                SessionProperty = _session;
                DataTable _dt = new DataTable();
                _ent.ClassName = "ProjectRegistrasi";
                _ent.MethodName = "ProjectRegistrasiView";
                _ent.ProjectCode = _session.ReffKey;
                _ent = DocumentSolutionController.DocSolProcess<DocSolEntities>(_ent);
                lblCustomerCode.Text = _ent.CustomerCode;
                _custcode = _ent.CustomerCode;
                lblCustomerName.Text = _ent.CompanyName;
                ucUpload.Visibility = Visibility.Hidden;
                lblProjectCode.Text = _session.ReffKey;
                lblProjectName.Text = _ent.ProjectName;
                lblProjectType.Text = _ent.ProjectType;
                _ent.ClassName = "DocType";
                _ent.MethodName = "DocTypeRetrieve";
                _ent.LineOfBusiness = _ent.ProjectType;
                _dt = DocumentSolutionController.DocSolProcess<DataTable>(_ent);

                //List<string> data = new List<string>();
                //if (_dt.Rows.Count > 0)
                //{
                //    foreach (DataRow _row in _dt.Rows)
                //    {
                //        data.Add(_row["Result"].ToString());
                //    }
                //}

                cboDocumentType.ItemsSource = _dt.DefaultView;
                oApproval.Visibility = System.Windows.Visibility.Hidden;
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.DocumentContent",
                    ClassName = "DocumentContentEntry",
                    FunctionName = "DocumentContentEntry",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        private void cboDocumentType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            try
            {
                oDocContent.DocumentType = ((DataRowView)cboDocumentType.SelectedItem)["Result"].ToString();  //cboDocumentType.SelectedValue.ToString();
               
                oDocContent.GenerateControls();
                cboDocumentType.IsEnabled = false;
                

                ucUpload.DocumentType = oDocContent.DocumentType;

                ucUpload.UserLogin = SessionProperty.UserName;
                ucUpload.TransCode = SessionProperty.ReffKey;
                ucUpload.BindingValueMax();
                oApproval.DocumentType = oDocContent.DocumentType;
                ucUpload.Visibility = Visibility.Visible;
                _ent.ClassName = "ApprovalProcess";
                _ent.MethodName = "ApprovalRequestVisibility";
                _ent.ProjectCode = SessionProperty.ReffKey;
                _ent.DocumentType = oDocContent.DocumentType;
                if (DocumentSolutionController.DocSolProcess<Boolean>(_ent))
                {
                    oApproval.Visibility = Visibility.Visible;
                    _ent.DocContentNeedApproval = true;
                }
                else
                {
                    oApproval.Visibility = Visibility.Hidden;
                    _ent.DocContentNeedApproval = false;
                }

                //oApproval.Visibility = false;
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.DocumentContent",
                    ClassName = "DocumentContentEntry",
                    FunctionName = "cboDocumentType_SelectionChanged",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        private void btnBack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                SessionProperty.ReffKey = lblCustomerCode.Text;
                RedirectPage redirect = new RedirectPage(this, "DocumentContent.DocumentUploadPaging", SessionProperty);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.DocumentContent",
                    ClassName = "DocumentContentEntry",
                    FunctionName = "btnBack_Click",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        private void btnSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                DataTable dtContent = new DataTable();
                dtContent = oDocContent.RetrieveValue();
                
                _ent.DtContent = dtContent;
                _ent.UserLogin = SessionProperty.UserName;
                ucUpload.DocumentTypeUpload = cboDocumentType.Text;
                _ent.ApprovalNotes = oApproval.Notes;
                _ent.RequestTo = oApproval.RequestTo;
                _ent.MaturityDt = dpMaturityDt.SelectedDate.Value;
                ucUpload.CheckAndUpload(_ent);
                
                RedirectPage redirect = new RedirectPage(this, "DocumentContent.DocumentUploadPaging", SessionProperty);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.Customer",
                    ClassName = "DocumentContentEntry",
                    FunctionName = "btnBack_Click",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

    }
}
