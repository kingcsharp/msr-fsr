CREATE TABLE [dbo].[Portal_RequirementSteps](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerSubmittedRequirementId] [int] NOT NULL,
	[ObjectId] [nvarchar](50) NOT NULL,
	[Process] [nvarchar](MAX) NULL,
	[Step] [int] NULL,
	[StandardDirectLaborMinutes] FLOAT NOT NULL,
	[StandardMachineMinutes] FLOAT NOT NULL,
	[ReplacementCost] [decimal](18, 2) NULL,
	[Utilization] [decimal](5, 2) NULL,
	[UsefulLife] [int] NULL,
	[EquipExpensePerMinute] [decimal](18, 2) NULL,
	[AnnualRM] [decimal](18, 2) NULL,
	[RMPerMinute] [decimal](18, 2) NULL,
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

