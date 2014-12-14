﻿using Adibrata.BusinessProcess.Entities.Base;
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

        public string Ext { get; set; }
        public byte[] FileBinary { get; set; }
        public Int64 DocTransId { get; set; }
        public string FileName { get; set; }
        public DateTime DateCreated { get; set; }
        public decimal SizeFileBytes { get; set; }
        public string Pixel { get; set; }
        public string ComputerName { get; set; }
        public string DPI { get; set; }
        public Int64 Id { get; set; }
        public long CustomerID { get; set; }
        public string CustomerCode { get; set; }
        public string AgrmntNo { get; set; }
        public string TransId { get; set; }
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


        public string ProjectCode { get; set; }
        public string ProjectType { get; set; }
        public string ProjectName { get; set; }
    }
}

