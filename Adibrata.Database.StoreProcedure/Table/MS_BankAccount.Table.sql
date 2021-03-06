
/****** Object:  Table [dbo].[MS_BankAccount]    Script Date: 11/3/2014 8:27:58 AM ******/
-- SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
-- SET ANSI_PADDING ON
--GO
CREATE TABLE [dbo].[MS_BankAccount](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OfficeID] [int] NOT NULL,
	[BankAccCode] [nvarchar](10) NOT NULL,
	[CurrID] [int] NULL,
	[BankAccName] [nvarchar](50) NULL,
	[BankID] [int] NULL,
	[BankBranch] [nvarchar](50) NULL,
	[AccountName] [nvarchar](50) NULL,
	[AccountNo] [nvarchar](50) NULL,
	[CoaCode] [nvarchar](50) NULL,
	[EndingBalance] [numeric](17, 2) NULL,
	[BankAccountType] [nvarchar](5) NULL,
	[BankPurpose] [nvarchar](5) NULL,
	[BankAddress] [nvarchar](100) NULL,
	[BankRT] [nvarchar](3) NULL,
	[BankRW] [nvarchar](3) NULL,
	[BankKelurahan] [nvarchar](30) NULL,
	[BankKecamatan] [nvarchar](30) NULL,
	[BankCity] [nvarchar](30) NULL,
	[BankZipCode] [nvarchar](5) NULL,
	[BankAreaPhone1] [nvarchar](4) NULL,
	[BankPhone1] [nvarchar](10) NULL,
	[BankAreaPhone2] [nvarchar](4) NULL,
	[BankPhone2] [nvarchar](10) NULL,
	[BankAreaFax] [nvarchar](4) NULL,
	[BankFax] [nvarchar](10) NULL,
	[EMail] [nvarchar](50) NULL,
	[MobilePhone] [varchar](20) NULL,
	[ContactPersonName] [nvarchar](50) NULL,
	[ContactPersonTitle] [nvarchar](50) NULL,
	[IsGenerateTextAvailable] [char](1) NULL,
	[IsActive] [char](1) NULL,
	[Seq_No] [int] NULL,
	[Length_Number] [int] NULL,
	[ResetFlag] [char](1) NULL,
	[Prefix] [nvarchar](10) NULL,
	[IsOffice] [char](1) NULL,
	[IsYear] [char](1) NULL,
	[IsMonth] [char](1) NULL,
	[Suffix] [nvarchar](10) NULL,
	[IsOffBalanceSheet] [char](1) NULL,
	[IsSyariah] [char](1) NULL,
	[FormatNumber] [nvarchar](100) NULL,
	[UsrUpd] [nvarchar](20) NULL,
	[DtmUpd] [smalldatetime] NULL,
	[UsrCrt] [nvarchar](20) NULL,
	[DtmCrt] [smalldatetime] NULL,
 CONSTRAINT [PK_MS_BankAccount] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

--GO
