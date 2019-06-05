CREATE TABLE [dbo].[Portal_EquipmentMaintenance] (
    [Id]                   INT             IDENTITY (1, 1) NOT NULL,
    [ObjectId]             NVARCHAR (50)   NULL,
    [ScanBarcode]          NVARCHAR (130)  NULL,
    [RoomEquipment]        NVARCHAR (100)  NULL,
    [DateTime]             DATETIME        NULL,
    [RequestedById]        NVARCHAR (50)   NULL,
    [AssignedToId]         NVARCHAR (50)   NULL,
    [TroubleState]         BIT             NULL,
    [MaintenanceTask]      NVARCHAR (50)   NULL,
    [Comments]             NVARCHAR (4000) NULL,
    [Status]               NVARCHAR (50)   NOT NULL,
    [StrNTLogin]           NVARCHAR (50)   NOT NULL,
    [PemLastCompletedDate] DATETIME        NULL,
    [FrequencyField]       INT             NULL,
    [CreatedDate]          DATETIME        NOT NULL,
    [UpdatedDate]          DATETIME        NULL,
    CONSTRAINT [PK_PortalEquipmentMaintenance] PRIMARY KEY CLUSTERED ([Id] ASC)
);





GO

