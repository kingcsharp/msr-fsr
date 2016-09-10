CREATE TABLE [dbo].[A_MESSAGES] (
    [ID]            VARCHAR (50)    NOT NULL,
    [MESSAGE]       NVARCHAR (4000) NULL,
    [SENDER]        VARCHAR (50)    NOT NULL,
    [PARENT_ID]     VARCHAR (50)    NULL,
    [IMPORTANCE]    VARCHAR (50)    NULL,
    [DATE_CREATED]  DATETIME        NULL,
    [DRCM]          DATETIME        NULL,
    [MODBY]         VARCHAR (50)    NULL,
    [TO_COUNT]      REAL            NULL,
    [TO_READ_COUNT] REAL            NULL,
    [CC_COUNT]      REAL            NULL,
    [CC_READ_COUNT] REAL            NULL,
    [STATUS]        VARCHAR (50)    NULL,
    [HIDE_MESSAGE]  SMALLINT        NULL,
    [DATE_SENT]     DATETIME        NULL,
    [NOTIFY]        INT             NULL,
    [FUTURE_DATE]   DATETIME        NULL,
    CONSTRAINT [PK_A_MESSAGES] PRIMARY KEY CLUSTERED ([ID] ASC)
);

