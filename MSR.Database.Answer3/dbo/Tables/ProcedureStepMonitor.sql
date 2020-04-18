CREATE TABLE [dbo].[ProcedureStepMonitor] (
    [Id]              INT      IDENTITY (1, 1) NOT NULL,
    [ProcedureStepId] INT      NOT NULL,
    [MonitorTPLId]    INT      NOT NULL,
    [CreatedBy]       INT      NOT NULL,
    [CreateOn]        DATETIME NOT NULL,
    [LastUpdatedBy]   INT      NULL,
    [LastUpdatedOn]   DATETIME NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProcedureStepMonitorCreatedById] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_ProcedureStepMonitorLastUpdatedById] FOREIGN KEY ([LastUpdatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_ProcedureStepMonitorMonitorId] FOREIGN KEY ([MonitorTPLId]) REFERENCES [dbo].[MonitorTemplate] ([Id]),
    CONSTRAINT [FK_ProcedureStepMonitorProcedureStepId] FOREIGN KEY ([ProcedureStepId]) REFERENCES [dbo].[ProcedureStep] ([Id])
);

