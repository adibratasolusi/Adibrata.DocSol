
/****** Object:  Table [dbo].[AprvlAltrnUsr]    Script Date: 11/3/2014 8:27:58 AM ******/
-- SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
-- SET ANSI_PADDING ON
--GO
CREATE TABLE [dbo].[AprvlAltrnUsr](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[AprvlSchemeCode] [varchar](50) NOT NULL,
	[AprvlTypeCode] [varchar](50) NOT NULL,
	[UsrAprvl] [varchar](50) NOT NULL,
	[AltSeqNum] [int] NOT NULL,
	[EscalationUsr] [varchar](50) NOT NULL,
	[IsActive] [int] NOT NULL,
	[UsrUpd] [varchar](50) NULL,
	[DtmUpd] [smalldatetime] NULL,
	[UsrCrt] [varchar](50) NULL,
	[dtmCrt] [smalldatetime] NULL,
 CONSTRAINT [PK_AprvlAltrnUsr] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [Approval]
) ON [Approval]

--GO
-- SET ANSI_PADDING ON
--GO
