CREATE TABLE [dbo].[A_MONITOR_TEMPLATES] (
    [ID]                         VARCHAR (50)   NOT NULL,
    [MONITOR_TYPE]               VARCHAR (50)   NULL,
    [Input_type]                 VARCHAR (50)   NULL,
    [LIST_SOURCE]                VARCHAR (50)   NULL,
    [DESCRIPTION]                VARCHAR (2000) NULL,
    [START_SYSTEM_TASK]          VARCHAR (50)   NULL,
    [START_TYPE]                 VARCHAR (50)   NULL,
    [STOP_SYSTEM_TASK]           VARCHAR (50)   NULL,
    [STOP_TYPE]                  VARCHAR (50)   NULL,
    [COUNTER_OR_CLOCK]           VARCHAR (50)   NULL,
    [CLOCK_UNIT]                 VARCHAR (50)   NULL,
    [HIGHEST_THRESHOLD]          REAL           NULL,
    [HIGH_THRESHOLD]             REAL           NULL,
    [TARGET]                     REAL           NULL,
    [LOW_THRESHOLD]              REAL           NULL,
    [LOWEST_THRESHOLD]           REAL           NULL,
    [SHOULD_BE]                  VARCHAR (50)   NULL,
    [OPINION]                    SMALLINT       NULL,
    [DRCM]                       DATETIME       NULL,
    [MODBY]                      VARCHAR (50)   NULL,
    [RELATED_OBJECT_TYPE]        VARCHAR (50)   NULL,
    [RELATED_OBJECT_DESCRIPTION] NVARCHAR (200) NULL,
    [TARGET_ANSWER_ID]           VARCHAR (50)   NULL,
    [HIDE_TARGET]                SMALLINT       NULL,
    [PROCEDURE_ID]               VARCHAR (50)   NULL,
    [STEP_ID]                    VARCHAR (50)   NULL,
    [PEOPLE_ID]                  VARCHAR (50)   NULL,
    [PART_ID]                    VARCHAR (50)   NULL,
    [USE_RESULT]                 SMALLINT       NULL,
    [FAIL_STOP]                  SMALLINT       NULL,
    [YES_NO_ANSWER]              SMALLINT       NULL,
    [CORRECT_ANSWER_ID]          VARCHAR (50)   NULL,
    [TEXT_TARGET]                NVARCHAR (50)  NULL,
    [TASK_ID]                    VARCHAR (50)   NULL,
    [ROLL_UP_ID]                 VARCHAR (50)   NULL,
    [CREATED_BY]                 VARCHAR (50)   NULL,
    [IS_PASSING]                 TINYINT        NULL,
    [OBJECT_ID]                  VARCHAR (50)   NULL,
    [IS_AUTO]                    TINYINT        NULL,
    [TOLERANCE]                  FLOAT (53)     NULL,
    [MY_ANSWER]                  VARCHAR (1000) NULL,
    [RELATED_OBJECT_ID]          VARCHAR (50)   NULL,
    [FAIL_ACTION]                VARCHAR (50)   NULL,
    [TARGET_OBJECT_TYPE]         VARCHAR (50)   NULL,
    [TARGET_OBJECT]              VARCHAR (50)   NULL,
    [PRINT_ORDER]                FLOAT (53)     NULL,
    [SKIP_MODE]                  VARCHAR (10)   NULL,
    [ALWAYS_PASS]                TINYINT        NULL,
    [CANT_CHANGE]                TINYINT        NULL,
    [SENSOR_MAPPING_ID]          INT            NULL,
    CONSTRAINT [PK_A_KPI_TEMPLATES] PRIMARY KEY CLUSTERED ([ID] ASC)
);








GO
CREATE NONCLUSTERED INDEX [IX_A_MONITOR_TEMPLATES_EXECPLAN_01]
    ON [dbo].[A_MONITOR_TEMPLATES]([TASK_ID] ASC);

