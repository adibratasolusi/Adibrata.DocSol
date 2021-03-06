
/****** Object:  Table [dbo].[JrnlSchmHdr]    Script Date: 11/3/2014 8:27:58 AM ******/
-- SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
-- SET ANSI_PADDING ON
--GO
CREATE TABLE [dbo].[JrnlSchmHdr](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TrxSchmCode] [nvarchar](20) NULL,
	[TrxSchmDesc] [nvarchar](50) NULL,
	[TableTrx] [nvarchar](50) NULL,
	[Trx_Code] [nvarchar](50) NULL,
	[OfficeID] [nvarchar](50) NULL,
	[AgrmntID] [nvarchar](50) NULL,
	[OfficeID_X] [nvarchar](50) NULL,
	[PostingDt] [nvarchar](50) NULL,
	[ValueDt] [nvarchar](50) NULL,
	[ReffNo] [nvarchar](50) NULL,
	[BankAccId] [nvarchar](50) NULL,
	[CurrID] [nvarchar](50) NULL,
	[CurrRate] [nvarchar](50) NULL,
	[BankPortion] [int] NULL,
	[RcvDisbFlag] [char](1) NULL,
	[CashierID] [nvarchar](50) NULL,
	[CashierOpen] [nvarchar](50) NULL,
	[AmountTrx] [nvarchar](50) NULL,
	[WOP] [nvarchar](50) NULL,
	[JrnlFlags] [char](1) NULL,
	[ReceivedFrom] [nvarchar](50) NULL,
	[ReceiptNo] [nvarchar](50) NULL,
	[IsCreatePaymentHistory] [char](1) NULL,
	[IsCreateJournal] [char](1) NULL,
	[IsCreateCashBankMutation] [char](1) NULL,
	[JrnlTrxID] [int] NULL,
	[UsrCrt] [nvarchar](20) NULL,
	[DtmCrt] [datetime] NULL,
	[UsrUpd] [nvarchar](20) NULL,
	[DtmUpd] [datetime] NULL,
 CONSTRAINT [PK_JournalSchemeHdr] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [JournalEngine]
) ON [JournalEngine]

--GO
-- SET ANSI_PADDING ON
--GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Di isi dengan no agreement' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'JrnlSchmHdr', @level2type=N'COLUMN',@level2name=N'ReffNo'
--GO
