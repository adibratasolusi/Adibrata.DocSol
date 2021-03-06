
/****** Object:  Table [dbo].[CoaSchmHdr]    Script Date: 11/3/2014 8:27:58 AM ******/
--SET ANSI_NULLS ON
----GO
----SET QUOTED_IDENTIFIER ON
----GO
--SET ANSI_PADDING ON
----GO
CREATE TABLE [dbo].[CoaSchmHdr](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[IsActive] [char](1) NOT NULL,
	[DtmCrt] [datetime] NULL,
	[UsrCrt] [nvarchar](50) NULL,
	[UsrUpd] [nvarchar](50) NOT NULL,
	[DtmUpd] [datetime] NOT NULL,
 CONSTRAINT [PK_CoaSchmName] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [JournalEngine]
) ON [JournalEngine]

--GO
