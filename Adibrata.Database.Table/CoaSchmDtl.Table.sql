
/****** Object:  Table [dbo].[CoaSchmDtl]    Script Date: 11/3/2014 8:27:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CoaSchmDtl](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CoaSchmHdrID] [int] NOT NULL,
	[CoaName] [nvarchar](50) NOT NULL,
	[CoaCode] [nvarchar](25) NOT NULL,
	[CoaDesc] [nvarchar](50) NOT NULL,
	[UsrUpd] [nvarchar](20) NULL,
	[DtmUpd] [datetime] NULL,
	[UsrCrt] [nvarchar](20) NULL,
	[DtmCrt] [datetime] NULL,
 CONSTRAINT [PK_CoaSchmDtl] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [JournalEngine]
) ON [JournalEngine]

GO
ALTER TABLE [dbo].[CoaSchmDtl]  WITH NOCHECK ADD  CONSTRAINT [FK_CoaSchmDtl_CoaSchmHdr] FOREIGN KEY([CoaSchmHdrID])
REFERENCES [dbo].[CoaSchmHdr] ([ID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[CoaSchmDtl] CHECK CONSTRAINT [FK_CoaSchmDtl_CoaSchmHdr]
GO
ALTER TABLE [dbo].[CoaSchmDtl]  WITH CHECK ADD  CONSTRAINT [FK_CoaSchmDtl_Ms_Coa] FOREIGN KEY([CoaName])
REFERENCES [dbo].[Ms_Coa] ([CoaName])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[CoaSchmDtl] CHECK CONSTRAINT [FK_CoaSchmDtl_Ms_Coa]
GO
