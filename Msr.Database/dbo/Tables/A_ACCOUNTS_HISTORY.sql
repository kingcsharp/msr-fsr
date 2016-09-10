CREATE TABLE [dbo].[A_ACCOUNTS_HISTORY] (
    [ID]                     VARCHAR (50)   NOT NULL,
    [OBJECT_ID]              VARCHAR (50)   NULL,
    [NAME]                   VARCHAR (2000) NULL,
    [REFERENCE_PO]           VARCHAR (50)   NULL,
    [REFERENCE_NAME]         VARCHAR (4000) NULL,
    [OPEN_DATE]              DATETIME       NULL,
    [CLOSE_DATE]             DATETIME       NULL,
    [SUPPLIER_CO]            VARCHAR (50)   NULL,
    [CUSTOMER_CO]            VARCHAR (50)   NULL,
    [CUSTOMER_BILL_CO]       VARCHAR (50)   NULL,
    [MAXIMUM_USES]           INT            NULL,
    [TOTAL_PURCHASE_LIMIT]   MONEY          NULL,
    [CREDIT_LIMIT]           MONEY          NULL,
    [APPROVAL_WF]            VARCHAR (50)   NULL,
    [INVOICE_TRIGGER]        VARCHAR (50)   NULL,
    [INVOICE_PERIOD_NUMBER]  VARCHAR (50)   NULL,
    [INVOICE_PERIOD_TYPE]    VARCHAR (50)   NULL,
    [FIRST_INVOICE_DATE]     DATETIME       NULL,
    [NEXT_INVOICE_DATE]      DATETIME       NULL,
    [PAYMENT_GRACE_PERIOD]   INT            NULL,
    [LATE_FEE_PERCENTAGE]    REAL           NULL,
    [REAPPLY_LATE_FEE]       INT            NULL,
    [DRCM]                   DATETIME       NULL,
    [MODBY]                  VARCHAR (50)   NULL,
    [ACCT_TYPE]              VARCHAR (50)   NULL,
    [TOTAL_PURCHASES]        MONEY          NULL,
    [BALANCE]                MONEY          CONSTRAINT [DF_A_ACCOUNTS_HISTORY_BALANCE] DEFAULT ((0)) NULL,
    [AMT_INVOICED]           MONEY          NULL,
    [ACCT_STATUS]            VARCHAR (50)   NULL,
    [PRODUCT_ID]             VARCHAR (50)   NULL,
    [PARENT_ACCOUNT]         VARCHAR (50)   NULL,
    [LABOR_INCLUDED]         SMALLINT       NULL,
    [CONSUMABLES_INCLUDED]   SMALLINT       NULL,
    [NONCONSUMABLE_INCLUDED] SMALLINT       NULL,
    [HAS_CHILD]              TINYINT        NULL,
    [INVOICED_BALANCE]       MONEY          CONSTRAINT [DF_A_ACCOUNTS_HISTORY_INVOICED_BALANCE] DEFAULT ((0)) NULL,
    [UNINVOICED_BALANCE]     MONEY          CONSTRAINT [DF_A_ACCOUNTS_HISTORY_UNINVOICED_BALANCE] DEFAULT ((0)) NULL,
    [TOTAL_CREDITS]          MONEY          CONSTRAINT [DF_A_ACCOUNTS_HISTORY_TOTAL_CREDITS] DEFAULT ((0)) NULL,
    [TOTAL_DEBITS]           MONEY          CONSTRAINT [DF_A_ACCOUNTS_HISTORY_TOTAL_DEBITS] DEFAULT ((0)) NULL,
    [BILLING_EMAIL]          VARCHAR (500)  NULL,
    [TAX_RATE]               FLOAT (53)     NULL,
    [TOTAL_TAX]              MONEY          NULL,
    CONSTRAINT [PK_A_ACCOUNTS_HISTORY] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO

CREATE           TRIGGER A_ACCOUNTS_HISTORY_INSERT
ON dbo.A_ACCOUNTS_HISTORY
AFTER INSERT
AS
declare @ID as nvarchar(50)
declare @NAME as nvarchar(2000)
declare @MODBY as nvarchar(50)
SELECT @ID = ID,
	@MODBY = MODBY,
	@NAME = NAME FROM INSERTED
exec A_SP_OBJECT_ADD 'A_ACCOUNTS_HISTORY',@ID,@NAME,@MODBY,null,null,null


GO

CREATE     TRIGGER A_ACCOUNTS_HISTORY_UPDATE
ON dbo.A_ACCOUNTS_HISTORY
AFTER UPDATE
AS
declare @OBJ_ID as nvarchar(50)
declare @ID as nvarchar(50)
declare @NAME as nvarchar(4000)
declare @MODBY as nvarchar(50)
print 'In the Accounts History Update Trigger'
SELECT
@ID = ID,
@OBJ_ID = OBJECT_ID,
@NAME = NAME,
@MODBY = MODBY 
FROM INSERTED

declare @myObjName as nvarchar(2000)
SELECT @myObjName = OBJ_DESC FROM A_OBJECTS WHERE ID = @OBJ_ID
if isNull(@myObjName,'') <> isNull(@NAME,'')
	begin
	UPDATE A_OBJECTS SET
	OBJ_DESC = @NAME,
	MODBY = @MODBY,
	DRCM = getDATE()
	WHERE ID = @OBJ_ID
	end

