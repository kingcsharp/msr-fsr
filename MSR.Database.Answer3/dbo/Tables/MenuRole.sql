CREATE TABLE [dbo].[MenuRole] (
    [Id]            INT      IDENTITY (1, 1) NOT NULL,
    [MenuItemId]    INT      NULL,
    [RoleId]        INT      NOT NULL,
    [CreatedBy]     INT      NOT NULL,
    [CreatedOn]     DATETIME NOT NULL,
    [LastUpdatedBy] INT      NULL,
    [LastUpdatedOn] DATETIME NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MenuRoleCreatedById] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_MenuRoleLastUpdatedById] FOREIGN KEY ([LastUpdatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_RoleMenuRoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([Id])
);

