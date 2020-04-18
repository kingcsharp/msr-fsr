CREATE TABLE [dbo].[ProductApproval] (
    [Id]                    INT           IDENTITY (1, 1) NOT NULL,
    [ProductId]             INT           NOT NULL,
    [StatusId]              INT           NOT NULL,
    [Name]                  VARCHAR (100) NOT NULL,
    [Revision]              INT           NOT NULL,
    [CustomerId]            INT           NOT NULL,
    [CustomerRequirementId] INT           NULL,
    [ProcedureId]           INT           NOT NULL,
    [PartId]                INT           NOT NULL,
    [EquipmentCost]         MONEY         NOT NULL,
    [MaterialCost]          MONEY         NOT NULL,
    [SalesTax]              MONEY         NULL,
    [TotalSalePrice]        MONEY         NOT NULL,
    [CycleTime]             INT           NULL,
    [CreatedBy]             INT           NOT NULL,
    [CreateOn]              DATETIME      NOT NULL,
    [LastUpdatedBy]         INT           NOT NULL,
    [LastUpdatedOn]         DATETIME      NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

