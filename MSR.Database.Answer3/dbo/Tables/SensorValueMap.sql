CREATE TABLE [dbo].[SensorValueMap] (
    [SensorMappingId] INT            IDENTITY (1, 1) NOT NULL,
    [Site]            NVARCHAR (50)  NULL,
    [SensorName]      NVARCHAR (50)  NOT NULL,
    [MonitoringId]    NVARCHAR (250) NOT NULL,
    [LocationId]      INT            NULL,
    PRIMARY KEY CLUSTERED ([SensorMappingId] ASC)
);

