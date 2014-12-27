CREATE TABLE [DocTransActivity](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NULL,
	[DocTransId] [bigint] NULL,
	[Description] [varchar](50) NULL,
	[Dtm] [datetime] NULL,
 CONSTRAINT [PK_DocTransActivity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
))