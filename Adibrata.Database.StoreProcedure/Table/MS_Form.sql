
/****** Object:  Table [dbo].[MS_Form]    Script Date: 11/25/2014 2:30:00 PM ******/
-- SET ANSI_NULLS ON
--GO

--SET QUOTED_IDENTIFIER ON
--GO

-- SET ANSI_PADDING ON
--GO

Create TABLE [dbo].[MS_Form](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	FormCode varchar(50), 
	[FormName] [varchar](50) NULL,
	[FormURL] [varchar](255) NULL,
	[ISActive] [int] NULL,
	[DtmUpd] [datetime] NULL,
	[UsrUpd] [varchar](50) NULL,
	[UsrCrt] [datetime] NULL,
	[DtmCrt] [varchar](50) NULL,
 CONSTRAINT [PK__MS_Form__3214EC0780C4DB6E] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

--GO

-- SET ANSI_PADDING ON
--GO

