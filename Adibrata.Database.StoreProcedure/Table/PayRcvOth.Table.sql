
/****** Object:  Table [dbo].[PayRcvOth]    Script Date: 11/3/2014 8:27:58 AM ******/
-- SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
CREATE TABLE [dbo].[PayRcvOth](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[PayRcvID] [bigint] NULL,
	[Trx_Code] [nvarchar](50) NULL,
	[OtherRcvID] [bigint] NULL,
	[CoaName] [nvarchar](50) NULL,
	[AmtRcv] [numeric](17, 2) NULL,
	[UsrCrt] [nvarchar](50) NULL,
	[DtmCrt] [datetime] NULL,
	[UsrUpd] [nvarchar](50) NULL,
	[DtmUpd] [datetime] NULL,
 CONSTRAINT [PK_PaymentReceiveDtl] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PaymentReceive]
) ON [PaymentReceive]

