CREATE TABLE [dbo].[PurchaseOrderApproval] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [PurchaseOrderId]    INT           NOT NULL,
    [StatusId]           INT           NOT NULL,
    [Name]               VARCHAR (100) NOT NULL,
    [ReferencePO]        VARCHAR (50)  NOT NULL,
    [ReferenceName]      VARCHAR (100) NOT NULL,
    [OpenDate]           DATETIME      NOT NULL,
    [CloseDate]          DATETIME      NULL,
    [TotalPurchaseLimit] MONEY         NULL,
    [CustomerReference]  VARCHAR (100) NULL,
    [CreatedBy]          INT           NOT NULL,
    [CreateOn]           DATETIME      NOT NULL,
    [LastUpdatedBy]      INT           NULL,
    [LastUpdatedOn]      DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

