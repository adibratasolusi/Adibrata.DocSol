Drop Table UserActivity
go 
CREATE TABLE [dbo].[UserActivity]
(
	[ID] INT NOT NULL IDENTITY PRIMARY KEY, 
	FormUrl varchar(50), 
	UserLogin varchar (20), 
	DateTimeAccess SmallDateTime, 
	[UsrUpd] [varchar](50) NULL,
	[DtmUpd] [smalldatetime] NULL,
	[UsrCrt] [varchar](50) NULL,
	[DtmCrt] [smalldatetime] NULL

)
