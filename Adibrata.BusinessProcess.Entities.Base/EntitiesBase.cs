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
        public DataTable PagingTable { get; set; }
        public int StartRecord { get; set; }
        public int EndRecord { get; set; }
        public string WhereCond { get; set; }
        public string SortBy { get; set; }
        public int TotalRecord { get; set; }
        public string UserLogin { get; set; }
        public int IsActive { get; set; }
        public int IsSystem { get; set; }
        public string UsrUpd { get; set; }
        public string UsrCrt { get; set; }
        public Boolean IsEdit { get; set; }

        #region "For Report Document"
        public byte[] ReportOutput { get; set; }
        public string MimeDocument { get; set; }
        public string FileDocumentName { get; set; }
        #endregion 

        public int OfficeID { get; set; }
        public DateTime PostingDate { get; set; }
        public DateTime EntryDate { get; set; }
        //test
    }
}
