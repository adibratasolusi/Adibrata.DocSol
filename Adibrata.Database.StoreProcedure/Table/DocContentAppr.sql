CREATE TABLE [dbo].[DocContentAppr]
(
	[ID] BIGINT PRIMARY KEY, 
	NextLevelAppr varchar(50), 
	DocTypeCode VARCHAR(50), 
	DocTransID BIGINT, 
	LastUserAppr Varchar(50),
	DocApprStat varchar(50), 
	DocTransReqDate smalldatetime null, 
	CurrentLevelAppr varchar(50), 
	DtmLastApproval smalldatetime,
	DocTransApprFinal smalldatetime, 
	[UsrUpd] [varchar](50) NULL,
	[DtmUpd] [smalldatetime] NULL,
	[UsrCrt] [varchar](50),
	[DtmCrt] [smalldatetime] 
) On Approval
