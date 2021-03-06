/****** Object:  Table [dbo].[Agrmnt]    Script Date: 11/3/2014 8:27:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Agrmnt](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[AgrmntCode] [nvarchar](50) NULL,
	[AppID] [bigint] NULL,
	[OfficeID] [int] NULL,
	[CoyID] [int] NULL,
	[ContrStat] [nchar](3) NULL,
	[DefaultStat] [nchar](3) NULL,
	[PrepHoldStat] [nvarchar](10) NULL,
	[CurrID] [int] NULL,
	[ProductID] [bigint] NULL,
	[InstalSchm] [nvarchar](2) NULL,
	[InstallAmt] [decimal](17, 2) NULL,
	[PaidAmt] [decimal](17, 2) NULL,
	[WaivedAmt] [decimal](17, 2) NULL,
	[LCInstallAmt] [decimal](17, 2) NULL,
	[LCInstallPaid] [decimal](17, 2) NULL,
	[LCInstallWaived] [decimal](17, 2) NULL,
	[LastLCInstallCalcDate] [date] NULL,
	[LCInsurAmt] [decimal](17, 2) NULL,
	[LCInsurPaid] [decimal](17, 2) NULL,
	[LCInsurWaived] [decimal](17, 2) NULL,
	[LastLCInsurCalcDate] [date] NULL,
	[DtmUpd] [datetime] NULL,
	[UsrUpd] [nvarchar](20) NULL,
	[DtmCrt] [datetime] NULL,
	[UsrCrt] [nvarchar](20) NULL,
 CONSTRAINT [PK_Agrmnt] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [Agreement]
) ON [Agreement]

GO
ALTER TABLE [dbo].[Agrmnt]  WITH CHECK ADD  CONSTRAINT [FK_Agrmnt_Application] FOREIGN KEY([ID])
REFERENCES [dbo].[Application] ([ID])
GO
ALTER TABLE [dbo].[Agrmnt] CHECK CONSTRAINT [FK_Agrmnt_Application]
GO
