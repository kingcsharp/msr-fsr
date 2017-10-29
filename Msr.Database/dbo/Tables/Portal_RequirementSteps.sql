CREATE TABLE [dbo].[Portal_RequirementSteps] (
    [Id]                         INT             IDENTITY (1, 1) NOT NULL,
    [ObjectId]                   NVARCHAR (50)   NOT NULL,
    [Process]                    NVARCHAR (50)   NULL,
    [Step]                       INT             NULL,
    [StandardDirectLaborMinutes] DECIMAL (10, 2) NOT NULL,
    [StandardMachineMinutes]     DECIMAL (10, 2) NOT NULL,
    [ReplacementCost]            DECIMAL (18, 2) NULL,
    [Utilization]                DECIMAL (5, 2)  NULL,
    [UsefulLife]                 DECIMAL (18, 2) NULL,
    [EquipExpensePerMinute]      DECIMAL (18, 2) NULL,
    [AnnualRM]                   DECIMAL (18, 2) NULL,
    [RMPerMinute]                DECIMAL (18, 2) NULL,
    CONSTRAINT [PK_Portal_RequirementSteps] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Portal_RequirementSteps_Portal_RequirementSteps] FOREIGN KEY ([ObjectId]) REFERENCES [dbo].[Portal_CustomerRequirement] ([Id])
);



