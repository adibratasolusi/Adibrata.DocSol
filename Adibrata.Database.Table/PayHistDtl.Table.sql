
/****** Object:  Table [dbo].[PayHistDtl]    Script Date: 11/3/2014 8:27:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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

GO
SET ANSI_PADDING ON
GO
ALTER TABLE [dbo].[PayHistDtl] ADD  CONSTRAINT [DF_PayHistDtl_AssetSeqNo]  DEFAULT ((0)) FOR [AssetSeqNo]
GO
ALTER TABLE [dbo].[PayHistDtl] ADD  CONSTRAINT [DF_PayHistDtl_YearNum]  DEFAULT ((0)) FOR [YearNum]
GO
ALTER TABLE [dbo].[PayHistDtl]  WITH NOCHECK ADD  CONSTRAINT [FK_PayHistDtl_PayHistHdr] FOREIGN KEY([PayHistHdrID])
REFERENCES [dbo].[PayHistHdr] ([ID])
GO
ALTER TABLE [dbo].[PayHistDtl] CHECK CONSTRAINT [FK_PayHistDtl_PayHistHdr]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Payment Allocation ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayHistDtl', @level2type=N'COLUMN',@level2name=N'CoaName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Installment Sequence No.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayHistDtl', @level2type=N'COLUMN',@level2name=N'InstSeqNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayHistDtl', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Amount Penambah' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayHistDtl', @level2type=N'COLUMN',@level2name=N'DebitAmt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Amount Pengurang' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayHistDtl', @level2type=N'COLUMN',@level2name=N'CreditAmt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Late Charges Amount' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayHistDtl', @level2type=N'COLUMN',@level2name=N'LCAmt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'User Updated' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayHistDtl', @level2type=N'COLUMN',@level2name=N'UsrUpd'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date Time Updated' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayHistDtl', @level2type=N'COLUMN',@level2name=N'DtmUpd'
GO
