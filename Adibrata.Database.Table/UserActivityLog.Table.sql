
/****** Object:  Table [dbo].[UserActivityLog]    Script Date: 11/3/2014 8:27:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserActivityLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[ActivityDate] [datetime] NOT NULL,
	[IPAddress] [varchar](50) NOT NULL,
	[BrowserVersion] [varchar](50) NOT NULL,
	[UsrUpd] [varchar](50) NULL,
	[DtmUpd] [datetime] NULL,
	[UsrCrt] [varchar](50) NULL,
	[DtmCrt] [datetime] NULL,
 CONSTRAINT [PK_UserActivityLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON
GO
