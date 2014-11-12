using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace Adibrata.Framework.ReportDocument
{
    [Serializable]    
    public class ReportingEntities
    {
        public string UserName { get; set; }
        public string ReportName { get; set; }
        public string ReportDate { get; set; }
        public string ReportPath { get; set; }
        public DataTable ReportData { get; set; }
        public DataTable SubReportData { get; set; }
        public Byte[] ReportResult { get; set; }
        public string MimeDocument { get; set; }
        public string Encoding { get; set;  }
        public string FileNameDocument { get; set; }
        public string DataSetName { get; set; }
        public string SubReportDataSetName { get; set; }
    }
}
