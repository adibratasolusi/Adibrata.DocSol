CREATE TABLE [dbo].[MSForm]
(
	[Id] BIGINT NOT NULL PRIMARY KEY, 
	FormName Varchar(50), 
	FormURL Varchar(255), 
	ISActive int, 
	DtmUpd Datetime, 
	UsrUpd Varchar(50), 
	UsrCrt DateTime, 
	DtmCrt Varchar(50)
)
