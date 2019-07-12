CREATE TABLE [dbo].[A_ORDER_ITEMS] (
    [ID]                              VARCHAR (50)    NOT NULL,
    [ORDER_ID]                        VARCHAR (50)    NULL,
    [PRODUCT_ID]                      VARCHAR (50)    NULL,
    [QTY]                             FLOAT (53)      NULL,
    [RECURRING]                       TINYINT         NULL,
    [RECUR_PERIOD]                    VARCHAR (500)   NULL,
    [RECUR_COUNT]                     INT             NULL,
    [RECUR_START_DATE]                DATETIME        NULL,
    [RECUR_STOP_DATE]                 DATETIME        NULL,
    [RECUR_ACCOUNT]                   VARCHAR (50)    NULL,
    [RECUR_AUTO_FILL]                 TINYINT         NULL,
    [SPECIAL_DISCOUNT]                FLOAT (53)      NULL,
    [SPECIAL_DISC_REASON]             NVARCHAR (500)  NULL,
    [UNIT_PRICE]                      MONEY           NULL,
    [UNIT_ESTIMATE]                   MONEY           NULL,
    [EXPEDITE_PRODUCTION]             FLOAT (53)      NULL,
    [EXPEDITE_REASON]                 NVARCHAR (500)  NULL,
    [COMMENTS]                        NVARCHAR (50)   NULL,
    [DRCM]                            DATETIME        NULL,
    [MODBY]                           VARCHAR (50)    NULL,
    [PARENT]                          VARCHAR (50)    NULL,
    [PROD_PRICE_LIST]                 VARCHAR (50)    NULL,
    [QUOTE_ID]                        VARCHAR (50)    NULL,
    [TOTAL_QTY]                       REAL            NULL,
    [TOTAL_PRICE]                     REAL            NULL,
    [PARENT_QTY]                      REAL            NULL,
    [PROC_SYS_ID]                     VARCHAR (50)    NULL,
    [DEST]                            VARCHAR (50)    NULL,
    [FROM_LOC]                        VARCHAR (50)    NULL,
    [TO_LOC]                          VARCHAR (50)    NULL,
    [ADD_COST_ID]                     VARCHAR (50)    NULL,
    [FLAT_RATE]                       MONEY           NULL,
    [EX_DESC]                         NVARCHAR (1000) NULL,
    [EST_WEIGHT]                      FLOAT (53)      NULL,
    [EST_WEIGHT_UNIT]                 VARCHAR (50)    NULL,
    [PPL_HIST_ID]                     VARCHAR (50)    NULL,
    [SOURCE_ID]                       VARCHAR (50)    NULL,
    [PURCHASE_HIST_ID]                VARCHAR (50)    NULL,
    [ACCOUNT_ID]                      VARCHAR (50)    NULL,
    [BILL_TYPE]                       VARCHAR (10)    NULL,
    [QTY_FILLED]                      FLOAT (53)      NULL,
    [QTY_NEEDS_FILLING]               FLOAT (53)      NULL,
    [DUE_DATE]                        DATETIME        NULL,
    [ORIG_DUE_DATE]                   DATETIME        NULL,
    [ACT_DUE_DATE]                    DATETIME        NULL,
    [PROD_TIME]                       FLOAT (53)      NULL,
    [PROD_TIME_UNIT]                  VARCHAR (50)    NULL,
    [CUST_LINE_ITEM]                  VARCHAR (10)    NULL,
    [MT_NUM]                          VARCHAR (50)    NULL,
    [SHIP_DATE]                       DATETIME        NULL,
    [SUPPLIER_ID]                     VARCHAR (50)    NULL,
    [PROD_PRICE_LIST_HIST_ID]         VARCHAR (50)    NULL,
    [GroupWO]                         BIT             DEFAULT ((0)) NOT NULL,
    [MATERIAL_TRANSFER_TICKET_NUMBER] VARCHAR (50)    NULL,
    CONSTRAINT [PK_A_ORDER_ITEMS] PRIMARY KEY CLUSTERED ([ID] ASC)
);






GO




CREATE          TRIGGER [dbo].[A_ORDER_ITEM_INSERT_UPDATE]
ON [dbo].[A_ORDER_ITEMS]
AFTER UPDATE, INSERT
AS
print 'In the trigger A_ORDER_ITEM_INSERT_UPDATE'
declare @productID varchar(50),@apObj varchar(50),
	@wt float, @wtType varchar(50),
	@apObjTable varchar(50),@ID varchar(50),
	@qty float, @purchHistID varchar(50),
	@prodID varchar(50),@modby varchar(50),
	@prodHistID varchar(50),@procHistID varchar(50),
	@prodListPrice varchar(50),@cline varchar(50)
