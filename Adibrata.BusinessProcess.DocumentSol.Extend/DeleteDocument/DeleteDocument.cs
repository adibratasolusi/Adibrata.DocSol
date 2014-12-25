using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.Configuration;
using Adibrata.Framework.DataAccess;
using Adibrata.Framework.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adibrata.BusinessProcess.DocumentSol.Extend
{

    public class DeleteDocument : Adibrata.BusinessProcess.DocumentSol.Core.DeleteDocument
    {
        static string ConnectionString = AppConfig.Config("ConnectionString");
        public void DeleteDocumentStatus(DocSolEntities _ent)
        {
            SqlParameter[] sqlParams;
            SqlDataReader _rdr;
            try
            {
                #region "List Parameter SQL"
                sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@ID", SqlDbType.VarChar, 50);
                sqlParams[0].Value = _ent.Id;
           

                _rdr = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "spDeleteDocumentStatus", sqlParams);


                _rdr.Close();
                #endregion
            }
            catch (Exception _exp)
            {
                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.DocumentSol.Extend",
                    ClassName = "DeleteDocument",
                    FunctionName = "DeleteDocument",
                    ExceptionNumber = 1,
                    EventSource = "DeleteDocument",
                    ExceptionObject = _exp,
                    EventID = 200, // 80 Untuk DocumentManagement
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
                #endregion
            }
        }
    }
}
