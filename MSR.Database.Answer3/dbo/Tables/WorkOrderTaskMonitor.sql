CREATE TABLE [dbo].[WorkOrderTaskMonitor] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [WorkOrderTaskId] INT            NOT NULL,
    [MonitorTPLId]    INT            NOT NULL,
    [NumVal]          INT            NULL,
    [TextVal]         VARCHAR (255)  NULL,
    [MultiVal]        VARCHAR (50)   NULL,
    [SensorMappingId] INT            NOT NULL,
    [Comment]         NVARCHAR (MAX) NULL,
    [CreatedBy]       INT            NOT NULL,
    [CreateOn]        DATETIME       NOT NULL,
    [LastUpdatedBy]   INT            NOT NULL,
    [LastUpdatedOn]   DATETIME       NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_WorkOrderTaskMonitorCreatedById] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_WorkOrderTaskMonitorLastUpdatedById] FOREIGN KEY ([LastUpdatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_WorkOrderTaskMonitorMonitorTPLId] FOREIGN KEY ([MonitorTPLId]) REFERENCES [dbo].[MonitorTemplate] ([Id]),
    CONSTRAINT [FK_WorkOrderTaskMonitorWorkOrderTaskId] FOREIGN KEY ([WorkOrderTaskId]) REFERENCES [dbo].[WorkOrderTask] ([Id])
);

