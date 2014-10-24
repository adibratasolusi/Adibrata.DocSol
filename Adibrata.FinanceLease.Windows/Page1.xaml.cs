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
using Adibrata.Framework.Logging;
using System.Diagnostics;
using Adibrata.Framework.Messaging;
using System.Net.Mail;
using System.Net;
using System.Configuration;


namespace Adibrata.FinanceLease.Windows
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
            
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
            }
            catch (Exception _exp)
                {
                    ErrorLogEntities _ent = new ErrorLogEntities
                    {
                        NameSpace = " Adibrata.FInanceLease.Web",
                        ClassName = "_Default",
                        EventSource = "Web Login",
                        EventID = 2,
                        ExceptionNumber = 2,
                        ExceptionDescription = _exp.Message,
                        ExceptionObject = _exp,
                        UserName = "Test"
                    };
                    try
                    {
                        ErrorLogWPF _err = new ErrorLogWPF();
                        _err.ErrorLog_WPF(_ent); }
                    catch (Exception experror) { MessageBox.Show(experror.Message); }
                }
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            Page2 p = new Page2("henry");
            this.NavigationService.Navigate(p);
        }

     

    }
}
