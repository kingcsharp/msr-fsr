CREATE TABLE [dbo].[A_PROD_PRICE_LIST_HISTORY] (
    [ID]                       VARCHAR (50)   NOT NULL,
    [PRODUCT]                  VARCHAR (50)   NOT NULL,
    [CUSTOMER]                 VARCHAR (50)   NULL,
    [UNIT]                     VARCHAR (50)   NULL,
    [MIN_QUANTITY]             NUMERIC (18)   NULL,
    [UNIT_PRICE]               REAL           NULL,
    [EST_UNIT_PRICE]           MONEY          NULL,
    [EST_LABOR_PRICE]          MONEY          NULL,
    [EST_PARTS_PROV_STAY]      MONEY          NULL,
    [EST_PARTS_PROV_TAKE_BACK] MONEY          NULL,
    [EST_PARTS_CONSUMED]       MONEY          NULL,
    [INVOICE_FROM]             VARCHAR (50)   NULL,
    [PRODUCTION_TIME]          FLOAT (53)     NULL,
    [PRODUCTION_TIME_UNIT]     VARCHAR (50)   NULL,
    [CAPACITY]                 FLOAT (53)     NULL,
    [CAPACITY_UNIT]            VARCHAR (50)   NULL,
    [DRCM]                     DATETIME       NULL,
    [MODBY]                    VARCHAR (50)   NULL,
    [OBJECT_ID]                VARCHAR (50)   NULL,
    [PRODUCT_NAME]             NVARCHAR (200) NULL,
    [CUSTOMER_NAME]            NVARCHAR (200) NULL,
    [FOR_INDIVIDUAL_SALE]      TINYINT        NULL,
    CONSTRAINT [PK_A_PROD_PRICE_LIST_HISTORY] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO

CREATE          TRIGGER A_PROD_PRICE_LIST_HISTORY_INSERT
ON dbo.A_PROD_PRICE_LIST_HISTORY
AFTER INSERT
AS
declare @ID as nvarchar(50)
declare @PRODUCT as nvarchar(50)
declare @CUSTOMER as nvarchar(50)
declare @MODBY as nvarchar(50)
SELECT @ID = ID,
	@MODBY = MODBY,
	@PRODUCT = PRODUCT,
	@CUSTOMER = CUSTOMER
FROM INSERTED

declare @objName as nvarchar(100)
SELECT @objName = isnull(NAME,'') FROM A_V_PRODUCTS_APPROVED_DATA WHERE ID = @PRODUCT
SELECT @objName = @objName + ' price list for ' + isNull(NAME,'') FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @CUSTOMER


exec A_SP_OBJECT_ADD 'A_PROD_PRICE_LIST_HISTORY',@ID,@objName,@MODBY,null,null,null


GO


CREATE            TRIGGER A_PROD_PRICE_LIST_HISTORY_UPDATE
ON dbo.A_PROD_PRICE_LIST_HISTORY
AFTER UPDATE
AS
declare @ID as nvarchar(50)
declare @PRODUCT as nvarchar(50)
declare @CUSTOMER as nvarchar(50)
declare @MODBY as nvarchar(50)
declare @OBJ_ID as nvarchar(50)
SELECT @ID = ID,
	@MODBY = MODBY,
	@PRODUCT = PRODUCT,
	@CUSTOMER = CUSTOMER,
	@OBJ_ID = OBJECT_ID
FROM INSERTED

if update(PRODUCT) OR UPDATE(CUSTOMER)
	begin
	declare @objName as nvarchar(100)
	SELECT @objName = isnull(NAME,'') FROM A_V_PRODUCTS_APPROVED_DATA WHERE ID = @PRODUCT
	SELECT @objName = @objName + ' price list for ' + isNull(NAME,'') FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @CUSTOMER
	UPDATE A_OBJECTS SET OBJ_DESC = @objName WHERE ID = @OBJ_ID
	end

if update(PRODUCT)
	begin
	declare @prodName as nvarchar(200)
	SELECT @prodName = NAME FROM A_V_PRODUCTS_APPROVED_DATA WHERE ID = @PRODUCT
	UPDATE A_PROD_PRICE_LIST_HISTORY 
	SET PRODUCT_NAME = @prodName
	WHERE ID = @ID
	end
if update(CUSTOMER)
	begin
	declare @custName as nvarchar(200)
	SELECT @custName = NAME FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @CUSTOMER
	UPDATE A_PROD_PRICE_LIST_HISTORY 
	SET CUSTOMER_NAME = @custName
	WHERE ID = @ID
	end


