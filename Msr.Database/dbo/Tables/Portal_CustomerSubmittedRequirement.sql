GO

CREATE TABLE [dbo].[Portal_CustomerSubmittedRequirement](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SubmittedDate] [datetime] NOT NULL,
	[SubmittedBy] [nvarchar](50) NOT NULL,
	[Customer] [nvarchar](50) NULL,
	[Company] [nvarchar](50) NULL,
	[Division] [nvarchar](50) NULL,
	[PartKitNo] [nvarchar](50) NULL,
	[Description] [nvarchar](100) NULL,
	[Respresentative] [nvarchar](50) NULL,
	[Status] [nvarchar](50) NULL,
	[QuoteJson] [ntext] NULL,
	[CustomerRequirementJson] [ntext] NULL,
	[SupplierId] [nvarchar](50) NULL,
	[LocationId] [nvarchar](50) NULL,
	[ProductName] [nvarchar](50) NULL,
	[PartId] [nvarchar](50) NULL,
	[ProcedureId] [nvarchar](50) NULL,
	[ProductId] [nvarchar](50) NULL,
	ProductWorkflowId [nvarchar](50) NULL,
	[LeadTime] float NULL,
	[Price] float NULL,
 CONSTRAINT [PK_Portal_CustomerSubmittedRequirements] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

