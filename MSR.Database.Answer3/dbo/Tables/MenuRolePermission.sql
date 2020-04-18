CREATE TABLE [dbo].[MenuRolePermission] (
    [Id]            INT      IDENTITY (1, 1) NOT NULL,
    [MenuRoleId]    INT      NULL,
    [CanRead]       BIT      NULL,
    [CanCreate]     BIT      NULL,
    [CanEdit]       BIT      NULL,
    [CanActivate]   BIT      NULL,
    [CanApprove]    BIT      NULL,
    [CanDelete]     BIT      NULL,
    [CreatedBy]     INT      NOT NULL,
    [CreatedOn]     DATETIME NOT NULL,
    [LastUpdatedBy] INT      NULL,
    [LastUpdatedOn] DATETIME NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MenuRolePermissionLastUpdatedById] FOREIGN KEY ([LastUpdatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_MenuRolePermissionsCreatedById] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_RoleMenuPermissionRoleId] FOREIGN KEY ([MenuRoleId]) REFERENCES [dbo].[MenuRole] ([Id])
);

