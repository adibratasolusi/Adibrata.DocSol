using Adibrata.BusinessProcess.Report.Entities;
using Adibrata.Framework.Caching;
using Adibrata.Framework.Logging;
using System;
using System.Reflection;

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
            string _methodname, _classname;
            try
            {
                #region "Load Assembly"
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
                #endregion 

                #region "Load Class"
                // Load Class
                _classname = _ent.AssemblyName + "." + _ent.ClassName;
                if (!DataCache.Contains(_classname))
                {
                    _type = _objassembly.GetType(_classname);
                    DataCache.Insert<Type>(_classname, _type);
                }
                else
                {
                    _type = DataCache.Get<Type>(_classname);
                }
                #endregion 
                
                #region "Load Method"
                // Load Method
                _methodname = _classname + "." + _ent.MethodName;

                if (!DataCache.Contains(_methodname))
                {
                    _obj = Activator.CreateInstance(_type);
                    DataCache.Insert<object>(_methodname, _obj);
                }
                else
                {
                    _obj = DataCache.Get<object>(_methodname);
                }
                #endregion 
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
