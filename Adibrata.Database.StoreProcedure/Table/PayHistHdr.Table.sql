
/****** Object:  Table [dbo].[PayHistHdr]    Script Date: 11/3/2014 8:27:58 AM ******/
-- SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
-- SET ANSI_PADDING ON
--GO
CREATE TABLE [dbo].[PayHistHdr](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[AgrmntID] [bigint] NOT NULL,
	[HistSeqNo] [smallint] NOT NULL,
	[ValueDt] [datetime] NOT NULL,
	[PostingDt] [datetime] NOT NULL,
	[Amt] [numeric](17, 2) NOT NULL,
	[WOP] [char](2) NOT NULL,
	[OfficeID_X] [char](3) NOT NULL,
	[BankAccID] [char](10) NOT NULL,
	[IsCorrection] [char](1) NOT NULL,
	[CorrHistSeq] [smallint] NOT NULL,
	[PrintedNum] [smallint] NULL,
	[PrintBy] [nvarchar](50) NULL,
	[LastPrtDt] [datetime] NULL,
	[JrnlCode] [nvarchar](50) NOT NULL,
	[VoucherNo] [nvarchar](50) NULL,
	[JrnlTrxID] [int] NOT NULL,
	[DefaultStat] [nvarchar](3) NOT NULL,
	[JrnlJobID] [bigint] NULL,
	[UsrUpd] [nvarchar](20) NULL,
	[DtmUpd] [smalldatetime] NULL,
	[UsrCrt] [nvarchar](20) NULL,
	[DtmCrt] [smalldatetime] NULL,
 CONSTRAINT [PK_PayHistHdr] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PayHistH]
) ON [PayHistH]

--GO
-- SET ANSI_PADDING ON
--GO
