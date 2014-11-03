using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Adibrata.BusinessProcess.Entities.Base;

namespace Adibrata.BusinessProcess.Report.Entities
{
    [Serializable]
    public class ReportEntities : EntitiesBase
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

    }
}
