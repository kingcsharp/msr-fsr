CREATE TABLE [dbo].[A_MEETING_AGENDA_ITEMS] (
    [ID]             VARCHAR (50)    NOT NULL,
    [TEXT]           NVARCHAR (4000) NULL,
    [FACILITATOR]    VARCHAR (50)    NULL,
    [START_DATE]     DATETIME        NULL,
    [DURATION]       INT             NULL,
    [ITEM]           REAL            NULL,
    [ROOT]           VARCHAR (50)    NOT NULL,
    [PARENT]         VARCHAR (50)    NULL,
    [AUTHOR]         VARCHAR (50)    NULL,
    [MODBY]          VARCHAR (50)    NULL,
    [DRCM]           DATETIME        NULL,
    [KEY_STATEMENTS] VARCHAR (4000)  NULL,
    [RELATED_ITEM]   VARCHAR (50)    NULL,
    CONSTRAINT [PK_A_MEETING_AGENDA_ITEMS] PRIMARY KEY CLUSTERED ([ID] ASC)
);

