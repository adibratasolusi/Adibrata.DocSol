CREATE TABLE [dbo].[CustCoy]
(
	[Id] BIGINT NOT NULL PRIMARY KEY, 
	CustID BIGINT, 
	CustName Varchar(100), 
	NPWP varchar(20), 
	SIUP Varchar(50), 
	TDP Varchar(50), 
	AkteNo Varchar (50),
	Address Varchar(100), 
	RT Varchar(4), 
	RW Varchar (4), 
	Kelurahan Varchar (50), 
	Kecamatan Varchar(50),
	City Varchar(50),  
	ZipCode varchar (12), 
	DtmUpd Datetime, 
	UsrUpd Varchar(50), 
	UsrCrt DateTime, 
	DtmCrt Varchar(50)
)
