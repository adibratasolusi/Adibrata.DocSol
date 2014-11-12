using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adibrata.BusinessProcess.UserManagement.Core;
using Adibrata.BusinessProcess.UserManagement.Entities;
using Adibrata.Framework.Logging;
using Adibrata.Framework.DataAccess;
using System.Data;
using Adibrata.Framework.ReportDocument;

using Adibrata.Configuration;

namespace Adibrata.BusinessProcess.UserManagement.Extend
{
    public class UserRegister : Adibrata.BusinessProcess.UserManagement.Core.UserRegister
    {
        static string ConnectionString = AppConfig.Config("ConnectionString");

        public virtual DataTable UserRegisterListReportData(UserManagementEntities _ent)
        {
            DataTable dt = new DataTable();
            

            dt.Load(SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "spUserRegisterListReport"));

            return dt;
        }
    }
}
