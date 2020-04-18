CREATE TABLE [dbo].[RoleChildRoleMap] (
    [Id]            INT      IDENTITY (1, 1) NOT NULL,
    [RoleIdId]      INT      NOT NULL,
    [ChildRoleId]   INT      NOT NULL,
    [CreatedBy]     INT      NOT NULL,
    [CreatedOn]     DATETIME NOT NULL,
    [LastUpdatedBy] INT      NULL,
    [LastUpdatedOn] DATETIME NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

