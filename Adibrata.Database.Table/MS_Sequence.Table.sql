
/****** Object:  Table [dbo].[MS_Sequence]    Script Date: 11/3/2014 8:27:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MS_Sequence](
	[MSSequenceID] [char](10) NOT NULL,
	[OfficeID] [char](3) NOT NULL,
	[Seq_Name] [nvarchar](50) NOT NULL,
	[Seq_No] [int] NOT NULL,
	[Length_Number] [int] NOT NULL,
	[ResetFlag] [char](1) NOT NULL,
	[Prefix] [varchar](10) NOT NULL,
	[Suffix] [varchar](10) NOT NULL,
	[IsOffice] [char](1) NOT NULL,
	[IsYear] [char](1) NOT NULL,
	[IsMonth] [char](1) NOT NULL,
	[FormatNumber] [nvarchar](100) NULL,
	[UsrUpd] [varchar](50) NOT NULL,
	[DtmUpd] [datetime] NOT NULL,
	[UsrCrt] [varchar](50) NULL,
	[DtmCrt] [datetime] NULL,
 CONSTRAINT [PK_MS_Sequence] PRIMARY KEY CLUSTERED 
(
	[MSSequenceID] ASC,
	[OfficeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'User Updated' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MS_Sequence', @level2type=N'COLUMN',@level2name=N'UsrUpd'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date Time Updated' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MS_Sequence', @level2type=N'COLUMN',@level2name=N'DtmUpd'
GO
