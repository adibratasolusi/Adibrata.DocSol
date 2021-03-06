
/****** Object:  Table [dbo].[MsUser]    Script Date: 11/3/2014 8:27:58 AM ******/
-- SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
-- SET ANSI_PADDING ON
--GO
CREATE TABLE [dbo].[Ms_User](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[FullName] [varchar](50) NOT NULL,
	[Active] [int] NOT NULL,
	[IsConnect] [int] NOT NULL,
	[Password] [varchar](300) NULL,
	[SecurityQuestion] [varchar](50)  NULL,
	[SecurityAnswer] [varchar](50)  NULL,
	[ExpiredDate] [datetime] NULL,
	[SeqWrongPwd] [smallint] NULL,
	[UsrUpd] [varchar](50) NULL,
	[DtmUpd] [smalldatetime] NULL,
	[UsrCrt] [varchar](50) NULL,
	[DtmCrt] [smalldatetime] NULL,
 CONSTRAINT [PK_Sec_MsUser] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 100) ON [PRIMARY]
) ON [PRIMARY]

--GO
-- SET ANSI_PADDING ON
--GO
