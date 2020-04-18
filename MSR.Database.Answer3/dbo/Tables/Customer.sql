CREATE TABLE [dbo].[Customer] (
    [Id]                     INT            IDENTITY (1, 1) NOT NULL,
    [OldId]                  INT            NOT NULL,
    [Name]                   NVARCHAR (100) NULL,
    [Address]                NVARCHAR (100) NULL,
    [Phone]                  NVARCHAR (20)  NULL,
    [PrimaryContactUserId]   INT            NULL,
    [SecondaryContactUserId] INT            NULL,
    [LocationId]             INT            NULL,
    [CreatedBy]              INT            NULL,
    [CreatedOn]              DATETIME       NOT NULL,
    [LastUpdatedBy]          INT            NULL,
    [LastUpdatedOn]          DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CustomerLocationId] FOREIGN KEY ([LocationId]) REFERENCES [dbo].[Location] ([Id]),
    CONSTRAINT [FK_CustomerPrimaryContactUserId] FOREIGN KEY ([PrimaryContactUserId]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_CustomerSecondaryContactUserId] FOREIGN KEY ([SecondaryContactUserId]) REFERENCES [dbo].[User] ([Id])
);

