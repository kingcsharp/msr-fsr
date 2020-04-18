CREATE TABLE [dbo].[Purchase] (
    [Id]                     INT          IDENTITY (1, 1) NOT NULL,
    [PurchaseOrderId]        INT          NOT NULL,
    [PurchaseOrderProductId] INT          NOT NULL,
    [CustomerPurchaseNumber] VARCHAR (50) NULL,
    [LocationId]             INT          NOT NULL,
    [SerialNumber]           VARCHAR (20) NOT NULL,
    [Qty]                    INT          NOT NULL,
    [CustomerLineNumber]     VARCHAR (10) NULL,
    [MTTN]                   VARCHAR (10) NULL,
    [PurchasePrice]          MONEY        NOT NULL,
    [StatusId]               INT          NOT NULL,
    [CreatedBy]              INT          NOT NULL,
    [CreateOn]               DATETIME     NOT NULL,
    [LastUpdatedBy]          INT          NOT NULL,
    [LastUpdatedOn]          DATETIME     NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Purchase_Purchase] FOREIGN KEY ([Id]) REFERENCES [dbo].[Purchase] ([Id]),
    CONSTRAINT [FK_Purchase_PurchaseOrderProductId] FOREIGN KEY ([PurchaseOrderProductId]) REFERENCES [dbo].[PurchaseOrderProduct] ([Id]),
    CONSTRAINT [FK_Purchase_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [dbo].[Status] ([Id])
);

