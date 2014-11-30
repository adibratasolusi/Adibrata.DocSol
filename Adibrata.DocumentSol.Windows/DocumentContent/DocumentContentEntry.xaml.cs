using System.Windows.Controls;
using Adibrata.Framework.Logging;
using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Controller;
using Adibrata.BusinessProcess.DocumentSol.Entities;
using System.Data;
using System;
using Adibrata.Windows.UserController;
using Adibrata.Framework.Logging;

namespace Adibrata.DocumentSol.Windows.DocumentContent
{
    /// <summary>
    /// Interaction logic for DocumentContentEntry.xaml
    /// </summary>
    public partial class DocumentContentEntry : Page
    {
        DocSolEntities _ent = new DocSolEntities();
        SessionEntities SessionProperty = new SessionEntities();

        public DocumentContentEntry(SessionEntities _session)
        {
            try
            {
                InitializeComponent();
                this.DataContext = new MainVM(new Shell());
                DataTable _dt = new DataTable();

                _ent.ClassName = "DocType";
                _ent.MethodName = "DocTypeRetrieve";
                _ent.LineOfBusiness = "Consumer Finance";
                _dt = DocumentSolutionController.DocSolProcess<DataTable>(_ent);
                
                cboDocumentType.ItemsSource = _dt.DefaultView;
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
                oDocContent.DocumentType = cboDocumentType.SelectedValue.ToString();
                TextBlock txtInput = (TextBlock)this.cboDocumentType.FindName("txtValue");
               
                oDocContent.GenerateControls();
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

    }
}
