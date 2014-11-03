using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Adibrata.BusinessProcess.Entities.Base
{
    public class EntitiesBase
    {
        public string AssemblyName { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public string ConnectionString { get { return "Server=.;Database=TBIG;User Id=sa;Password=Alpha2014"; } }

        public DataTable PagingTable { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get { return 20; } }
        public string WhereCond { get; set; }
        public string SortBy { get; set; }
        public int TotalRecord { get; set; }
        public string UserLogin { get; set; }
        //test
    }
}
