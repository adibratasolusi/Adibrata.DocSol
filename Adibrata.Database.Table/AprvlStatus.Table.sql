
/****** Object:  Table [dbo].[AprvlStatus]    Script Date: 11/3/2014 8:27:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AprvlStatus](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[AprvlCode] [varchar](50) NOT NULL,
	[IsFinal] [int] NOT NULL,
	[AprvlSchemeID] [bigint] NOT NULL,
	[RequestDate] [datetime] NOT NULL,
	[UsrRequest] [varchar](12) NOT NULL,
	[AprvlDate] [datetime] NULL,
	[AprvlStatus] [char](1) NULL,
	[AprvlResult] [char](1) NULL,
	[AprvlNote] [varchar](8000) NULL,
	[LimitValue] [numeric](20, 2) NULL,
	[AprvlValue] [numeric](20, 2) NULL,
	[IsForward] [int] NULL,
	[UsrAprvl] [varchar](12) NOT NULL,
	[UsrSecurityCode] [varchar](50) NULL,
	[AprvlRoleID] [varchar](50) NULL,
	[TransactionCode] [varchar](20) NULL,
	[WOA] [varchar](50) NULL,
	[IsEverRejected] [int] NULL,
	[FinalLevelAprvl] [numeric](2, 0) NULL,
	[ExpiredAprvlTime] [datetime] NULL,
	[NotificationTime] [datetime] NULL,
	[MainUsrAprvl] [char](12) NULL,
	[IsHold] [int] NULL,
	[PathLevelAprvl] [varchar](8000) NULL,
	[PathSeqNum] [int] NULL,
	[AprvlReasonID] [varchar](10) NULL,
	[UsrUpd] [varchar](50) NULL,
	[DtmUpd] [smalldatetime] NULL,
	[UsrCrt] [varchar](50) NULL,
	[dtmCrt] [smalldatetime] NULL,
 CONSTRAINT [PK_AprvlStatus] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [Approval]
) ON [Approval]

GO
SET ANSI_PADDING ON
GO
ALTER TABLE [dbo].[AprvlStatus]  WITH CHECK ADD  CONSTRAINT [FK_AprvlStatus_AprvlTypeScheme] FOREIGN KEY([AprvlSchemeID])
REFERENCES [dbo].[AprvlTypeScheme] ([ID])
GO
ALTER TABLE [dbo].[AprvlStatus] CHECK CONSTRAINT [FK_AprvlStatus_AprvlTypeScheme]
GO
