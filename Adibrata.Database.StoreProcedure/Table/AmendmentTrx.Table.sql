
/****** Object:  Table [dbo].[AmendmentTrx]    Script Date: 11/3/2014 8:27:58 AM ******/
-- SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO

CREATE TABLE [dbo].[AmendmentTrx](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AmendmentID] [int] NOT NULL,
	[AmendmentCode] [nvarchar](50) NOT NULL,
	[AmendmentType] [nvarchar](20) NOT NULL,
	[AppCode] [nvarchar](20) NOT NULL,
	[AgrmtCode] [nvarchar](20) NOT NULL,
	[OfficeCode] [nvarchar](5) NOT NULL,
	[Status] [nvarchar](3) NOT NULL,
	[RequestDate] [date] NULL,
	[ApprovedDate] [date] NULL,
	[ExecuteDate] [date] NULL,
	[EffectiveDate] [date] NULL,
	[DtmUpd] [datetime] NULL,
	[UsrUpd] [nvarchar](50) NULL,
	[DtmCrt] [datetime] NOT NULL,
	[UsrCrt] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_AmendmentTrx] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [Amendment]
) ON [Amendment]

--GO
