
/****** Object:  Table [dbo].[AprvlStatusLogs]    Script Date: 11/3/2014 8:27:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AprvlStatusLogs](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[AprvlStatusID] [bigint] NULL,
	[AprvlNo] [varchar](50) NOT NULL,
	[AprvlSeqNum] [int] NOT NULL,
	[UsrRequest] [varchar](12) NOT NULL,
	[AprvlSchemeID] [char](10) NOT NULL,
	[RequestDate] [datetime] NULL,
	[SecurityCode] [char](18) NULL,
	[WOA] [char](1) NULL,
	[IsViaSMS] [int] NOT NULL,
	[IsViaEmail] [int] NOT NULL,
	[IsViaFax] [int] NOT NULL,
	[FaxNo] [char](10) NULL,
	[AreaFaxNO] [char](4) NOT NULL,
	[AprvlDate] [datetime] NULL,
	[AprvlStatus] [char](1) NULL,
	[AprvlResult] [char](1) NULL,
	[AprvlNote] [varchar](8000) NULL,
	[LimitValue] [numeric](18, 2) NULL,
	[AprvlValue] [numeric](18, 2) NULL,
	[IsForward] [int] NULL,
	[UsrAprvl] [varchar](12) NULL,
	[UsrSecurityCode] [varchar](12) NULL,
	[AprvlRoleID] [varchar](3) NULL,
	[TransactionNo] [varchar](30) NULL,
	[MainUsrAprvl] [varchar](12) NULL,
	[IsEverRejected] [int] NULL,
	[NumOfAutomaticEscalation] [int] NULL,
	[PathLevelAprvl] [varchar](8000) NULL,
	[PathSeqNum] [int] NULL,
	[AprvlReasonID] [varchar](10) NULL,
	[UsrUpd] [varchar](50) NULL,
	[DtmUpd] [smalldatetime] NULL,
	[UsrCrt] [varchar](50) NULL,
	[DtmCrt] [smalldatetime] NULL,
 CONSTRAINT [PK_AprvlStatusLogs] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [Approval]
) ON [Approval]

GO
SET ANSI_PADDING ON
GO
ALTER TABLE [dbo].[AprvlStatusLogs]  WITH CHECK ADD  CONSTRAINT [FK_AprvlStatusLogs_AprvlStatus] FOREIGN KEY([AprvlStatusID])
REFERENCES [dbo].[AprvlStatus] ([ID])
GO
ALTER TABLE [dbo].[AprvlStatusLogs] CHECK CONSTRAINT [FK_AprvlStatusLogs_AprvlStatus]
GO
