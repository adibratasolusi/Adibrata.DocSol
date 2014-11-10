using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adibrata.BusinessProcess.Entities.Base;
using System.Data;
namespace Adibrata.BusinessProcess.Approval.Entities
{
    [Serializable]
    public class ApprovalEntities:EntitiesBase
    {
        public int ApprovalID { get; set; }
        public int ApprovalShemeID { get; set; }
        public int ApprovalTypeID { get; set; }
        public string ReasonCode { get; set; }
        public string ReasonDescription { get; set; }

        public string ApprovalSchemeCode { get; set; }
        public string ApprovalSchemeDesc { get; set; }
        public string IsStaging { get; set; }
        public string IsLimited { get; set; }
        public string DocumentLabel { get; set; }
        public string DocumentLink { get; set; }
        public string Label1 { get; set; }
        public string Label2 { get; set; }
        public string LinkLable1 { get; set; }
        public string LinkLable2 { get; set; }
        public string MethodLable1 { get; set; }
        public string MethodLable2 { get; set; }
        public string LabelNote { get; set; }
        public string MethodNote { get; set; }
        public string LimitLabel { get; set; }

        public string OtherLink1 { get; set; }
        public string OtherLink2 { get; set; }
        public string OtherLink3 { get; set; }
        public string OtherLink4 { get; set; }
        public string OtherLink5 { get; set; }

        public string OtherLinkUrl1 { get; set; }
        public string OtherLinkUrl2 { get; set; }
        public string OtherLinkUrl3 { get; set; }
        public string OtherLinkUrl4 { get; set; }
        public string OtherLinkUrl5 { get; set; }

        public DataTable ApprovalSchemeViewData { get; set;}
        public DataTable ApprovalPathViewData { get; set; }
        public int ApprovalSeqNo{ get; set; }
        
        public int CanFinalReject { get; set; }
        public int CanFinalApprove { get; set; }
        public int CanEscalation { get; set; }
        public int CanChangeFinalLevel { get; set; }

        public string RejectAction { get; set; }

        public int ApprovalDuration { get; set; }
        public int ApprovalPathLevel { get; set; }
        public string ApprovalPathDescription { get; set; }
        public int ApprovalPathID { get; set; }
        public decimal MaximumLimit { get; set; }
    }
}
