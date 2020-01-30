-- Note this table is in a separate database on production
-- USE [EquipmentMonitoring]

-- used by Sensor_Current_Values

CREATE TABLE [dbo].[Hillsboro_UpdateTable](
	[ItemID] [varchar](255) NOT NULL,
	[ItemCurrentValue] [varchar](2000) NULL,
	[ItemTimeStamp] [datetime] NULL,
	[ItemQuality] [varchar](255) NULL,
	[ServerProgId] [varchar](100) NULL,
	[ServerAddress] [varchar](100) NULL,
	[GroupName] [varchar](100) NULL,
	[ReadMode] [varchar](50) NULL,
	[ItemDataType] [varchar](100) NULL,
	[ItemAccessRights] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

