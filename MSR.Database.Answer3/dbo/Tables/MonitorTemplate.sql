CREATE TABLE [dbo].[MonitorTemplate] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [MonitorType]      VARCHAR (20)  NOT NULL,
    [InputType]        VARCHAR (20)  NOT NULL,
    [Description]      VARCHAR (200) NOT NULL,
    [ListSource]       VARCHAR (20)  NULL,
    [ShouldBe]         VARCHAR (20)  NULL,
    [HighestThreshold] REAL          NULL,
    [HighThreshold]    REAL          NULL,
    [LowestThreshold]  REAL          NULL,
    [LowThreshold]     REAL          NULL,
    [Target]           REAL          NULL,
    [FailAction]       VARCHAR (20)  NOT NULL,
    [SensorMappingId]  INT           NOT NULL,
    [SendNCREmail]     BIT           NULL,
    [CreatedBy]        INT           NOT NULL,
    [CreateOn]         DATETIME      NOT NULL,
    [LastUpdatedBy]    INT           NULL,
    [LastUpdatedOn]    DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

