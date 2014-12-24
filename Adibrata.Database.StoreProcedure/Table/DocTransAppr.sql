--Drop TABLE [DocTransAppr]
CREATE TABLE [dbo].[DocTransAppr]
(
	[ID] BIGINT PRIMARY KEY, 
	DocTransID BIGINT, 
	DocTypeCode VARCHAR(50), 
	DocTransReqUser Varchar(50), 
	DocTransReqDate smalldatetime null, 
	DocTransApprCode Varchar(50), 
	NextLevelAppr int, 
	CurrentLevelAppr int, 
	LastUserAppr Varchar(50),
	DocApprStat varchar(50), 
	DtmLastApproval smalldatetime,
	DocTransApprFinal smalldatetime, 
	[UsrUpd] [varchar](50) NULL,
	[DtmUpd] [smalldatetime] NULL,
	[UsrCrt] [varchar](50),
	[DtmCrt] [smalldatetime] 
) On Approval

Go 
Create Index IX_DocContentAppr_DocTypeCode_CurrentLevelApp on DocTransAppr (DocTypeCode, CurrentLevelAppr) On IndexTbl
Create Index IX_DocContentAppr_DocTypeCode_NextLevelAppr on DocTransAppr (DocTypeCode, NextLevelAppr) On IndexTbl

Create Index IX_DocContentAppr_DocTransApprCode on DocTransAppr (DocTransApprCode) On IndexTbl
