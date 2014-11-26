using Adibrata.BusinessProcess.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adibrata.BusinessProcess.DocumentSol.Entities
{
    [Serializable]
    public class DocSolEntities : EntitiesBase
    {
        public string AgrmntNo { get; set; }
        public string DocType { get; set; }
        public string Crit { get; set; }
        public string By { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyRT { get; set; }
        public string CompanyRW { get; set; }
        public string CompanyZipCode { get; set; }
        public string CompanyCity { get; set; }
        public string CompanyKelurahan { get; set; }
        public string CompanyKecamatan { get; set; }
        public string CompanyNPWP { get; set; }
        public string CompanyTDP { get; set; }
        public string CompanyNotary { get; set; }
        public string CompanySiup { get; set; }
    }
}
