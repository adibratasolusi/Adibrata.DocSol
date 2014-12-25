CREATE TABLE [dbo].[DocTransActivity](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NULL,
	[DocTransId] [bigint] NULL,
	[Description] [varchar](50) NULL,
	[Dtm] [datetime] NULL,
 CONSTRAINT [PK_DocTransActivity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO