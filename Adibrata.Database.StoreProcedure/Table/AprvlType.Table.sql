
/****** Object:  Table [dbo].[AprvlType]    Script Date: 11/3/2014 8:27:58 AM ******/
-- SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
-- SET ANSI_PADDING ON
--GO
CREATE TABLE [dbo].[AprvlType](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[AprvlTypeCode] [varchar](50) NOT NULL,
	[AprvlTypeDesc] [varchar](100) NOT NULL,
	[FinalMethodName] [varchar](50) NOT NULL,
	[IsActive] [int] NULL,
	[UsrUpd] [varchar](50) NULL,
	[DtmUpd] [smalldatetime] NULL,
	[UsrCrt] [varchar](50) NULL,
	[DtmCrt] [smalldatetime] NULL,
 CONSTRAINT [PK_AprvlType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [Approval]
) ON [Approval]

--GO
-- SET ANSI_PADDING ON
--GO
