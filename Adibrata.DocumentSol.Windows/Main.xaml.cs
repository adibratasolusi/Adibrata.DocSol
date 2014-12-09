using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Framework.Logging;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Adibrata.Windows.UserController;
using Adibrata.Controller.UserManagement;
using System.Data;
using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.BusinessProcess.UserManagement.Entities;


namespace Adibrata.DocumentSol.Windows
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        SessionEntities SessionProperty = new SessionEntities();
        public Main(SessionEntities _session)
        {
            InitializeComponent();
            SessionProperty = _session;
            lblLoginName.Text = SessionProperty.UserName.ToUpper();
            lblBusinessDate.Text = DateTime.Now.ToString("dd/MMMM/yyyy");
            RedirectPage redirect = new RedirectPage(frmWorksheet, "Form.FormRegistrasiPaging", SessionProperty);
        }

        private void TreeMenuGenerate()
        {

        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {

            UserManagementEntities _ent = new UserManagementEntities
            {
                MethodName = "SearchEngineMenu",
                ClassName = "MainMenu"
            };
            
            DataTable dt = new DataTable();
            _ent.Form = searchTextBox.Text;
            dt = UserManagementController.UserManagement<DataTable>(_ent);

            dtgMenu.ItemsSource = dt.DefaultView;
            dtgMenu.CanUserSortColumns = false;
        }

        private void hpMenu_Click(object sender, RoutedEventArgs e)
        {
            int i = dtgMenu.SelectedIndex;
            DataGridHelper dtgHelper = new DataGridHelper();
            dtgHelper.dtg = dtgMenu;
            DataGridCell cell = dtgHelper.GetCell(i, 1);
            //var asd = cell.Content;
            TextBlock formUrl = dtgHelper.GetVisualChild<TextBlock>(cell); // pass the DataGridCell as a parameter to GetVisualChild

            string _formUrl = formUrl.Text;

            RedirectPage redirect = new RedirectPage(frmWorksheet, _formUrl, SessionProperty);
            //RedirectPage(frmWorksheet, _formUrl, SessionProperty);
            //MessageBox.Show(_value);
           // frmWorksheet.NavigationService.Navigate( new Uri( "pack://application:,,,/AssemblyName;component/Resources/logo.png"+ _formUrl),UriKind.Relative);
            //this.NavigationService.Navigate(new AgrmntUploadProc(_value));
        }
    }
}
