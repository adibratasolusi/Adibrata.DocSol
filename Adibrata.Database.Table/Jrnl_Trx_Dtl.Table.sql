
/****** Object:  Table [dbo].[Jrnl_Trx_Dtl]    Script Date: 11/3/2014 8:27:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Jrnl_Trx_Dtl](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Jrnl_Trx_Hdr_ID] [bigint] NOT NULL,
	[SequenceNo] [int] NOT NULL,
	[CoaCoy] [nvarchar](50) NULL,
	[CoaOffice] [nvarchar](30) NOT NULL,
	[CoaCode] [nvarchar](50) NULL,
	[JrnlTrxID] [int] NOT NULL,
	[Tr_Desc] [varchar](max) NULL,
	[Post] [char](1) NOT NULL,
	[CurrID] [int] NULL,
	[Curr_Rate] [numeric](17, 2) NOT NULL,
	[Amt] [numeric](17, 2) NOT NULL,
	[AmtOri] [numeric](17, 2) NULL,
	[CoaCode_X] [char](25) NULL,
	[CoaOffice_X] [char](3) NULL,
	[CoaName] [nvarchar](50) NULL,
	[ProductId] [bigint] NULL,
	[DepartID] [int] NULL,
	[UsrUpd] [nvarchar](20) NULL,
	[DtmUpd] [datetime] NULL,
	[UsrCrt] [nvarchar](20) NOT NULL,
	[DtmCrt] [datetime] NOT NULL,
 CONSTRAINT [PK_Jrnl_Trx_Dtl] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [JournalD]
) ON [JournalD] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING ON
GO
ALTER TABLE [dbo].[Jrnl_Trx_Dtl]  WITH NOCHECK ADD  CONSTRAINT [FK_Jrnl_Trx_Dtl_Jrnl_Trx_Hdr] FOREIGN KEY([Jrnl_Trx_Hdr_ID])
REFERENCES [dbo].[Jrnl_Trx_Hdr] ([ID])
GO
ALTER TABLE [dbo].[Jrnl_Trx_Dtl] CHECK CONSTRAINT [FK_Jrnl_Trx_Dtl_Jrnl_Trx_Hdr]
GO
ALTER TABLE [dbo].[Jrnl_Trx_Dtl]  WITH CHECK ADD  CONSTRAINT [FK_Jrnl_Trx_Dtl_Ms_Coa] FOREIGN KEY([CoaName])
REFERENCES [dbo].[Ms_Coa] ([CoaName])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Jrnl_Trx_Dtl] CHECK CONSTRAINT [FK_Jrnl_Trx_Dtl_Ms_Coa]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'No urut supaya tidak duplikasi' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Jrnl_Trx_Dtl', @level2type=N'COLUMN',@level2name=N'SequenceNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Company Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Jrnl_Trx_Dtl', @level2type=N'COLUMN',@level2name=N'CoaCoy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Branch Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Jrnl_Trx_Dtl', @level2type=N'COLUMN',@level2name=N'CoaOffice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nilai Diambil dari MS_JrnlTrx' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Jrnl_Trx_Dtl', @level2type=N'COLUMN',@level2name=N'JrnlTrxID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Keterangan Detail Journal' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Jrnl_Trx_Dtl', @level2type=N'COLUMN',@level2name=N'Tr_Desc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'''D'' - Debit atau ''C'' - Credit' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Jrnl_Trx_Dtl', @level2type=N'COLUMN',@level2name=N'Post'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Jrnl_Trx_Dtl', @level2type=N'COLUMN',@level2name=N'Amt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Kontra Account, Diisi dengan COA Cash/Bank apabila Cash Bank Transaction' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Jrnl_Trx_Dtl', @level2type=N'COLUMN',@level2name=N'CoaCode_X'
GO
