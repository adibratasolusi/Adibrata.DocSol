
/****** Object:  Table [dbo].[AgrmntInsurAsset]    Script Date: 11/3/2014 8:27:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AgrmntInsurAsset](
	[ID] [bigint] NOT NULL,
	[AgrmntInsurDtlID] [bigint] NULL,
	[DtmUpd] [datetime] NULL,
	[UsrUpd] [nvarchar](20) NULL,
	[DtmCrt] [datetime] NULL,
	[UsrCrt] [nvarchar](20) NULL,
 CONSTRAINT [PK_AgrmntInsurAsset] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [AgrmntInsurance]
) ON [AgrmntInsurance]

GO
ALTER TABLE [dbo].[AgrmntInsurAsset]  WITH NOCHECK ADD  CONSTRAINT [FK_AgrmntInsurAsset_AgrmntInsurDtl] FOREIGN KEY([AgrmntInsurDtlID])
REFERENCES [dbo].[AgrmntInsurDtl] ([ID])
GO
ALTER TABLE [dbo].[AgrmntInsurAsset] CHECK CONSTRAINT [FK_AgrmntInsurAsset_AgrmntInsurDtl]
GO
