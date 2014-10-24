//------------------------------------------------------------------------------
// <copyright file="CSSqlClassFile.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Adibrata.Database.Script.StoreProcedure
{
    class LCCalculationClass
    {
        StringBuilder sb = new StringBuilder();
        SqlCommand _cmd = new SqlCommand ();
       
        public decimal LCInstallmentCalculation(int AgrmntID, DateTime ValueDate)
        {

            DataTable _dtInst = new DataTable();
            SqlDataReader _rdr; 
            sb.AppendLine("		select insseqno, DueDt, (InstallAmt - PaidAmt - WaivedAmt) as installmentamount, PaidDt ");
			sb.AppendLine("		from dbo.InstSchdl with (nolock) "); 
			sb.AppendLine("		where InstSchdl.AgrmntID = @AgrmntID and DueDt < @ValueDate  and ");
			sb.AppendLine("		(InstallAmt - PaidAmt - WaivedAmt > 0) and ");
			sb.AppendLine("		exists (select 1 from Agrmnt where ID = InstSchdl.AgrmntID and Agrmnt.ContrStat Not in ('EXP', 'RRD')) ");
            sb.AppendLine("		order by insseqno ");
            using (SqlConnection _conn = new SqlConnection("context connection=true"))
            {
                if (_conn.State == ConnectionState.Closed) { _conn.Open(); }
                using (_cmd = _conn.CreateCommand())
                {
                    _cmd.CommandText = sb.ToString();
                    _cmd.Connection = _conn;

                    _cmd.Parameters.AddWithValue("@AgrmntID", AgrmntID);
                    _cmd.Parameters.AddWithValue("@ValueDate", ValueDate);
                    _rdr = _cmd.ExecuteReader();
                    _dtInst.Load(_rdr);
                    _rdr.Close();
                }
                if (_conn.State == ConnectionState.Closed) { _conn.Open(); }
            }
                 

            return 0;
        }
#region "remark"
//                Set @EndDate = @valueDate

//            If @PaidDate Is Null
//            Begin
//                set @dateRange = datediff(day,@duedate,@valuedate)
//                Set @StartDate = @DueDate
//            End
//            else	if @PaidDate > @duedate  
//            Begin
//                set @dateRange = datediff(day,@PaidDate, @valuedate)			
//                Set @StartDate = @PaidDate
//            End
//            else 
//            Begin
//                set @dateRange = datediff(day,@duedate,@valuedate)
//                Set @StartDate = @duedate
//            End 
			
//            if @lccalcmethod = 'CD'
//            begin
//                set @HolidayRange = 0

//            end
//            else if @lccalcmethod = 'WD'
//            begin
//                set @HolidayRange = dbo.fnHolidayRange(@StartDate,@valuedate)
//            end
//            else 
//            begin 
//                set @HolidayRange = 0
//            end
			
//            -------Shirley 11 September 2012 -- ANJF-1238 ------
//            --- SAF-217 -- tambah Filter --- 12 Nov 2013 -- Shirley ----
//            if @PaidDate Is not Null And @PaidDate > @duedate
//                set @graceperiod=0
//            ------ End Modified  by  Shirley 11 September 2012 -- ANJF-1238 ------		
			
//            -- Added by Erick 25 Feb 2013, cek LCMinimum diambil dari GenSetting
//            IF ((@OSInstallment) * (@percentagepenalty/1000) > 0)
//            BEGIN
//                if((@OSInstallment) * (@percentagepenalty/1000) <= @LCMinAmtGenSetting)
//                    BEGIN
//                        if (@dateRange > @graceperiod + @HolidayRange)
//                        Begin
//                            set @AmountLCCurrent = @AmountLCCurrent + ceiling((@dateRange * @LCMinAmtGenSetting)* 1.0/@CurrencyRounded)*@CurrencyRounded
//                        End
//                    else
//                        Begin
//                            set @AmountLCCurrent = @AmountLCCurrent + 0
//                        End
//                    END
//                -- End Added by Erick 25 Feb 2013
//                ELSE
//                    Begin
//                        if (@dateRange > @graceperiod + @HolidayRange)
//                            Begin
//                                set @AmountLCCurrent = @AmountLCCurrent + ceiling(((@dateRange * @OSInstallment) * (@percentagepenalty/1000))* 1.0/@CurrencyRounded)*@CurrencyRounded
//                            End
//                        else
//                            Begin
//                                set @AmountLCCurrent = @AmountLCCurrent + 0
//                            End
//                    End
//            END
//-- 		end
//        fetch next from curInstallmentSchedule into @insseqno,@duedate,@OSInstallment, @paiddate
//    end 	
#endregion 
        public decimal LCInsuranceCalculation (int AgrmntID, DateTime Valuedate)
        {
            return 0;
        }
        
    }
}
