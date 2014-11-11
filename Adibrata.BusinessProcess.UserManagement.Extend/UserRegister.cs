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

        public virtual UserManagementEntities UserREgisterListReport(UserManagementEntities _ent)
        {
            DataTable dt = new DataTable();
            

            dt.Load(SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "spUserRegisterListReport"));

            ReportingEntities _rptent = new ReportingEntities { 
                DataSetName = "dsUserRegisterList",
                ReportName = @"UserManagement\UserRegisterReport.rdlc", 
                ReportData = dt, 
                FileNameDocument ="UserRegisterReport.PDF"};

            ReportServerRDLC _objreport = new ReportServerRDLC(_rptent);

            _rptent = _objreport.ReportOutput(_rptent, ReportServerRDLC.DocumentType.PDF);

            _ent.ReportOutput = _rptent.ReportResult;
            _ent.MimeDocument = _rptent.MimeDocument;
            _ent.FileDocumentName = _rptent.FileNameDocument;
            return _ent;
        }
    }
}
