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

namespace Adibrata.DocumentSol.Windows.StorageMonitoring.FileNumber
{
    /// <summary>
    /// Interaction logic for FileNumberDetail.xaml
    /// </summary>
    public partial class FileNumberDetail : Page
    {
        SessionEntities SessionProperty = new SessionEntities();
        public FileNumberDetail(SessionEntities _session)
        {
            try
            {
                InitializeComponent();
                this.DataContext = new MainVM(new Shell());
                SessionProperty = _session;
                if (SessionProperty.ReffKey == "jpg" || SessionProperty.ReffKey == "png")
                {
                    preview.Visibility = Visibility.Visible;
                }
                summarysize();
                summarysizedatagrid();


            }
            catch (Exception _exp)
            {
                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.ImageProcess.Lock",
                    ClassName = "ImageLockDetail",
                    FunctionName = "ImageLockDetail",
                    ExceptionNumber = 1,
                    EventSource = "ImageDetail",
                    ExceptionObject = _exp,
                    EventID = 200, // 70 Untuk User Management
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
                #endregion
            }
        }
        void summarysize()
        {
            DocSolEntities _ent = new DocSolEntities
            {
                MethodName = "StorageSizeDetail",
                ClassName = "StorageMonitoring"
            };
            _ent.Ext = SessionProperty.ReffKey;

            DataTable dt = new DataTable();
            dt = DocumentSolutionController.DocSolProcess<DataTable>(_ent);
            txtExtension.Text = SessionProperty.ReffKey;
            txtAverageSize.Text = dt.Rows[0]["Average"].ToString();
            txtNumberFile.Text = dt.Rows[0]["totalfile"].ToString();
            txtMaxSize.Text = dt.Rows[0]["Maximum"].ToString();
            txtMinSize.Text = dt.Rows[0]["Minimum"].ToString();

        }
        void summarysizedatagrid()
        {

            DocSolEntities _ent = new DocSolEntities
            {
                MethodName = "StorageExtDetail",
                ClassName = "StorageMonitoring"
            };
            _ent.Ext = SessionProperty.ReffKey;

            DataTable dt = new DataTable();
            dt = DocumentSolutionController.DocSolProcess<DataTable>(_ent);
            dgPaging.ItemsSource = dt.DefaultView;

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RedirectPage redirect = new RedirectPage(this, "StorageMonitoring.FileNumber.FileNumberPaging", SessionProperty);
            }
            catch (Exception _exp)
            {
                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = " Adibrata.DocumentSol.Windows.StorageMonitoring.FileNumber",
                    ClassName = "FileNumberDetail",
                    FunctionName = "btnBack_Click",
                    ExceptionNumber = 1,
                    EventSource = "FileNumberDetail",
                    ExceptionObject = _exp,
                    EventID = 200, // 70 Untuk User Managemetn
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
                #endregion
            }
        }
    }

}

