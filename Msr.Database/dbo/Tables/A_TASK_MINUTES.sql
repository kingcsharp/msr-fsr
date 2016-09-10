CREATE TABLE [dbo].[A_TASK_MINUTES] (
    [ID]              VARCHAR (50) NOT NULL,
    [TASK_ID]         VARCHAR (50) NOT NULL,
    [WORKER_ID]       VARCHAR (50) NOT NULL,
    [ACTUAL_MINUTES]  INT          NOT NULL,
    [PLANNED_MINUTES] INT          NULL,
    [MANUAL]          TINYINT      NULL,
    [DRCM]            DATETIME     NULL,
    [MODBY]           VARCHAR (50) NULL,
    CONSTRAINT [PK_A_TASK_MINUTES] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'1 = manually recorded 0 or null = automatically done', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'A_TASK_MINUTES', @level2type = N'COLUMN', @level2name = N'MANUAL';

