CREATE TABLE [dbo].[WorkOrderPart] (
    [Id]            INT          IDENTITY (1, 1) NOT NULL,
    [WorkOrderId]   INT          NOT NULL,
    [PartId]        INT          NOT NULL,
    [ParentId]      INT          NOT NULL,
    [SerialNumber]  VARCHAR (50) NULL,
    [CreatedBy]     INT          NOT NULL,
    [CreateOn]      DATETIME     NOT NULL,
    [LastUpdatedBy] INT          NOT NULL,
    [LastUpdatedOn] DATETIME     NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_WorkOrderPart_WorkOrderPart] FOREIGN KEY ([Id]) REFERENCES [dbo].[WorkOrderPart] ([Id]),
    CONSTRAINT [FK_WorkOrderPartCreatedById] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_WorkOrderPartLastUpdatedById] FOREIGN KEY ([LastUpdatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_WorkOrderPartParentId] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[WorkOrderPart] ([Id]),
    CONSTRAINT [FK_WorkOrderPartPartId] FOREIGN KEY ([PartId]) REFERENCES [dbo].[Part] ([Id]) ON UPDATE CASCADE,
    CONSTRAINT [FK_WorkOrderPartWorkOrderId] FOREIGN KEY ([WorkOrderId]) REFERENCES [dbo].[WorkOrder] ([Id])
);

