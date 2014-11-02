using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using Adibrata.BusinessProcess.Views.Entities;
using Adibrata.Framework.Logging;
using Adibrata.Controller;
using Adibrata.Framework.Caching;

namespace Adibrata.Controller.Paging
{
    public static class ViewController
    {
        public static T ViewData <T>(ViewEntities _ent)
        {
            Assembly _objassembly = null;
            Type _type = null;
            object _obj = null; 
            var _result = default(T);
            try
            {
                _ent.AssemblyName = "Adibrata.BusinessProcess.View.Extend";
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

                _result = (T)_type.InvokeMember(_ent.MethodName, BindingFlags.InvokeMethod, null, _obj, _param);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = "",
                    NameSpace = "Adibrata.Controller.UserManagement",
                    ClassName = "UserManagementController",
                    FunctionName = "UserManagement",
                    ExceptionNumber = 1,
                    EventSource = "UserMangement",
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
