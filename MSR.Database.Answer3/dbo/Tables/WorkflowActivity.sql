CREATE TABLE [dbo].[WorkflowActivity] (
    [Id]                INT           IDENTITY (1, 1) NOT NULL,
    [Name]              VARCHAR (100) NOT NULL,
    [ApprovalTableName] VARCHAR (100) NOT NULL,
    [IsActive]          BIT           NULL,
    [CreateRevision]    BIT           NULL,
    [CreatedBy]         INT           NOT NULL,
    [CreatedOn]         DATETIME      NOT NULL,
    [LastUpdatedBy]     INT           NULL,
    [LastUpdatedOn]     DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

