CREATE          procedure [dbo].[A_SP_PRODUCTS_COPY_ONE]
	@newObjID nvarchar(50) OUTPUT,
	@strID nvarchar(50),
	@copyPrefix nvarchar(50),
	@strNTLogin nvarchar(50)
as
--Make a new ID for the copy
declare @newID as nvarchar(50)
exec sp_getUniqueID3 @newID OUTPUT
--Insert the new one
INSERT INTO A_PRODUCTS_HISTORY ([ID], [NAME], SUPPLIER_ID, COMMENTS, PROCEDURE_ID,
APP_OBJECT, SHIP_OR_LABOR, CUSTOMIZABLE, REQ_FORM, MGR_TEAM, SALES_TAX, OBJECT_ID, PARENT_ID,
[DRCM], [MODBY], CUST_MGR_ROLE, AVAILABILITY,TotalSalePrice,MaterialCost,CustomerRequirementId,IsProduct)
SELECT @newID as ID, @copyPrefix + [NAME],SUPPLIER_ID, COMMENTS, PROCEDURE_ID,
APP_OBJECT, SHIP_OR_LABOR, CUSTOMIZABLE, REQ_FORM, MGR_TEAM, SALES_TAX, OBJECT_ID, PARENT_ID,
getDate() as DRCM, @strNTLogin as MODBY, CUST_MGR_ROLE, AVAILABILITY,TotalSalePrice,MaterialCost, CustomerRequirementId,IsProduct
FROM A_PRODUCTS_HISTORY WHERE ID = @strID

--find out what object id the new one got
SELECT @newObjID = OBJECT_ID FROM A_PRODUCTS_HISTORY WHERE ID = @newID
print 'The new object ID is ' + @newObjID

print 'Need to copy the related Objects that this product is used on'
--Copy Over related Tables
INSERT INTO A_PRODUCT_OBJ_USED_ON_LINK 
SELECT newID() AS ID, @newID,OBJECT_ID,GetDAte(),@strNTLogin FROM A_PRODUCT_OBJ_USED_ON_LINK
WHERE PRODUCT_ID = @strID







