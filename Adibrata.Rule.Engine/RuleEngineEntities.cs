using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Adibrata.Framework.Rule
{
    [Serializable]    
    public class RuleEngineEntities :Adibrata.BusinessProcess.Entities.Base.EntitiesBase
    {
        public string RuleName { get; set; }
        public int NumOfCondition { get; set; }
        //public DataTable ListValue { get; set; }
        public Dictionary<string, string> Listvalue;
        public DataTable DtListValue { get; set; }
        public DataTable DtResultRule { get; set; }
        public string PathFile { get; set; }
    }
}
