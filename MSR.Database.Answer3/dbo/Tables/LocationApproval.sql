CREATE TABLE [dbo].[LocationApproval] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [LocationId]      INT            NOT NULL,
    [OldId]           INT            NOT NULL,
    [Name]            NVARCHAR (100) NULL,
    [Address1]        NVARCHAR (100) NULL,
    [Address2]        NVARCHAR (100) NULL,
    [City]            NVARCHAR (100) NULL,
    [State]           NVARCHAR (100) NULL,
    [PostalCode]      NVARCHAR (15)  NULL,
    [Country]         NVARCHAR (50)  NULL,
    [Phone]           NVARCHAR (20)  NULL,
    [ParentId]        INT            NULL,
    [InternalAddress] NVARCHAR (20)  NULL,
    [InvoiceClass]    NVARCHAR (20)  NULL,
    [CreatedBy]       INT            NOT NULL,
    [CreatedOn]       DATETIME       NOT NULL,
    [LastUpdatedBy]   INT            NULL,
    [LastUpdatedOn]   DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

