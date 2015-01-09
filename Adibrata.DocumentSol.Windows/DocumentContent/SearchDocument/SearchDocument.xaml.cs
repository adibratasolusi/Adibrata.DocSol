using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Framework.Logging;
using Adibrata.Windows.UserController;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
namespace Adibrata.DocumentSol.Windows.DocumentContent
{
    /// <summary>
    /// Interaction logic for SearchDocument.xaml
    /// </summary>
    public partial class SearchDocument : Page
    {
        SessionEntities SessionProperty;
        public SearchDocument(SessionEntities _session)
        {
            List<string> searchby = new List<string>();
            try
            {
                InitializeComponent();
                this.DataContext = new MainVM(new Shell());
                SessionProperty = _session;
                GridDataGrid.Visibility = Visibility.Hidden;
                GridPaging.Visibility = Visibility.Hidden;
                searchby.Add("String");
                searchby.Add("Date");
                searchby.Add("Number");
                cboSearchBy.ItemsSource = searchby;
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.DocumentContent",
                    ClassName = "SearchDocument",
                    FunctionName = "SearchDocument",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        public SearchDocument(SessionEntities _session, string SearchCriteria)
        {
            List<string> searchby = new List<string>();
            try
            {
                InitializeComponent();
                this.DataContext = new MainVM(new Shell());
                SessionProperty = _session;
                GridDataGrid.Visibility = Visibility.Hidden;
                GridPaging.Visibility = Visibility.Hidden;
                searchby.Add("String");
                searchby.Add("Date");
                searchby.Add("Number");
                cboSearchBy.ItemsSource = searchby;
                SearchDocumentProcess(SearchCriteria);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.DocumentContent",
                    ClassName = "SearchDocument",
                    FunctionName = "SearchDocument",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }


        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int i = dgPaging.SelectedIndex;
                DataGridHelper oDataGrid = new DataGridHelper();
                oDataGrid.dtg = dgPaging;
                DataGridCell cell = oDataGrid.GetCell(i, 1);
                TextBlock ReffKey = oDataGrid.GetVisualChild<TextBlock>(cell); // pass the DataGridCell as a parameter to GetVisualChild
                SessionProperty.IsEdit = true;
                SessionProperty.ReffKey = ReffKey.Text;
                SessionProperty.SourceForm = "DocumentContent.SearchDocument";
                RedirectPage redirect = new RedirectPage(this, "UploadInquiry.UploadDetailInquiry", SessionProperty);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.DocumentContent",
                    ClassName = "SearchDocument",
                    FunctionName = "btnView_Click",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        private void SearchDocumentProcess(string searchcriteria)
        {
            StringBuilder sb = new StringBuilder(8000);
            
            try
            {
                GridDataGrid.Visibility = Visibility.Visible;
                GridPaging.Visibility = Visibility.Visible;
                oPaging.ClassName = "DocumentSearch";
                oPaging.MethodName = "DocumentSearchPaging";
                oPaging.dgObj = dgPaging;
                oPaging.WhereCond2 = SearchSpesific();
                oPaging.WhereCond = searchcriteria;
                oPaging.SortBy = " Rank Asc ";
                oPaging.UserName = SessionProperty.UserName;
                oPaging.PagingData();
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.DocumentContent.SearchDocument",
                    ClassName = "SearchDocument",
                    FunctionName = "SearchDocumentProcess",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        private string SearchSpesific()
        {
            StringBuilder sb = new StringBuilder(8000);
            String[] words;
            String _wordsearch;
            if (cboSearchBy.SelectedValue != null)
            {
                if (txtSearchSpesific.Text.Contains(">") || txtSearchSpesific.Text.Contains("=")
                    || txtSearchSpesific.Text.Contains("<") || txtSearchSpesific.Text.Contains("<=")
                    || txtSearchSpesific.Text.Contains(">=") || txtSearchSpesific.Text.ToUpper().Contains("Between")
                    )
                {
                    if (cboSearchBy.SelectedValue.ToString() == "Date")
                    {
                        if (!txtSearchSpesific.Text.Contains("/"))
                        {
                            MessageBox.Show("Please Enter Value with Date Format");
                        }
                        else
                        {
                            if (txtSearchSpesific.Text.ToUpper().Contains("BETWEEN"))
                            {
                                _wordsearch = txtSearchSpesific.Text.ToUpper().Replace("BETWEEN", "|");
                                words = _wordsearch.ToUpper().Split('|');
                                sb.Append(" ContentName = '");
                                sb.Append(words[0]);
                                sb.Append("' AND ContenValueDate ");
                                sb.Append(" Between ");
                                sb.Append(words[1]);
                            }
                            else
                                if (txtSearchSpesific.Text.ToUpper().Contains(">"))
                                {
                                    words = txtSearchSpesific.Text.ToUpper().Split('>');
                                    sb.Append(" ContentName ='");
                                    sb.Append(words[0]);
                                    sb.Append("' AND ContenValueDate ");
                                    sb.Append(" > ");
                                    sb.Append(words[1]);
                                }
                                else
                                    if (txtSearchSpesific.Text.ToUpper().Contains("<"))
                                    {

                                        words = txtSearchSpesific.Text.ToUpper().Split('<');
                                        sb.Append(" ContentName ='");
                                        sb.Append(words[0]);
                                        sb.Append("' AND ContenValueDate ");
                                        sb.Append(" < ");
                                        sb.Append(words[1]);
                                    }
                                    else
                                        if (txtSearchSpesific.Text.ToUpper().Contains(">="))
                                        {
                                            _wordsearch = txtSearchSpesific.Text.Replace(">=", "*");
                                            words = _wordsearch.ToUpper().Split('*');
                                            sb.Append(" ContentName ='");
                                            sb.Append(words[0]);
                                            sb.Append("' AND ContenValueDate ");
                                            sb.Append(" >= ");
                                            sb.Append(words[1]);
                                        }
                                        else
                                            if (txtSearchSpesific.Text.ToUpper().Contains("<="))
                                            {
                                                _wordsearch = txtSearchSpesific.Text.Replace(">=", "&");
                                                words = _wordsearch.ToUpper().Split('&');
                                                sb.Append(" ContentName ='");
                                                sb.Append(words[0]);
                                                sb.Append("' AND ContenValueDate ");
                                                sb.Append(" <= ");
                                                sb.Append(words[1]);
                                            }
                                            else
                                                if (txtSearchSpesific.Text.ToUpper().Contains("="))
                                                {
                                                    words = txtSearchSpesific.Text.ToUpper().Split('=');
                                                    sb.Append(" ContentName ='");
                                                    sb.Append(words[0]);
                                                    sb.Append("' AND ContenValueDate ");
                                                    sb.Append(" = ");
                                                    sb.Append(words[1]);
                                                }
                        }
                    }
                    else
                        if (cboSearchBy.SelectedValue.ToString() == "Number" && cboSearchBy.SelectedValue != null)
                        {
                            if (txtSearchSpesific.Text.Contains("/"))
                            {
                                MessageBox.Show("Please Enter Value with Number Format");
                            }
                            else
                            {
                                if (txtSearchSpesific.Text.ToUpper().Contains("BETWEEN"))
                                {
                                    txtSearchSpesific.Text.ToUpper().Replace("BETWEEN", "|");
                                    words = txtSearchSpesific.Text.ToUpper().Split('|');
                                    sb.Append(" ContentName ='");
                                    sb.Append(words[0]);
                                    sb.Append("' AND ContentValueNumeric ");
                                    sb.Append(" Between ");
                                    sb.Append(words[1]);
                                }
                                else
                                    if (txtSearchSpesific.Text.ToUpper().Contains(">"))
                                    {

                                        words = txtSearchSpesific.Text.ToUpper().Split('>');
                                        sb.Append(" ContentName ='");
                                        sb.Append(words[0]);
                                        sb.Append("' AND ContentValueNumeric ");
                                        sb.Append(" > ");
                                        sb.Append(words[1]);
                                    }
                                    else
                                        if (txtSearchSpesific.Text.ToUpper().Contains("<"))
                                        {

                                            words = txtSearchSpesific.Text.ToUpper().Split('<');
                                            sb.Append(" ContentName ='");
                                            sb.Append(words[0]);
                                            sb.Append("' AND ContentValueNumeric ");
                                            sb.Append(" < ");
                                            sb.Append(words[1]);
                                        }
                                        else
                                            if (txtSearchSpesific.Text.ToUpper().Contains(">="))
                                            {
                                                _wordsearch = txtSearchSpesific.Text.Replace(">=", "*");
                                                words = _wordsearch.ToUpper().Split('*');
                                                sb.Append(" ContentName ='");
                                                sb.Append(words[0]);
                                                sb.Append("' AND ContentValueNumeric ");
                                                sb.Append(" >= ");
                                                sb.Append(words[1]);
                                            }
                                            else
                                                if (txtSearchSpesific.Text.ToUpper().Contains("<="))
                                                {
                                                    _wordsearch = txtSearchSpesific.Text.Replace(">=", "&");
                                                    words = _wordsearch.ToUpper().Split('&');
                                                    sb.Append(" ContentName ='");
                                                    sb.Append(words[0]);
                                                    sb.Append("' AND ContentValueNumeric ");
                                                    sb.Append(" <= ");
                                                    sb.Append(words[1]);
                                                }
                                                else
                                                    if (txtSearchSpesific.Text.ToUpper().Contains("="))
                                                    {

                                                        words = txtSearchSpesific.Text.ToUpper().Split('=');
                                                        sb.Append(" ContentName ='");
                                                        sb.Append(words[0]);
                                                        sb.Append("' AND ContentValueNumeric ");
                                                        sb.Append(" = ");
                                                        sb.Append(words[1]);
                                                    }
                            }
                        }
                        else
                        {
                            if (txtSearchSpesific.Text.ToUpper().Contains("%") && cboSearchBy.SelectedValue != null)
                            {

                                words = txtSearchSpesific.Text.ToUpper().Split('>');
                                sb.Append(" ContentName ='");
                                sb.Append(words[0]);
                                sb.Append("' AND ContentValue ");
                                sb.Append(" Like ");
                                sb.Append(words[1]);
                            }
                            else
                                if (cboSearchBy.SelectedValue != null)
                                {

                                    words = txtSearchSpesific.Text.ToUpper().Split('>');
                                    sb.Append(" ContentName ='");
                                    sb.Append(words[0]);
                                    sb.Append("' AND ContentValue ");
                                    sb.Append(" = ");
                                    sb.Append(words[1]);
                                }
                        }
                }
                else
                {
                    MessageBox.Show("Please Enter the Operand like '=, >, <, >=, <=, or Between'");
                }
            }
            return sb.ToString();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //if (txtSearch.Text == "")
            //{
            //    MessageBox.Show("Please Enter Search Criteria");
            //}
            //else
            //{
                SearchDocumentProcess(txtSearch.Text);
            //}
        }
    }
}
