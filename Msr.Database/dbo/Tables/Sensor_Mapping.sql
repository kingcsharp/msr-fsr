CREATE TABLE [dbo].[Sensor_Mapping] (
    [SensorMappingID] INT            NOT NULL,
    [Site]            NVARCHAR (50)  NULL,
    [SensorName]      NVARCHAR (50)  NOT NULL,
    [MonitoringID]    NVARCHAR (250) NOT NULL,
    CONSTRAINT [PK_Sensor_Mapping] PRIMARY KEY CLUSTERED ([SensorMappingID] ASC)
);




