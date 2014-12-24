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
using Adibrata.Controller.UserManagement;
using Adibrata.BusinessProcess.UserManagement.Entities;
namespace Adibrata.DocumentSol.Windows
{
    public class RedirectPage : Page
    {
        
        public RedirectPage(Page Source, string PageName, SessionEntities _ent)
        {
            UserManagementEntities _entUsrMgmt = new UserManagementEntities();
            _entUsrMgmt.FormPath = PageName;
            _entUsrMgmt.ClassName = "LoginActivity";
            _entUsrMgmt.MethodName = "LoginActivitySave";
            _entUsrMgmt.UserLogin = _ent.UserName;
            UserManagementController.UserManagement<string>(_entUsrMgmt);
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
                #endregion

                #region "DocumentsMaintenance"
                case "DocumentMaintenance.DeleteDocumentPaging": Source.NavigationService.Navigate(new DocumentMaintenance.DeleteDocumentPaging(_ent)); break;
                case "DocumentMaintenance.DeleteDocumentInquiryPaging": Source.NavigationService.Navigate(new DocumentMaintenance.DeleteDocumentInquiryPaging(_ent)); break;
                case "DocumentMaintenance.DeleteDocumentDetail": Source.NavigationService.Navigate(new DocumentMaintenance.DeleteDocumentDetail(_ent)); break;
                case "DocumentMaintenance.DeleteDocumentInquiryDetail": Source.NavigationService.Navigate(new DocumentMaintenance.DeleteDocumentInquiryDetail(_ent)); break;


                #endregion

                #region "ImageProcess"
                case "ImageProcess.Lock.ImageLockPaging": Source.NavigationService.Navigate(new ImageProcess.Lock.ImageLockPaging(_ent)); break;
                case "ImageProcess.Lock.ImageLockDetail": Source.NavigationService.Navigate(new ImageProcess.Lock.ImageLockDetail(_ent)); break;
                case "ImageProcess.Unlock.UnlockImagePaging": Source.NavigationService.Navigate(new ImageProcess.Unlock.UnlockImagePaging(_ent)); break;
                case "ImageProcess.Unlock.UnlockImageDetail": Source.NavigationService.Navigate(new ImageProcess.Unlock.UnlockImageDetail(_ent)); break;
                case "ImageProcess.WaterMark.WaterMarkPaging": Source.NavigationService.Navigate(new ImageProcess.WaterMark.WaterMarkPaging(_ent)); break;
                case "ImageProcess.WaterMark.WaterMarkDetail": Source.NavigationService.Navigate(new ImageProcess.WaterMark.WaterMarkDetail(_ent)); break;
              
                #endregion

                #region "FileNumber"
                case "StorageMonitoring.FileNumber.FileNumberPaging": Source.NavigationService.Navigate(new StorageMonitoring.FileNumber.FileNumberPaging(_ent)); break;
                case "StorageMonitoring.FileNumber.FileNumberDetail": Source.NavigationService.Navigate(new StorageMonitoring.FileNumber.FileNumberDetail(_ent)); break;

                #endregion

                #region "Upload Rule"
                case "RuleUpload.RuleEngineUpload": Source.NavigationService.Navigate(new RuleUpload.RuleEngineUpload(_ent)); break;
                case "RuleUpload.RuleSchemePaging": Source.NavigationService.Navigate(new RuleUpload.RuleSchemePaging(_ent)); break;
                #endregion 

                #region "Document Content Approval"
                case "Approval.ApprovalPaging": Source.NavigationService.Navigate(new DocumentContent.Approval.ApprovalPaging(_ent)); break;
                case "Approval.ApprovalProcessScreen": Source.NavigationService.Navigate(new DocumentContent.Approval.ApprovalProcessScreen(_ent)); break;
                #endregion 

                #region "Project"
                case "Project.CustomerProjectPaging": Source.NavigationService.Navigate(new Project.CustomerProjectPaging(_ent)); break;
                case "Project.ProjectPaging": Source.NavigationService.Navigate(new Project.ProjectPaging(_ent)); break;
                case "Project.ProjectAddEdit": Source.NavigationService.Navigate(new Project.ProjectAddEdit(_ent)); break;

                #endregion

                #region "Scan Multi"
                case "ImageScanMulti.ImageScanMultiProcess": Source.NavigationService.Navigate(new ImageScanMulti.ImageScanMultiProcess()); break;

                #endregion

                #region "Upload Inquiry"
                case "UploadInquiry.UploadInquiry": Source.NavigationService.Navigate(new UploadInquiry.UploadInquiry(_ent)); break;
                case "UploadInquiry.UploadDetailInquiry": Source.NavigationService.Navigate(new UploadInquiry.UploadDetailInquiry(_ent)); break;

                #endregion

                #region "Summary"
                case "StorageMonitoring.SummarySize": Source.NavigationService.Navigate(new StorageMonitoring.SummarySize(_ent)); break;
     
                #endregion


                #region "StorageMonitoring"
                case "StorageMonitoring.FileStorageSize.FileStoragePaging": Source.NavigationService.Navigate(new StorageMonitoring.FileStorageSize.FileStoragePaging(_ent)); break;
     
                #endregion



            }
        }

