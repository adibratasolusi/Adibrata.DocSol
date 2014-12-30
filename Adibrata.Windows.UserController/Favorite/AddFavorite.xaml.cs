using Adibrata.BusinessProcess.UserManagement.Entities;
using Adibrata.Controller.UserManagement;
using Adibrata.Framework.Logging;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Adibrata.Windows.UserController.Favorite
{
    /// <summary>
    /// Interaction logic for AddFavorite.xaml
    /// </summary>
    public partial class AddFavorite : UserControl
    {
        public string FormUrl { get; set; }
        public string UserLogin { get; set; }

        public AddFavorite()
        {
            InitializeComponent();
        }

        public  void DisableFavorit ()
        {
            Boolean _disabled;
            try
            {
                UserManagementEntities _ent = new UserManagementEntities { ClassName = "FavoriteMenu", MethodName = "FavoriteMenuDisable" };
                _ent.FormURL = FormUrl;
                _ent.UserLogin = UserLogin;
                _disabled = UserManagementController.UserManagement<Boolean>(_ent);
                btnFavorite.IsEnabled = _disabled;

            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = UserLogin,
                    NameSpace = "Adibrata.Windows.UserController.Favorite",
                    ClassName = "AddFavorite",
                    FunctionName = "AddFavoriteSave",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);

            }
        }
        private void AddFavoriteSave (string FormUrl, string UserLogin)
        {
            
            try
            {
                UserManagementEntities _ent = new UserManagementEntities { ClassName = "FavoriteMenu", MethodName = "FavoriteMenuAdd" };
                _ent.FormURL = FormUrl;
                _ent.UserLogin = UserLogin;
                UserManagementController.UserManagement<string>(_ent);
                DisableFavorit();
            }
             catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = UserLogin,
                    NameSpace = "Adibrata.Windows.UserController.Favorite",
                    ClassName = "AddFavorite",
                    FunctionName = "AddFavoriteSave",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
               
            }
        }

        
        private void btnFavorite_Click(object sender, RoutedEventArgs e)
        {
            AddFavoriteSave(this.FormUrl, this.UserLogin);
        }

    }
}
