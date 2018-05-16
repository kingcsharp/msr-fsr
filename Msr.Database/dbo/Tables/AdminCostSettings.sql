CREATE TABLE [dbo].[Portal_AdminCostSetting]
(
	[Id]              INT             IDENTITY (1, 1) NOT NULL,
    [RMAnnualRate]    DECIMAL (18, 2) NOT NULL,
    [LaborRateMinute] DECIMAL (18, 2) NOT NULL,
    [YearsHours]      INT             NOT NULL,
    [HourMinutes]     INT             NOT NULL,
    CONSTRAINT [PK_Portal_AdminCostSetting] PRIMARY KEY CLUSTERED ([Id] ASC)
)
