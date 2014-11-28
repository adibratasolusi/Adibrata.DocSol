CREATE TABLE [dbo].DocTrans
(
	[Id] BIGINT NOT NULL IDENTITY PRIMARY KEY, 
	TransID BIGINT, 
	DocTypeCode varchar(50), 
	[UsrUpd] [varchar](50) NULL,
	[DtmUpd] [smalldatetime] NULL,
	[UsrCrt] [varchar](50) NULL,
	[DtmCrt] [smalldatetime] NULL
) on FileUpload
