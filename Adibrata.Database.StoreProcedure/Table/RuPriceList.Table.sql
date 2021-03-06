
/****** Object:  Table [dbo].[RuPriceList]    Script Date: 11/3/2014 8:27:58 AM ******/
-- SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
CREATE TABLE [dbo].[RuPriceList](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Field1] [nvarchar](50) NULL,
	[Field2] [nvarchar](50) NULL,
	[Field3] [nvarchar](50) NULL,
	[Result] [nvarchar](50) NULL,
 CONSTRAINT [PK_RuPriceList] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [RuleEngine]
) ON [RuleEngine]

--GO
