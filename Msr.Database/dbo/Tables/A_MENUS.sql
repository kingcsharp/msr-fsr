CREATE TABLE [dbo].[A_MENUS] (
    [ID]         NVARCHAR (50)  NOT NULL,
    [URL]        NVARCHAR (255) NULL,
    [DRCM]       NVARCHAR (50)  NULL,
    [MODBY]      NVARCHAR (50)  NULL,
    [NUM]        INT            NULL,
    [NAME]       NVARCHAR (50)  NULL,
    [INFO]       NVARCHAR (255) NULL,
    [MENU_GROUP] NVARCHAR (50)  NULL,
	[Icon] NVARCHAR (255)  NULL,
	[GroupIcon] NVARCHAR (255)  NULL,
	[OrderNumber] int  NOT NULL,
	[IsParent] BIT  NOT NULL DEFAULT (0),

    CONSTRAINT [PK_A_MENUS] PRIMARY KEY CLUSTERED ([ID] ASC)
);

