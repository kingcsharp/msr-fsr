CREATE TABLE [dbo].[ProcedureStepDocumentApproval] (
    [Id]                      INT      IDENTITY (1, 1) NOT NULL,
    [ProcedureStepApprovalId] INT      NOT NULL,
    [DocumentId]              INT      NOT NULL,
    [CreatedBy]               INT      NOT NULL,
    [CreateOn]                DATETIME NOT NULL,
    [LastUpdatedBy]           INT      NULL,
    [LastUpdatedOn]           DATETIME NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProcedureStepDocumentAppprovalCreatedById] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_ProcedureStepDocumentApprovalDocumentId] FOREIGN KEY ([DocumentId]) REFERENCES [dbo].[Document] ([Id]),
    CONSTRAINT [FK_ProcedureStepDocumentApprovalLastUpdatedById] FOREIGN KEY ([LastUpdatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_ProcedureStepDocumentProcedureApprovalId] FOREIGN KEY ([ProcedureStepApprovalId]) REFERENCES [dbo].[ProcedureStepApproval] ([Id])
);

