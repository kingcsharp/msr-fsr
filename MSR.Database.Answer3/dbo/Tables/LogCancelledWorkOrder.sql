CREATE TABLE [dbo].[LogCancelledWorkOrder] (
    [Id]          INT      IDENTITY (1, 1) NOT NULL,
    [WorkOrderId] INT      NOT NULL,
    [WasInvoiced] BIT      NULL,
    [CreatedBy]   INT      NOT NULL,
    [CreatedOn]   DATETIME NOT NULL,
    CONSTRAINT [PK_LogCancelledWorkOrder] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LogCancelledWorkOrder_CreatedById] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_LogCancelledWorkOrder_WorkOrderId] FOREIGN KEY ([WorkOrderId]) REFERENCES [dbo].[WorkOrder] ([Id])
);

