using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.Controller;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Adibrata.Framework.Logging;

namespace Adibrata.Windows.UserController.DocContent
{
    /// <summary>
    /// Interaction logic for UCDocumentContent.xaml
    ///                     //Button btnClickMe = new Button();
    //btnClickMe.Content = "Click Me";
    //btnClickMe.Name = "btnClickMe";
    //btnClickMe.Click += new RoutedEventHandler(this.CallMeClick);

    //oStackPanel.Children.Add(btnClickMe);
    /// </summary>
    public partial class UCDocumentContent : UserControl
    {
        public string UserLogin { get; set; }
        public string DocumentType { get; set; }
        public DataTable ListContent { get; set; }

        DataTable _dtcontent = new DataTable();
        public UCDocumentContent()
        {
            InitializeComponent();
        }
        public void GenerateControls()
        {
            try
            {
                DocSolEntities _ent = new DocSolEntities { ClassName = "DocContent", MethodName = "DocContentRetrieve", UserLogin = this.UserLogin, DocumentType = this.DocumentType };
                _dtcontent = DocumentSolutionController.DocSolProcess<DataTable>(_ent);
                PanelInput.Children.Clear();
                PanelLabel.Children.Clear();
                


                if (_dtcontent.Rows.Count > 0)
                {

                    foreach (DataRow _row in _dtcontent.Rows)
                    {
                        TextBlock DocContentDescription = new TextBlock();
                        DocContentDescription.Name = _row["Field2"].ToString().Replace(" ", "");
                        DocContentDescription.Text = _row["Field2"].ToString();
                        DocContentDescription.HorizontalAlignment = HorizontalAlignment.Stretch;
                        DocContentDescription.Width = 200;
                        
                        DocContentDescription.SetResourceReference(TextBlock.StyleProperty, "TextBlockStyle");
                        PanelInput.Children.Remove(DocContentDescription);
                        PanelLabel.Children.Add(DocContentDescription);
                        
                        PanelLabel.RegisterName(DocContentDescription.Name, DocContentDescription);
                        string _datatype = _row["DataType"].ToString().ToUpper();

                        switch (_row["DataType"].ToString().ToUpper())
                        {
                            case "STRING":
                                {
                                    TextBox txtInput = new TextBox();
                                    txtInput.Name = _row["Result"].ToString().Trim();
                                    txtInput.Text = "-";
                                    txtInput.Width = 400;
                                    txtInput.Margin.Top.Equals(5);
                                    //txtInput.SetResourceReference(TextBlock.StyleProperty, "textStyle");
                                    PanelInput.Children.Add(txtInput);
                                    
                                    PanelInput.RegisterName(txtInput.Name, txtInput);
                                }
                                break;
                            case "DATE":
                                {
                                    DatePicker txtInput = new DatePicker();
                                    txtInput.Name = _row["Result"].ToString().Trim();
                                    txtInput.Text = DateTime.Now.ToString();
                                    txtInput.Width = 400;
                                    PanelInput.Children.Add(txtInput);
                                    txtInput.Margin.Top.Equals(5);
                                    PanelInput.RegisterName(txtInput.Name, txtInput);
                                }
                                break;
                            case "NUMBER":
                                {
                                    TextBox txtInput = new TextBox();
                                    txtInput.Name = _row["Result"].ToString().Trim();
                                    txtInput.Text = "0";
                                    txtInput.Width = 400;
                                    txtInput.Margin.Top.Equals(5);
                                    //txtInput.SetResourceReference(TextBlock.StyleProperty, "textStyle");
                                    PanelInput.Children.Add(txtInput);
                     
                                    PanelInput.RegisterName(txtInput.Name, txtInput);
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
                    UserLogin = this.UserLogin,
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

        public DataTable RetrieveValue()
        {
            DataTable _dtfinal = new DataTable();
            try
            {
                _dtfinal = _dtcontent.Copy();

                _dtfinal.Columns.Add("EntryValue", typeof(string));
                _dtfinal.Columns.Add("EntryValueDate", typeof(DateTime));
                _dtfinal.Columns.Add("EntryValueNumber", typeof(double));
                _dtfinal.AcceptChanges();
                if (_dtfinal.Rows.Count > 0)
                {

                    foreach (DataRow _row in _dtfinal.Rows)
                    {
                        switch (_row["DataType"].ToString().ToUpper())
                        {
                            case "STRING":
                                {
                                    TextBox txtInput = (TextBox)this.PanelInput.FindName(_row["Result"].ToString().Trim());
                                    _row["EntryValue"] = txtInput.Text;
                                    
                                }
                                break;
                            case "DATE":
                                {
                                    DatePicker txtInput = (DatePicker)this.PanelInput.FindName(_row["Result"].ToString().Trim());
                                    _row["EntryValue"] = txtInput.SelectedDate.ToString();
                                    _row["EntryValueDate"] = txtInput.SelectedDate;
                                }
                                break;
                            case "NUMBER":
                                {
                                    TextBox txtInput = (TextBox)this.PanelInput.FindName(_row["Result"].ToString().Trim());
                                    _row["EntryValue"] = txtInput.Text;
                                    try
                                    {
                                        _row["EntryValueNumber"] = Convert.ToDecimal(txtInput.Text);
                                    }
                                    catch { MessageBox.Show("Please Enter For " + _row["Field2"].ToString()); }
                                }
                                break;
                        }
                        _row.AcceptChanges();
                    }
                }
                _dtfinal.AcceptChanges();
                this.ListContent = _dtfinal;
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = this.UserLogin,
                    NameSpace = "Adibrata.Windows.UserController.DocContent",
                    ClassName = "UCDocumentContent",
                    FunctionName = "RetrieveValue",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
            return this.ListContent;
        }
        
    }
}
