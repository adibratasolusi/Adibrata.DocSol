using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Adibrata.Framework.Caching;
using Adibrata.Framework.Logging;
using Adibrata.BusinessProcess.Entities.Base;
using System.Windows;
using System.Windows.Controls;

namespace Adibrata.DocumentSol.Windows
{
    public class RedirectPage : Page
    {
        
        public RedirectPage(Page Source, string PageName, SessionEntities _ent)
        {
            switch (PageName)
            {
                #region "MainForm"
                case "MainForm": Source.NavigationService.Navigate(new Main(_ent)); break;
                #endregion 

                #region "Customer"
                case "Customer.CustomerPaging": Source.NavigationService.Navigate(new Customer.CustomerPaging(_ent)); break;
                case "Customer.CustomerAddEdit":  Source.NavigationService.Navigate(new Customer.CustomerAddEdit(_ent)); break;
                #endregion

                #region "UserRegistration"
                case "UserManagement.UserRegistrationPaging": Source.NavigationService.Navigate(new UserManagement.UserRegistrationPaging(_ent)); break;
                case "UserManagement.UserRegistrationAddEdit": Source.NavigationService.Navigate(new UserManagement.UserRegistrationAddEdit(_ent)); break;
                
                #endregion 

                #region "Form Registrasi"
                case "Form.FormRegistrasiPaging": Source.NavigationService.Navigate(new Form.FormRegistrasiPaging(_ent)); break;
                case "Form.FormRegistrasiSave": Source.NavigationService.Navigate(new Form.FormRegistrasiSave(_ent)); break;
                #endregion 

                #region "DocumentContentEntry"
                case "DocumentContent.DocumentContentEntry": Source.NavigationService.Navigate(new DocumentContent.DocumentContentEntry(_ent)); break;
                case "DocumentContent.DocumentUploadPaging": Source.NavigationService.Navigate(new DocumentContent.DocumentUploadPaging(_ent)); break;
                #endregion

                #region "Upload Rule"
                case "RuleUpload.RuleEngineUpload": Source.NavigationService.Navigate(new RuleUpload.RuleEngineUpload(_ent)); break;
                case "RuleUpload.RuleSchemePaging": Source.NavigationService.Navigate(new RuleUpload.RuleSchemePaging(_ent)); break;
                #endregion 

                #region "Document Content Approval"
                case "Approval.ApprovalPaging": Source.NavigationService.Navigate(new DocumentContent.Approval.ApprovalPaging(_ent)); break;
                case "Approval.ApprovalProcessScreen": Source.NavigationService.Navigate(new DocumentContent.Approval.ApprovalProcessScreen(_ent)); break;
                #endregion 
            }
        }

        public RedirectPage(Frame Source, string PageName, SessionEntities _ent)
        {
            switch (PageName)
            {
                #region "MainForm"
                case "MainForm": Source.NavigationService.Navigate(new Main(_ent)); break;
                #endregion

                #region "Customer"
                case "Customer.CustomerPaging": Source.NavigationService.Navigate(new Customer.CustomerPaging(_ent)); break;
                case "Customer.CustomerAddEdit": Source.NavigationService.Navigate(new Customer.CustomerAddEdit(_ent)); break;
                #endregion

                #region "UserRegistration"
                case "UserManagement.UserRegistrationPaging": Source.NavigationService.Navigate(new UserManagement.UserRegistrationPaging(_ent)); break;
                case "UserManagement.UserRegistrationAddEdit": Source.NavigationService.Navigate(new UserManagement.UserRegistrationAddEdit(_ent)); break;

                #endregion

                #region "Form Registrasi"
                case "Form.FormRegistrasiPaging": Source.NavigationService.Navigate(new Form.FormRegistrasiPaging(_ent)); break;
                case "Form.FormRegistrasiSave": Source.NavigationService.Navigate(new Form.FormRegistrasiSave(_ent)); break;
                #endregion

                #region "DocumentContentEntry"
                case "DocumentContent.DocumentContentEntry": Source.NavigationService.Navigate(new DocumentContent.DocumentContentEntry(_ent)); break;
                case "DocumentContent.DocumentUploadPaging": Source.NavigationService.Navigate(new DocumentContent.DocumentUploadPaging(_ent)); break;
                #endregion

                #region "Upload Rule"
                case "RuleUpload.RuleEngineUpload": Source.NavigationService.Navigate(new RuleUpload.RuleEngineUpload(_ent)); break;
                case "RuleUpload.RuleSchemePaging": Source.NavigationService.Navigate(new RuleUpload.RuleSchemePaging(_ent)); break;
                    
                #endregion 

                #region "Document Content Approval"
                case "Approval.ApprovalPaging": Source.NavigationService.Navigate(new DocumentContent.Approval.ApprovalPaging(_ent)); break;
                case "Approval.ApprovalProcessScreen": Source.NavigationService.Navigate(new DocumentContent.Approval.ApprovalProcessScreen(_ent)); break;
                #endregion 

                #region "Image Processing"
                case "ImageProcessing.WaterMark": Source.NavigationService.Navigate(new ImageProcess.Watermark()); break;
                case "ImageProcessing.Grayscale": Source.NavigationService.Navigate(new ImageProcess.Grayscale()); break;
                case "ImageProcessing.Rotate": Source.NavigationService.Navigate(new ImageProcess.Rotate()); break;
                case "ImageProcessing.Canvas": Source.NavigationService.Navigate(new ImageProcess.Canvas()); break;
                case "ImageProcessing.GetDataImage": Source.NavigationService.Navigate(new ImageProcess.DataImage()); break;
                case "ImageProcessing.ImageToPdf": Source.NavigationService.Navigate(new ImageProcess.ImageToPdf()); break;
                case "ImageProcessing.ImageEncryption": Source.NavigationService.Navigate(new ImageProcess.Encryption()); break;
                case "ImageProcessing.ImageDecryption": Source.NavigationService.Navigate(new ImageProcess.Decryption()); break;
                case "ImageProcessing.ImageLock": Source.NavigationService.Navigate(new ImageProcess.Lock()); break;
                case "ImageProcessing.ImageUnlock": Source.NavigationService.Navigate(new ImageProcess.Unlock()); break;
                case "ImageProcessing.ImageScanner": Source.NavigationService.Navigate(new ImageProcess.Scanner()); break;
                
                #endregion
            }

        }
    }
}
