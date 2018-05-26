CREATE TABLE [dbo].[Portal_RequirementSteps](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerSubmittedRequirementId] [int] NOT NULL,
	[ObjectId] [nvarchar](50) NOT NULL,
	[Process] [nvarchar](MAX) NULL,
	[Step] [int] NULL,
 CONSTRAINT [PK_Portal_RequirementSteps] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Portal_RequirementSteps]  WITH CHECK ADD  CONSTRAINT [FK_Portal_RequirementSteps_Portal_CustomerSubmittedRequirement] FOREIGN KEY([CustomerSubmittedRequirementId])
REFERENCES [dbo].[Portal_CustomerSubmittedRequirement] ([Id])
GO

ALTER TABLE [dbo].[Portal_RequirementSteps] CHECK CONSTRAINT [FK_Portal_RequirementSteps_Portal_CustomerSubmittedRequirement]
GO

