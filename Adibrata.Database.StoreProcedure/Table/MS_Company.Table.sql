
/****** Object:  Table [dbo].[MS_Company]    Script Date: 11/3/2014 8:27:58 AM ******/
-- SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
-- SET ANSI_PADDING ON
--GO
CREATE TABLE [dbo].[MS_Company](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CompanyCode] [nvarchar](5) NULL,
	[CompanyName] [nvarchar](50) NULL,
	[CompanyAddress] [nvarchar](50) NULL,
	[CompanyRT] [nvarchar](3) NULL,
	[CompanyRW] [nvarchar](3) NULL,
	[CompanyKelurahan] [nvarchar](50) NULL,
	[CompanyKecamatan] [nvarchar](50) NULL,
	[CompanyZipCode] [nvarchar](10) NULL,
	[CompanyAreaPhone] [nvarchar](5) NULL,
	[CompanyNoPhone] [nvarchar](20) NULL,
	[CompanyAreaFax] [nvarchar](5) NULL,
	[CompanyNoFax] [nvarchar](20) NULL,
	[BODDate] [datetime] NULL,
	[BDCurrent] [datetime] NULL,
	[EODDate] [datetime] NULL,
	[BDPrevious] [datetime] NULL,
	[BDNext] [datetime] NULL,
	[EOMPrevious] [datetime] NULL,
	[EOMNext] [datetime] NULL,
	[IsBranchClosed] [char](1) NULL,
	[IsCashierClosed] [char](1) NULL,
	[UsrUpd] [nvarchar](50) NULL,
	[DtmUpd] [date] NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [Organization]
) ON [Organization]

--GO
-- SET ANSI_PADDING ON
--GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date of Beginning of Day' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MS_Company', @level2type=N'COLUMN',@level2name=N'BODDate'
--GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date of Current Business Date' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MS_Company', @level2type=N'COLUMN',@level2name=N'BDCurrent'
--GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date of End of Day' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MS_Company', @level2type=N'COLUMN',@level2name=N'EODDate'
--GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date of Previous Business Date' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MS_Company', @level2type=N'COLUMN',@level2name=N'BDPrevious'
--GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date of Next Business Date' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MS_Company', @level2type=N'COLUMN',@level2name=N'BDNext'
--GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'User Updated' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MS_Company', @level2type=N'COLUMN',@level2name=N'UsrUpd'
--GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date Time Updated' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MS_Company', @level2type=N'COLUMN',@level2name=N'DtmUpd'
--GO
