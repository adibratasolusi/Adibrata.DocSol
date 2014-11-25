using Adibrata.Configuration;
using Adibrata.Framework.Messaging;
using SharpBits.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Adibrata.DocumentSol.Windows.AgrmntUpload
{
    /// <summary>
    /// Interaction logic for AgrmntUploadProc.xaml
    /// </summary>
    public partial class AgrmntUploadProc : Page
    {
        public AgrmntUploadProc(string agrmntNo)
        {

            InitializeComponent();
            //ucBITS.SetScreen(agrmntNo);
        }

    }
}
