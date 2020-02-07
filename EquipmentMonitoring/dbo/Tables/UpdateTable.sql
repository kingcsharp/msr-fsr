CREATE TABLE [dbo].[UpdateTable] (
    [ItemID]           VARCHAR (255)  NOT NULL,
    [ItemCurrentValue] VARCHAR (2000) NULL,
    [ItemTimeStamp]    DATETIME       NULL,
    [ItemQuality]      VARCHAR (255)  NULL,
    [ServerProgId]     VARCHAR (100)  NULL,
    [ServerAddress]    VARCHAR (100)  NULL,
    [GroupName]        VARCHAR (100)  NULL,
    [ReadMode]         VARCHAR (50)   NULL,
    [ItemDataType]     VARCHAR (100)  NULL,
    [ItemAccessRights] VARCHAR (50)   NULL,
    PRIMARY KEY CLUSTERED ([ItemID] ASC)
);

