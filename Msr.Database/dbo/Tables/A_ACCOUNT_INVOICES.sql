CREATE TABLE [dbo].[A_ACCOUNT_INVOICES] (
    [ID]                    VARCHAR (50)   NOT NULL,
    [ACCOUNT_ID]            VARCHAR (50)   NOT NULL,
    [INVOICE_DATE]          DATETIME       NULL,
    [DRCM]                  DATETIME       NULL,
    [MODBY]                 VARCHAR (50)   NULL,
    [STATUS]                VARCHAR (50)   NULL,
    [AMT_PAID]              MONEY          NULL,
    [NEW_ITEMS_AMT]         MONEY          NULL,
    [PREVIOUS_BALANCE]      MONEY          NULL,
    [PAYMENT_AMOUNT]        MONEY          NULL,
    [DISPUTED_AMOUNT]       MONEY          NULL,
    [TOTAL_DUE]             MONEY          NULL,
    [DUE_DATE]              DATETIME       NULL,
    [LATE_FEES]             MONEY          NULL,
    [DATE_SENT_TO_CUSTOMER] DATETIME       NULL,
    [PURCHASE_ID]           VARCHAR (50)   NULL,
    [NAME]                  VARCHAR (1000) NULL,
    [CREATE_DATE]           DATETIME       NULL,
    [INVOICE_TYPE]          VARCHAR (50)   NULL,
    [INVOICE_BALANCE]       MONEY          CONSTRAINT [DF_A_ACCOUNT_INVOICES_INVOICE_BALANCE] DEFAULT ((0)) NULL,
    [TOTAL_TAX]             MONEY          NULL,
    [PO_NUMBER]             VARCHAR (1000) NULL,
    CONSTRAINT [PK_A_ACCOUNT_INVOICES] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO






CREATE     TRIGGER A_ACCOUNT_INVOICES_INSERT_UPDATE
ON dbo.A_ACCOUNT_INVOICES
AFTER INSERT, UPDATE
AS
declare @ID as varchar(50),@invDate datetime,@createDate datetime,@purchaseID varchar(50),@invType varchar(50)
declare @NAME as nvarchar(2000),@AMT_New money,@AMT_Paid money
SELECT @ID = ID,
	@NAME = NAME,
	@invDate = INVOICE_DATE,
	@createDate = CREATE_DATE,
	@purchaseID = PURCHASE_ID,
	@invType = INVOICE_TYPE,
	@AMT_New = NEW_ITEMS_AMT,
	@AMT_Paid = AMT_PAID
FROM INSERTED

if @createDate is null
	begin
	set @createDate = getDate()
	UPDATE A_ACCOUNT_INVOICES SET CREATE_DATE = @createDate WHERE ID = @ID
	end

if @NAME is null
	begin
	SELECT @NAME = 'Invoice #' + @ID + ' Created ' + convert(varchar(50),@createDate,1) + isNull(' Invoices ' + convert(nvarchar(50),@invDate,1),'')
	UPDATE A_ACCOUNT_INVOICES SET NAME = @NAME WHERE ID = @ID
	end








