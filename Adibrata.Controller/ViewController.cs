using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using Adibrata.BusinessProcess.Views.Entities;
using Adibrata.Framework.Logging;
namespace Adibrata.Controller.Paging
{
    public class ViewController
    {
        public static DataTable ViewData(ViewEntities _ent)
        {
            DataTable dtview = new DataTable();
            _ent.AssemblyName = "Adibrata.BusinessProcess.View.Extend";
            Assembly test = Assembly.Load(_ent.AssemblyName);
            Type _type = test.GetType(_ent.ClassName);
            //New Non Static Classs
            object _obj = Activator.CreateInstance(_type);
            object[] _param = new object[] { _ent };

            dtview = (DataTable)_type.InvokeMember(_ent.MethodName, BindingFlags.InvokeMethod, null, _obj, _param);

            //static sub --> dtpaging = (DataTable)_type.GetMethod(_ent.MethodName).Invoke(null, _param);
            return dtview;

        }
    }
}
