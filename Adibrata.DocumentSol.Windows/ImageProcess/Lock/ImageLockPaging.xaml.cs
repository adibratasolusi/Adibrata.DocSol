﻿using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Framework.Logging;
using Adibrata.Windows.UserController;
using System;
using System.Collections.Generic;
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

namespace Adibrata.DocumentSol.Windows.ImageProcess.Lock
{
   public partial class ImageLockPaging : Page
    {
        SessionEntities SessionProperty;
        public ImageLockPaging(SessionEntities _session)
        {
            try
            {
                InitializeComponent();
                this.DataContext = new MainVM(new Shell());
                SessionProperty = _session;
            }
            catch (Exception _exp)
        {
                ErrorLogEntities _errent = new ErrorLogEntities
           {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.ImageProcess.Lock",
                    ClassName = " ImageLockPaging",
                    FunctionName = "ImageLockPaging",
                    ExceptionNumber = 1,
                    EventSource = "Delete",
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
                oPaging.ClassName = "ImageProcessPaging";
                oPaging.MethodName = "LockPaging";
                //"DeleteDocumentPaging"
                oPaging.dgObj = dgPaging;
                if (txtTransId.Text != "")
                {
                    sb.Append(" And ");
                    if (txtTransId.Text.Contains("%"))
                    {
                        sb.Append(" DocTransCode LIKE '");
                    }
                    else
                    {
                        sb.Append(" DocTransCode = '");
                    }
                    sb.Append(txtTransId.Text);
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
                    NameSpace = "Adibrata.DocumentSol.Windows.ImageProcess.Lock",
                    ClassName = "ImageLockPaging",
                    FunctionName = "ImageLockPaging",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }

        }

        private void btnLock_Click(object sender, RoutedEventArgs e)
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
                RedirectPage redirect = new RedirectPage(this, "ImageProcess.Lock.ImageLockDetail", SessionProperty);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.ImageProcess.Lock",
                    ClassName = "ImageLockPaging",
                    FunctionName = "btnLock_Click",
                    ExceptionNumber = 1,
                    EventSource = "ImageLock",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

    }
}

