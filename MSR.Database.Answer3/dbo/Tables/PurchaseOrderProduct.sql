CREATE TABLE [dbo].[PurchaseOrderProduct] (
    [Id]              INT      IDENTITY (1, 1) NOT NULL,
    [PurchaseOrderId] INT      NOT NULL,
    [ProductId]       INT      NOT NULL,
    [ProductRevision] INT      NOT NULL,
    [CreatedBy]       INT      NOT NULL,
    [CreateOn]        DATETIME NOT NULL,
    [LastUpdatedBy]   INT      NULL,
    [LastUpdatedOn]   DATETIME NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

