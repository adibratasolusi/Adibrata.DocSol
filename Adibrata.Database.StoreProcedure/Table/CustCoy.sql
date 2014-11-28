﻿CREATE TABLE [dbo].[CustCoy]
(
	[ID] BIGINT NOT NULL PRIMARY KEY, 
	CustID BIGINT, 
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
	FullAddress varchar(5000), 
	[UsrUpd] [varchar](50) NULL,
	[DtmUpd] [smalldatetime] NULL,
	[UsrCrt] [varchar](50) NULL,
	[DtmCrt] [smalldatetime] NULL,
)
