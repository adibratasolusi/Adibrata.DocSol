﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Adibrata.Rule.Engine
{
    [Serializable]    
    public class RuleEngineEntities
    {
        public string RuleName { get; set; }
        public int NumOfCondition { get; set; }
        //public DataTable ListValue { get; set; }
        public Dictionary<string, string> Listvalue;
        public DataTable DtListValue { get; set; }
        public DataTable DtResultRule { get; set; }
        public string ConnectionString { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string WhereCond { get; set; }
        public string SortBy { get; set; }
    }
}
