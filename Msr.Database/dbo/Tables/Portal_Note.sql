CREATE TABLE [dbo].[Portal_Note](
	[Id] [uniqueidentifier] NOT NULL,
	[EntityId] [nvarchar](50) NOT NULL,
	[EntityTypeId] [int] NOT NULL,
	[Message] [nvarchar](max) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](128) NULL,
 CONSTRAINT [PK_Portal_Notes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


