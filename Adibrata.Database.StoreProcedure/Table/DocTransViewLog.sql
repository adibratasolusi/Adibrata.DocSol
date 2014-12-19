CREATE TABLE [dbo].[DocTransViewLog]
(
	[Id] BIGINT NOT NULL IDENTITY PRIMARY KEY, 
	DocTransID BigInt, 
	UserLogin varchar(50), 
	ViewTime smalldatetime,
	[UsrUpd] [varchar](50) NULL,
	[DtmUpd] [smalldatetime] NULL,
	[UsrCrt] [varchar](50) NULL,
	[DtmCrt] [smalldatetime] NULL, 

)
