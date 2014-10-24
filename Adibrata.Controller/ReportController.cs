using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using Adibrata.BusinessProcess.Report.Entities;
using Adibrata.Framework.Logging;

namespace Adibrata.Controller.Report
{
    class ReportController
    {
        public static DataTable ReportData(ReportEntities _ent)
        {
            DataTable dtreport = new DataTable();
            _ent.AssemblyName = "Adibrata.BusinessProcess.Report.Extend";
            Assembly test = Assembly.Load(_ent.AssemblyName);
            Type _type = test.GetType(_ent.ClassName);
            //New Non Static Classs
            object _obj = Activator.CreateInstance(_type);
            object[] _param = new object[] { _ent };

            dtreport = (DataTable)_type.InvokeMember(_ent.MethodName, BindingFlags.InvokeMethod, null, _obj, _param);

            //static sub --> dtpaging = (DataTable)_type.GetMethod(_ent.MethodName).Invoke(null, _param);
            return dtreport;
        }
    }
}
