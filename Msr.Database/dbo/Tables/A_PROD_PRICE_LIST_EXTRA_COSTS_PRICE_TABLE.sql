CREATE TABLE [dbo].[A_PROD_PRICE_LIST_EXTRA_COSTS_PRICE_TABLE] (
    [ID]                    VARCHAR (50) NOT NULL,
    [MIN_NUM]               FLOAT (53)   NULL,
    [MAX_NUM]               FLOAT (53)   NULL,
    [PER_UNIT_COST]         MONEY        NULL,
    [FLAT_RATE]             MONEY        NULL,
    [PER_UNIT_APPLIES_OVER] FLOAT (53)   NULL,
    [DRCM]                  DATETIME     NULL,
    [MODBY]                 VARCHAR (50) NULL,
    [PPLEC_ID]              VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_A_PROD_PRICE_LIST_EXTRA_COSTS_PRICE_TABLE] PRIMARY KEY CLUSTERED ([ID] ASC)
);

