using Adibrata.Configuration;
using Adibrata.Framework.DataAccess;
using Adibrata.Framework.Logging;
using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using Adibrata.Framework.Caching;


namespace Adibrata.Framework.Rule
{
    public static class RuleEngineProcess
    {
        static string Connectionstring = AppConfig.Config("ConnectionString");
        public static RuleEngineEntities RuleEngineValue(RuleEngineEntities _ent)
        {

            StringBuilder sb = new StringBuilder();
            int _counter = 1;

            DataTable _dtrule = new DataTable();
            try
            {
                _dtrule = _ent.DtListValue;

                sb.Append("Select Resullt from ");
                sb.Append(_ent.RuleName);
                sb.Append(" where ");
                sb.Append("");
                string _value;
                if (_dtrule.Rows.Count > 0)
                {
                    foreach (DataRow _row in _dtrule.Rows)
                    {
                        _value = "Value" + _counter.ToString().Trim();
                        sb.Append("Field"); sb.Append(_counter.ToString()); sb.Append(" = '");
                        sb.Append((string)_row["Value" + _counter.ToString().Trim()]); sb.Append(_counter.ToString()); sb.Append("' ");
                        if (_counter < _ent.NumOfCondition) sb.Append(" And ");
                        _counter++;
                    }
                }
                _ent.DtResultRule = (DataTable)SqlHelper.ExecuteDataset(Connectionstring, CommandType.Text, sb.ToString()).Tables[0];
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = "RuleEngine",
                    NameSpace = "Adibrata.Framework.Rule",
                    ClassName = "RuleEngineProcess",
                    FunctionName = "ResultRuleEngine",
                    ExceptionNumber = 1,
                    EventSource = "RuleEngine",
                    ExceptionObject = _exp,
                    EventID = 80, // 80 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
            return _ent;

        }

        public static DataTable RuleEngineResultList(RuleEngineEntities _ent)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder cachename = new StringBuilder();
            DataTable _dtrule = new DataTable();
            try
            {
                sb.Append("Select * from ");
                sb.Append(_ent.RuleName);
                sb.Append(" with (nolock) ");
                if (_ent.WhereCond != "")
                {
                    sb.Append(" where ");
                    sb.Append(_ent.WhereCond);
                }
                cachename.Append(_ent.RuleName);
                cachename.Append(sb.ToString());
                cachename.Append("List");
                
                if (!DataCache.Contains(cachename.ToString()))
                {
                    //_dtrule = _ent.DtListValue;

                   

                    _dtrule.Load(SqlHelper.ExecuteReader(Connectionstring, CommandType.Text, sb.ToString()));
                    DataCache.Insert<DataTable>(cachename.ToString(), _dtrule);

                }
                else
                {
                    _dtrule = DataCache.Get<DataTable>(cachename.ToString());
                }
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = "RuleEngine",
                    NameSpace = "Adibrata.Framework.Rule",
                    ClassName = "RuleEngineProcess",
                    FunctionName = "ResultRuleEngine",
                    ExceptionNumber = 1,
                    EventSource = "RuleEngine",
                    ExceptionObject = _exp,
                    EventID = 80, // 80 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
            return _dtrule;
        }
        #region "Process Upload Rule"
        public static RuleEngineEntities UploadRuleEngine(RuleEngineEntities _ent)
        {
            StringBuilder sb = new StringBuilder();
            DataTable _dt = new DataTable();
            try
            {
                _dt = FillDataRuleEngine(_ent.PathFile, _ent);

                _ent.DtListValue = _dt;

                sb.Append("IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='"); sb.Append(_ent.RuleName); sb.Append("')");
                sb.Append(" Drop Table "); sb.AppendLine(_ent.RuleName);
                SqlHelper.ExecuteNonQuery(Connectionstring, CommandType.Text, sb.ToString());

                sb.Clear();
                sb.Append("Create Table ");
                sb.Append(_ent.RuleName); sb.Append("(ID Int Identity, ");
                int _counter;
                for (_counter = 1; _counter <= _ent.NumOfCondition; _counter++)
                {
                    sb.Append("Field"); sb.Append(_counter.ToString()); sb.Append(" nvarchar(50), ");

                }
                sb.Append(" Result nvarchar(50), ");
                sb.Append(" UsrCrt nvarchar(50),");
                sb.Append(" DtmCrt SmallDateTime ");
                
                sb.Append(" CONSTRAINT [PK_"); sb.Append(_ent.RuleName); sb.Append("] PRIMARY KEY CLUSTERED (	[ID] ASC )");
                sb.AppendLine("WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON RuleEngine) On RuleEngine");
                SqlHelper.ExecuteNonQuery(Connectionstring, CommandType.Text, sb.ToString());

                sb.Clear();
                #region "CREATE INDEX "
                sb.Append("CREATE NONCLUSTERED INDEX IX_");
                sb.Append(_ent.RuleName);
                sb.Append(" ON ");
                sb.Append(_ent.RuleName);
                sb.Append(" ( ");

                for (_counter = 1; _counter <= _ent.NumOfCondition; _counter++)
                {
                    sb.Append("Field"); sb.Append(_counter);
                    if (_counter != _ent.NumOfCondition) sb.Append(", ");
                }
                sb.Append(") on IndexTbl");
                SqlHelper.ExecuteNonQuery(Connectionstring, CommandType.Text, sb.ToString());
                sb.Clear();
                sb.Append("CREATE UNIQUE NONCLUSTERED INDEX IX_UNIQUE");
                sb.Append(_ent.RuleName);
                sb.Append(" ON ");
                sb.Append(_ent.RuleName);
                sb.Append(" ( ");

                for (_counter = 1; _counter <= _ent.NumOfCondition; _counter++)
                {
                    sb.Append("Field"); sb.Append(_counter);
                    if (_counter != _ent.NumOfCondition) sb.Append(", ");
                }
                sb.Append(", Result) on IndexTbl");
                SqlHelper.ExecuteNonQuery(Connectionstring, CommandType.Text, sb.ToString());
                #endregion 
                sb.Clear();
                sb.Append("Insert Into "); sb.Append(_ent.RuleName); sb.Append(" (");
                for (_counter = 1; _counter <= _ent.NumOfCondition; _counter++)
                {
                    sb.Append("Field"); sb.Append(_counter);
                    sb.Append(", ");
                }
                sb.Append("Result, UsrCrt, DtmCrt) Values (");
                for (_counter = 1; _counter <= _ent.NumOfCondition; _counter++)
                {
                    sb.Append("@Field"); sb.Append(_counter);
                    sb.Append(", ");
                }

                sb.Append(" @Result, @UsrCrt, @DtmCrt)");
                string _paramname;
                string _colomname;
                _counter = 1;
                SqlParameter[] sqlParams = new SqlParameter[_ent.NumOfCondition+3];
                for (_counter = 0; _counter <= _ent.NumOfCondition - 1; _counter++)
                {
                    _paramname = "@Field" + (_counter+1).ToString();
                    sqlParams[_counter] = new SqlParameter(_paramname, SqlDbType.VarChar);
                }
                _paramname = "@Result";

                sqlParams[_ent.NumOfCondition] = new SqlParameter(_paramname, SqlDbType.VarChar);
                sqlParams[_ent.NumOfCondition + 1] = new SqlParameter("@UsrCrt", SqlDbType.NVarChar,50);
                sqlParams[_ent.NumOfCondition + 2] = new SqlParameter("@DtmCrt", SqlDbType.SmallDateTime);


                if (_ent.DtListValue.Rows.Count > 0)
                {
                    foreach (DataRow _row in _ent.DtListValue.Rows)
                    {
                        for (_counter = 0; _counter <= _ent.NumOfCondition - 1; _counter++)
                        {
                            _paramname = "@Field" + (_counter+1).ToString();
                            _colomname = "Field" + (_counter+1).ToString();
                            sqlParams[_counter].Value = (string)_row[_colomname];
                        }
                        sqlParams[_ent.NumOfCondition].Value = (string)_row["ResultField"];
                        sqlParams[_ent.NumOfCondition + 1].Value = _ent.UserLogin;
                        sqlParams[_ent.NumOfCondition + 2].Value = DateTime.Now;

                        SqlHelper.ExecuteNonQuery(Connectionstring, CommandType.Text, sb.ToString(), sqlParams);
                    }
                }
                RuleListSave(_ent);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = "RuleEngine",
                    NameSpace = "Adibrata.Framework.Rule",
                    ClassName = "RuleEngineProcess",
                    FunctionName = "UploadRuleEngine",
                    ExceptionNumber = 1,
                    EventSource = "RuleEngine",
                    ExceptionObject = _exp,
                    EventID = 80, // 80 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        
            return _ent;
        }
        
        private static DataTable PrepareTableRule(RuleEngineEntities _ent)
        {
            int _counter;
            string _fieldname;
            DataTable _dt = new DataTable();
            for (_counter = 1; _counter <= _ent.NumOfCondition; _counter++)
            {
                _fieldname = "Field" + _counter.ToString().Trim();
                //_valuename = "Value" + _counter.ToString().Trim();
                _dt.Columns.Add(_fieldname, typeof(string));
                //_dt.Columns.Add(_valuename, typeof(string));

            }
            _dt.Columns.Add("ResultField", typeof(string));
            //_dt.Columns.Add("ResultValue", typeof(string));
            return _dt;
        }

        private static DataTable FillDataRuleEngine(string _path, RuleEngineEntities _ent)
        {
            DataRow _rowtemp;
            int _counter = 1;
            int _counterfield;
            DataTable _dtrule = new DataTable();
            DataTable _dtexcel = ReadDataExcel(_path);
            try
            {
                foreach (DataRow _row in _dtexcel.Rows)
                {
                    if (_counter == 1)
                    {
                        _ent.RuleName = (string)_row[0];
                        _ent.NumOfCondition = Convert.ToInt32(_row[1]);
                        _dtrule = PrepareTableRule(_ent);
                    }
                    if (_counter >= 4 && _row[0].ToString().ToUpper() != "{END}")
                    {
                        _rowtemp = _dtrule.NewRow();
                        for (_counterfield = 1; _counterfield <= _ent.NumOfCondition; _counterfield++)
                        {
                            string _namefield;
                            _namefield = "Field" + _counterfield.ToString();
                            //_valuefield = "Value" + _counterfield.ToString();

                            //_rowtemp[_namefield] = "Field" + _counterfield.ToString();
                            _rowtemp[_namefield] = _row[_counterfield];

                        }
                        _rowtemp["ResultField"] = _row[_ent.NumOfCondition + 1];
                        _dtrule.Rows.Add(_rowtemp);
                    }
                    _counter++;
                }
                _dtrule.AcceptChanges();
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = "RuleEngine",
                    NameSpace = "Adibrata.Framework.Rule",
                    ClassName = "RuleEngineProcess",
                    FunctionName = "FillDataRuleEngine",
                    ExceptionNumber = 1,
                    EventSource = "RuleEngine",
                    ExceptionObject = _exp,
                    EventID = 80, // 80 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
            return _dtrule;
        }

        private static DataTable ReadDataExcel(string _pathfile)
        {
            OleDbConnection oledbConn = new OleDbConnection();
            DataTable ds = new DataTable();
            try
            {
                // need to pass relative path after deploying on server
                // string path;//= System.IO.Path.GetFullPath(Server.MapPath("D:\RuleEngine\Rule Engine Simulation.xlsx"));
                /* connection string  to work with excel file. HDR=Yes - indicates 
                   that the first row contains columnnames, not data. HDR=No - indicates 
                   the opposite. "IMEX=1;" tells the driver to always read "intermixed" 
                   (numbers, dates, strings etc) data columns as text. 
                Note that this option might affect excel sheet write access negative. */

                if (Path.GetExtension(_pathfile) == ".xls")
                {
                    oledbConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + _pathfile + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"");
                }
                else if (Path.GetExtension(_pathfile) == ".xlsx")
                {
                    oledbConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + _pathfile + "; Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';");
                }
                oledbConn.Open();
                OleDbCommand cmd = new OleDbCommand(); ;
                OleDbDataAdapter oleda = new OleDbDataAdapter();


                cmd.Connection = oledbConn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [Sheet1$]";
                oleda = new OleDbDataAdapter(cmd);
                oleda.Fill(ds);

            }
            // need to catch possible exceptions
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = "RuleEngine",
                    NameSpace = "Adibrata.Framework.Rule",
                    ClassName = "RuleEngineProcess",
                    FunctionName = "ReadDataExcel",
                    ExceptionNumber = 1,
                    EventSource = "RuleEngine",
                    ExceptionObject = _exp,
                    EventID = 80, // 80 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
            finally
            {
                oledbConn.Close();
            }
            return ds;
        }

        private static void RuleListSave (RuleEngineEntities _ent)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("spRuleEngineListSave");
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@RuleCode", SqlDbType.NVarChar, 50);
                sqlParams[1] = new SqlParameter("@RuleName", SqlDbType.NVarChar, 50);
                sqlParams[2] = new SqlParameter("@RuleFileName", SqlDbType.NVarChar, 50);
                sqlParams[3] = new SqlParameter("@UsrCrt", SqlDbType.NVarChar, 50);
                sqlParams[4] = new SqlParameter("@DtmCrt", SqlDbType.SmallDateTime);
                sqlParams[0].Value = _ent.RuleCode;
                sqlParams[1].Value = _ent.RuleName;
                sqlParams[2].Value = _ent.PathFile;
                sqlParams[3].Value = _ent.UserLogin;
                sqlParams[4].Value = DateTime.Now;
                SqlHelper.ExecuteNonQuery(Connectionstring, CommandType.StoredProcedure, sb.ToString(), sqlParams);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = "RuleEngine",
                    NameSpace = "Adibrata.Framework.Rule",
                    ClassName = "RuleEngineProcess",
                    FunctionName = "RuleListSave",
                    ExceptionNumber = 1,
                    EventSource = "RuleEngine",
                    ExceptionObject = _exp,
                    EventID = 80, // 80 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }
        #endregion 
    }
}

