CREATE TABLE [dbo].[ProcedureStepTemplate] (
    [Id]              INT             IDENTITY (1, 1) NOT NULL,
    [Title]           VARCHAR (100)   NOT NULL,
    [StepText]        NVARCHAR (4000) NOT NULL,
    [SystemTaskId]    INT             NULL,
    [Duration]        FLOAT (53)      NOT NULL,
    [DurationType]    VARCHAR (20)    NOT NULL,
    [ReplacementCost] MONEY           NULL,
    [Utilization]     REAL            NULL,
    [EquipmentTime]   FLOAT (53)      NULL,
    [Roles]           VARCHAR (255)   NOT NULL,
    [Comments]        NVARCHAR (4000) NOT NULL,
    [CreatedBy]       INT             NOT NULL,
    [CreateOn]        DATETIME        NOT NULL,
    [LastUpdatedBy]   INT             NULL,
    [LastUpdatedOn]   DATETIME        NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProcedureStepTplCreatedById] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_ProcedureStepTplLastUpdatedById] FOREIGN KEY ([LastUpdatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_ProcedureStepTplSystemTaskId] FOREIGN KEY ([SystemTaskId]) REFERENCES [dbo].[SystemTask] ([Id])
);

