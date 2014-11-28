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

namespace Adibrata.Windows.UserController
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class Callendar : UserControl
    {
        public string DateString
        {
            get { return DpCallendar.SelectedDate.Value.ToString("dd/MM/yyyy"); }
            //set { DpCallendar.Value = new DateTime(2001, 10, 20); }
        }

        public string DateValue
        {
            get { return DpCallendar.SelectedDate.Value.ToShortDateString(); }
            //set { DpCallendar.Value = new DateTime(2001, 10, 20); }
        }

        public DateTime DateSet {get;set;}
        public Callendar()
        {
            InitializeComponent();
         
            DpCallendar.SelectedDateFormat = DatePickerFormat.Short;
            DpCallendar.DisplayDate = this.DateSet;

            DpCallendar.Text = this.DateSet.ToString();
            
        }

        private void DpCallendar_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
       


        
    }

}
