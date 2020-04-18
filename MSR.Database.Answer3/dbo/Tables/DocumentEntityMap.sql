CREATE TABLE [dbo].[DocumentEntityMap] (
    [Id]              INT          IDENTITY (1, 1) NOT NULL,
    [DocumentId]      INT          NOT NULL,
    [EntityTableName] VARCHAR (50) NOT NULL,
    [EntityId]        INT          NOT NULL,
    [CreatedBy]       INT          NULL,
    [CreatedOn]       DATETIME     NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DocumentEntityMapCreatedById] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_DocumentEntityMapDocumentId] FOREIGN KEY ([DocumentId]) REFERENCES [dbo].[Document] ([Id])
);

