CREATE TABLE [dbo].[WorkflowGroup] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (100) NULL,
    [IsActive]      BIT            DEFAULT ((1)) NULL,
    [CreatedBy]     INT            NOT NULL,
    [CreatedOn]     DATETIME       NOT NULL,
    [LastUpdatedBy] INT            NULL,
    [LastUpdatedOn] DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

