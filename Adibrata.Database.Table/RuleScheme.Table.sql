
/****** Object:  Table [dbo].[RuleScheme]    Script Date: 11/3/2014 8:27:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RuleScheme](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[RuleSchmCode] [nvarchar](20) NULL,
	[RuleSchmDesc] [nvarchar](50) NULL,
	[RuleFile] [varbinary](max) NULL,
	[RuleFileName] [varchar](500) NULL,
	[DtmUpd] [datetime] NULL,
	[UsrUpd] [nvarchar](20) NULL,
	[DtmCrt] [datetime] NULL,
	[UsrCrt] [nvarchar](20) NULL,
 CONSTRAINT [PK_RuleSchemeHdr] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [RuleEngine]
) ON [RuleEngine] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING ON
GO
