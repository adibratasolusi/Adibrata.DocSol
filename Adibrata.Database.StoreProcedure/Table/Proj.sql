CREATE TABLE [dbo].[Proj]
(
	[Id] BIGINT NOT NULL PRIMARY KEY, 
	ProjCode Varchar(50), 
	ProjName Varchar(50), 
	ProjType Varchar(50), 
	CustID BIGINT
)
