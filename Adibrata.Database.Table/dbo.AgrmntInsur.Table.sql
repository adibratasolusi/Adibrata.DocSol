USE [TBIG]
GO
/****** Object:  Table [dbo].[AgrmntInsur]    Script Date: 11/3/2014 8:27:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AgrmntInsur](
	[ID] [bigint] NOT NULL,
	[AgrmntID] [bigint] NULL,
	[InsCoyOfficeID] [bigint] NULL,
 CONSTRAINT [PK_AgrmntInsur] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [AgrmntInsurance]
) ON [AgrmntInsurance]

GO
