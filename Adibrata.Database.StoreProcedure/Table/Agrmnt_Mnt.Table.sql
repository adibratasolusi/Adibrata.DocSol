
/****** Object:  Table [dbo].[Agrmnt_Mnt]    Script Date: 11/3/2014 8:27:58 AM ******/
-- SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
CREATE TABLE [dbo].[Agrmnt_Mnt](
	[ID] [bigint] NOT NULL,
	[AgrmntCode] [nvarchar](50) NULL,
	[AppID] [bigint] NULL,
	[OfficeID] [int] NULL,
	[CoyID] [int] NULL,
	[LCGracePeriod] [smallint] NULL,
	[LCPercentage] [decimal](5, 3) NULL,
	[LastLCInstCalcDt] [date] NULL,
	[LCCalCMethod] [nvarchar](2) NULL,
	[DtmUpd] [datetime] NULL,
	[UsrUpd] [nvarchar](20) NULL,
	[DtmCrt] [datetime] NULL,
	[UsrCrt] [nvarchar](20) NULL,
 CONSTRAINT [PK_Agrmnt_Mnt] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

--GO
