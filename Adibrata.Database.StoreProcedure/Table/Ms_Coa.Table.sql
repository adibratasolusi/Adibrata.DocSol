
/****** Object:  Table [dbo].[Ms_Coa]    Script Date: 11/3/2014 8:27:58 AM ******/
-- SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
-- SET ANSI_PADDING ON
--GO
CREATE TABLE [dbo].[Ms_Coa](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CoaCoy] [nvarchar](50) NULL,
	[CoaName] [nvarchar](50) NOT NULL,
	[CoaCode] [nvarchar](50) NOT NULL,
	[CoaDesc] [nvarchar](50) NOT NULL,
	[IsAgreement] [char](1) NOT NULL,
	[IsSystem] [char](1) NOT NULL,
	[IsScheme] [char](1) NOT NULL,
	[IsPettyCash] [char](1) NOT NULL,
	[IsPaymentReceive] [char](1) NOT NULL,
	[IsHOTransaction] [char](1) NOT NULL,
	[IsHeader] [char](1) NOT NULL,
	[Group_Coa] [char](10) NOT NULL,
	[IsActive] [char](1) NOT NULL,
	[AccountID] [char](25) NOT NULL,
	[isSyariah] [char](1) NOT NULL,
	[UsrCrt] [nvarchar](20) NULL,
	[DtmCrt] [datetime] NULL,
	[UsrUpd] [nvarchar](20) NULL,
	[DtmUpd] [datetime] NULL,
 CONSTRAINT [PK_Ms_Coa_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [JournalEngine]
) ON [JournalEngine]

--GO

