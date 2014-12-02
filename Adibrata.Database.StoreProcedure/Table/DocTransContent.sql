
CREATE TABLE [dbo].[DocTransContent]
(
	[Id] BIGINT NOT NULL IDENTITY PRIMARY KEY, 
	DocTypeCode Varchar(50), 
	DocTransID BIGINT, 
	ContentName varchar(50), -- Field2
	ContentValue Varchar(8000), --EntryValue
	ContenValueDate DateTime, --EntryValueDate
	ContentValueNumeric Numeric (17,2), --EntryValueNumber
	[UsrUpd] [varchar](50) NULL,
	[DtmUpd] [smalldatetime] NULL,
	[UsrCrt] [varchar](50) NULL,
	[DtmCrt] [smalldatetime] NULL
) on FileUpload
