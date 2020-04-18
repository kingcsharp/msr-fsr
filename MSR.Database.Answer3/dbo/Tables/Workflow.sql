CREATE TABLE [dbo].[Workflow] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [Name]          VARCHAR (100) NULL,
    [IsActive]      BIT           NULL,
    [CreatedBy]     INT           NOT NULL,
    [CreatedOn]     DATETIME      NOT NULL,
    [LastUpdatedBy] INT           NULL,
    [LastUpdatedOn] DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

