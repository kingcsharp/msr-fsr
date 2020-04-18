CREATE TABLE [dbo].[ProcedureStepMonitorApproval] (
    [Id]                      INT      IDENTITY (1, 1) NOT NULL,
    [ProcedureStepApprovalId] INT      NOT NULL,
    [MonitorTPLId]            INT      NOT NULL,
    [CreatedBy]               INT      NOT NULL,
    [CreateOn]                DATETIME NOT NULL,
    [LastUpdatedBy]           INT      NULL,
    [LastUpdatedOn]           DATETIME NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProcedureStepMonitorAppprovalCreatedById] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_ProcedureStepMonitorApprovalLastUpdatedById] FOREIGN KEY ([LastUpdatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_ProcedureStepMonitorApprovalMonitorId] FOREIGN KEY ([MonitorTPLId]) REFERENCES [dbo].[MonitorTemplate] ([Id]),
    CONSTRAINT [FK_ProcedureStepMonitorProcedureApprovalId] FOREIGN KEY ([ProcedureStepApprovalId]) REFERENCES [dbo].[ProcedureStepApproval] ([Id])
);

