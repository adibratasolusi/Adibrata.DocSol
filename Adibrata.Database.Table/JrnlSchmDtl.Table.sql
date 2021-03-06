
/****** Object:  Table [dbo].[JrnlSchmDtl]    Script Date: 11/3/2014 8:27:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[JrnlSchmDtl](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[JrnlSchmHdrID] [int] NULL,
	[SequenceNo] [int] NULL,
	[CoaName] [nvarchar](50) NULL,
	[CoaSourceTable] [nvarchar](50) NULL,
	[IsCoaHeader] [char](1) NULL,
	[Post] [char](1) NULL,
	[IsMultipleDtl] [char](1) NULL,
	[TblSourceDtl] [nvarchar](20) NULL,
	[ColSourceDtl] [nvarchar](50) NULL,
	[ColFilterDtlID] [nvarchar](50) NULL,
	[IsCreatePaymentHistoryDetail] [char](1) NULL,
	[IsCreateCashBankMutationDetail] [char](1) NULL,
	[IsCreateJournalDetail] [char](1) NULL,
	[DepartID] [nvarchar](50) NULL,
	[UsrCrt] [nvarchar](20) NULL,
	[DtmCrt] [datetime] NULL,
	[UsrUpd] [nvarchar](20) NULL,
	[DtmUpd] [datetime] NULL,
 CONSTRAINT [PK_JournalSchemeDtl] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [JournalEngine]
) ON [JournalEngine]

GO
SET ANSI_PADDING ON
GO
ALTER TABLE [dbo].[JrnlSchmDtl]  WITH NOCHECK ADD  CONSTRAINT [FK_JrnlSchmDtl_JrnlSchmHdr] FOREIGN KEY([JrnlSchmHdrID])
REFERENCES [dbo].[JrnlSchmHdr] ([ID])
GO
ALTER TABLE [dbo].[JrnlSchmDtl] CHECK CONSTRAINT [FK_JrnlSchmDtl_JrnlSchmHdr]
GO
ALTER TABLE [dbo].[JrnlSchmDtl]  WITH CHECK ADD  CONSTRAINT [FK_JrnlSchmDtl_Ms_Coa] FOREIGN KEY([CoaName])
REFERENCES [dbo].[Ms_Coa] ([CoaName])
GO
ALTER TABLE [dbo].[JrnlSchmDtl] CHECK CONSTRAINT [FK_JrnlSchmDtl_Ms_Coa]
GO
