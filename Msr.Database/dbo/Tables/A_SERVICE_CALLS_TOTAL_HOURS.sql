CREATE TABLE [dbo].[A_SERVICE_CALLS_TOTAL_HOURS] (
    [ID]           VARCHAR (50) NOT NULL,
    [WEEKLY_ID]    VARCHAR (50) NULL,
    [NORMAL_HOURS] REAL         NULL,
    [OT_HOURS]     REAL         NULL,
    [TOTAL_HOURS]  REAL         NULL,
    [NT_0]         REAL         NULL,
    [NT_1]         REAL         NULL,
    [NT_2]         REAL         NULL,
    [NT_3]         REAL         NULL,
    [NT_4]         REAL         NULL,
    [NT_5]         REAL         NULL,
    [NT_6]         REAL         NULL,
    [OT_0]         REAL         NULL,
    [OT_1]         REAL         NULL,
    [OT_2]         REAL         NULL,
    [OT_3]         REAL         NULL,
    [OT_4]         REAL         NULL,
    [OT_5]         REAL         NULL,
    [OT_6]         REAL         NULL,
    [DRCM]         DATETIME     NULL,
    [MODBY]        VARCHAR (50) NULL,
    CONSTRAINT [PK_A_SERVICE_CALLS_TOTAL_HOURS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_A_SERVICE_CALLS_TOTAL_HOURS_A_SERVICE_CALLS_WEEKLY_REPORTS] FOREIGN KEY ([WEEKLY_ID]) REFERENCES [dbo].[A_SERVICE_CALLS_WEEKLY_REPORTS] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


GO
ALTER TABLE [dbo].[A_SERVICE_CALLS_TOTAL_HOURS] NOCHECK CONSTRAINT [FK_A_SERVICE_CALLS_TOTAL_HOURS_A_SERVICE_CALLS_WEEKLY_REPORTS];

