CREATE TABLE [dbo].[CustomerRequirement] (
    [Id]                      INT            NOT NULL,
    [SubmittedDate]           DATETIME       NOT NULL,
    [SubmittedBy]             NVARCHAR (50)  NOT NULL,
    [CustomerId]              NVARCHAR (50)  NULL,
    [Company]                 NVARCHAR (50)  NULL,
    [Division]                NVARCHAR (50)  NULL,
    [PartKitNo]               NVARCHAR (50)  NULL,
    [Description]             NVARCHAR (100) NULL,
    [Respresentative]         NVARCHAR (50)  NULL,
    [Status]                  NVARCHAR (50)  NULL,
    [QuoteJson]               NTEXT          NULL,
    [CustomerRequirementJson] NTEXT          NULL,
    [SupplierId]              NVARCHAR (50)  NULL,
    [LocationId]              NVARCHAR (50)  NULL,
    [ProductName]             NVARCHAR (50)  NULL,
    [PartId]                  NVARCHAR (50)  NULL,
    [ProcedureId]             NVARCHAR (50)  NULL,
    [ProductWorkflowId]       NVARCHAR (50)  NULL,
    [LeadTime]                FLOAT (53)     NULL,
    [Price]                   FLOAT (53)     NULL
);

