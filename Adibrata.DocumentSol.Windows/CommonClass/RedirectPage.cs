using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Adibrata.Framework.Caching;
using Adibrata.Framework.Logging;
using Adibrata.BusinessProcess.Entities.Base;
using System;
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
            }

        }
    }
}
