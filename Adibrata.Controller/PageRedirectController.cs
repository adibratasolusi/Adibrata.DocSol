using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Adibrata.Framework.Caching;
using Adibrata.Framework.Logging;
using Adibrata.BusinessProcess.Entities.Base;

namespace Adibrata.Controller.PageRedirect
{
    public class PageRedirectController
    {
        public static object RedirectPage(string AssemblyName, string PageName, SessionEntities _ent)
        {
            var _result = default(object);
            Assembly _objassembly = null;
            Type _type = null;
            object _obj = null;
            string _methodname, _classname;
            try
            {
                // Load Assembly

                //ConstructorInfo constr = typeof(Person).GetConstructor(new[] { typeof(string) });
                //ParameterInfo p0 = constr.GetParameters()[0];
                //object defaultValue = p0.DefaultValue;
                //Person p = (Person)constr.Invoke(new[] { defaultValue });
                //// ...or using Activator
                //Person p = (Person)Activator.CreateInstance(typeof(Person), defaultValue);

                if (AssemblyName == "")
                {
                    _ent.AssemblyName = "Adibrata.DocumentSol.Windows";
                }
                    else
                    {
                        _ent.AssemblyName = "Adibrata.DocumentSol.Windows." + AssemblyName;

                    }
                //#region "Load Assembly"
                //// Load Assembly
                //if (!DataCache.Contains(_ent.AssemblyName))
                //{
                //    _objassembly = Assembly.Load(_ent.AssemblyName);
                //    DataCache.Insert<Assembly>(_ent.AssemblyName, _objassembly);
                //}
                //else
                //{ _objassembly = DataCache.Get<Assembly>(_ent.AssemblyName); }
                //#endregion

                #region "Load Class"
                // Load Class
                _classname = _ent.AssemblyName + "." + _ent.ClassName;
                //if (!DataCache.Contains(_classname))
                //{
                //    _type = _objassembly.GetType(_classname);
                //    DataCache.Insert<Type>(_classname, _type);
                //}
                //else { _type = DataCache.Get<Type>(_classname); }
                #endregion

                #region "Load Method"
                // Load Method
                _methodname = _classname + "." + _ent.MethodName;

                if (!DataCache.Contains(_methodname))
                {
                     object[] _param = new object[] { _ent };
                    _obj = Activator.CreateInstance(_ent.AssemblyName,_classname,_param);
                    DataCache.Insert<object>(_methodname, _obj);
                }
                else { _obj = Activator.CreateInstance(_type); }
                #endregion

                //New Non Static Classs

                //object[] _param = new object[] { _ent };

                //_type.GetMethod(_ent.MethodName);

                //var prog = new Program();
                //dtpaging = (DataTable)_type.GetMethod(_ent.MethodName).Invoke(prog, _obj);

                //_result = (T)_type.InvokeMember(_ent.MethodName, BindingFlags.InvokeMethod, null, _obj, _param);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = "",
                    NameSpace = "Adibrata.Controller.PageRedirect",
                    ClassName = "PageRedirectController",
                    FunctionName = "PagingData",
                    ExceptionNumber = 1,
                    EventSource = "RedirectPage",
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
