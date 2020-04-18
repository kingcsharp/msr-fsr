CREATE TABLE [dbo].[WorkflowGroupRoleMap] (
    [Id]              INT      IDENTITY (1, 1) NOT NULL,
    [AnswerRoleId]    INT      NOT NULL,
    [WorkflowGroupId] INT      NOT NULL,
    [CreatedBy]       INT      NOT NULL,
    [CreatedOn]       DATETIME NOT NULL,
    [LastUpdatedBy]   INT      NULL,
    [LastUpdatedOn]   DATETIME NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

