CREATE TABLE [dbo].[SensorLocationMap] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [SensorMappingId] INT           NOT NULL,
    [InternalAddress] NVARCHAR (20) NOT NULL,
    [CreatedBy]       INT           NOT NULL,
    [CreatedOn]       DATETIME      NOT NULL,
    [LastUpdatedBy]   INT           NULL,
    [LastUpdatedOn]   DATETIME      NULL,
    CONSTRAINT [PK_Sensor_Location_Mapping] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SensorLocationMap_CreatedById] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_SensorLocationMap_SensorMappingId] FOREIGN KEY ([SensorMappingId]) REFERENCES [dbo].[SensorValueMap] ([SensorMappingId]),
    CONSTRAINT [FK_SensorLocationMap_UpdatedById] FOREIGN KEY ([LastUpdatedBy]) REFERENCES [dbo].[User] ([Id])
);

