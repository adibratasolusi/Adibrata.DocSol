using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Framework.Logging;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Adibrata.Windows.UserController;
using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.Controller;
using System.Data;
using System.Collections.Generic;

namespace Adibrata.DocumentSol.Windows.Project
{
    /// <summary>
    /// Interaction logic for ProjectAddEdit.xaml
    /// </summary>
    public partial class ProjectAddEdit : Page
    {
        SessionEntities SessionProperty = new SessionEntities();
        string _custcode;
        public ProjectAddEdit(SessionEntities _session)
        {
            try
            {
                InitializeComponent();
                this.DataContext = new MainVM(new Shell());
                SessionProperty = _session;
                DocSolEntities _ent = new DocSolEntities();
                DataTable _dt = new DataTable();
                _ent.ClassName = "ProjectRegistrasi";
                _ent.MethodName = "ProjectTypeReceive";
                _dt = DocumentSolutionController.DocSolProcess<DataTable>(_ent);
                List<string> data = new List<string>();
                if (_dt.Rows.Count > 0)
                {
                    foreach (DataRow _row in _dt.Rows)
                    {
                        data.Add(_row["Result"].ToString());
                    }
                }

                cboProjectType.ItemsSource = data;

                if (_session.IsEdit)
                {
                    _ent.ClassName = "ProjectRegistrasi";
                    _ent.MethodName = "ProjectRegistrasiView";
                    _ent.ProjectCode = _session.ReffKey;
                    _ent = DocumentSolutionController.DocSolProcess<DocSolEntities>(_ent);
                    lblCustomerCode.Text = _ent.CustomerCode;
                    lblCustomerName.Text = _ent.CompanyName;
                    lblDescProjCode.Text = "Project Code";
                    lblProjectCode.Text = _session.ReffKey;
                    txtPrjectName.Text = _ent.ProjectName;
                    
                    for (int i = 0; i <= cboProjectType.Items.Count; i++)
                    {

                        cboProjectType.SelectedIndex = i;
                        
                        if (cboProjectType.SelectedValue.ToString() == _ent.ProjectType)
                        {
                            
                            break;
                        }
                    }

                }
                else
                {
                    _ent.ClassName = "CustomerRegistrasi";
                    _ent.MethodName = "CustomerCompanyRegistrasiView";
                    _ent.CustomerCode = _session.ReffKey;

                    _ent = DocumentSolutionController.DocSolProcess<DocSolEntities>(_ent);
                    lblCustomerCode.Text = _session.ReffKey;
                    lblCustomerName.Text = _ent.CompanyName;
                }

            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.Project",
                    ClassName = "ProjectAddEdit",
                    FunctionName = "ProjectAddEdit",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ( cboProjectType.SelectedItem == null)
                { MessageBox.Show("Please Select Project Type"); }
                else
                    if (txtPrjectName.Text == "")
                    { MessageBox.Show("Please Enter Project Name"); }
                    else
                    {
                        DocSolEntities _ent = new DocSolEntities { ProjectName = txtPrjectName.Text, ProjectType = cboProjectType.SelectedItem.ToString() };
                        _ent.MethodName = "ProjectRegistrasiAdd";
                        _ent.ClassName = "ProjectRegistrasi";
                        _ent.UserLogin = SessionProperty.UserName;
                        _ent.CustomerCode = SessionProperty.ReffKey;
                        _ent.IsEdit = SessionProperty.IsEdit;
                        _ent.ProjectCode = SessionProperty.ReffKey;
                        DocumentSolutionController.DocSolProcess<string>(_ent);
                        SessionProperty.ReffKey = lblCustomerCode.Text;
                        RedirectPage redirect = new RedirectPage(this, "Project.ProjectPaging", SessionProperty);
                    }
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.Project",
                    ClassName = "ProjectPaging",
                    FunctionName = "btnEdit_Click",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SessionProperty.ReffKey = _custcode;
                RedirectPage redirect = new RedirectPage(this, "Project.ProjectPaging", SessionProperty);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.Project",
                    ClassName = "ProjectPaging",
                    FunctionName = "btnEdit_Click",
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
