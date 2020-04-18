CREATE TABLE [dbo].[MenuGroup] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [URL]           NVARCHAR (255) NOT NULL,
    [Name]          VARCHAR (50)   NOT NULL,
    [Info]          NVARCHAR (100) NOT NULL,
    [Icon]          NVARCHAR (50)  NULL,
    [OrderNumber]   INT            NOT NULL,
    [CreatedBy]     INT            NOT NULL,
    [CreatedOn]     DATETIME       NOT NULL,
    [LastUpdatedBy] INT            NULL,
    [LastUpdatedOn] DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MenuGroupCreatedById] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_MenuGroupLastUpdatedById] FOREIGN KEY ([LastUpdatedBy]) REFERENCES [dbo].[User] ([Id])
);

