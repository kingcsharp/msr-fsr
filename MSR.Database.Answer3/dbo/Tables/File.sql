CREATE TABLE [dbo].[File] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (100) NOT NULL,
    [FileURL]     VARCHAR (MAX) NOT NULL,
    [ContentType] VARCHAR (100) NOT NULL,
    [CreatedBy]   INT           NOT NULL,
    [CreateOn]    DATETIME      NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FileCreatedById] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id])
);

