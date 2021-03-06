﻿using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Controller;
using Adibrata.Framework.Logging;
using Adibrata.Windows.UserController;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Adibrata.DocumentSol.Windows.ImageProcess.Unlock
{
    /// <summary>
    /// Interaction logic for UnlockImageDetail.xaml
    /// </summary>
    public partial class UnlockImageDetail : Page
    {
        SessionEntities SessionProperty = new SessionEntities();
        Int64 id;
        public UnlockImageDetail(SessionEntities _session)
        {
            try
            {
                InitializeComponent();
                this.DataContext = new MainVM(new Shell());
                SessionProperty = _session;
                ucView.Session = SessionProperty;
                ucView.DocTransCode = SessionProperty.ReffKey;
                id = ucView.DocTransId;

         
            }
            catch (Exception _exp)
            {
                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.DocumentSol.Windows.ImageProcess.Unlock",
                    ClassName = "UnlockImageDetail",
                    FunctionName = "UnlockImageDetail",
                    ExceptionNumber = 1,
                    EventSource = "UnlockImageDetail",
                    ExceptionObject = _exp,
                    EventID = 200, // 70 Untuk User Management
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
                #endregion
            }
        }

       

  

        private void btnUnlock_Click(object sender, RoutedEventArgs e)
        {
            DocSolEntities _ent = new DocSolEntities
            {
                MethodName = "ImageStatusUnlocked",
                ClassName = "ImageProcess"
            };
            _ent.Id = id;
            _ent.UserName = SessionProperty.UserName;

            DocumentSolutionController.DocSolProcess<string>(_ent);
            MessageBox.Show("Sukses");
            RedirectPage redirect = new RedirectPage(this, "ImageProcess.Unlock.UnlockImagePaging", SessionProperty);
 
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

        //    RedirectPage redirect = new RedirectPage(this, "ImageProcess.Unlock.UnlockImagePaging", SessionProperty);
            try
            {
                RedirectPage redirect = new RedirectPage(this, "ImageProcess.Unlock.UnlockImagePaging", SessionProperty);
            }
            catch (Exception _exp)
            {
                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = " Adibrata.DocumentSol.Windows.ImageProcess.Unlock",
                    ClassName = "UnlockImageDetail",
                    FunctionName = "btnBack_Click",
                    ExceptionNumber = 1,
                    EventSource = "DeleteDocumentInquiryDetail",
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
