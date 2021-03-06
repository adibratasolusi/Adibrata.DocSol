
/****** Object:  Table [dbo].[AprvlPathTreeMember]    Script Date: 11/3/2014 8:27:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AprvlPathTreeMember](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[AprvlPathTreeID] [bigint] NOT NULL,
	[AprvlSchemeID] [bigint] NOT NULL,
	[AprvlSeqNum] [int] NOT NULL,
	[LoginID] [varchar](50) NOT NULL,
	[LimitValue] [decimal](18, 2) NOT NULL,
	[ProcessDuration] [varchar](18) NULL,
	[AutoUsr] [varchar](50) NULL,
	[UsrUpd] [varchar](50) NULL,
	[DtmUpd] [smalldatetime] NULL,
	[UsrCrt] [varchar](50) NULL,
	[dtmCrt] [smalldatetime] NULL,
 CONSTRAINT [PK_AprvlPathTreeMember] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [Approval]
) ON [Approval]

GO
SET ANSI_PADDING ON
GO
ALTER TABLE [dbo].[AprvlPathTreeMember]  WITH CHECK ADD  CONSTRAINT [FK_AprvlPathTreeMember_AprvlPathTree] FOREIGN KEY([AprvlPathTreeID])
REFERENCES [dbo].[AprvlPathTree] ([ID])
GO
ALTER TABLE [dbo].[AprvlPathTreeMember] CHECK CONSTRAINT [FK_AprvlPathTreeMember_AprvlPathTree]
GO
