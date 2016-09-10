CREATE TABLE [dbo].[PartsTransactionLog] (
    [Id]                INT          IDENTITY (1, 1) NOT NULL,
    [PartId]            INT          NOT NULL,
    [SerialNumber]      VARCHAR (50) NOT NULL,
    [PurchaseHistoryId] INT          NULL,
    [CreatedDate]       DATETIME     NOT NULL,
    [TaskId]            INT          NOT NULL,
    CONSTRAINT [PK_PartsTransactionLog] PRIMARY KEY CLUSTERED ([Id] ASC)
);

