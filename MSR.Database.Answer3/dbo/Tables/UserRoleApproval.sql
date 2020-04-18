CREATE TABLE [dbo].[UserRoleApproval] (
    [Id]                    INT      IDENTITY (1, 1) NOT NULL,
    [UserApprovalId]        INT      NOT NULL,
    [UserId]                INT      NOT NULL,
    [RoleId]                INT      NULL,
    [CertificationFromDate] DATETIME NULL,
    [CertificationToDate]   DATETIME NULL,
    [CreatedBy]             INT      NOT NULL,
    [CreatedOn]             DATETIME NOT NULL,
    [LastUpdatedBy]         INT      NULL,
    [LastUpdatedOn]         DATETIME NULL,
    CONSTRAINT [PK_UserRoleApproval] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserRoleApprovalCreatedById] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_UserRoleApprovalLastUpdatedById] FOREIGN KEY ([LastUpdatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_UserRoleApprovalRoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([Id]),
    CONSTRAINT [FK_UserRoleApprovalUserApprovalId] FOREIGN KEY ([UserApprovalId]) REFERENCES [dbo].[UserApproval] ([Id]),
    CONSTRAINT [FK_UserRoleApprovalUserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
);

