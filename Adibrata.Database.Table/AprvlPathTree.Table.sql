
/****** Object:  Table [dbo].[AprvlPathTree]    Script Date: 11/3/2014 8:27:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AprvlPathTree](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[AprvlSchemeID] [bigint] NOT NULL,
	[AprvlSeqNum] [int] NOT NULL,
	[ParentAprvlSeqNum] [int] NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[Level_] [numeric](2, 0) NOT NULL,
	[MaxLimit] [numeric](18, 2) NOT NULL,
	[CanFinalReject] [int] NOT NULL,
	[CanFinalApprove] [int] NOT NULL,
	[CanEscalation] [int] NOT NULL,
	[RejectAction] [char](1) NOT NULL,
	[CanChangeFinalLevel] [int] NULL,
	[AprvlDuration] [numeric](4, 0) NULL,
	[UsrUpd] [varchar](50) NULL,
	[DtmUpd] [smalldatetime] NULL,
	[UsrCrt] [varchar](50) NULL,
	[dtmCrt] [smalldatetime] NULL,
 CONSTRAINT [PK_AprvlPathTree] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [Approval]
) ON [Approval]

GO
SET ANSI_PADDING ON
GO
