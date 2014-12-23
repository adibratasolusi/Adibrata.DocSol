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
   
    public class ImageProcess : Adibrata.BusinessProcess.DocumentSol.Core.ImageProcess
    {
        static string ConnectionString = AppConfig.Config("ConnectionString");
        
        public void ImageStatusLocked(DocSolEntities _ent)//method
        {
            SqlParameter[] sqlParams;
            SqlDataReader _rdr;
            try
            {
                #region "List Parameter SQL"
                sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@ID", SqlDbType.VarChar, 50);
                sqlParams[0].Value = _ent.Id;


                _rdr = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "spImageLockedStatus", sqlParams);


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
                    ClassName = "ImageProcess",
                    FunctionName = "ImageStatusLocked",
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
        
        public void ImageStatusUnlocked(DocSolEntities _ent)//method
        {
            SqlParameter[] sqlParams;
            SqlDataReader _rdr;
            try
            {
                #region "List Parameter SQL"
                sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@ID", SqlDbType.VarChar, 50);
                sqlParams[0].Value = _ent.Id;


                _rdr = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "spImageUnlockedStatus", sqlParams);


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
                    ClassName = "ImageProcess",
                    FunctionName = "ImageStatusLocked",
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
