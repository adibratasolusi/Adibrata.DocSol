using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace Adibrata.BusinessProcess.UserManagement.Entities
{
    public class UserManagementEntities
    {
        public string AssemblyName { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public DataTable ReportTable { get; set; }
        public byte[] ReportData { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get { return 20; } }
        public string WhereCond { get; set; }
        public string SortBy { get; set; }
        public int TotalRecord { get; set; }
        public string ConnectionString { get; set; } // Connection string di set di ui

        public string UserName { get; set; }
        public string Pass { get; set; }
        public string IsActive { get; set; }
        public string IsConnect { get; set; }
        public string SecQuestion { get; set; }
        public string SecAnswer { get; set; }
        public DateTime ExpiredDt { get; set; }
        public Int64 seqWrongPwd { get; set; }
        public string UsrCrt { get; set; }
        public string UsrUpd { get; set; }
        public string OldPass { get; set; }


    }
}
