CREATE TABLE [dbo].[Portal_EquipmentMaintenance] (
    [Id]                NVARCHAR (50)   NOT NULL,
    [ObjectId]          NVARCHAR (50)   NULL,
    [ScanBarcode]       NVARCHAR (130)  NULL,
    [ParentLocation]    NVARCHAR (100)  NOT NULL,
    [SubLocationFirst]  NVARCHAR (100)  NULL,
    [SubLocationSecond] NVARCHAR (100)  NULL,
    [DateTime]          DATETIME        NULL,
    [RequestedById]       NVARCHAR (50)   NOT NULL,
    [TroubleState]      BIT             NULL,
    [MaintenanceTask]   NVARCHAR (50)   NULL,
    [Comments]          NVARCHAR (4000) NULL,
    [Status]            NVARCHAR (50)   NOT NULL,
    [StrNTLogin]        NVARCHAR (50)   NOT NULL,
    [ApprovedById]        VARCHAR (50)    NULL,
    CONSTRAINT [PK_PortalEquipmentMaintenance] PRIMARY KEY CLUSTERED ([Id] ASC)
);



GO

