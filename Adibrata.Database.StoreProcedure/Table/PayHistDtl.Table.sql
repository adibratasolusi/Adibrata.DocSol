
/****** Object:  Table [dbo].[PayHistDtl]    Script Date: 11/3/2014 8:27:58 AM ******/
-- SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
-- SET ANSI_PADDING ON
--GO
CREATE TABLE [dbo].[PayHistDtl](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[PayHistHdrID] [bigint] NOT NULL,
	[CoaName] [nvarchar](50) NOT NULL,
	[InstSeqNo] [smallint] NOT NULL,
	[AssetSeqNo] [smallint] NOT NULL,
	[YearNum] [int] NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[DebitAmt] [numeric](17, 2) NOT NULL,
	[CreditAmt] [numeric](17, 2) NOT NULL,
	[LCAmt] [numeric](17, 2) NOT NULL,
	[LCDays] [smallint] NOT NULL,
	[UsrUpd] [nvarchar](20) NULL,
	[DtmUpd] [smalldatetime] NULL,
	[UsrCrt] [nvarchar](20) NULL,
	[DtmCrt] [smalldatetime] NULL,
 CONSTRAINT [PK_PayHistDtl] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PayHistD]
) ON [PayHistD]

--GO
-- SET ANSI_PADDING ON
--GO
