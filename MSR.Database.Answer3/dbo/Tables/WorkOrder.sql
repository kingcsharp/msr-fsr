CREATE TABLE [dbo].[WorkOrder] (
    [Id]                 INT      IDENTITY (1, 1) NOT NULL,
    [PurchaseId]         INT      NOT NULL,
    [ProductId]          INT      NOT NULL,
    [Price]              MONEY    NOT NULL,
    [ScheduledStartDate] DATETIME NOT NULL,
    [ScheduledEndDate]   DATETIME NOT NULL,
    [ActualStartDate]    DATETIME NULL,
    [ActualEndDate]      DATETIME NULL,
    [HasNCR]             BIT      DEFAULT ((0)) NOT NULL,
    [CreatedBy]          INT      NOT NULL,
    [CreateOn]           DATETIME NOT NULL,
    [LastUpdatedBy]      INT      NOT NULL,
    [LastUpdatedOn]      DATETIME NOT NULL,
    [LocationId]         INT      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_WorkOrder_PurchaseId] FOREIGN KEY ([PurchaseId]) REFERENCES [dbo].[Purchase] ([Id]),
    CONSTRAINT [FK_WorkOrder_WorkOrder] FOREIGN KEY ([Id]) REFERENCES [dbo].[WorkOrder] ([Id]),
    CONSTRAINT [FK_WorkOrderCreatedById] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_WorkOrderLastUpdatedById] FOREIGN KEY ([LastUpdatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_WorkOrderLocationId] FOREIGN KEY ([LocationId]) REFERENCES [dbo].[Location] ([Id])
);

