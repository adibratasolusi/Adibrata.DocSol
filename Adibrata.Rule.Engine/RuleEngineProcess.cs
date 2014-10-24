using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Adibrata.Rule.Engine
{
    public static class RuleEngineProcess
    {
        public static RuleEngineEntities ResultRuleEngine(RuleEngineEntities _ent)
        {
            SqlCommand _comm = new SqlCommand();
            SqlConnection _conn = new SqlConnection();
            SqlDataReader _rdr; 
            StringBuilder sb = new StringBuilder();
            int _counter = 1;
      
            DataTable _dtrule = new DataTable();
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
                    _counter ++;
                }
            }
            using (SqlConnection _con = new SqlConnection(_ent.ConnectionString))
            {
                if (_con.State == ConnectionState.Closed) { _con.Open(); };
                using (SqlCommand _cmd = _con.CreateCommand())
                {
                    _cmd.CommandText = sb.ToString();
                    _rdr = _cmd.ExecuteReader();
                    _ent.DtResultRule.Load(_rdr);
                }
            }
            return _ent;

        }
        private static void ExecuteQuery(string SQLCommand, string ConnectionString)
        {
            using (SqlConnection _con = new SqlConnection(ConnectionString))
            {
                if (_con.State == ConnectionState.Closed) { _con.Open(); };
                using (SqlCommand _cmd = _con.CreateCommand())
                {
                    _cmd.CommandText = SQLCommand;
                    _cmd.ExecuteNonQuery();
                }
                if (_con.State == ConnectionState.Open) { _con.Close(); };
            }

        }
        public static RuleEngineEntities UploadRuleEngine(RuleEngineEntities _ent)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='"); sb.Append(_ent.RuleName); sb.Append("')");
            sb.Append(" Drop Table "); sb.AppendLine (_ent.RuleName);
            ExecuteQuery(sb.ToString(), _ent.ConnectionString);

            sb.Clear();
            sb.Append ("Create Table ");
            sb.Append(_ent.RuleName); sb.Append("(ID Int Identity, ");
            int _counter;
            for (_counter = 1; _counter <= _ent.NumOfCondition; _counter++)
            {
                sb.Append("Field"); sb.Append(_counter.ToString()); sb.Append(" nvarchar(50), ");

            }
            sb.Append(" Result nvarchar(50)");
            sb.Append(" CONSTRAINT [PK_"); sb.Append (_ent.RuleName); sb.Append ("] PRIMARY KEY CLUSTERED (	[ID] ASC )");
            sb.AppendLine("WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON RuleEngine) On RuleEngine");
            ExecuteQuery(sb.ToString(), _ent.ConnectionString);

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
            ExecuteQuery(sb.ToString(), _ent.ConnectionString);

            sb.Clear();
            sb.Append ("Insert Into ");sb.Append (_ent.RuleName); sb.Append (" (");
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
                            for (_counter = 1; _counter <= _ent.NumOfCondition; _counter ++ )
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
           

           
            return _ent;
        }

        public static DataTable ListRuleEngine (RuleEngineEntities _ent)
        {
            DataTable _dt = new DataTable();
            
            StringBuilder sb = new StringBuilder();
            sb.Append("Exec spRuleEnginePaging @currentpage, @pagesize, @wherecond, @sortby");
            using (SqlConnection _con = new SqlConnection(_ent.ConnectionString))
            {
                if (_con.State == ConnectionState.Closed) { _con.Open(); };
                using (SqlCommand _cmd = _con.CreateCommand())
                {
                    _cmd.CommandText = sb.ToString();
                    _cmd.Parameters.AddWithValue("@currentpage", (int)_ent.CurrentPage);
                    _cmd.Parameters.AddWithValue("@pagesize", (int)_ent.PageSize);
                    _cmd.Parameters.AddWithValue("@wherecond", (string)_ent.WhereCond);
                    _cmd.Parameters.AddWithValue("@sortby", (string)_ent.SortBy);

                    _dt.Load(_cmd.ExecuteReader());

                    _cmd.Parameters.Clear();


                }
                if (_con.State == ConnectionState.Open) { _con.Close(); };
            }
            return _dt;
        }
    }
}

