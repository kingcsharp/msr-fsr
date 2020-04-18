CREATE TABLE [dbo].[FileEntityMap] (
    [Id]              INT          IDENTITY (1, 1) NOT NULL,
    [FileId]          INT          NOT NULL,
    [EntityTableName] VARCHAR (50) NOT NULL,
    [EntityId]        INT          NOT NULL,
    [CreatedBy]       INT          NULL,
    [CreatedOn]       DATETIME     NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FileEntityMapCreatedById] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id])
);

