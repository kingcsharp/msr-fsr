CREATE TABLE [dbo].[PurchaseOrderProductApproval] (
    [Id]                      INT      IDENTITY (1, 1) NOT NULL,
    [PurchaseOrderApprovalId] INT      NOT NULL,
    [CreatedBy]               INT      NOT NULL,
    [CreateOn]                DATETIME NOT NULL,
    [LastUpdatedBy]           INT      NULL,
    [LastUpdatedOn]           DATETIME NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

