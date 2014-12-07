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
        public long CustomerID { get; set; }
        public string AgrmntNo { get; set; }
        public Int64 TransId { get; set; }
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
        public string DocumentType { get; set; }
        public string LineOfBusiness { get; set; }
        #region "Approval Doc Content"
        public string UserApprovalPath { get; set; }
        public string CurrentApprovalPath { get; set; }
        public string NextApprovalPath { get; set; }
        public string ApprovalStatus { get; set; }
        public string ApprovalNotes { get; set; }
        public Boolean IsFinal { get; set; }
        #endregion 

    }
}

