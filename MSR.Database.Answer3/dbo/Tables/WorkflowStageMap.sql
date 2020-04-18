CREATE TABLE [dbo].[WorkflowStageMap] (
    [Id]              INT      IDENTITY (1, 1) NOT NULL,
    [WorkflowId]      INT      NOT NULL,
    [WorkflowStageId] INT      NOT NULL,
    [CreatedBy]       INT      NOT NULL,
    [CreatedOn]       DATETIME NOT NULL,
    [LastUpdatedBy]   INT      NULL,
    [LastUpdatedOn]   DATETIME NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

