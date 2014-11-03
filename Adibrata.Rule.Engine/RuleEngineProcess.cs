using Adibrata.Configuration;
using Adibrata.Framework.DataAccess;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Adibrata.Framework.Logging;
using System; 


namespace Adibrata.Rule.Engine
{
    public static class RuleEngineProcess
    {
        static string Connectionstring = AppConfig.Config("ConnectionString");
        public static RuleEngineEntities ResultRuleEngine(RuleEngineEntities _ent)
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
                    UserName = "RuleEngine",
                    NameSpace = " Adibrata.Rule.Engine",
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
      
        public static RuleEngineEntities UploadRuleEngine(RuleEngineEntities _ent)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
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
                sb.Append(" Result nvarchar(50)");
                sb.Append(" CONSTRAINT [PK_"); sb.Append(_ent.RuleName); sb.Append("] PRIMARY KEY CLUSTERED (	[ID] ASC )");
                sb.AppendLine("WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON RuleEngine) On RuleEngine");
                SqlHelper.ExecuteNonQuery(Connectionstring, CommandType.Text, sb.ToString());

                sb.Clear();
                sb.Append("Create Index IX_");
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
                sb.Append("Insert Into "); sb.Append(_ent.RuleName); sb.Append(" (");
                for (_counter = 1; _counter <= _ent.NumOfCondition; _counter++)
                {
                    sb.Append("Field"); sb.Append(_counter);
                    sb.Append(", ");
                }
                sb.Append("Result) Values (");
                for (_counter = 1; _counter <= _ent.NumOfCondition; _counter++)
                {
                    sb.Append("@Field"); sb.Append(_counter);
                    sb.Append(", ");
                }

                sb.Append(" @Result)");
                string _paramname;
                string _colomname;
                _counter = 1;

                using (SqlConnection _con = new SqlConnection(_ent.ConnectionString))
                {
                    if (_con.State == ConnectionState.Closed) { _con.Open(); };
                    using (SqlCommand _cmd = _con.CreateCommand())
                    {
                        if (_ent.DtListValue.Rows.Count > 0)
                        {
                            _cmd.CommandText = sb.ToString();
                            foreach (DataRow _row in _ent.DtListValue.Rows)
                            {
                                for (_counter = 1; _counter <= _ent.NumOfCondition; _counter++)
                                {
                                    _paramname = "@Field" + _counter.ToString();
                                    _colomname = "Field" + _counter.ToString();
                                    _cmd.Parameters.AddWithValue(_paramname, (string)_row[_colomname]);
                                }
                                _paramname = "@Result";
                                _cmd.Parameters.AddWithValue(_paramname, (string)_row["ResultField"]);
                                _cmd.ExecuteNonQuery();
                                _cmd.Parameters.Clear();
                            }

                        }

                    }
                    if (_con.State == ConnectionState.Open) { _con.Close(); };
                }

            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = "RuleEngine",
                    NameSpace = " Adibrata.Rule.Engine",
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

      
    }
}

