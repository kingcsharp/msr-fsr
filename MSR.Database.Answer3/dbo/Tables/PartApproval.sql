CREATE TABLE [dbo].[PartApproval] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [PartId]        INT           NOT NULL,
    [StatusId]      INT           NOT NULL,
    [Name]          VARCHAR (100) NOT NULL,
    [PartNumber]    VARCHAR (100) NOT NULL,
    [OEMPartNumber] VARCHAR (100) NULL,
    [IsKit]         BIT           NULL,
    [Qty]           INT           NOT NULL,
    [NickName]      VARCHAR (100) NULL,
    [ParentId]      INT           NULL,
    [MaximumCycles] INT           NULL,
    [CreatedBy]     INT           NOT NULL,
    [CreateOn]      DATETIME      NOT NULL,
    [LastUpdatedBy] INT           NULL,
    [LastUpdatedOn] DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PartApprovalCreatedById] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_PartApprovalLastUpdatedById] FOREIGN KEY ([LastUpdatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_PartApprovalPartId] FOREIGN KEY ([PartId]) REFERENCES [dbo].[Part] ([Id])
);

