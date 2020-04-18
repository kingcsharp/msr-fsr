CREATE TABLE [dbo].[Document] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [Name]          VARCHAR (100) NOT NULL,
    [Revision]      INT           NOT NULL,
    [RoleId]        INT           NOT NULL,
    [Comments]      VARCHAR (MAX) NULL,
    [CreatedBy]     INT           NOT NULL,
    [CreateOn]      DATETIME      NOT NULL,
    [LastUpdatedBy] INT           NULL,
    [LastUpdatedOn] DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

