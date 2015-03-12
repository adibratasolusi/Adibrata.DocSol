using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Controller;
using Adibrata.Framework.Logging;
using Adibrata.Framework.Security;
using Adibrata.Windows.UserController;
using System;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Adibrata.DocumentSol.Windows.DocumentContent
{
    /// <summary>
    /// Interaction logic for PasswordDocument.xaml
    /// </summary>
    public partial class PasswordDocument : Page
    {
        string username;
        string dotranscode;
    
        Boolean ispass, isvalid;

        string _passwordentry, _passwordstore;
        SessionEntities SessionProperty;
        DocSolEntities _ent = new DocSolEntities();
        public PasswordDocument(SessionEntities _session)
        {
            try
            {
                InitializeComponent();
                this.DataContext = new MainVM(new Shell());
                SessionProperty = _session;
                //oFavorite.UserLogin = SessionProperty.UserName;
                //oFavorite.FormUrl = "EditUploadDocument.EditDocumentUpload";
                //oFavorite.DisableFavorit();
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.EditUploadDocument",
                    ClassName = "EditDocumentUpload",
                    FunctionName = "EditDocumentUpload",
                    ExceptionNumber = 1,
                    EventSource = "EditDocumentUpload",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int i = dgPaging.SelectedIndex;
                DataGridHelper oDataGrid = new DataGridHelper();
                oDataGrid.dtg = dgPaging;
                DataGridCell cell = oDataGrid.GetCell(i, 1);
                DataGridCell cellpass = oDataGrid.GetCell(i, 9);
                TextBlock TBusername = oDataGrid.GetVisualChild<TextBlock>(cellpass);
                TextBlock ReffKey = oDataGrid.GetVisualChild<TextBlock>(cell); // pass the DataGridCell as a parameter to GetVisualChild
                SessionProperty.IsEdit = true;
                dotranscode = ReffKey.Text;
                
                SessionProperty.ReffKey = ReffKey.Text;
                SessionProperty.doctranscode = ReffKey.Text;

               username= TBusername.Text;
               SessionProperty.UsrCrt = username;
                if (username != SessionProperty.UserName)
                { ispass = true; }else{ispass = false; }
                if (username != "")
                {
                    getId(); SessionProperty.IsActive = 1;
                    if (ispass == false)
                    {
                        dotranscode = ReffKey.Text;
                        RedirectPage redirect = new RedirectPage(this, "DocumentContent.PasswordDocumentDetail", SessionProperty);
                    }
                    else
                    {
                        txtIdPaging.Text = dotranscode;
                        Popuppass.IsOpen = true;
                    }
                }
                else
                {
                    getId(); SessionProperty.IsActive = 0;
                    RedirectPage redirect = new RedirectPage(this, "DocumentContent.PasswordDocumentDetail", SessionProperty);
                }




            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.DocumentContent",
                    ClassName = "EditUploadSave",
                    FunctionName = "btnEdit_Click",
                    ExceptionNumber = 1,
                    EventSource = "EditDocumentUpload",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        //private void validUsername()
        //{
        //    string username;

        //    try
        //    {
        //        DocSolEntities _ent = new DocSolEntities();
        //        DataTable _dt = new DataTable();
        //        _ent.ClassName = "DocContent";
        //        _ent.MethodName = "DocValidPassword";
        //        _ent.DocTransId = Convert.ToInt64(SessionProperty.ReffKey);
        //        _dt = DocumentSolutionController.DocSolProcess<DataTable>(_ent);
        //        username = _ent.UserName;
        //        if (username != SessionProperty.UserName)
        //        { ispass = true; }
        //        else { ispass = false; }

        //    }
        //    catch (Exception _exp)
        //    {
        //        ErrorLogEntities _errent = new ErrorLogEntities
        //        {
        //            UserLogin = SessionProperty.UserName,
        //            NameSpace = "Adibrata.DocumentSol.Windows.UploadInquiry",
        //            ClassName = "UploadDetailInquiry",
        //            FunctionName = "bindBinary",
        //            ExceptionNumber = 1,
        //            EventSource = "UploadInquiry",
        //            ExceptionObject = _exp,
        //            EventID = 200, // 1 Untuk Framework 
        //            ExceptionDescription = _exp.Message
        //        };
        //        ErrorLog.WriteEventLog(_errent);
        //    }

        //}

        private void getId()
        {

            try
            {
                DocSolEntities _ent = new DocSolEntities();
                _ent.ClassName = "UploadProcess";
                _ent.MethodName = "DocTransGetTransID";
                _ent.DocTransCode = SessionProperty.ReffKey;
                _ent.UserName = SessionProperty.UserName;
                SessionProperty.ReffKey = Convert.ToString(DocumentSolutionController.DocSolProcess<Int64>(_ent));
                _ent.DocTransId = Convert.ToInt64(SessionProperty.ReffKey);
            }
            catch (Exception _exp)
            {

                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.DocumentContent",
                    ClassName = "PasswordDocument",
                    FunctionName = "getId",
                    ExceptionNumber = 1,
                    EventSource = "btnDetail_Click",
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
                oPaging.ClassName = "DocContentPaging";
                oPaging.MethodName = "DocTransPasswordPaging";
                oPaging.dgObj = dgPaging;
                if (txtCustCode.Text != "" || txtCustName.Text != "" || txtProjCode.Text != "" || txtProjName.Text != "" || txtDocType.Text != "")
                {
                    sb.Append(" where ");
                    if (txtCustCode.Text != "")
                    {

                        if (txtCustCode.Text.Contains("%"))
                        {
                            sb.Append(" Cust.CustCode LIKE '");
                        }
                        else
                        {
                            sb.Append(" Cust.CustCode = '");
                        }
                        sb.Append(txtCustCode.Text);
                        sb.Append("'");
                    }

                    if (txtCustName.Text != "")
                    {

                        if (txtCustName.Text.Contains("%"))
                        {
                            sb.Append("  Cust.CustName LIKE '");
                        }
                        else
                        {
                            sb.Append("  Cust.CustName = '");
                        }
                        sb.Append(txtCustName.Text);
                        sb.Append("'");
                    }
                    if (txtProjCode.Text != "")
                    {

                        if (txtProjCode.Text.Contains("%"))
                        {
                            sb.Append(" Proj.ProjCode LIKE '");
                        }
                        else
                        {
                            sb.Append(" Proj.ProjCode = '");
                        }
                        sb.Append(txtProjCode.Text);
                        sb.Append("'");
                    }
                    if (txtProjName.Text != "")
                    {

                        if (txtProjName.Text.Contains("%"))
                        {
                            sb.Append(" Proj.ProjName LIKE '");
                        }
                        else
                        {
                            sb.Append(" Proj.ProjName = '");
                        }
                        sb.Append(txtProjName.Text);
                        sb.Append("'");
                    }
                    if (txtDocType.Text != "")
                    {

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
                }
                oPaging.WhereCond = sb.ToString();
                oPaging.SortBy = " DocTrans.TransID Asc ";
                oPaging.UserName = SessionProperty.UserName;
                oPaging.PagingData();
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.UploadInquiry",
                    ClassName = "UploadInquiry",
                    FunctionName = "btnSearch_Click",
                    ExceptionNumber = 1,
                    EventSource = "UploadInquiry",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }

        }

        private void btnvalidpass_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _ent.ClassName = "UploadProcess";
                _ent.MethodName = "DocTransGetTransID";
                _ent.DocTransCode = txtIdPaging.Text;
                _ent.UserName = SessionProperty.UserName;
                SessionProperty.ReffKey = Convert.ToString(DocumentSolutionController.DocSolProcess<Int64>(_ent));
                _ent.DocTransId = Convert.ToInt64(SessionProperty.ReffKey);
                DocSolEntities ent = new DocSolEntities();
                DataTable _dtp = new DataTable();
                ent.ClassName = "DocContent";
                ent.MethodName = "DocOpenPassword";
                ent.DocTransId = _ent.DocTransId;
                _dtp = DocumentSolutionController.DocSolProcess<DataTable>(ent);
                ent.Password = txtinpassword.Password;
                _passwordentry = Encryption.EncryptToSHA3(ent.Password);
                _passwordstore = ent.PasswordStore;
                if (_passwordstore == _passwordentry && _passwordentry != "")
                {
                    isvalid = true;
                    Popuppass.IsOpen = false;
                    RedirectPage redirect = new RedirectPage(this, "DocumentContent.PasswordDocumentDetail", SessionProperty);
                }
                else
                {
                    isvalid = false;
                    MessageBox.Show("wrong Password");
                    txtinpassword.Password = "";
                }
            }
            catch (Exception _exp)
            {

                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.DocumentContent",
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

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Popuppass.IsOpen = false;

        }
    }
}
