create PROCEDURE [dbo].[Portal_Create_UpdateProduct]
@newID varchar(50) OUTPUT,
@messages varchar(500) OUTPUT,
@productId varchar(50),
@loginId varchar(50),
@procedureId varchar(50),
@partId varchar(50),
@supplierId varchar(50),
@custId varchar(50),
@productName varchar(50),
@leadTime float,
@price float,
@totalSalePrice decimal,
@materialCost decimal
AS

BEGIN TRANSACTION 

BEGIN TRY
	-- Testing
	--declare @p1 varchar(50)
	--declare @p2 varchar(50)
	--exec Portal_CreateProduct @p1 output,@p2 output,'1618','1718','1168','1566','q27'
	--select @p1, @p2

	declare @productHistoryId varchar(50)

	declare @p2 varchar(500)

	exec A_SP_PRODUCT_UPDATE_ONE_PRODUCT @newID output,@p2 output,@productId,NULL,@supplierId,@productName,'',@procedureId,@partId,'0',NULL,NULL,'0',NULL,NULL,NULL,NULL,NULL,'0',NULL,@loginId,@totalSalePrice,@materialCost

	exec A_SP_FILES_DELETE_LINKS @newID,'REQ_FORM',@loginId

	DELETE FROM A_PRODUCTS_QUICK_PRICE WHERE PROD_HIST_ID = (SELECT ID FROM A_PRODUCTS_HISTORY WHERE OBJECT_ID = @newID)

	SELECT @productHistoryId=ID FROM A_PRODUCTS_HISTORY WHERE OBJECT_ID = @newID

	INSERT INTO A_PRODUCTS_QUICK_PRICE (ID,PROD_HIST_ID,CUST_ID,PRICE,DRCM,MODBY,CREATE_PRICE_LIST,PROD_TIME,PROD_TIME_UNIT,CAPACITY,CAPACITY_UNITS) 
	VALUES (newID(),@productHistoryId,@custId, @price,getDate(),@loginId,'1',@leadTime,'TIME_SYS_DAYS',NULL,'minute')

	----exec A_SP_OBJECT_GET_DATA @newID,@loginId
	----declare @p1 varchar(4000)

	----exec A_SP_OBJECT_CHECK_FOR_VALIDITY @p1 output,@p2 output,@newID,@loginId
	----exec A_SP_OBJECT_SHOW_APPLICABLE_WORKFLOWS @newID,@loginId
	----exec A_SP_OBJECT_START_WF @p1 output,@p2 output,@newID,'37',@productName,'APPROVED',NULL,@loginId
	----exec A_SP_OBJECT_GET_MAIN_DATA @newID,@loginId
	----EXEC A_SP_OBJECT_CHECKED_TO_ME @newID,@loginId
	----EXEC A_SP_OBJECT_CHECKED_TO_SUBORDINATE @newID,@loginId
	----exec A_SP_ADMIN_SQL_TO_RUN_EXECUTE
	----exec A_SP_PRODUCTS_GET_EDIT_INFORMATION @newID,@loginId

COMMIT TRANSACTION

END TRY
BEGIN CATCH
  ROLLBACK TRANSACTION
END CATCH
GO

