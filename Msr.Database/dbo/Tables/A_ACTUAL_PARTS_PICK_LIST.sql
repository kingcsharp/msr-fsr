CREATE TABLE [dbo].[A_ACTUAL_PARTS_PICK_LIST] (
    [ID]                    VARCHAR (50) NULL,
    [PARENT_ID]             VARCHAR (50) NULL,
    [CHILD_ID]              VARCHAR (50) NULL,
    [CHILD_LOCATION]        VARCHAR (50) NULL,
    [CHILD_QTY]             FLOAT (53)   NULL,
    [DATE_PICKED]           DATETIME     NULL,
    [PICK_VERIFIED]         TINYINT      NULL,
    [VERIFIED_BY]           VARCHAR (50) NULL,
    [IS_BRAND_NEW]          TINYINT      NULL,
    [MERGED_INTO]           VARCHAR (50) NULL,
    [MERGED_INTO_NICK_NAME] VARCHAR (50) NULL
);

