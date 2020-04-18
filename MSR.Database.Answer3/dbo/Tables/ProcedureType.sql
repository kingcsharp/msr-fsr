CREATE TABLE [dbo].[ProcedureType] (
    [Id]                  INT           IDENTITY (1, 1) NOT NULL,
    [Name]                VARCHAR (100) NOT NULL,
    [ProcedureVerbTypeId] INT           NULL,
    [CreatedBy]           INT           NOT NULL,
    [CreateOn]            DATETIME      NOT NULL,
    [LastUpdatedBy]       INT           NULL,
    [LastUpdatedOn]       DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProcedureTypeProcedureTypeVerbId] FOREIGN KEY ([ProcedureVerbTypeId]) REFERENCES [dbo].[ProcedureVerbType] ([Id])
);

