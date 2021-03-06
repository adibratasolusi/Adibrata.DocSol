
/****** Object:  Table [dbo].[Ms_Journal_Seq]    Script Date: 11/3/2014 8:27:58 AM ******/
-- SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
-- SET ANSI_PADDING ON
--GO
CREATE TABLE [dbo].[Ms_Journal_Seq](
	[OfficeID] [int] NOT NULL,
	[JrnlTrxID] [int] NOT NULL,
	[JournalName] [nvarchar](50) NOT NULL,
	[Seq_No] [int] NOT NULL,
	[Length_Number] [int] NOT NULL,
	[ResetFlag] [char](1) NOT NULL,
	[Prefix] [varchar](10) NOT NULL,
	[Suffix] [varchar](10) NOT NULL,
	[IsOffice] [char](1) NOT NULL,
	[IsYear] [char](1) NOT NULL,
	[IsMonth] [char](1) NOT NULL,
	[FormatNumber] [nvarchar](100) NULL,
	[UsrUpd] [varchar](50) NULL,
	[DtmUpd] [datetime] NULL,
	[UsrCrt] [varchar](50) NOT NULL,
	[DtmCrt] [datetime] NOT NULL,
 CONSTRAINT [PK_JournalSeqNo] PRIMARY KEY CLUSTERED 
(
	[OfficeID] ASC,
	[JrnlTrxID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [JournalEngine]
) ON [JournalEngine]

--GO
-- SET ANSI_PADDING ON
--GO
CREATE TABLE [dbo].[Ms_Journal_Seq]  WITH NOCHECK ADD  CONSTRAINT [FK_Ms_Journal_Seq_MS_JrnlTrx] FOREIGN KEY([JrnlTrxID])
REFERENCES [dbo].[MS_JrnlTrx] ([ID])
--GO
CREATE TABLE [dbo].[Ms_Journal_Seq] CHECK CONSTRAINT [FK_Ms_Journal_Seq_MS_JrnlTrx]
--GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'User Updated' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Ms_Journal_Seq', @level2type=N'COLUMN',@level2name=N'UsrUpd'
--GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date Time Updated' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Ms_Journal_Seq', @level2type=N'COLUMN',@level2name=N'DtmUpd'
--GO
