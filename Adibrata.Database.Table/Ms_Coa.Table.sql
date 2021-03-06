
/****** Object:  Table [dbo].[Ms_Coa]    Script Date: 11/3/2014 8:27:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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

GO
SET ANSI_PADDING ON
GO
ALTER TABLE [dbo].[Ms_Coa] ADD  CONSTRAINT [DF_Ms_Coa_IsAgreement]  DEFAULT ((0)) FOR [IsAgreement]
GO
ALTER TABLE [dbo].[Ms_Coa] ADD  CONSTRAINT [DF_Ms_Coa_IsSystem]  DEFAULT ((0)) FOR [IsSystem]
GO
ALTER TABLE [dbo].[Ms_Coa] ADD  CONSTRAINT [DF_Ms_Coa_IsScheme]  DEFAULT ((0)) FOR [IsScheme]
GO
ALTER TABLE [dbo].[Ms_Coa] ADD  CONSTRAINT [DF_Ms_Coa_IsPettyCash]  DEFAULT ((0)) FOR [IsPettyCash]
GO
ALTER TABLE [dbo].[Ms_Coa] ADD  CONSTRAINT [DF_Ms_Coa_IsHOTransaction]  DEFAULT ((0)) FOR [IsHOTransaction]
GO
ALTER TABLE [dbo].[Ms_Coa] ADD  CONSTRAINT [DF_Ms_Coa_IsHeader]  DEFAULT ((0)) FOR [IsHeader]
GO
ALTER TABLE [dbo].[Ms_Coa] ADD  CONSTRAINT [DF_Ms_Coa_Group_Coa]  DEFAULT ('-') FOR [Group_Coa]
GO
ALTER TABLE [dbo].[Ms_Coa] ADD  CONSTRAINT [DF_Ms_Coa_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Ms_Coa] ADD  CONSTRAINT [DF_Ms_Coa_AccountID]  DEFAULT ('-') FOR [AccountID]
GO
ALTER TABLE [dbo].[Ms_Coa] ADD  CONSTRAINT [DF_Ms_Coa_DtmCrt]  DEFAULT (getdate()) FOR [DtmCrt]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Apakah payment allocation ini hanya ada di HO' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Ms_Coa', @level2type=N'COLUMN',@level2name=N'IsHOTransaction'
GO