        public RedirectPage(Frame Source, string PageName, SessionEntities _ent)
        {
            UserManagementEntities _entUsrMgmt = new UserManagementEntities();
            _entUsrMgmt.FormPath = PageName;
            _entUsrMgmt.ClassName = "LoginActivity";
            _entUsrMgmt.MethodName = "LoginActivitySave";
            _entUsrMgmt.UserLogin = _ent.UserName;
            UserManagementController.UserManagement<string>(_entUsrMgmt);
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
                #endregion

                #region "Upload Rule"
                case "RuleUpload.RuleEngineUpload": Source.NavigationService.Navigate(new RuleUpload.RuleEngineUpload(_ent)); break;
                case "RuleUpload.RuleSchemePaging": Source.NavigationService.Navigate(new RuleUpload.RuleSchemePaging(_ent)); break;
                    
                #endregion 

                #region "Document Content Approval"
                case "Approval.ApprovalPaging": Source.NavigationService.Navigate(new DocumentContent.Approval.ApprovalPaging(_ent)); break;
                case "Approval.ApprovalProcessScreen": Source.NavigationService.Navigate(new DocumentContent.Approval.ApprovalProcessScreen(_ent)); break;
                #endregion 

                #region "DocumentsMaintenance"
                case "DocumentMaintenance.DeleteDocumentPaging": Source.NavigationService.Navigate(new DocumentMaintenance.DeleteDocumentPaging(_ent)); break;
                case "DocumentMaintenance.DeleteDocumentInquiryPaging": Source.NavigationService.Navigate(new DocumentMaintenance.DeleteDocumentInquiryPaging(_ent)); break;
                #endregion

                #region "ImageProcess"
                case "ImageProcess.Lock.ImageLockPaging": Source.NavigationService.Navigate(new ImageProcess.Lock.ImageLockPaging(_ent)); break;
                case "ImageProcess.Lock.ImageLockDetail": Source.NavigationService.Navigate(new ImageProcess.Lock.ImageLockDetail(_ent)); break;
                case "ImageProcess.Unlock.UnlockImagePaging": Source.NavigationService.Navigate(new ImageProcess.Unlock.UnlockImagePaging(_ent)); break;
                case "ImageProcess.Unlock.UnlockImageDetail": Source.NavigationService.Navigate(new ImageProcess.Unlock.UnlockImageDetail(_ent)); break;
                case "ImageProcess.WaterMark.WaterMarkPaging": Source.NavigationService.Navigate(new ImageProcess.WaterMark.WaterMarkPaging(_ent)); break;
                case "ImageProcess.WaterMark.WaterMarkDetail": Source.NavigationService.Navigate(new ImageProcess.WaterMark.WaterMarkDetail(_ent)); break;
              
                #endregion

                #region "FileNumber"
                case "StorageMonitoring.FileNumber.FileNumberPaging": Source.NavigationService.Navigate(new StorageMonitoring.FileNumber.FileNumberPaging(_ent)); break;
                case "StorageMonitoring.FileNumber.FileNumberDetail": Source.NavigationService.Navigate(new StorageMonitoring.FileNumber.FileNumberDetail(_ent)); break;
              
                #endregion

                #region "Summary"
                case "StorageMonitoring.SummarySize": Source.NavigationService.Navigate(new StorageMonitoring.SummarySize(_ent)); break;

                #endregion

                #region "StorageMonitoring"
                case "StorageMonitoring.FileStorageSize.FileStoragePaging": Source.NavigationService.Navigate(new StorageMonitoring.FileStorageSize.FileStoragePaging(_ent)); break;

                #endregion



                //#region "Image Processing"
                //case "ImageProcessing.WaterMark": Source.NavigationService.Navigate(new ImageProcess.Watermark()); break;
                //case "ImageProcessing.Grayscale": Source.NavigationService.Navigate(new ImageProcess.Grayscale()); break;
                //case "ImageProcessing.Rotate": Source.NavigationService.Navigate(new ImageProcess.Rotate()); break;
                //case "ImageProcessing.Canvas": Source.NavigationService.Navigate(new ImageProcess.Canvas()); break;
                //case "ImageProcessing.GetDataImage": Source.NavigationService.Navigate(new ImageProcess.DataImage()); break;
                //case "ImageProcessing.ImageToPdf": Source.NavigationService.Navigate(new ImageProcess.ImageToPdf()); break;
                //case "ImageProcessing.ImageEncryption": Source.NavigationService.Navigate(new ImageProcess.Encryption()); break;
                //case "ImageProcessing.ImageDecryption": Source.NavigationService.Navigate(new ImageProcess.Decryption()); break;
                //case "ImageProcessing.ImageLock": Source.NavigationService.Navigate(new ImageProcess.Lock()); break;
                //case "ImageProcessing.ImageUnlock": Source.NavigationService.Navigate(new ImageProcess.Unlock()); break;
                //case "ImageProcessing.ImageScanner": Source.NavigationService.Navigate(new ImageProcess.Scanner()); break;
                
                //#endregion


                #region "Project"
                case "Project.CustomerProjectPaging": Source.NavigationService.Navigate(new Project.CustomerProjectPaging(_ent)); break;
                case "Project.ProjectPaging": Source.NavigationService.Navigate(new Project.ProjectPaging(_ent)); break;
                case "Project.ProjectAddEdit": Source.NavigationService.Navigate(new Project.ProjectAddEdit(_ent)); break;

                #endregion


                #region "Scan Multi"
                case "ImageScanMulti.ImageScanMultiProcess": Source.NavigationService.Navigate(new ImageScanMulti.ImageScanMultiProcess()); break;
                
                #endregion

                #region "Upload Inquiry"
                case "UploadInquiry.UploadInquiry": Source.NavigationService.Navigate(new UploadInquiry.UploadInquiry(_ent)); break;
                case "UploadInquiry.UploadDetailInquiry": Source.NavigationService.Navigate(new UploadInquiry.UploadDetailInquiry(_ent)); break;

                #endregion
            }

        }
    }
}
