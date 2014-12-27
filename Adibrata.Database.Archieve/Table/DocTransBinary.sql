CREATE TABLE [DocTransBinary](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DocTransID] [bigint] NULL,
	[FileName] [varchar](8000) NULL,
	[DateCreated] [datetime] NULL,
	[SizeFileBytes] [numeric](20, 0) NULL,
	[Pixel] [varchar](100) NULL,
	[ComputerName] [varchar](100) NULL,
	[DPI] [varchar](200) NULL,
	[FileBinary] [varbinary](max) NULL,
	[UsrUpd] [varchar](50) NULL,
	[DtmUpd] [smalldatetime] NULL,
	[UsrCrt] [varchar](50) NULL,
	[DtmCrt] [smalldatetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
))


ALTER TABLE [dbo].[DocTransBinary]  WITH CHECK ADD  CONSTRAINT [FK_DocTransBinary_DocTrans] FOREIGN KEY([DocTransID])
REFERENCES [dbo].[DocTrans] ([Id])
GO

ALTER TABLE [dbo].[DocTransBinary] CHECK CONSTRAINT [FK_DocTransBinary_DocTrans]
GO
