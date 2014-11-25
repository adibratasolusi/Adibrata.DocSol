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
        public DataTable ReportTable { get; set; }
        public byte[] ReportData { get; set; }
    }
}
