CREATE TABLE [dbo].[InvoiceItem] (
    [Id]              INT      IDENTITY (1, 1) NOT NULL,
    [InvoiceId]       INT      NOT NULL,
    [PurchaseOrderId] INT      NOT NULL,
    [WorkOrderId]     INT      NOT NULL,
    [CreatedBy]       INT      NOT NULL,
    [CreateOn]        DATETIME NOT NULL,
    [LastUpdatedBy]   INT      NOT NULL,
    [LastUpdatedOn]   DATETIME NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InvoiceItemCreatedById] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_InvoiceItemInvoiceId] FOREIGN KEY ([InvoiceId]) REFERENCES [dbo].[Invoice] ([Id]),
    CONSTRAINT [FK_InvoiceItemLastUpdatedById] FOREIGN KEY ([LastUpdatedBy]) REFERENCES [dbo].[User] ([Id])
);

