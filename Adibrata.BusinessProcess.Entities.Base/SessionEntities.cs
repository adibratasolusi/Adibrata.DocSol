using System;

namespace Adibrata.BusinessProcess.Entities.Base
{
    [Serializable]
    public class SessionEntities: Adibrata.BusinessProcess.Entities.Base.EntitiesBase
    {
        public string UserName { get; set; }
        public DateTime BusinessDate { get; set; }
        
        public string FormID { get; set; }
        public string FullName { get; set; }
        public string ReffKey { get; set; }

        public DateTime StartLoginTime { get; set;  }
    }
}
