
/****** Object:  Table [dbo].[AprvlTypeScheme]    Script Date: 11/3/2014 8:27:58 AM ******/
-- SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
-- SET ANSI_PADDING ON
--GO
CREATE TABLE [dbo].[AprvlTypeScheme](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[AprvlSchemeCode] [varchar](50) NOT NULL,
	[AprvlSchemeName] [varchar](50) NOT NULL,
	[AprvlTypeID] [bigint] NOT NULL,
	[IsStaging] [int] NOT NULL,
	[IsLimitNeeded] [int] NOT NULL,
	[TransactionNoLabel] [varchar](30) NULL,
	[TransactionNoLink] [varchar](150) NULL,
	[OptionalLabel1] [varchar](30) NULL,
	[OptionalLabel2] [varchar](30) NULL,
	[OptionalLink1] [varchar](500) NULL,
	[OptionalLink2] [varchar](500) NULL,
	[OptionalSQL1] [varchar](500) NULL,
	[OptionalSQL2] [varchar](500) NULL,
	[NoteLabel] [varchar](30) NULL,
	[NoteSQLCmd] [varchar](500) NULL,
	[LimitLabel] [varchar](50) NULL,
	[LimitSQLCmd] [varchar](500) NULL,
	[LinkLabel1] [varchar](30) NULL,
	[LinkLabel2] [varchar](30) NULL,
	[LinkLabel3] [varchar](30) NULL,
	[LinkLabel4] [varchar](30) NULL,
	[LinkLabel5] [varchar](30) NULL,
	[LinkAddress1] [varchar](500) NULL,
	[LinkAddress2] [varchar](500) NULL,
	[LinkAddress3] [varchar](500) NULL,
	[LinkAddress4] [varchar](500) NULL,
	[LinkAddress5] [varchar](500) NULL,
	[IsSystem] [int] NULL,
	[UsrUpd] [varchar](50) NULL,
	[DtmUpd] [smalldatetime] NULL,
	[UsrCrt] [varchar](50) NULL,
	[DtmCrt] [smalldatetime] NULL,
 CONSTRAINT [PK_AprvlTypeScheme] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [Approval]
) ON [Approval]

--GO
-- SET ANSI_PADDING ON
--GO
CREATE TABLE [dbo].[AprvlTypeScheme]  WITH CHECK ADD  CONSTRAINT [FK_AprvlTypeScheme_AprvlType] FOREIGN KEY([AprvlTypeID])
REFERENCES [dbo].[AprvlType] ([ID])
--GO
CREATE TABLE [dbo].[AprvlTypeScheme] CHECK CONSTRAINT [FK_AprvlTypeScheme_AprvlType]
--GO
