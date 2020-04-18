CREATE TABLE [dbo].[ProcedureApproval] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [ProcedureId]     INT           NOT NULL,
    [StatusId]        INT           NOT NULL,
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
    CONSTRAINT [FK_ProcedureApprovalCreatedById] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_ProcedureApprovalLastUpdatedById] FOREIGN KEY ([LastUpdatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_ProcedureApprovalProcedureId] FOREIGN KEY ([ProcedureId]) REFERENCES [dbo].[Procedure] ([Id]),
    CONSTRAINT [FK_ProcedureApprovalProcedureTypeId] FOREIGN KEY ([ProcedureTypeId]) REFERENCES [dbo].[ProcedureType] ([Id]),
    CONSTRAINT [FK_ProcedureApprovalStatusId] FOREIGN KEY ([StatusId]) REFERENCES [dbo].[Status] ([Id])
);

