CREATE TABLE [dbo].[DocTransContent](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DocTypeCode] [varchar](50) NULL,
	[DocTransID] [bigint] NULL,
	[ContentName] [varchar](50) NULL,
	[ContentValue] [varchar](8000) NULL,
	[ContenValueDate] [datetime] NULL,
	[ContentValueNumeric] [numeric](17, 2) NULL,
	[ContentSearchTag] [varchar](8000) NULL,
	[UsrUpd] [varchar](50) NULL,
	[DtmUpd] [smalldatetime] NULL,
	[UsrCrt] [varchar](50) NULL,
	[DtmCrt] [smalldatetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [FileUpload]
) ON [FileUpload]
