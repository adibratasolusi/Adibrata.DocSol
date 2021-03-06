
/****** Object:  Table [dbo].[AgrmntAsset]    Script Date: 11/3/2014 8:27:58 AM ******/
-- SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
CREATE TABLE [dbo].[AgrmntAsset](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[AgrmntID] [bigint] NULL,
	[AssetID] [int] NULL,
	[YearNum] [int] NULL,
	[DtmUpd] [datetime] NULL,
	[UsrUpd] [nvarchar](20) NULL,
	[DtmCrt] [datetime] NULL,
	[UsrCrt] [nvarchar](20) NULL,
 CONSTRAINT [PK_AgrmntAsset] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [AgreementAsset]
) ON [AgreementAsset]

--GO
CREATE TABLE [dbo].[AgrmntAsset]  WITH CHECK ADD  CONSTRAINT [FK_AgrmntAsset_Agrmnt] FOREIGN KEY([AgrmntID])
REFERENCES [dbo].[Agrmnt] ([ID])
--GO
CREATE TABLE [dbo].[AgrmntAsset] CHECK CONSTRAINT [FK_AgrmntAsset_Agrmnt]
--GO
