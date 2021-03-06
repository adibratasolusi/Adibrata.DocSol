
/****** Object:  Table [dbo].[PayRcvDtl]    Script Date: 11/3/2014 8:27:58 AM ******/
-- SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
-- SET ANSI_PADDING ON
--GO
CREATE TABLE [dbo].[PayRcvDtl](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[PayRcvID] [bigint] NULL,
	[InstSeqNo] [int] NULL,
	[AssetSeqNo] [int] NULL,
	[YearNum] [int] NULL,
	[AmtRcv] [decimal](17, 2) NULL,
	[LCAmt] [decimal](17, 2) NULL,
	[LCDays] [int] NULL,
	[IsInstallPaid] [char](1) NULL,
	[IsLCInstallPaid] [char](1) NULL,
	[IsInsurancePaid] [char](1) NULL,
	[IsLCInsurPaid] [char](1) NULL,
	[UsrCrt] [nvarchar](20) NULL,
	[DtmCrt] [datetime] NULL,
	[UsrUpd] [nvarchar](20) NULL,
	[DtmUpd] [datetime] NULL,
 CONSTRAINT [PK_PayRcvDtl] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PaymentReceive]
) ON [PaymentReceive]

--GO
-- SET ANSI_PADDING ON
--GO
