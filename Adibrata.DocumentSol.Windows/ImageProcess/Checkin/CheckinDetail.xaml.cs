﻿using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Controller;
using Adibrata.Framework.Logging;
using Adibrata.Windows.UserController;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Adibrata.DocumentSol.Windows.ImageProcess.Checkin
{
    /// <summary>
    /// Interaction logic for CheckinDetail.xaml
    /// </summary>
    public partial class CheckinDetail : Page
    {
        SessionEntities SessionProperty = new SessionEntities();
        Int64 id;
        public CheckinDetail(SessionEntities _session)
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
                    NameSpace = "Adibrata.DocumentSol.Windows.ImageProcess.Checkout",
                    ClassName = "CheckoutDetail",
                    FunctionName = "CheckoutDetail",
                    ExceptionNumber = 1,
                    EventSource = "CheckoutDetail",
                    ExceptionObject = _exp,
                    EventID = 200, // 70 Untuk User Management
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
                #endregion
            }
        }

        private void btnCheckIn_Click(object sender, RoutedEventArgs e)
        {
            DocSolEntities _ent = new DocSolEntities
            {
                MethodName = "DocTransCheckIn",
                ClassName = "ImageProcess"
            };
            _ent.Id = id; 
            _ent.UserName = SessionProperty.UserName;

            DocumentSolutionController.DocSolProcess<string>(_ent);
            MessageBox.Show("Check In Succes");
            RedirectPage redirect = new RedirectPage(this, "ImageProcess.Checkin.CheckinPaging", SessionProperty);
    
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

            RedirectPage redirect = new RedirectPage(this, "ImageProcess.Checkin.CheckinPaging", SessionProperty);
        }
    }
}
