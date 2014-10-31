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
    public static class ReportController
    {
        public static T ReportData<T>(ReportEntities _ent)
        {
            var _result = default(T);
            try
            {
                DataTable dtreport = new DataTable();
                _ent.AssemblyName = "Adibrata.BusinessProcess.Report.Extend";
                Assembly test = Assembly.Load(_ent.AssemblyName);
                Type _type = test.GetType(_ent.ClassName);
                //New Non Static Classs
                object _obj = Activator.CreateInstance(_type);
                object[] _param = new object[] { _ent };

                _result =  (T)_type.InvokeMember(_ent.MethodName, BindingFlags.InvokeMethod, null, _obj, _param);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = "",
                    NameSpace = "Adibrata.Controller.Report",
                    ClassName = "ReportController",
                    FunctionName = "ReportData",
                    ExceptionNumber = 1,
                    EventSource = "ReportData",
                    ExceptionObject = _exp,
                    EventID = 90, // 90 Untuk Controller
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
            //static sub --> dtpaging = (DataTable)_type.GetMethod(_ent.MethodName).Invoke(null, _param);
            return _result;
        }
    }
}
