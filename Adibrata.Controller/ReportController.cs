using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using Adibrata.BusinessProcess.Report.Entities;
using Adibrata.Framework.Logging;
using Adibrata.Framework.Caching;

namespace Adibrata.Controller.Report
{
    public static class ReportController
    {
        public static T ReportData<T>(ReportEntities _ent)
        {
            var _result = default(T);
            Assembly _objassembly = null;
            Type _type = null;
            object _obj = null; 
            try
            {
                
                _ent.AssemblyName = "Adibrata.BusinessProcess.Report.Extend";
                if (!DataCache.Contains(_ent.AssemblyName))
                {
                    _objassembly = Assembly.Load(_ent.AssemblyName);
                    DataCache.Insert<Assembly>(_ent.AssemblyName, _objassembly);
                }
                else
                {
                    _objassembly = DataCache.Get<Assembly>(_ent.AssemblyName);
                }

                if (!DataCache.Contains(_ent.ClassName))
                {
                    _type = _objassembly.GetType(_ent.ClassName);
                    _obj = Activator.CreateInstance(_type);
                    DataCache.Insert<Type>(_ent.ClassName, _type);
                }
                else
                {
                    _type = DataCache.Get<Type>(_ent.ClassName);
                    _obj = Activator.CreateInstance(_type);
                }
                object[] _param = new object[] { _ent };

                _result =  (T)_type.InvokeMember(_ent.MethodName, BindingFlags.InvokeMethod, null, _obj, _param);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = "",
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
