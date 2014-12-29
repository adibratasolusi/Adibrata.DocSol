USE [SMARTCMS]
GO

/****** Object:  Table [dbo].[MS_Menu]    Script Date: 12/29/2014 2:23:49 PM ******/
DROP TABLE [dbo].[MS_Menu]
GO

/****** Object:  Table [dbo].[MS_Menu]    Script Date: 12/29/2014 2:23:49 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MS_Menu](
	[MenuLevel] [bigint] NOT NULL,
	[FormID] [bigint] NULL,
	[ParentLevel] [bigint] NULL,
	[Root] [smallint] NULL,
	[MenuDesc] [varchar](50) NULL,
	[IsForm] [smallint] NULL,
	[IsActive] [smallint] NULL,
	[MenuOrder] [smallint] NULL CONSTRAINT [DF_MS_Menu_MenuOrder]  DEFAULT ((0)),
	[UsrUpd] [varchar](50) NULL,
	[DtmUpd] [smalldatetime] NULL,
	[UsrCrt] [varchar](50) NULL,
	[DtmCrt] [smalldatetime] NULL,
 CONSTRAINT [PK_MS_Menu_1] PRIMARY KEY CLUSTERED 
(
	[MenuLevel] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING ON
GO

