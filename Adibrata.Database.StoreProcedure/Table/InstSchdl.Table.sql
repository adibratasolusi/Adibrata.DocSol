
/****** Object:  Table [dbo].[InstSchdl]    Script Date: 11/3/2014 8:27:58 AM ******/
-- SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
CREATE TABLE [dbo].[InstSchdl](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[AgrmntID] [bigint] NULL,
	[InsSeqNo] [int] NULL,
	[DueDt] [date] NULL,
	[InstallAmt] [numeric](17, 2) NULL,
	[PaidAmt] [numeric](17, 2) NULL,
	[WaivedAmt] [numeric](17, 2) NULL,
	[PaidDt] [date] NULL,
	[PrincipalAmt] [numeric](17, 2) NULL,
	[InterestAmt] [numeric](17, 2) NULL,
	[OSPrincipalAmt] [numeric](17, 2) NULL,
	[OSInterestAmt] [numeric](17, 2) NULL,
	[DtmUpd] [datetime] NULL,
	[UsrUpd] [nvarchar](20) NULL,
	[DtmCrt] [datetime] NULL,
	[UsrCrt] [nvarchar](20) NULL,
 CONSTRAINT [PK_InstSchdl] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [InstSchedule]
) ON [InstSchedule]

--GO
CREATE TABLE [dbo].[InstSchdl]  WITH CHECK ADD  CONSTRAINT [FK_InstSchdl_Agrmnt] FOREIGN KEY([AgrmntID])
REFERENCES [dbo].[Agrmnt] ([ID])
--GO
CREATE TABLE [dbo].[InstSchdl] CHECK CONSTRAINT [FK_InstSchdl_Agrmnt]
--GO
