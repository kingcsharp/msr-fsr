CREATE TABLE [dbo].[ProcedureVerbType] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [Name]          VARCHAR (100) NOT NULL,
    [CreatedBy]     INT           NOT NULL,
    [CreateOn]      DATETIME      NOT NULL,
    [LastUpdatedBy] INT           NULL,
    [LastUpdatedOn] DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

