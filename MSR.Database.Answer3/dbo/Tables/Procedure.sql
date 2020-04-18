CREATE TABLE [dbo].[Procedure] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [Name]            VARCHAR (100) NOT NULL,
    [ProcedureTypeId] INT           NOT NULL,
    [Revision]        INT           NOT NULL,
    [Duration]        FLOAT (53)    NOT NULL,
    [DurationType]    VARCHAR (20)  NOT NULL,
    [CreatedBy]       INT           NOT NULL,
    [CreateOn]        DATETIME      NOT NULL,
    [LastUpdatedBy]   INT           NULL,
    [LastUpdatedOn]   DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProcedureCreatedById] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_ProcedureLastUpdatedById] FOREIGN KEY ([LastUpdatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_ProcedureProcedureTypeId] FOREIGN KEY ([ProcedureTypeId]) REFERENCES [dbo].[ProcedureType] ([Id])
);

