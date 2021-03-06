
/****** Object:  Table [dbo].[CashBankMutHdr]    Script Date: 11/3/2014 8:27:58 AM ******/
-- SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
-- SET ANSI_PADDING ON
--GO
CREATE TABLE [dbo].[CashBankMutHdr](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[OfficeID] [int] NULL,
	[VoucherNo] [nvarchar](50) NULL,
	[ValueDt] [date] NULL,
	[PostingDt] [date] NULL,
	[Description] [nvarchar](100) NULL,
	[RcvDsbFlag] [char](1) NULL,
	[WOP] [nvarchar](5) NULL,
	[Amount] [numeric](17, 2) NULL,
	[RcvFrom] [nvarchar](50) NULL,
	[ReffNo] [nvarchar](50) NULL,
	[ReceiptNo] [nvarchar](50) NULL,
	[BankAccID] [int] NULL,
	[CurrID] [int] NULL,
	[CashierId] [int] NULL,
	[OpeningSequence] [int] NULL,
	[OfficeID_X] [int] NULL,
	[IsReconcile] [char](1) NULL,
	[ReconcileDate] [smalldatetime] NULL,
	[ReconcileBy] [nvarchar](20) NULL,
	[JrnlTrxID] [int] NULL,
	[AgrmntID] [bigint] NULL,
	[JrnlShmHdrID] [int] NULL,
	[JrnlCode] [nvarchar](50) NULL,
	[JrnlJobID] [bigint] NULL,
	[UsrUpd] [nvarchar](20) NULL,
	[DtmUpd] [smalldatetime] NULL,
	[UsrCrt] [nvarchar](20) NULL,
	[DtmCrt] [smalldatetime] NULL,
 CONSTRAINT [PK_CashBankMutHdr] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [BankBookH]
) ON [BankBookH]

--GO
-- SET ANSI_PADDING ON
--GO
