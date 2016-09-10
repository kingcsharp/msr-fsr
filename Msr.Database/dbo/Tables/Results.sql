CREATE TABLE [dbo].[Results] (
    [ID]        NVARCHAR (50) NOT NULL,
    [NAME]      NVARCHAR (50) NOT NULL,
    [SOURCE]    NVARCHAR (50) NULL,
    [HELP_DESK] NVARCHAR (50) NULL,
    [TYPE]      NVARCHAR (50) NULL,
    [WRITER]    NVARCHAR (50) NULL,
    [HIDDEN]    BIT           NOT NULL,
    [DRCM]      DATETIME      NULL,
    [MODBY]     NVARCHAR (50) NULL
);

