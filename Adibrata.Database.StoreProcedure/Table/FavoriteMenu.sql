CREATE TABLE [dbo].[FavoriteMenu](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserID] [bigint] NULL,
	[FormID] [bigint] NULL,
	[DtmUpd] [datetime] NULL,
	[UsrUpd] [varchar](20) NULL,
	[UsrCrt] [varchar](20) NULL,
	[DtmCrt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
))
