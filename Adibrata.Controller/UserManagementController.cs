using Adibrata.BusinessProcess.UserManagement.Entities;
using Adibrata.Framework.Caching;
using Adibrata.Framework.Logging;
using System;
using System.Reflection;

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
            string _methodname, _classname;
            try
            {
                #region "Load Assembly"
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
                _methodname = _ent.ClassName + "." + _ent.MethodName;

                if (!DataCache.Contains(_methodname))
                {
                    _obj = Activator.CreateInstance(_type);
                    DataCache.Insert<object>(_methodname, _obj);
                }
                else
                {
                    _obj = Activator.CreateInstance(_type);
                }
                #endregion 
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
