
/****** Object:  Table [dbo].[MS_Setting]    Script Date: 11/3/2014 8:27:58 AM ******/
-- SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
CREATE TABLE [dbo].[MS_Setting](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SettingTypeID] [int] NULL,
	[Description] [nvarchar](50) NULL,
	[Value] [nvarchar](50) NULL,
	[DtmUpd] [datetime] NULL,
	[UsrUpd] [nvarchar](20) NULL,
	[DtmCrt] [datetime] NULL,
	[UsrCrt] [nvarchar](20) NULL,
 CONSTRAINT [PK_MS_Setting] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [Setting]
) ON [Setting]

--GO
CREATE TABLE [dbo].[MS_Setting]  WITH NOCHECK ADD  CONSTRAINT [FK_MS_Setting_MS_SettingType] FOREIGN KEY([SettingTypeID])
REFERENCES [dbo].[MS_SettingType] ([ID])
--GO
CREATE TABLE [dbo].[MS_Setting] CHECK CONSTRAINT [FK_MS_Setting_MS_SettingType]
--GO
