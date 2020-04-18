CREATE TABLE [dbo].[WorkflowGroupStageMap] (
    [Id]              INT      IDENTITY (1, 1) NOT NULL,
    [AnswerStageId]   INT      NOT NULL,
    [WorkflowGroupId] INT      NOT NULL,
    [CreatedBy]       INT      NOT NULL,
    [CreatedOn]       DATETIME NOT NULL,
    [LastUpdatedBy]   INT      NULL,
    [LastUpdatedOn]   DATETIME NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

