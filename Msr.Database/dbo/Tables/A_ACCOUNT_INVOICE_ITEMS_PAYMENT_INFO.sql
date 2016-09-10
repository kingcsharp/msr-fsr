CREATE TABLE [dbo].[A_ACCOUNT_INVOICE_ITEMS_PAYMENT_INFO] (
    [ID]              VARCHAR (50) NULL,
    [INVOICE_ITEM_ID] VARCHAR (50) NULL,
    [PAYMENT_METHOD]  VARCHAR (50) NULL,
    [CHECK_NO]        VARCHAR (50) NULL,
    [EXP_MO]          INT          NULL,
    [EXP_YEAR]        INT          NULL,
    [RECORDED_BY]     VARCHAR (50) NULL,
    [CC_NO]           VARCHAR (50) NULL,
    [AMOUNT_SPENT]    MONEY        NULL
);

