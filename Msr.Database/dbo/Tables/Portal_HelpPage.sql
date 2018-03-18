CREATE TABLE [dbo].[Portal_HelpPage](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[FriendlyUrl] [nvarchar](max) NOT NULL,
	[Content] [ntext] NULL,
	[Roles] [nvarchar](max) NOT NULL,
	[Category] [nvarchar](100) NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
