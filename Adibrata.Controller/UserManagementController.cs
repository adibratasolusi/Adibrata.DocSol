using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adibrata.BusinessProcess.UserManagement.Entities;
using System.Reflection;
using Adibrata.Framework.Logging;
using Adibrata.Framework.Caching;

namespace Adibrata.Controller.UserManagement
{
   public static class UserManagementController
    {
       public static T UserManagement<T>(UserManagementEntities _ent)
        {
            var _result = default(T);
            Assembly _objassembly = null;
            Type _type = null;
            object _obj = null; 
            try
            {
                _ent.AssemblyName = "Adibrata.BusinessProcess.UserManagement.Extend";
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
