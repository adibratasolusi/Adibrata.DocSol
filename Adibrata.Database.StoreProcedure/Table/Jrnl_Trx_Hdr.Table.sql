
/****** Object:  Table [dbo].[Jrnl_Trx_Hdr]    Script Date: 11/3/2014 8:27:58 AM ******/
-- SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
-- SET ANSI_PADDING ON
--GO
CREATE TABLE [dbo].[Jrnl_Trx_Hdr](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[OfficeID] [int] NOT NULL,
	[JrnlCode] [nvarchar](50) NOT NULL,
	[PeriodYear] [char](4) NOT NULL,
	[PeriodMonth] [char](2) NOT NULL,
	[TrxDate] [datetime] NOT NULL,
	[Reff_No] [varchar](20) NULL,
	[Reff_Date] [datetime] NULL,
	[TrxDesc] [varchar](100) NOT NULL,
	[JrnlAmt] [numeric](17, 2) NOT NULL,
	[Status_Tr] [char](2) NOT NULL,
	[Flag] [char](1) NOT NULL,
	[IsActive] [char](1) NULL,
	[IsValid] [char](1) NULL,
	[JournalNo] [char](6) NULL,
	[AgrmntID] [bigint] NULL,
	[JrnlTrxID] [int] NULL,
	[JrnlSchmHdrId] [int] NULL,
	[JobID] [bigint] NULL,
	[UsrUpd] [nvarchar](20) NULL,
	[DtmUpd] [date] NULL,
	[UsrCrt] [nvarchar](20) NOT NULL,
	[DtmCrt] [datetime] NOT NULL,
 CONSTRAINT [PK_Jrnl_Trx_Hdr] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [JournalH]
) ON [JournalH]

--GO
-- SET ANSI_PADDING ON
--GO
