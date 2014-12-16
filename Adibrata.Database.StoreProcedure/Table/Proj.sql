
CREATE TABLE [dbo].[Proj]
(
	[Id] BIGINT NOT NULL IDENTITY PRIMARY KEY, 
	ProjCode Varchar(50), 
	ProjName Varchar(50), 
	ProjType Varchar(50), 
	CustID BIGINT, 
	[DtmUpd] [datetime] NULL,
	[UsrUpd] [varchar](50) NULL,
	[DtmCrt] [datetime] NULL,
	[UsrCrt] [varchar](50) NULL,
)
