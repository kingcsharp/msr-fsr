CREATE TABLE [dbo].[A_PROJECTS] (
    [ID]                         VARCHAR (50)    NOT NULL,
    [DATE_CREATED]               DATETIME        NULL,
    [INITIATOR]                  VARCHAR (50)    NULL,
    [NAME]                       NVARCHAR (500)  NULL,
    [PRIORITY_LEVEL]             SMALLINT        NULL,
    [OBJECTIVE]                  NVARCHAR (1000) NULL,
    [LEADER]                     VARCHAR (50)    NULL,
    [SECURITY_LEVEL]             VARCHAR (50)    NULL,
    [ORIGINAL_PLANNED_STOP_DATE] DATETIME        NULL,
    [CURRENT_PLANNED_STOP_DATE]  DATETIME        NULL,
    [ACTUAL_STOP_DATE]           DATETIME        NULL,
    [MONTH_GOALS]                NVARCHAR (1000) NULL,
    [ISSUES]                     NVARCHAR (1000) NULL,
    [SUMMARY]                    NVARCHAR (1000) NULL,
    [STATUS]                     VARCHAR (50)    NULL,
    [DRCM]                       DATETIME        NULL,
    [MODBY]                      VARCHAR (50)    NULL,
    [DISCUSSION_ID]              VARCHAR (50)    NULL,
    CONSTRAINT [PK_A_PROJECTS] PRIMARY KEY CLUSTERED ([ID] ASC)
);

