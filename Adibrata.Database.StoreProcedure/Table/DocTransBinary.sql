CREATE TABLE [dbo].[DocTransBinary]
(
	[Id] BIGINT NOT NULL IDENTITY PRIMARY KEY, 
	DocTransID BIGINT, 
	[FileName] varchar (8000), 
	[DateCreated] Datetime, 
	SizeFileBytes Numeric (20), 
	Pixel varchar(100), 
	ComputerName varchar(100), 
	DPI varchar(200), 
	[FileBinary] varbinary (max), 
	[UsrUpd] [varchar](50) NULL,
	[DtmUpd] [smalldatetime] NULL,
	[UsrCrt] [varchar](50) NULL,
	[DtmCrt] [smalldatetime] NULL
) on FileUpload
