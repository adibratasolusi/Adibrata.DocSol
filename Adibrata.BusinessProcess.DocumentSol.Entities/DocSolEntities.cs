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
    }
}
