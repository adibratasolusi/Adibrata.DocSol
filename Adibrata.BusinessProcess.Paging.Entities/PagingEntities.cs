using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Adibrata.BusinessProcess.Entities.Base;

namespace Adibrata.BusinessProcess.Paging.Entities
{
    [Serializable]
    public class PagingEntities :EntitiesBase
    {


        public string WhereCond2 { get; set; }
    }
}
