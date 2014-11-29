using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Framework.Logging;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using Adibrata.Windows.UserController;

namespace Adibrata.DocumentSol.Windows.Form
{
    /// <summary>
    /// Interaction logic for FormRegistrasiSave.xaml
    /// </summary>
    public partial class FormRegistrasiSave : Page
    {
        SessionEntities SessionProperty = new SessionEntities();
        public FormRegistrasiSave(SessionEntities _session)
        {
            InitializeComponent();
            this.DataContext = new MainVM(new Shell());
        }
    }
}
