
/****** Object:  Table [dbo].[AgrmntInsurDtl]    Script Date: 11/3/2014 8:27:58 AM ******/
-- SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
CREATE TABLE [dbo].[AgrmntInsurDtl](
	[ID] [bigint] NOT NULL,
	[AgrmntInsurHdrID] [bigint] NULL,
	[YearNum] [int] NULL,
	[InsurNumber] [int] NULL,
	[DtmUpd] [datetime] NULL,
	[UsrUpd] [nvarchar](20) NULL,
	[DtmCrt] [datetime] NULL,
	[UsrCrt] [nvarchar](20) NULL,
 CONSTRAINT [PK_AgrmntInsurDtl] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [AgrmntInsurance]
) ON [AgrmntInsurance]

--GO
CREATE TABLE [dbo].[AgrmntInsurDtl]  WITH NOCHECK ADD  CONSTRAINT [FK_AgrmntInsurDtl_AgrmntInsur] FOREIGN KEY([AgrmntInsurHdrID])
REFERENCES [dbo].[AgrmntInsur] ([ID])
--GO
CREATE TABLE [dbo].[AgrmntInsurDtl] CHECK CONSTRAINT [FK_AgrmntInsurDtl_AgrmntInsur]
--GO
CREATE TABLE [dbo].[AgrmntInsurDtl]  WITH NOCHECK ADD  CONSTRAINT [FK_AgrmntInsurDtl_AgrmntInsurHdr] FOREIGN KEY([AgrmntInsurHdrID])
REFERENCES [dbo].[AgrmntInsurHdr] ([ID])
--GO
CREATE TABLE [dbo].[AgrmntInsurDtl] CHECK CONSTRAINT [FK_AgrmntInsurDtl_AgrmntInsurHdr]
--GO
