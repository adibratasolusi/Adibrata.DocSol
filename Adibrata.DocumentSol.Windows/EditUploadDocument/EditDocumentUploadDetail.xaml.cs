using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Controller;
using Adibrata.Framework.Logging;
using Adibrata.Windows.UserController;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Adibrata.DocumentSol.Windows.EditUploadDocument
{
    /// <summary>
    /// Interaction logic for EditDocumentUploadDetail.xaml
    /// </summary>
    public partial class EditDocumentUploadDetail : Page
    {
        DocSolEntities _ent = new DocSolEntities();
        SessionEntities SessionProperty = new SessionEntities();
        //string _custcode;
        public EditDocumentUploadDetail(SessionEntities _session)
        {
            try
            {
                DocSolEntities _ent = new DocSolEntities();
                InitializeComponent();
                this.DataContext = new MainVM(new Shell());
                SessionProperty = _session;
                string _doctranscode = SessionProperty.ReffKey;
                _ent.ClassName = "UploadProcess";
                _ent.MethodName = "DocTransGetTransID";
                _ent.DocTransCode = SessionProperty.ReffKey;
                _ent.UserName = SessionProperty.UserName;
                //_ent.DocTransId = Convert.ToInt64(SessionProperty.ReffKey);
                SessionProperty.ReffKey = Convert.ToString(DocumentSolutionController.DocSolProcess<Int64>(_ent));


                setScreen();

                GenerateControls();

                //txtDocTransId.Text = _ent.DocTransCode;

                //BindContent();
                //BindBinary();
                //WebBrowser browser = new WebBrowser();
                //browser.NavigateToString("www.detik.com");
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.UploadInquiry",
                    ClassName = "UploadDetailInquiry",
                    FunctionName = "btnEdit_Click",
                    ExceptionNumber = 1,
                    EventSource = "UploadInquiry",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);

            }
        }


        private void setScreen()
        {
            try
            {
                DocSolEntities _ent = new DocSolEntities();
                DataTable _dt = new DataTable();
                _ent.ClassName = "EditDocument";
                _ent.MethodName = "EditUpload";
                //_ent.DocTransId = SessionProperty.ReffKey;
                _ent.Id = Convert.ToInt64(SessionProperty.ReffKey);
                _dt = DocumentSolutionController.DocSolProcess<DataTable>(_ent);
                lblCustomerCode.Text = _dt.Rows[0]["CustCode"].ToString();
                lblCustomerName.Text = _dt.Rows[0]["CustName"].ToString();
                lblProjectCode.Text = _dt.Rows[0]["ProjCode"].ToString();
                lblProjectName.Text = _dt.Rows[0]["ProjName"].ToString();
                lblProjectType.Text = _dt.Rows[0]["ProjType"].ToString();
                lblDocumentType.Text = _dt.Rows[0]["DocTypeCode"].ToString();
                _ent.Id = _ent.DocTransId;
                //lblDocumentType.Text = _dt.Rows[1]["DocTypeCode"].ToString();
            }


            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.UploadInquiry",
                    ClassName = "UploadDetailInquiry",
                    FunctionName = "bindBinary",
                    ExceptionNumber = 1,
                    EventSource = "UploadInquiry",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RedirectPage redirect = new RedirectPage(this, "EditUploadDocument.EditDocumentUpload", SessionProperty);
            }
            catch (Exception _exp)
            {
                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = " Adibrata.DocumentSol.Windows.EditUploadDocument",
                    ClassName = "EditDocumentUploadDetail",
                    FunctionName = "btnBack_Click",
                    ExceptionNumber = 1,
                    EventSource = "EditDocumentUploadDetail",
                    ExceptionObject = _exp,
                    EventID = 200, // 70 Untuk User Managemetn
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
                #endregion
            }
        }

        public void GenerateControls()
        {
            //DataTable _dtcontent = new DataTable();
            DataTable _dtValue = new DataTable();
           

            try
            {
                //DocSolEntities _ent = new DocSolEntities();
                
                //    _ent.ClassName = "EditDocument";
                //    _ent.MethodName = "EditUploadContent";
                //    _ent.UserLogin = SessionProperty.UserLogin;
                //    _ent.DocumentType = lblDocumentType.Text;
                //    _ent.DocTransId = Convert.ToInt64(SessionProperty.ReffKey);
           
                //_dtcontent = DocumentSolutionController.DocSolProcess<DataTable>(_ent);


                DocSolEntities _ent = new DocSolEntities();
                
                    _ent.ClassName = "EditDocument";
                    _ent.MethodName = "EditUploadValue";
                    _ent.UserLogin = SessionProperty.UserLogin;
                    _ent.DocumentType = lblDocumentType.Text;
                    _ent.DocTransId = Convert.ToInt64(SessionProperty.ReffKey);
            
                _dtValue = DocumentSolutionController.DocSolProcess<DataTable>(_ent);
               
                spValue.Children.Clear();
                spContent.Children.Clear();


                if (_dtValue.Rows.Count > 0 )
                {
                   

                    foreach (DataRow _row in _dtValue.Rows)
                    {
                        //DataRow _rowv = _dtValue.NewRow();


                        TextBlock DocContentDescription = new TextBlock();
                        DocContentDescription.Name = _row["Field2"].ToString().Replace(" ", "");
                        DocContentDescription.Text = _row["Field2"].ToString();
                        DocContentDescription.HorizontalAlignment = HorizontalAlignment.Stretch;
                        DocContentDescription.Width = 200;
                        //_dtValue.Equals(_row["ContentValue"]);
                   
                        DocContentDescription.SetResourceReference(TextBlock.StyleProperty, "TextBlockStyle");
                        spValue.Children.Remove(DocContentDescription);
                        spContent.Children.Add(DocContentDescription);

                        spContent.RegisterName(DocContentDescription.Name, DocContentDescription);
                        //System.Type type = rdr.GetFieldType(c);
                        //switch (Type.GetTypeCode(type))
                        //{
                        //    case TypeCode.DateTime:
                        //        break;
                        //    case TypeCode.String:
                        //        break;
                        //    default: break;
                        //}

                        string input = _row["Result"].ToString().ToLower();
                        string _datatype = input.Split(new char[] { '(', ')' })[1];
                        
                       
                        switch (_datatype.ToUpper())
                            {   
                                case "STRING":
                                    {
                                        //string _dataValue = _row["DocTypeCode"].ToString().ToUpper();

                                        TextBox txtInput = new TextBox();
                                        txtInput.Name = _row["Result"].ToString().Replace(")", "").Replace("(","").Replace("string","").Replace("String","").Trim();
                                        txtInput.Text = _row["ContentValue"].ToString();
                                        txtInput.Width = 400;
                                        txtInput.Margin.Top.Equals(5);
                                        //txtInput.SetResourceReference(TextBlock.StyleProperty, "textStyle");
                                        spValue.Children.Add(txtInput);

                                        spValue.RegisterName(txtInput.Name, txtInput);

                                    }
                                    break;
                                case "DATE":
                                    {
                                        DatePicker txtInput = new DatePicker();
                                        txtInput.Name = _row["Result"].ToString().Replace(")", "").Replace("(", "").Replace("Date", "").Replace("date", "").Trim();
                                        txtInput.Text = _row["ContentValue"].ToString();
                                        txtInput.Width = 400;
                                        spValue.Children.Add(txtInput);
                                        txtInput.Margin.Top.Equals(5);
                                        spValue.RegisterName(txtInput.Name, txtInput);
                                    }
                                    break;
                                case "NUMBER":
                                    {
                                        TextBox txtInput = new TextBox();
                                        txtInput.Name = _row["Result"].ToString().Replace(")", "").Replace("(","").Replace("number","").Trim();
                                        txtInput.Text = _row["ContentValue"].ToString();
                                        txtInput.Width = 400;
                                        txtInput.Margin.Top.Equals(5);
                                        //txtInput.SetResourceReference(TextBlock.StyleProperty, "textStyle");
                                        spValue.Children.Add(txtInput);

                                        spValue.RegisterName(txtInput.Name, txtInput);
                                    }
                                    break;
                            }
                           
                    }
                    }
                }
        
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserLogin,
                    NameSpace = "Adibrata.Windows.UserController.DocContent",
                    ClassName = "UCDocumentContent",
                    FunctionName = "GenerateControls",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }

        }

        public void ValueContent()
        {
            DataTable _dtValue = new DataTable();
            try
            {
                DocSolEntities _entValue = new DocSolEntities();
                
                    _ent.ClassName = "EditDocument";
                    _ent.MethodName = "EditUploadValue";
                   _ent.UserName = SessionProperty.UserName;
                    _ent.DocumentType = lblDocumentType.Text;
                     _ent.DocTransId = Convert.ToInt64(SessionProperty.ReffKey);
                
           
                _dtValue = DocumentSolutionController.DocSolProcess<DataTable>(_ent);
               
            }
            catch (Exception _exp)
            {

                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserLogin,
                    NameSpace = "Adibrata.Windows.UserController.DocContent",
                    ClassName = "UCDocumentContent",
                    FunctionName = "GenerateControls",
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
