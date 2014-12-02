Drop Table [DocTransContent]
CREATE TABLE [dbo].[DocTransContent]
(
	[Id] BIGINT NOT NULL IDENTITY PRIMARY KEY, 
	DocTypeCode Varchar(50), 
	DocTransID BIGINT, 
	ContentName varchar(50), 
	ContentValue Varchar(8000), 
	ContenValueDate DateTime,
	ContentValueNumeric Numeric (17,2), 
	ContentSearchTag Varchar (8000), 
	[UsrUpd] [varchar](50) NULL,
	[DtmUpd] [smalldatetime] NULL,
	[UsrCrt] [varchar](50) NULL,
	[DtmCrt] [smalldatetime] NULL
) on FileUpload
