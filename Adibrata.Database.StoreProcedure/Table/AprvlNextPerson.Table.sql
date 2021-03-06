
/****** Object:  Table [dbo].[AprvlNextPerson]    Script Date: 11/3/2014 8:27:58 AM ******/
-- SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
-- SET ANSI_PADDING ON
--GO
CREATE TABLE [dbo].[AprvlNextPerson](
	[AprvlSchemeID] [char](5) NOT NULL,
	[AprvlSeqNum] [int] NOT NULL,
	[LoginID] [char](12) NOT NULL,
	[NextPerson] [char](12) NOT NULL,
	[Relation] [varchar](5) NULL,
	[UsrUpd] [varchar](50) NULL,
	[DtmUpd] [smalldatetime] NULL,
	[UsrCrt] [varchar](50) NULL,
	[dtmCrt] [smalldatetime] NULL,
 CONSTRAINT [PK_AprvlNextPerson] PRIMARY KEY CLUSTERED 
(
	[AprvlSchemeID] ASC,
	[AprvlSeqNum] ASC,
	[LoginID] ASC,
	[NextPerson] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [Approval]
) ON [Approval]

--GO
-- SET ANSI_PADDING ON
--GO
