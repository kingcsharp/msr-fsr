CREATE TABLE [dbo].[SensorsValue] (
    [ItemID]           VARCHAR (255)  NOT NULL,
    [ItemCurrentValue] VARCHAR (2000) NULL,
    [ItemTimeStamp]    DATETIME       NULL,
    [ItemQuality]      VARCHAR (255)  NULL,
    PRIMARY KEY CLUSTERED ([ItemID] ASC)
);

