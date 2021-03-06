
/****** Object:  Table [dbo].[PayRcv]    Script Date: 11/3/2014 8:27:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PayRcv](
	[ID] [bigint] NOT NULL,
	[Trx_Code] [nvarchar](50) NULL,
	[OfficeID] [int] NULL,
	[AgrmntID] [bigint] NULL,
	[CoaSchmID] [int] NULL,
	[ReffNo] [nvarchar](50) NULL,
	[CurrID] [int] NULL,
	[CurrRate] [numeric](17, 2) NULL,
	[PostingDt] [date] NULL,
	[ValueDt] [date] NULL,
	[BankAccID] [int] NULL,
	[WOP] [nvarchar](5) NULL,
	[CashierID] [int] NULL,
	[OpeningSeqNo] [int] NULL,
	[TotalPayment] [numeric](17, 2) NULL,
	[InstallPaid] [numeric](17, 2) NULL,
	[LCInstallPaid] [numeric](17, 2) NULL,
	[InsurPaid] [numeric](17, 2) NULL,
	[LCInsurPaid] [numeric](17, 2) NULL,
	[RcvdFrom] [nvarchar](50) NULL,
	[ReceiptNo] [nvarchar](50) NULL,
	[OfficeID_X] [int] NULL,
	[JrnlNo] [nvarchar](50) NULL,
	[VoucherNo] [nvarchar](50) NULL,
	[HistSeqNo] [int] NULL,
	[JrnlJobID] [int] NULL,
	[JrnlJobCode] [nvarchar](50) NULL,
	[UsrCrt] [nvarchar](50) NULL,
	[DtmCrt] [datetime] NULL,
	[UsrUpd] [nvarchar](50) NULL,
	[DtmUpd] [datetime] NULL,
 CONSTRAINT [PK_PaymentReceive] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PaymentReceive]
) ON [PaymentReceive]

GO
ALTER TABLE [dbo].[PayRcv] ADD  CONSTRAINT [DF_PaymentReceive_UsrCrt]  DEFAULT (N'system_user()') FOR [UsrCrt]
GO
ALTER TABLE [dbo].[PayRcv] ADD  CONSTRAINT [DF_PaymentReceive_DtmCrt]  DEFAULT (getdate()) FOR [DtmCrt]
GO
ALTER TABLE [dbo].[PayRcv]  WITH CHECK ADD  CONSTRAINT [FK_PaymentReceive_Agrmnt] FOREIGN KEY([AgrmntID])
REFERENCES [dbo].[Agrmnt] ([ID])
GO
ALTER TABLE [dbo].[PayRcv] CHECK CONSTRAINT [FK_PaymentReceive_Agrmnt]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ReffNo bisa dimasukkan AgreementCode' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayRcv', @level2type=N'COLUMN',@level2name=N'ReffNo'
GO
