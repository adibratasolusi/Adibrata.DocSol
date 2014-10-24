using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using Adibrata.BusinessProcess.Paging.Entities;
using System.Reflection;
using Adibrata.Framework.Logging;
using System.IO;
namespace Adibrata.Controller.Paging
{
    public class PagingController
    {
        public static DataTable PagingData(PagingEntities _ent)
         {
            DataTable dtpaging = new DataTable();
            try
            {
                _ent.AssemblyName = "Adibrata.BusinessProcess.Paging.Extend";
                //Assembly test = Assembly.GetExecutingAssembly();
                Assembly test = Assembly.Load(_ent.AssemblyName);
                Type _type = test.GetType(_ent.ClassName);
                //New Non Static Classs
                object _obj = Activator.CreateInstance(_type);

                object[] _param = new object[] { _ent };

                //_type.GetMethod(_ent.MethodName);

                //var prog = new Program();
                //dtpaging = (DataTable)_type.GetMethod(_ent.MethodName).Invoke(prog, _obj);

                dtpaging = (DataTable)_type.InvokeMember(_ent.MethodName, BindingFlags.InvokeMethod, null, _obj, _param);
            }
            catch (Exception exp)
            {
                string a;
                a = exp.InnerException.Message.ToString();
            }
            //static sub --> dtpaging = (DataTable)_type.GetMethod(_ent.MethodName).Invoke(null, _param);
            return dtpaging;
        }
    }
}
