using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace Adibrata.BusinessProcess.Views.Entities
{
    [Serializable]
    public class ViewEntities
    {
        public string AssemblyName { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public DataTable ViewTable { get; set; }
        public string WhereCond { get; set; }
        public string SortBy { get; set; }
        public string ConnectionString { get; set; } // Connection string di set di ui
    }
}
