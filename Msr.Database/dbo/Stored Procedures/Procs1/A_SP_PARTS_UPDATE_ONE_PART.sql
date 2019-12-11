
CREATE           PROCEDURE dbo.A_SP_PARTS_UPDATE_ONE_PART
@newObjID nvarchar(50) OUTPUT,
@messages nvarchar(500) OUTPUT,
@objID nvarchar(50),
@COMPANY  nvarchar(50),
@COMPANY_PART_NUMBER  nvarchar(50),
@NAME nvarchar(100),
@OEM_PART_NUMBER  nvarchar(100),
@PART_TYPE  nvarchar(50),
@SPARE  nvarchar(50),
@CONSUMABLE  nvarchar(50),
@UNIT  nvarchar(50),
@UNIT_SHIPPING_WEIGHT  nvarchar(50),
@subParts varchar(8000),
@CUSTOMER_SEE_AVAILABILITY varchar(50),
@SUPPLIER_SEE_AVAILABILITY varchar(50),
@SUPPLIER_SEE_INSTALL_BASE varchar(50),
@INTERNAL_EQUAL_PARTS varchar(8000),
@WEIGHT_TYPE varchar(50),
@CREATE_PROD tinyInt,
@SUPPLIER_CO varchar(50),
@PRODUCT_TYPE varchar(50),
@PROC_VERB nvarchar(100),
@SPECIAL_CUSTOMER varchar(8000),
@CUSTOMER_EXCEPTIONS varchar(8000),
@CUSTOMERS varchar(8000),
@PRICE decimal(18,2),
@strNTLogin nvarchar(50)
AS
print 'Saving Part'
SELECT @COMPANY = ROOT_COMPANY FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @strNTLogin
if @objID is null
	begin
	print 'The object ID is null'
	declare @newID as nvarchar(50)
	exec sp_getUniqueID3 @newID OUTPUT
	print 'The new ID is ' + isnull(@newID,'NULL??')
	INSERT INTO A_PARTS_HISTORY (
		ID,	COMPANY,COMPANY_PART_NUMBER , OEM_PART_NUMBER, NAME ,PART_TYPE ,
		SPARE ,	CONSUMABLE ,UNIT ,UNIT_SHIPPING_WEIGHT , DRCM, 
		MODBY, WEIGHT_TYPE) 
		VALUES(	@newID ,@COMPANY,@COMPANY_PART_NUMBER,@OEM_PART_NUMBER,@NAME,
		@PART_TYPE,@SPARE,@CONSUMABLE,@UNIT,@UNIT_SHIPPING_WEIGHT,
		getDate(),@strNTLogin,@WEIGHT_TYPE)
	if @COMPANY_PART_NUMBER IS NULL
		SELECT @COMPANY_PART_NUMBER = OBJECT_ID FROM A_PARTS_HISTORY WHERE ID = @newID
	end
else
	begin
	SELECT @newID = ID FROM A_PARTS_HISTORY WHERE OBJECT_ID = @objID
	end

UPDATE A_PARTS_HISTORY SET
	DRCM = getDate(),
	MODBY = @strNTLogin,
	COMPANY = @COMPANY,
	COMPANY_PART_NUMBER = @COMPANY_PART_NUMBER  ,
	OEM_PART_NUMBER = @OEM_PART_NUMBER,
	NAME = @NAME ,
	PART_TYPE = @PART_TYPE  ,
	SPARE = @SPARE  ,
	CONSUMABLE = @CONSUMABLE  ,
	UNIT = @UNIT  ,
	UNIT_SHIPPING_WEIGHT = @UNIT_SHIPPING_WEIGHT,
	CUSTOMER_SEE_AVAILABILITY = @CUSTOMER_SEE_AVAILABILITY,
	SUPPLIER_SEE_AVAILABILITY = @SUPPLIER_SEE_AVAILABILITY,
	SUPPLIER_SEE_INSTALL_BASE = @SUPPLIER_SEE_INSTALL_BASE,
	WEIGHT_TYPE = @WEIGHT_TYPE,
	CREATE_PROD = @CREATE_PROD,
	SUPPLIER_CO =@SUPPLIER_CO,
	PRODUCT_TYPE =@PRODUCT_TYPE,
	PROC_VERB =@PROC_VERB,
	PRICE = @PRICE
	WHERE ID = @newID

SELECT @newObjID = OBJECT_ID FROM A_PARTS_HISTORY WHERE ID = @newID
print 'The object ID returned is ' + @newObjID

print 'Now we need to save the internal Equal Parts List'
print 'First delete all the ones we used to have'
DELETE FROM A_PARTS_INTERNAL_EQUALS WHERE PART_ID = @newID
CREATE TABLE #TempItems (IT varchar(50))
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @INTERNAL_EQUAL_PARTS,','
INSERT INTO A_PARTS_INTERNAL_EQUALS(ID,PART_ID,EQUAL_PART_ID,DRCM,MODBY)
	SELECT newID(),@newID,ltrim(IT),getDate(),@strNTLogin FROM #TempItems


print 'Updating the future special customers referenced by this part'
print 'First delete all the ones we used to have'
DELETE FROM A_PARTS_FUTURE_SPECIAL_DISTRIBUTIONS WHERE PART_ID = @newID
print 'making a cursor to go through the spec items'
DELETE FROM #TempItems
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @SPECIAL_CUSTOMER,', '
INSERT INTO A_PARTS_FUTURE_SPECIAL_DISTRIBUTIONS 
	(ID,PART_ID,SPEC_ID,DRCM,MODBY)
	SELECT newID(),@newID,ltrim(IT),getDate(),@strNTLogin FROM #tempItems

print 'Updating the customers referenced by this part'
print 'First delete all the ones we used to have'
DELETE FROM A_PARTS_FUTURE_CUSTOMERS WHERE PART_ID = @newID
print 'making a cursor to go through the Customers'
DELETE FROM  #TempItems
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @CUSTOMERS,', '
INSERT INTO A_PARTS_FUTURE_CUSTOMERS
	(ID,PART_ID,CUST_ID,DRCM,MODBY)
	SELECT newID(),@newID,ltrim(IT),getDate(),@strNTLogin FROM #tempItems

print 'Updating the customer exceptons referenced by this part'
print 'First delete all the ones we used to have'
DELETE FROM A_PARTS_FUTURE_EXCEPTIONS WHERE PART_ID = @newID
print 'making a cursor to go through the Customers'
DELETE FROM  #TempItems
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @CUSTOMER_EXCEPTIONS,', '
INSERT INTO A_PARTS_FUTURE_EXCEPTIONS
	(ID,PART_ID,CUST_ID,DRCM,MODBY)
	SELECT newID(),@newID,ltrim(IT),getDate(),@strNTLogin FROM #tempItems









