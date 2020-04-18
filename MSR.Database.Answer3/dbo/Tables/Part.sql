CREATE TABLE [dbo].[Part] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [Name]          VARCHAR (100) NOT NULL,
    [PartNumber]    VARCHAR (100) NOT NULL,
    [OEMPartNumber] VARCHAR (100) NULL,
    [IsKit]         BIT           NULL,
    [Qty]           INT           DEFAULT ((1)) NOT NULL,
    [NickName]      VARCHAR (100) NULL,
    [ParentId]      INT           NULL,
    [MaximumCycles] INT           NULL,
    [CreatedBy]     INT           NOT NULL,
    [CreateOn]      DATETIME      NOT NULL,
    [LastUpdatedBy] INT           NULL,
    [LastUpdatedOn] DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LastUpdatedById] FOREIGN KEY ([LastUpdatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_PartCreatedById] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_PartParentPartId] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[Part] ([Id])
);