SELECT 
	@ID = ID,
	@purchHistID = PURCHASE_HIST_ID,
	@prodID = PRODUCT_ID,
	@modby = MODBY,
	@prodListPrice = PROD_PRICE_LIST,
	@purchHistID = PURCHASE_HIST_ID,
	@qty = QTY,
	@cline = CUST_LINE_ITEM
	

FROM INSERTED

exec A_SP_ORDER_ITEM_UPDATE_MY_BILL_TYPE @ID
print 'Back'
if update(CUST_LINE_ITEM)
	UPDATE A_ORDER_ITEMS SET CUST_LINE_ITEM = @cline where PARENT = @ID
if @purchHistID is not null
	exec A_SP_ORDER_ITEM_UPDATE_QTYS @ID

if update(PRODUCT_ID) or update(QTY) and
	(not exists(SELECT * FROM DELETED WHERE PRODUCT_ID = @prodID and QTY = @qty))
	begin
 	print 'The product has been changed so we need to figure out what the ap obj is'
	SELECT @productID = PRODUCT_ID,@qty = QTY,@ID = ID FROM INSERTED
	print 'The product ID is ' + @productID
	SELECT @apObj = APP_OBJECT FROM A_V_PRODUCTS_APPROVED_DATA WHERE ID = @productID
	print 'The ap Obj is ' + isNull(@apObj,'NULL')
	SELECT @apObjTable = OBJ_TABLE FROM A_V_APPROVED_OBJECTS WHERE ID = @apObj
	if @apObjTable = 'A_PARTS_HISTORY'
		begin
		print 'It is a partso get the weight'
		SELECT @wt = UNIT_SHIPPING_WEIGHT,@wtType = WEIGHT_TYPE FROM A_V_PARTS_APPROVED_DATA WHERE ID = @apObj
		end 
	UPDATE A_ORDER_ITEMS 
		SET EST_WEIGHT = @qty * @wt,
			EST_WEIGHT_UNIT = @wtType
			WHERE ID = @ID
	end

if update(PROD_PRICE_LIST)
	begin
	if @prodListPrice is not null
		begin
		declare @lt float,@ltu varchar(50)
		SELECT @lt = PRODUCTION_TIME, @ltu = PRODUCTION_TIME_UNIT FROM A_V_PROD_PRICE_LIST_APPROVED_DATA
			WHERE ID = @prodListPrice
		UPDATE A_ORDER_ITEMS SET PROD_TIME = @lt, PROD_TIME_UNIT = @ltu WHERE ID = @ID
		end

	end

if update(QTY)
	begin
	declare @oldQty float,@newQty Float,@mult Float,@pID varchar(50),@pMod varchar(50)
	SELECT @oldQty = QTY FROM DELETED
	SELECT @newQty = QTY,@pID = ID,@pMod = MODBY FROM INSERTED
	print 'The qty has changed for item number' + @pID
	SET @mult = @newQty / @oldQty
	declare @c as CURSOR, @it varchar(50)
	set @c = CURSOR FOR SELECT ID FROM A_ORDER_ITEMS WHERE PARENT = @pID
	open @c
	fetch next from @c INTO @it
	while @@fetch_status = 0
		begin
		print 'Changing the qty of ' + @it
		exec A_SP_ORDER_ITEM_CHANGE_QTY_WITH_MULTIPLER @it,@mult,@pMod
		fetch next from @c INTO @it
		end
	close @c
	deallocate @c
	end

--We need to update the purchse data so we have the rev of the procedure
-- and the rev of the product recorded
if not exists(SELECT * FROM DELETED)
	begin
	if @purchHistID is not null
		begin
		SELECT @prodHistID = HISTORY_REF_ID FROM A_PRODUCTS WHERE ID = @prodID
		SELECT @procHistID = HISTORY_REF_ID FROM A_PROCEDURES WHERE ID IN
			(SELECT PROCEDURE_ID FROM A_V_PRODUCTS_APPROVED_DATA WHERE ID = @prodID)

		INSERT INTO A_ORDER_ITEMS_PURCHASE_DATA
			(ORDER_ITEM_ID,PRODUCT_HIST_ID,PROCEDURE_HIST_ID,DRCM,MODBY)
		VALUES
			(@ID,
			@prodHistID,
			@procHistID,
			getdate(),
			@modby
			)
		end
	end

if @purchHistID is not null
	begin
	declare @ORIG_DUE_DATE datetime
	SELECT @ORIG_DUE_DATE = ORIG_DUE_DATE FROM INSERTED
	if @ORIG_DUE_DATE IS NULL
		UPDATE A_ORDER_ITEMS SET ORIG_DUE_DATE = DUE_DATE WHERE ID = @ID 
	end
--declare @purchHistID varchar(50)
--SELECT @purchHistID = PURCHASE_HIST_ID FROM A_ORDER_ITEMS WHERE ID = @ID
--PB Pulled this out because it was slowing down the purchase creation
--if @purchHistID is not null
--	exec A_SP_PURCHASE_UPDATE_STATUS @purchHistID




