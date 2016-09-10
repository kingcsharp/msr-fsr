CREATE TABLE [dbo].[A_PARTS] (
    [ID]               VARCHAR (50)  NOT NULL,
    [DRCM]             SMALLDATETIME NULL,
    [MODBY]            VARCHAR (50)  NULL,
    [PARTS_HISTORY_ID] VARCHAR (50)  NULL,
    [CO]               VARCHAR (50)  NULL,
    [IS_BATCH]         TINYINT       NULL,
    [STATUS]           VARCHAR (50)  NULL,
    [CREATING_CO]      VARCHAR (50)  NULL,
    CONSTRAINT [PK_A_PARTS] PRIMARY KEY CLUSTERED ([ID] ASC)
);

