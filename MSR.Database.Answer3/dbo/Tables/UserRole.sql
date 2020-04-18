CREATE TABLE [dbo].[UserRole] (
    [Id]                    INT      IDENTITY (1, 1) NOT NULL,
    [UserId]                INT      NOT NULL,
    [RoleId]                INT      NOT NULL,
    [CertificationFromDate] DATETIME NULL,
    [CertificationToDate]   DATETIME NULL,
    [CreatedBy]             INT      NOT NULL,
    [CreatedOn]             DATETIME NOT NULL,
    [LastUpdatedBy]         INT      NULL,
    [LastUpdatedOn]         DATETIME NULL,
    CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserRoleCreatedById] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_UserRoleLastUpdatedById] FOREIGN KEY ([LastUpdatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_UserRoleRoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([Id]),
    CONSTRAINT [FK_UserRoleUserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
);

