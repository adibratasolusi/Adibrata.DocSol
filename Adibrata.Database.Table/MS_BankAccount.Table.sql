
/****** Object:  Table [dbo].[MS_BankAccount]    Script Date: 11/3/2014 8:27:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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

GO
SET ANSI_PADDING ON
GO
ALTER TABLE [dbo].[MS_BankAccount] ADD  CONSTRAINT [DF_Table_1_CurrencyID]  DEFAULT ('-') FOR [CurrID]
GO
ALTER TABLE [dbo].[MS_BankAccount] ADD  CONSTRAINT [DF_MS_BankAccount_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[MS_BankAccount] ADD  CONSTRAINT [DF_MS_BankAccount_SequenceNo]  DEFAULT ((1)) FOR [Seq_No]
GO
ALTER TABLE [dbo].[MS_BankAccount] ADD  CONSTRAINT [DF_MS_BankAccount_ResetFlag]  DEFAULT ('M') FOR [ResetFlag]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Account Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MS_BankAccount', @level2type=N'COLUMN',@level2name=N'BankAccName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Bank Branch' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MS_BankAccount', @level2type=N'COLUMN',@level2name=N'BankBranch'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AccounNo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MS_BankAccount', @level2type=N'COLUMN',@level2name=N'AccountNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Account Type (B: Bank, C: Cash, M: Memorial)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MS_BankAccount', @level2type=N'COLUMN',@level2name=N'BankAccountType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Bank Purpose (for Escrow, Funding, Petty Cash, Cash)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MS_BankAccount', @level2type=N'COLUMN',@level2name=N'BankPurpose'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nama personil yg dapat dihubungi' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MS_BankAccount', @level2type=N'COLUMN',@level2name=N'ContactPersonName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Jabatan personil yg dapat dihubungi' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MS_BankAccount', @level2type=N'COLUMN',@level2name=N'ContactPersonTitle'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Counter for Voucher No' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MS_BankAccount', @level2type=N'COLUMN',@level2name=N'Seq_No'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Length of Sequence No' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MS_BankAccount', @level2type=N'COLUMN',@level2name=N'Length_Number'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence No mau direset : D - Daily, M - Monthly, Y - Yearly dan None (tidak direset)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MS_BankAccount', @level2type=N'COLUMN',@level2name=N'ResetFlag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Kode Prefix yang dimunculkan pada Voucher No' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MS_BankAccount', @level2type=N'COLUMN',@level2name=N'Prefix'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Kode Cabang dimunculkan' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MS_BankAccount', @level2type=N'COLUMN',@level2name=N'IsOffice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'2 Digit Tahun dimunculkan' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MS_BankAccount', @level2type=N'COLUMN',@level2name=N'IsYear'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Kode Bulan dimunculkan' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MS_BankAccount', @level2type=N'COLUMN',@level2name=N'IsMonth'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Kode Suffix yang dimunculkan pada Voucher No' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MS_BankAccount', @level2type=N'COLUMN',@level2name=N'Suffix'
GO
