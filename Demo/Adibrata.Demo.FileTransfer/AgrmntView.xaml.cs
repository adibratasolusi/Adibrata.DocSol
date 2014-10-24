using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
using System.Configuration;

namespace Adibrata.Demo.FileTransfer
{
    /// <summary>
    /// Interaction logic for AgrmntView.xaml
    /// </summary>
    public partial class AgrmntView : Page
    {
        string ConString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        string currentAgrmntNo;
        public AgrmntView(string agrmntNo = "0")
        {
            InitializeComponent();
            txtaAgrmntNo.Text = agrmntNo;
            currentAgrmntNo = agrmntNo;
            FillDataGrid();
        }

        private void FillDataGrid()
        {


            string CmdString = string.Empty;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                CmdString = "SELECT PATH_ID as TrxNo, DOC_TYPE as DocType,BINARY as PreviewBin,'file://PC195/Project/'+FILE_NAME as PreviewPath FROM PATH where AGREEMENT_NO = '"+currentAgrmntNo+"'";
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("DOC_MASTER");
                sda.Fill(dt);

                dgPaging.ItemsSource = dt.DefaultView;
                dgPaging.CanUserSortColumns = false;
            }
                

        }

    }
}
