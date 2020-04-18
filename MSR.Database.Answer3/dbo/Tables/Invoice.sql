CREATE TABLE [dbo].[Invoice] (
    [Id]            INT             IDENTITY (1, 1) NOT NULL,
    [StatusId]      INT             NOT NULL,
    [CustomerId]    INT             NOT NULL,
    [Description]   VARCHAR (100)   NOT NULL,
    [InvoiceDate]   DATETIME        NOT NULL,
    [InvoiceClass]  VARCHAR (10)    NOT NULL,
    [Subtotal]      MONEY           NOT NULL,
    [TaxPercentage] DECIMAL (10, 2) NULL,
    [Total]         MONEY           NOT NULL,
    [CreatedBy]     INT             NOT NULL,
    [CreateOn]      DATETIME        NOT NULL,
    [LastUpdatedBy] INT             NOT NULL,
    [LastUpdatedOn] DATETIME        NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InvoiceCreatedById] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_InvoiceLastUpdatedById] FOREIGN KEY ([LastUpdatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_InvoiceStatusId] FOREIGN KEY ([StatusId]) REFERENCES [dbo].[Status] ([Id])
);

