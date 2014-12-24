CREATE TABLE [dbo].[DocTransApprHist]
(
	[ID] BIGINT NOT NULL PRIMARY KEY,
	DocTransApprID BIGINT, 
	DocTransID BIGINT, 
	DocTypeCode VARCHAR(50), 
	DocTransReqUser Varchar(50), 
	CurrentLevelAppr Int,
	NextLevelAppr int,
	DocApprStat varchar(50), 
	LastUserAppr Varchar(50),
	NextLevelUser varchar(50), -- User Request
	ApprTimeSecond int,
	DocTransReqDate smalldatetime, 
	[UsrUpd] [varchar](50) NULL,
	[DtmUpd] [smalldatetime] NULL,
	[UsrCrt] [varchar](50),
	[DtmCrt] [smalldatetime] 
) On Approval
