
/****** Object:  Table [dbo].[MS_JrnlTrx]    Script Date: 11/3/2014 8:27:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_JrnlTrx](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TrxCode] [nvarchar](20) NULL,
	[TrxDesc] [nvarchar](50) NULL,
	[BatchCode] [nvarchar](3) NULL,
	[UsrUpd] [nvarchar](20) NULL,
	[DtmUpd] [datetime] NULL,
	[UsrCrt] [nvarchar](20) NULL,
	[DtmCrt] [datetime] NULL,
 CONSTRAINT [PK_MS_Transaction] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [JournalEngine]
) ON [JournalEngine]

GO
