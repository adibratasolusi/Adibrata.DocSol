//------------------------------------------------------------------------------
// <copyright file="CSSqlCodeFile.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;

namespace Alpha.Database.Script.StoreProcedure
{
    public static class CommonClass
    {
        public static AgreementEntities AgreementStatus(long AgrmntID)
        {

            AgreementEntities _ent = new AgreementEntities();
            StringBuilder sb = new StringBuilder();
            SqlDataReader _rdr;

            sb.Append("Select ContrStat, DefaultStat, PrepHoldStat, CurrID, ProductID, CoyID ");
            sb.Append(" from Agrmnt with (nolock) where ID = @ID");

            using (SqlConnection _con = new SqlConnection("context connection=true"))
            {
                if (_con.State == ConnectionState.Closed) { _con.Open(); };
                using (SqlCommand _cmd = _con.CreateCommand())
                {
                    if (_con.State == ConnectionState.Closed) { _con.Open(); }
                    _cmd.Connection = _con;
                    _cmd.CommandText = sb.ToString();
                    _cmd.Parameters.AddWithValue("@ID", AgrmntID);
                    
                    _rdr = _cmd.ExecuteReader();
                    while (_rdr.Read())
                    {
                        _ent.DefaultStatus = (string)_rdr["DefaultStat"];
                        _ent.ContractStatus = (string)_rdr["ContrStat"];
                        _ent.PrepaidHolStatus = (string)_rdr["PrepHoldStat"];
                        _ent.CurrencyID = (int)_rdr["CurrID"];
                        _ent.ProductID = (long)_rdr["ProductID"];
                        _ent.CompanyID = (int)_rdr["CoyID"];
                    }
                    _rdr.Close();
                    _cmd.Dispose();
                }
                if (_con.State == ConnectionState.Open) { _con.Close(); };
                _con.Dispose();
            }
            return _ent;
        }

        public static DateTime GetBusinessDate()
        {
            DateTime _businessdate;
            SqlConnection _conn= new SqlConnection();
            SqlCommand _cmd = new SqlCommand();

            using (_conn)
            {
                if (_conn.State == ConnectionState.Closed) { _conn.Open(); }
                using (_cmd = _conn.CreateCommand())
                {
                    _cmd.CommandText = "Select BDCurrent From MS_Company With (nolock) where ID = 1 ";
                    _businessdate = Convert.ToDateTime(_cmd.ExecuteScalar().ToString());

                }
            }
            return _businessdate;
        }

        public static DateTime GetAgreementCode(int _agrmntid)
        {
            DateTime _businessdate;
            SqlConnection _conn = new SqlConnection();
            SqlCommand _cmd = new SqlCommand();

            using (_conn)
            {
                if (_conn.State == ConnectionState.Closed) { _conn.Open(); }
                using (_cmd = _conn.CreateCommand())
                {
                    _cmd.CommandText = "Select AgrmntCode From MS_Company With (nolock) where ID = 1 ";
                    _businessdate = Convert.ToDateTime(_cmd.ExecuteScalar().ToString());

                }
            }
            return _businessdate;
        }

        public static string GetOfficeCode(int _officeid)
        {
            string _officecode;
            using (SqlConnection _con = new SqlConnection("context connection=true"))
            {
                if (_con.State == ConnectionState.Closed) { _con.Open(); };

                using (SqlCommand _cmd = _con.CreateCommand())
                {
                    try
                    {
                        _cmd.CommandText = "Select OfficeCode From MS_Office with (nolock) where ID = @ID";
                        _cmd.Connection = _con;
                        _cmd.Parameters.AddWithValue("@ID", _officeid);
                        _officecode = _cmd.ExecuteScalar().ToString();
                    }
                    catch (Exception exp)
                    {
                        throw new Exception(exp.Message);
                    }
                    finally
                    {
                        if (_con.State == ConnectionState.Open) { _con.Close(); };
                    }
                }
            }
            return _officecode;
        }


        #region"Send data to Table"

