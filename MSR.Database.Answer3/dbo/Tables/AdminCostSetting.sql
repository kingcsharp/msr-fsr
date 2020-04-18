CREATE TABLE [dbo].[AdminCostSetting] (
    [Id]              INT             NOT NULL,
    [RMAnnualRate]    DECIMAL (18, 2) NOT NULL,
    [LaborRateMinute] DECIMAL (18, 2) NOT NULL,
    [YearsHours]      INT             NOT NULL,
    [HourMinutes]     INT             NOT NULL
);

