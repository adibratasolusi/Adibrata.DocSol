CREATE TABLE [dbo].[DocContentApprHist]
(
	[ID] BIGINT NOT NULL PRIMARY KEY,
	DocContentApprID BIGINT, 
	DocTypeCode VARCHAR(50), 
	DocApprStat varchar(50), 
	LastUserAppr Varchar(50),
	CurrentLevelAppr varchar(50), 
	NextLevelAppr varchar(50), 
	ApprTimeSecond int,
	[UsrUpd] [varchar](50) NULL,
	[DtmUpd] [smalldatetime] NULL,
	[UsrCrt] [varchar](50),
	[DtmCrt] [smalldatetime] 
) On Approval
