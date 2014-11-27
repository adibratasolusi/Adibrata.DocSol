﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adibrata.BusinessProcess.Paging.Entities;
using Adibrata.Framework.DataAccess;
using Adibrata.Framework.Logging;
using Adibrata.Configuration;
using System.Data;
using System.Data.SqlClient;



namespace Adibrata.BusinessProcess.Paging.Extend
{
    public class CustomerRegistrasi
    {
        static string ConnectionString = AppConfig.Config("ConnectionString");

        public virtual DataTable CustomerPaging (PagingEntities _ent)
        {
            DataTable _dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append("spCustomerPaging");
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@StartRecord", SqlDbType.Int);
                sqlParams[0].Value = _ent.StartRecord;
                sqlParams[1] = new SqlParameter("@EndRecord", SqlDbType.Int);
                sqlParams[1].Value = _ent.EndRecord;

                sqlParams[2] = new SqlParameter("@wherecond", SqlDbType.VarChar, 500);
                sqlParams[2].Value = _ent.WhereCond;
                sqlParams[3] = new SqlParameter("@sortby", SqlDbType.VarChar, 500);
                sqlParams[3].Value = _ent.SortBy;
                _dt.Load(SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, sb.ToString(), sqlParams));
                
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = " Adibrata.BusinessProcess.Paging.Core.UserManagement",
                    ClassName = "UserRegisterPaging",
                    FunctionName = "UserRegister",
                    ExceptionNumber = 1,
                    EventSource = "UserRegister",
                    ExceptionObject = _exp,
                    EventID = 80, // 80 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
            return _dt;
            
        }
    }
}