CREATE TABLE [dbo].[DocumentApproval] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [DocumentId]    INT           NOT NULL,
    [StatusId]      INT           NOT NULL,
    [Name]          VARCHAR (100) NOT NULL,
    [Revision]      INT           NOT NULL,
    [RoleId]        INT           NOT NULL,
    [Comments]      VARCHAR (MAX) NULL,
    [CreatedBy]     INT           NOT NULL,
    [CreateOn]      DATETIME      NOT NULL,
    [LastUpdatedBy] INT           NULL,
    [LastUpdatedOn] DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DocumentApprovalCreatedById] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_DocumentApprovalDocumentIdId] FOREIGN KEY ([DocumentId]) REFERENCES [dbo].[Document] ([Id]),
    CONSTRAINT [FK_DocumentApprovalLastUpdatedById] FOREIGN KEY ([LastUpdatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_DocumentApprovalStatusId] FOREIGN KEY ([StatusId]) REFERENCES [dbo].[Status] ([Id])
);

