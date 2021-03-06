
/****** Object:  Table [dbo].[CashBankMutHdr]    Script Date: 11/3/2014 8:27:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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

GO
SET ANSI_PADDING ON
GO
ALTER TABLE [dbo].[CashBankMutHdr] ADD  CONSTRAINT [DF_CashBankMutationHdr_CurrencyID]  DEFAULT ('-') FOR [CurrID]
GO
ALTER TABLE [dbo].[CashBankMutHdr] ADD  CONSTRAINT [DF_CashBankMutationHdr_UsrCrt]  DEFAULT (suser_sname()) FOR [UsrCrt]
GO
ALTER TABLE [dbo].[CashBankMutHdr] ADD  CONSTRAINT [DF_CashBankMutationHdr_DtmCrt]  DEFAULT (getdate()) FOR [DtmCrt]
GO
ALTER TABLE [dbo].[CashBankMutHdr]  WITH NOCHECK ADD  CONSTRAINT [FK_CashBankMutHdr_JrnlJob] FOREIGN KEY([JrnlJobID])
REFERENCES [dbo].[JrnlJob] ([ID])
GO
ALTER TABLE [dbo].[CashBankMutHdr] CHECK CONSTRAINT [FK_CashBankMutHdr_JrnlJob]
GO
ALTER TABLE [dbo].[CashBankMutHdr]  WITH NOCHECK ADD  CONSTRAINT [FK_CashBankMutHdr_JrnlSchmHdr] FOREIGN KEY([JrnlShmHdrID])
REFERENCES [dbo].[JrnlSchmHdr] ([ID])
GO
ALTER TABLE [dbo].[CashBankMutHdr] CHECK CONSTRAINT [FK_CashBankMutHdr_JrnlSchmHdr]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Branch yang bertransaksi' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CashBankMutHdr', @level2type=N'COLUMN',@level2name=N'OfficeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'No. Voucher Penerimaan' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CashBankMutHdr', @level2type=N'COLUMN',@level2name=N'VoucherNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'R - Receive dan D - Disburse' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CashBankMutHdr', @level2type=N'COLUMN',@level2name=N'RcvDsbFlag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Way of Payment' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CashBankMutHdr', @level2type=N'COLUMN',@level2name=N'WOP'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Merupakan Net Amount (Grand Amount - Card Charge Amount By Bank)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CashBankMutHdr', @level2type=N'COLUMN',@level2name=N'Amount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Reference No' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CashBankMutHdr', @level2type=N'COLUMN',@level2name=N'ReffNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Bank Account BFI' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CashBankMutHdr', @level2type=N'COLUMN',@level2name=N'BankAccID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Login Id Cashier' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CashBankMutHdr', @level2type=N'COLUMN',@level2name=N'CashierId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Opening Sequence Cashier' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CashBankMutHdr', @level2type=N'COLUMN',@level2name=N'OpeningSequence'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Jika berbeda dengan BranchId berarti transaksi InterBranch' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CashBankMutHdr', @level2type=N'COLUMN',@level2name=N'OfficeID_X'
GO
