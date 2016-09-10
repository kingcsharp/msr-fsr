CREATE TABLE [dbo].[A_MEETINGS] (
    [ID]              VARCHAR (50)    NOT NULL,
    [MEETING_NAME]    NVARCHAR (500)  NULL,
    [COMMENT]         NVARCHAR (2000) NULL,
    [START_DATE]      DATETIME        NULL,
    [STOP_DATE]       DATETIME        NULL,
    [OWNER]           VARCHAR (50)    NULL,
    [HOST]            VARCHAR (50)    NULL,
    [TIME_KEEP]       VARCHAR (50)    NULL,
    [SCRIBE]          VARCHAR (50)    NULL,
    [LOCATION]        VARCHAR (50)    NULL,
    [WEB_LOCATION]    NVARCHAR (500)  NULL,
    [DATE_CREATED]    DATETIME        NULL,
    [STATUS]          VARCHAR (50)    NULL,
    [DRCM]            DATETIME        NULL,
    [MODBY]           VARCHAR (50)    NULL,
    [SETTING]         VARCHAR (50)    NULL,
    [DATE_EMAIL_SENT] DATETIME        NULL,
    [STYLE]           VARCHAR (10)    NULL,
    CONSTRAINT [PK_A_MEETINGS] PRIMARY KEY CLUSTERED ([ID] ASC)
);

