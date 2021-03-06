
/****** Object:  Table [dbo].[Jrnl_Trx_Hdr]    Script Date: 11/3/2014 8:27:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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

GO
SET ANSI_PADDING ON
GO
ALTER TABLE [dbo].[Jrnl_Trx_Hdr] ADD  CONSTRAINT [DF_Jrnl_Trx_Hdr_Flag]  DEFAULT ('O') FOR [Flag]
GO
ALTER TABLE [dbo].[Jrnl_Trx_Hdr] ADD  CONSTRAINT [DF_Jrnl_Trx_Hdr_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Jrnl_Trx_Hdr] ADD  CONSTRAINT [DF_Jrnl_Trx_Hdr_IsValid]  DEFAULT ((0)) FOR [IsValid]
GO
ALTER TABLE [dbo].[Jrnl_Trx_Hdr] ADD  CONSTRAINT [DF_Jrnl_Trx_Hdr_JournalNo]  DEFAULT ('') FOR [JournalNo]
GO
ALTER TABLE [dbo].[Jrnl_Trx_Hdr] ADD  CONSTRAINT [DF_Jrnl_Trx_Hdr_DtmUpd]  DEFAULT (getdate()) FOR [DtmUpd]
GO
ALTER TABLE [dbo].[Jrnl_Trx_Hdr] ADD  CONSTRAINT [DF_Jrnl_Trx_Hdr_UsrCrt]  DEFAULT (suser_sname()) FOR [UsrCrt]
GO
ALTER TABLE [dbo].[Jrnl_Trx_Hdr] ADD  CONSTRAINT [DF_Jrnl_Trx_Hdr_DtmCrt]  DEFAULT (getdate()) FOR [DtmCrt]
GO
ALTER TABLE [dbo].[Jrnl_Trx_Hdr]  WITH NOCHECK ADD  CONSTRAINT [FK_Jrnl_Trx_Hdr_JrnlJob] FOREIGN KEY([JobID])
REFERENCES [dbo].[JrnlJob] ([ID])
GO
ALTER TABLE [dbo].[Jrnl_Trx_Hdr] CHECK CONSTRAINT [FK_Jrnl_Trx_Hdr_JrnlJob]
GO
ALTER TABLE [dbo].[Jrnl_Trx_Hdr]  WITH NOCHECK ADD  CONSTRAINT [FK_Jrnl_Trx_Hdr_JrnlSchmHdr] FOREIGN KEY([JrnlSchmHdrId])
REFERENCES [dbo].[JrnlSchmHdr] ([ID])
GO
ALTER TABLE [dbo].[Jrnl_Trx_Hdr] CHECK CONSTRAINT [FK_Jrnl_Trx_Hdr_JrnlSchmHdr]
GO
ALTER TABLE [dbo].[Jrnl_Trx_Hdr]  WITH NOCHECK ADD  CONSTRAINT [FK_Jrnl_Trx_Hdr_MS_JrnlTrx] FOREIGN KEY([JrnlTrxID])
REFERENCES [dbo].[MS_JrnlTrx] ([ID])
GO
ALTER TABLE [dbo].[Jrnl_Trx_Hdr] CHECK CONSTRAINT [FK_Jrnl_Trx_Hdr_MS_JrnlTrx]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Generate dari master sequence journal' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Jrnl_Trx_Hdr', @level2type=N'COLUMN',@level2name=N'JrnlCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Reference Number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Jrnl_Trx_Hdr', @level2type=N'COLUMN',@level2name=N'Reff_No'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Reference Date' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Jrnl_Trx_Hdr', @level2type=N'COLUMN',@level2name=N'Reff_Date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'''OP'' Open; ''PO'' Posted to gl system' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Jrnl_Trx_Hdr', @level2type=N'COLUMN',@level2name=N'Status_Tr'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'''R'' - Receive, ''D'' - Disburse dan ''O'' - Others' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Jrnl_Trx_Hdr', @level2type=N'COLUMN',@level2name=N'Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'User Updated' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Jrnl_Trx_Hdr', @level2type=N'COLUMN',@level2name=N'UsrUpd'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date Time Updated' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Jrnl_Trx_Hdr', @level2type=N'COLUMN',@level2name=N'DtmUpd'
GO
