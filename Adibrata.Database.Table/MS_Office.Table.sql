
/****** Object:  Table [dbo].[MS_Office]    Script Date: 11/3/2014 8:27:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_Office](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OfficeCode] [nvarchar](5) NULL,
	[OfficeName] [nvarchar](50) NULL,
	[OfficeAddress] [nvarchar](50) NULL,
	[OfficeRT] [nvarchar](3) NULL,
	[OfficeRW] [nvarchar](3) NULL,
	[OfficeKelurahan] [nvarchar](50) NULL,
	[OfficeKecamatan] [nvarchar](50) NULL,
	[OfficeZipCode] [nvarchar](10) NULL,
	[OfficeAreaPhone] [nvarchar](5) NULL,
	[OfficeNoPhone] [nvarchar](20) NULL,
	[OfficeAreaFax] [nvarchar](5) NULL,
	[OfficeNoFax] [nvarchar](20) NULL,
 CONSTRAINT [PK_Office] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [Organization]
) ON [Organization]

GO
