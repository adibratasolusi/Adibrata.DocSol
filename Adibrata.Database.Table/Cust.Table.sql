
/****** Object:  Table [dbo].[Cust]    Script Date: 11/3/2014 8:27:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Cust](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CustCode] [nvarchar](20) NOT NULL,
	[CustName] [nvarchar](50) NOT NULL,
	[CustType] [char](1) NOT NULL,
	[FirstAgreementDate] [datetime] NULL,
	[CustGroupID] [int] NULL,
	[CollectibilityId] [int] NULL,
	[CustomerGroupLevel] [int] NULL,
	[CustomerGroupCode] [int] NULL,
	[UsrUpd] [nvarchar](50) NOT NULL,
	[DtmUpd] [datetime] NOT NULL,
	[UsrCrt] [nvarchar](50) NOT NULL,
	[DtmCrt] [datetime] NOT NULL,
 CONSTRAINT [PK__Customer__6ADAD1BF] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [Customer]
) ON [Customer]

GO
SET ANSI_PADDING ON
GO
