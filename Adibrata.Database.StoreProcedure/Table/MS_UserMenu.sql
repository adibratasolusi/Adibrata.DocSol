CREATE TABLE [dbo].[MS_UserMenu]
(
	[ID] BIGINT NOT NULL IDENTITY PRIMARY KEY, 
	UserID BIGINT, 
	FormID BIGINT, 
	[DtmUpd] [datetime] NULL,
	[UsrUpd] [varchar](50) NULL,
	[UsrCrt] [datetime] NULL,
	[DtmCrt] [varchar](50) NULL
)
