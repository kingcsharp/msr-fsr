CREATE TABLE [dbo].[A_TIME_ZONES] (
    [ID]          NVARCHAR (50)  NOT NULL,
    [DESCRIPTION] NVARCHAR (100) NULL,
    [G_DIFF]      REAL           NULL,
    [DRCM]        DATETIME       NULL,
    [MODBY]       NVARCHAR (50)  NULL,
    [NUM]         INT            NULL,
    [DS]          SMALLINT       NULL,
    CONSTRAINT [PK_A_TIME_ZONES] PRIMARY KEY CLUSTERED ([ID] ASC)
);

