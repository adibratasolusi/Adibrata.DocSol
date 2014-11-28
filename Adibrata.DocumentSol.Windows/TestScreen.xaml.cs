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
using Adibrata.Windows.UserController;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Adibrata.DocumentSol.Windows
{
    /// <summary>
    /// Interaction logic for TestScreen.xaml
    /// </summary>
    public partial class TestScreen : Page
    {
        private University uni = new University();
        private Dictionary<string, UserControl> userControls = new Dictionary<string, UserControl>();

        public TestScreen()
        {
            InitializeComponent();
            GenerateControls();
        }
        public void GenerateControls()
        {
            Department dp1 = new Department();
            dp1.Name = "Math";
            dp1.Subjects.Add(new Subject() { ID = "M101", Name = "PreCalculus" });
            dp1.Subjects.Add(new Subject() { ID = "M210", Name = "Calculus 1" });
            dp1.Subjects.Add(new Subject() { ID = "M211", Name = "Calculus 2" });
            dp1.Subjects.Add(new Subject() { ID = "M212", Name = "Calculus 3" });
            dp1.Subjects.Add(new Subject() { ID = "M213", Name = "Differential Equations" });
            dp1.Subjects.Add(new Subject() { ID = "M218", Name = "Linear Algebra" });
            Department dp2 = new Department();
            dp2.Name = "Computer Science";
            dp2.Subjects.Add(new Subject() { ID = "CS101", Name = "Introduction to Comuter" });
            dp2.Subjects.Add(new Subject() { ID = "CS201", Name = "Comuter Science 1" });
            dp2.Subjects.Add(new Subject() { ID = "CS202", Name = "Comuter Science 2" });
            dp2.Subjects.Add(new Subject() { ID = "CS210", Name = "Data Structure" });
            dp2.Subjects.Add(new Subject() { ID = "CS211", Name = "Database Management" });
            Department dp3 = new Department();
            dp3.Name = "Accounting";
            dp3.Subjects.Add(new Subject() { ID = "ACT101", Name = "Accounting 1" });
            dp3.Subjects.Add(new Subject() { ID = "ACT102", Name = "Accounting 2" });
            dp3.Subjects.Add(new Subject() { ID = "ACT201", Name = "Accounting 3" });
            dp3.Subjects.Add(new Subject() { ID = "ACT202", Name = "Accounting 4" });
            dp3.Subjects.Add(new Subject() { ID = "ACT203", Name = "Cost Accounting" });
            dp3.Subjects.Add(new Subject() { ID = "ACT214", Name = "Auditing" });
            uni.Departments.Add(dp1);
            uni.Departments.Add(dp2);
            uni.Departments.Add(dp3);
            
            Type type = this.GetType();
            Assembly assembly = type.Assembly;
            UserControl mathUC = (UserControl)assembly.CreateInstance(string.Format("{0}.MathUserControl", type.Namespace));
            //UserControl csUC = (UserControl)assembly.CreateInstance(string.Format("Adibrata.Windows.UserController.CSUserControl", type.Namespace));
            UserControl csUC = (UserControl)assembly.CreateInstance(string.Format("{0}.CSUserControl", type.Namespace));
            UserControl actUC = (UserControl)assembly.CreateInstance(string.Format("{0}.AccountingUserControl", type.Namespace));
            userControls.Add(dp1.Name, mathUC);
            userControls.Add(dp2.Name, csUC);
            userControls.Add(dp3.Name, actUC);

            DataContext = uni;
        }
        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Subject subject = e.NewValue as Subject;
            if (subject != null)
            {
                if (subject.ID.ElementAt(0) == 'M')
                {
                    container.Content = userControls["Math"];
                }
                else if (subject.ID.ElementAt(0) == 'A')
                {
                    container.Content = userControls["Accounting"];
                }
                else if (subject.ID.ElementAt(0) == 'C')
                {
                    container.Content = userControls["Computer Science"];
                }
                else
                {
                    container.Content = null;
                }
            }
        }

    

        public class Subject
        {
            public string ID
            { get; set; }
            public string Name
            { get; set; }
        }
        public class Department
        {
            public Department()
            {
                this.Subjects = new ObservableCollection<Subject>();
            }
            public string Name
            { get; set; }
            public ObservableCollection<Subject> Subjects
            { get; set; }
        }
        public class University
        {
            public University()
            {
                this.Departments = new ObservableCollection<Department>();
            }
            public ObservableCollection<Department> Departments
            { get; set; }

        }
    }
}
