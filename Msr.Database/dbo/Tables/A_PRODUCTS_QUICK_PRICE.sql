CREATE TABLE [dbo].[A_PRODUCTS_QUICK_PRICE] (
    [PROD_HIST_ID]           VARCHAR (50)  NOT NULL,
    [CUST_ID]                VARCHAR (50)  NULL,
    [PRICE]                  FLOAT (53)    NULL,
    [DRCM]                   DATETIME      NULL,
    [MODBY]                  VARCHAR (50)  NULL,
    [CREATE_PRICE_LIST]      TINYINT       NULL,
    [PROD_TIME]              FLOAT (53)    NULL,
    [PROD_TIME_UNIT]         VARCHAR (50)  NULL,
    [CAPACITY]               FLOAT (53)    NULL,
    [CAPACITY_UNITS]         VARCHAR (50)  NULL,
    [PRICE_LIST_MADE_OBJ_ID] VARCHAR (50)  NULL,
    [ORDER_ID]               VARCHAR (50)  NULL,
    [QUOTE_ID]               VARCHAR (50)  NULL,
    [ID]                     VARCHAR (50)  NOT NULL,
    [IS_KIT]                 TINYINT       NULL,
    [KIT_ID]                 VARCHAR (200) NULL,
    [KIT_QTY]                FLOAT (53)    NULL,
    CONSTRAINT [PK_A_PRODUCTS_QUICK_PRICE] PRIMARY KEY CLUSTERED ([ID] ASC)
);

