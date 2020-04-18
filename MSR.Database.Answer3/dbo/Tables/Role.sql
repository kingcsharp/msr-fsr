CREATE TABLE [dbo].[Role] (
    [Id]                  INT            IDENTITY (1, 1) NOT NULL,
    [Name]                VARCHAR (100)  NOT NULL,
    [IsCertificationRole] BIT            NULL,
    [CreatedBy]           INT            NOT NULL,
    [CreatedOn]           DATETIME       NOT NULL,
    [LastUpdatedBy]       INT            NULL,
    [LastUpdatedOn]       DATETIME       NULL,
    [OldId]               NVARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

