CREATE TABLE [DocTrans](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[TransID] [varchar](50) NULL,
	[DocTypeCode] [varchar](50) NULL,
	[UsrUpd] [varchar](50) NULL,
	[DtmUpd] [smalldatetime] NULL,
	[UsrCrt] [varchar](50) NULL,
	[DtmCrt] [smalldatetime] NULL,
	[DocTransStatus] [varchar](10) NULL,
	[ArchieveStatus] [varchar](10) NULL,
 CONSTRAINT [PK__DocTrans__3214EC07CC0350E2] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
))