
/****** Object:  Table [dbo].[PayHistHdr]    Script Date: 11/3/2014 8:27:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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

GO
SET ANSI_PADDING ON
GO
ALTER TABLE [dbo].[PayHistHdr] ADD  CONSTRAINT [DF_PayHistHdr_IsCorrection]  DEFAULT ((1)) FOR [IsCorrection]
GO
ALTER TABLE [dbo].[PayHistHdr] ADD  CONSTRAINT [DF_PayHistHdr_UsrCrt]  DEFAULT (N'system_user()') FOR [UsrCrt]
GO
ALTER TABLE [dbo].[PayHistHdr] ADD  CONSTRAINT [DF_PayHistHdr_DtmCrt]  DEFAULT (getdate()) FOR [DtmCrt]
GO
ALTER TABLE [dbo].[PayHistHdr]  WITH NOCHECK ADD  CONSTRAINT [FK_PayHistHdr_JrnlJob] FOREIGN KEY([JrnlJobID])
REFERENCES [dbo].[JrnlJob] ([ID])
GO
ALTER TABLE [dbo].[PayHistHdr] CHECK CONSTRAINT [FK_PayHistHdr_JrnlJob]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Branch ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayHistHdr', @level2type=N'COLUMN',@level2name=N'AgrmntID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Payment History Sequence No' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayHistHdr', @level2type=N'COLUMN',@level2name=N'HistSeqNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Value Date' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayHistHdr', @level2type=N'COLUMN',@level2name=N'ValueDt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Business Date' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayHistHdr', @level2type=N'COLUMN',@level2name=N'PostingDt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Total nilai pembayaran' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayHistHdr', @level2type=N'COLUMN',@level2name=N'Amt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Way of Payment' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayHistHdr', @level2type=N'COLUMN',@level2name=N'WOP'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Branch yang punya contract' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayHistHdr', @level2type=N'COLUMN',@level2name=N'OfficeID_X'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Bank Account Id jika related to Bank Account' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayHistHdr', @level2type=N'COLUMN',@level2name=N'BankAccID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Status dari payment history apakah bisa dikoreksi ?' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayHistHdr', @level2type=N'COLUMN',@level2name=N'IsCorrection'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Correction Id Code' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayHistHdr', @level2type=N'COLUMN',@level2name=N'CorrHistSeq'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Kwitansi Cetakan ke' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayHistHdr', @level2type=N'COLUMN',@level2name=N'PrintedNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Kwitansi dicetak oleh' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayHistHdr', @level2type=N'COLUMN',@level2name=N'PrintBy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Terakhir cetak kwitansi' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayHistHdr', @level2type=N'COLUMN',@level2name=N'LastPrtDt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'No. Reference Journal' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayHistHdr', @level2type=N'COLUMN',@level2name=N'JrnlCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Process Id apa  yang mengcreate PaymentHistory' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayHistHdr', @level2type=N'COLUMN',@level2name=N'JrnlTrxID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Status Default Application pada saat pembayaran' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayHistHdr', @level2type=N'COLUMN',@level2name=N'DefaultStat'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'User Updated' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayHistHdr', @level2type=N'COLUMN',@level2name=N'UsrUpd'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date Time Updated' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayHistHdr', @level2type=N'COLUMN',@level2name=N'DtmUpd'
GO