        public static void SendDataTable(DataTable dt)
        {
            bool[] coerceToString;  // Do we need to coerce this column to string?
            SqlMetaData[] metaData = ExtractDataTableColumnMetaData(dt, out coerceToString);

            SqlDataRecord record = new SqlDataRecord(metaData);
            SqlPipe pipe = SqlContext.Pipe;
            pipe.SendResultsStart(record);
            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    for (int index = 0; index < record.FieldCount; index++)
                    {
                        object value = row[index];
                        if (null != value && coerceToString[index])
                            value = value.ToString();
                        record.SetValue(index, value);
                    }

                    pipe.SendResultsRow(record);
                }
            }
            finally
            {
                pipe.SendResultsEnd();
            }
        }

        private static SqlMetaData[] ExtractDataTableColumnMetaData(DataTable dt, out bool[] coerceToString)
        {
            SqlMetaData[] metaDataResult = new SqlMetaData[dt.Columns.Count];
            coerceToString = new bool[dt.Columns.Count];
            for (int index = 0; index < dt.Columns.Count; index++)
            {
                DataColumn column = dt.Columns[index];
                metaDataResult[index] = SqlMetaDataFromColumn(column, out coerceToString[index]);
            }

            return metaDataResult;
        }

        private static Exception InvalidDataTypeCode(TypeCode code)
        {
            return new ArgumentException("Invalid type: " + code);
        }

        private static Exception UnknownDataType(Type clrType)
        {
            return new ArgumentException("Unknown type: " + clrType);
        }

        private static SqlMetaData SqlMetaDataFromColumn(DataColumn column, out bool coerceToString)
        {
            coerceToString = false;
            SqlMetaData sql_md = null;
            Type clrType = column.DataType;
            string name = column.ColumnName;
            switch (Type.GetTypeCode(clrType))
            {
                case TypeCode.Boolean: sql_md = new SqlMetaData(name, SqlDbType.Bit); break;
                case TypeCode.Byte: sql_md = new SqlMetaData(name, SqlDbType.TinyInt); break;
                case TypeCode.Char: sql_md = new SqlMetaData(name, SqlDbType.NVarChar, 1); break;
                case TypeCode.DateTime: sql_md = new SqlMetaData(name, SqlDbType.DateTime); break;
                case TypeCode.DBNull: throw InvalidDataTypeCode(TypeCode.DBNull);
                case TypeCode.Decimal: sql_md = new SqlMetaData(name, SqlDbType.Decimal, 18, 0); break;
                case TypeCode.Double: sql_md = new SqlMetaData(name, SqlDbType.Float); break;
                case TypeCode.Empty: throw InvalidDataTypeCode(TypeCode.Empty);
                case TypeCode.Int16: sql_md = new SqlMetaData(name, SqlDbType.SmallInt); break;
                case TypeCode.Int32: sql_md = new SqlMetaData(name, SqlDbType.Int); break;
                case TypeCode.Int64: sql_md = new SqlMetaData(name, SqlDbType.BigInt); break;
                case TypeCode.SByte: throw InvalidDataTypeCode(TypeCode.SByte);
                case TypeCode.Single: sql_md = new SqlMetaData(name, SqlDbType.Real); break;
                case TypeCode.String: sql_md = new SqlMetaData(name, SqlDbType.NVarChar, column.MaxLength);
                    break;
                case TypeCode.UInt16: throw InvalidDataTypeCode(TypeCode.UInt16);
                case TypeCode.UInt32: throw InvalidDataTypeCode(TypeCode.UInt32);
                case TypeCode.UInt64: throw InvalidDataTypeCode(TypeCode.UInt64);
                case TypeCode.Object:
                    sql_md = SqlMetaDataFromObjectColumn(name, column, clrType);
                    if (sql_md == null)
                    {
                        // Unknown type, try to treat it as string;
                        sql_md = new SqlMetaData(name, SqlDbType.NVarChar, column.MaxLength);
                        coerceToString = true;
                    }
                    break;

                default: throw UnknownDataType(clrType);
            }

            return sql_md;
        }

        private static SqlMetaData SqlMetaDataFromObjectColumn(string name, DataColumn column, Type clrType)
        {
            SqlMetaData sql_md = null;
            if (clrType == typeof(System.Byte[]) || clrType == typeof(SqlBinary) || clrType == typeof(SqlBytes) ||
        clrType == typeof(System.Char[]) || clrType == typeof(SqlString) || clrType == typeof(SqlChars))
                sql_md = new SqlMetaData(name, SqlDbType.VarBinary, column.MaxLength);
            else if (clrType == typeof(System.Guid))
                sql_md = new SqlMetaData(name, SqlDbType.UniqueIdentifier);
            else if (clrType == typeof(System.Object))
                sql_md = new SqlMetaData(name, SqlDbType.Variant);
            else if (clrType == typeof(SqlBoolean))
                sql_md = new SqlMetaData(name, SqlDbType.Bit);
            else if (clrType == typeof(SqlByte))
                sql_md = new SqlMetaData(name, SqlDbType.TinyInt);
            else if (clrType == typeof(SqlDateTime))
                sql_md = new SqlMetaData(name, SqlDbType.DateTime);
            else if (clrType == typeof(SqlDouble))
                sql_md = new SqlMetaData(name, SqlDbType.Float);
            else if (clrType == typeof(SqlGuid))
                sql_md = new SqlMetaData(name, SqlDbType.UniqueIdentifier);
            else if (clrType == typeof(SqlInt16))
                sql_md = new SqlMetaData(name, SqlDbType.SmallInt);
            else if (clrType == typeof(SqlInt32))
                sql_md = new SqlMetaData(name, SqlDbType.Int);
            else if (clrType == typeof(SqlInt64))
                sql_md = new SqlMetaData(name, SqlDbType.BigInt);
            else if (clrType == typeof(SqlMoney))
                sql_md = new SqlMetaData(name, SqlDbType.Money);
            else if (clrType == typeof(SqlDecimal))
                sql_md = new SqlMetaData(name, SqlDbType.Decimal, SqlDecimal.MaxPrecision, 0);
            else if (clrType == typeof(SqlSingle))
                sql_md = new SqlMetaData(name, SqlDbType.Real);
            else if (clrType == typeof(SqlXml))
                sql_md = new SqlMetaData(name, SqlDbType.Xml);
            else
                sql_md = null;

            return sql_md;
        }

        #endregion
    }

}


