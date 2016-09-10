CREATE TABLE [dbo].[A_ADMIN_EMAIL_ERROR_LOG] (
    [ID]               VARCHAR (50)    NULL,
    [TO_EMAIL]         NVARCHAR (200)  NULL,
    [FROM_EMAIL]       NVARCHAR (200)  NULL,
    [SUBJ]             NVARCHAR (1000) NULL,
    [BODY]             NVARCHAR (4000) NULL,
    [TO_ID]            VARCHAR (50)    NULL,
    [FILE_MAILED_FROM] VARCHAR (100)   NULL,
    [DRCM]             DATETIME        NULL,
    [MODBY]            VARCHAR (50)    NULL
);

