Drop TABLE [dbo].DocTrans
GO
CREATE TABLE [dbo].DocTrans
(
	[Id] BIGINT NOT NULL IDENTITY PRIMARY KEY, 
	TransID BIGINT, 
	TransDesc Varchar(100) null, 
	DocTransCode varchar(50) null, 
	DocTypeCode varchar(50), 
	DocTransStatus varchar (10), 
	DocAprStat varchar(3) null, 
	DocApprCurrLevel varchar(50) null,
	DocLastApprUser varchar(50) null, 
	ReqDate smalldatetime null, 
	FinalDate smalldatetime null, 
	FinalUser varchar(50) null, 
	[UsrUpd] [varchar](50) NULL,
	[DtmUpd] [smalldatetime] NULL,
	[UsrCrt] [varchar](50),
	[DtmCrt] [smalldatetime] 
	) on FileUpload
