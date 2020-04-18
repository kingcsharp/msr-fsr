CREATE TABLE [dbo].[CustomerApproval] (
    [Id]                    INT            IDENTITY (1, 1) NOT NULL,
    [CustomerId]            INT            NOT NULL,
    [StatusId]              INT            NOT NULL,
    [OldId]                 INT            NOT NULL,
    [Name]                  NVARCHAR (100) NULL,
    [Address]               NVARCHAR (100) NULL,
    [Phone]                 NVARCHAR (20)  NULL,
    [PrimaryContactUserId]  INT            NULL,
    [SecondarContactUserId] INT            NULL,
    [LocationId]            INT            NULL,
    [CreatedBy]             INT            NULL,
    [CreatedOn]             DATETIME       NOT NULL,
    [LastUpdatedBy]         INT            NULL,
    [LastUpdatedOn]         DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CustomerApprovalCreatedById] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_CustomerApprovalCustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([Id]),
    CONSTRAINT [FK_CustomerApprovalLastUpdatedById] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_CustomerApprovalLocationId] FOREIGN KEY ([LocationId]) REFERENCES [dbo].[Location] ([Id]),
    CONSTRAINT [FK_CustomerApprovalStatusId] FOREIGN KEY ([StatusId]) REFERENCES [dbo].[Status] ([Id])
);

