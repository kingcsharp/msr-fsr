CREATE TABLE [dbo].[WorkflowActivityMap] (
    [Id]                 INT      IDENTITY (1, 1) NOT NULL,
    [WorkflowId]         INT      NOT NULL,
    [WorkflowActivityId] INT      NOT NULL,
    [CreatedBy]          INT      NOT NULL,
    [CreatedOn]          DATETIME NOT NULL,
    [LastUpdatedBy]      INT      NULL,
    [LastUpdatedOn]      DATETIME NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

