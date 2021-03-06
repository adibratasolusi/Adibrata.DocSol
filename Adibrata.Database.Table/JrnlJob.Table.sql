
/****** Object:  Table [dbo].[JrnlJob]    Script Date: 11/3/2014 8:27:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JrnlJob](
	[ID] [bigint] NOT NULL,
	[JobCode] [nvarchar](50) NULL,
	[JrnlSchmID] [int] NULL,
	[CoaSchmID] [int] NULL,
	[JobDate] [date] NULL,
	[JobPost] [date] NULL,
	[JobStatus] [nchar](2) NULL,
	[CoyID] [int] NULL,
	[TrxID] [bigint] NULL,
	[TrxCode] [nvarchar](50) NULL,
	[DtmUpd] [smalldatetime] NULL,
	[UsrUpd] [nvarchar](50) NULL,
	[DtmCrt] [smalldatetime] NULL,
	[UsrCrt] [nvarchar](50) NULL,
 CONSTRAINT [PK_JrnlJob] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [JournalEngine]
) ON [JournalEngine]

GO
ALTER TABLE [dbo].[JrnlJob]  WITH NOCHECK ADD  CONSTRAINT [FK_JrnlJob_JrnlSchmHdr] FOREIGN KEY([JrnlSchmID])
REFERENCES [dbo].[JrnlSchmHdr] ([ID])
GO
ALTER TABLE [dbo].[JrnlJob] CHECK CONSTRAINT [FK_JrnlJob_JrnlSchmHdr]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Di ambil dari generate no sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'JrnlJob', @level2type=N'COLUMN',@level2name=N'JobCode'
GO
