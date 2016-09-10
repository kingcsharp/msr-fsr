
CREATE  PROCEDURE dbo.A_TASK_FIGURE_OUT_RECEIVE_LOCATION
@taskID varchar(50),
@strNTLogin varchar(50)
AS
declare @procStepID varchar(50),@destLoc varchar(50),
	@destType varchar(50),@purchItemID varchar(50),@productID varchar(50),
	@supplierID varchar(50),@fillItemID varchar(50),@acctID varchar(50)
SELECT @procStepID = PROCEDURE_STEP_ID FROM A_TASKS WHERE ID = @taskID
SELECT @destType = DESTINATION FROM A_PROCEDURE_STEPS WHERE ID = @procStepID
if @destType = 'SPEC_LOCATION'
	SELECT @destLoc = SPECIFIC_LOCATION FROM A_PROCEDURE_STEPS WHERE ID = @procStepID
if @destType = 'SUPPLIER_ADDRESS'
	begin
	print 'Trying to find the supplier address'
	SELECT @purchItemID = PURCHASE_ITEM_ID 
	FROM A_TASK_ORDER_INFORMATION 
	WHERE TASK_ID = 
		(SELECT PARENT_ID FROM A_TASKS WHERE ID = @taskID)
	SELECT top 1 @acctID = ACCOUNT_ID FROM A_ORDER_ITEMS WHERE (ID = @purchItemID OR PARENT = @purchItemID AND ACCOUNT_ID IS NOT NULL)
	if @acctID is not null
		begin
		SELECT @supplierID = SUPPLIER_CO FROM A_V_ACCOUNTS_APPROVED_DATA WHERE ID = @acctID
		if @supplierID is not null
			SELECT @destLoc = LOCATION FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @supplierID
		end
	if @destLoc is null
		begin
		SELECT @productID = PRODUCT_ID FROM A_ORDER_ITEMS WHERE ID = @purchItemID
		SELECT @supplierID = SUPPLIER_ID FROM A_V_PRODUCTS_APPROVED_DATA WHERE ID = @productID
		SELECT @destLoc = LOCATION FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @supplierID
		end
	UPDATE A_TASKS SET COMMENT = isNull(COMMENT,'') + 'Set the Destination location using the following data ' +
		' PurchItemID = ' + isNull(@purchItemID,'NULL') +
		' acctID = ' + isNull(@acctID,'NULL') +
		' @supplierID = ' + isNull(@supplierID,'NULL') +
		' @productID = ' + isNull(@productID,'NULL') + ' '
		WHERE ID = @taskID



	end
if @destType = 'ORIG_OBJ_LOCATION'
	begin
	print 'Trying to find the original Object Location'
	SELECT @fillItemID = OBJECT_ID 
	FROM A_TASK_OBJECT_LINK 
	WHERE TASK_ID = 
		(SELECT PARENT_ID FROM A_TASKS WHERE ID = @taskID)
	SELECT @destLoc = LOCATION FROM A_V_ACTUAL_PARTS_APPROVED_DATA WHERE ID = @fillItemID
	end

if not exists(SELECT * FROM A_TASK_ORDER_INFORMATION WHERE TASK_ID = @taskID)
		INSERT INTO A_TASK_ORDER_INFORMATION (TASK_ID,ACTUAL_TO_LOC,MODBY,DRCM)
		VALUES (@taskID,@destLoc,@strNTLogin,getDate())
else
		UPDATE A_TASK_ORDER_INFORMATION
		SET ACTUAL_TO_LOC = @destLoc
		WHERE TASK_ID = @taskID


