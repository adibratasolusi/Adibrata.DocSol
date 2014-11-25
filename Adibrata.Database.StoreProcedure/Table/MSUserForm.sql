CREATE TABLE [dbo].[MSUserForm]
(
	[Id] INT NOT NULL PRIMARY KEY, 
	MSFormID BigInt, 
	MSUserID BigInt, 
	IsActive int, 
	DtmUpd Datetime, 
	UsrUpd Varchar(50), 
	UsrCrt DateTime, 
	DtmCrt Varchar(50)
)
