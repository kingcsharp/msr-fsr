CREATE         PROCEDURE [dbo].[A_SP_PRODUCT_UPDATE_ONE_PRODUCT]
@newID varchar(50) OUTPUT,
@messages varchar(500) OUTPUT,
@objID varchar(50),
@PARENT_ID varchar(50),
@SUPPLIER_ID varchar(50),
@NAME nvarchar(200),
@COMMENTS nvarchar(2000),
@PROCEDURE_ID varchar(50),
@APP_OBJECT varchar(50),
@CUSTOMIZABLE varchar(50),
@REQ_FORM varchar(8000),
@MGR_TEAM varchar(50),
@SALES_TAX varchar(50),
@OBJ_USED_ON varchar(8000),
@MGR_ROLE varchar(50),
@OEM varchar(200),
@MODEL varchar(200),
@PROCESS_AREA varchar(200),
@COPPER tinyInt,
@MM varchar(200),
@strNTLogin varchar(50),
@totalSalePrice REAL,
@materialCost REAL,
@customerRequirementId int
AS
BEGIN TRANSACTION
print 'Starting procedure A_SP_PRODUCT_UPDATE_ONE_PRODUCT'
if @objID is null
	begin
		print 'ID is Null  we need to create this product'
		exec sp_GetUniqueID3 @newID OUTPUT
		INSERT INTO A_PRODUCTS_HISTORY (ID,NAME,MODBY,DRCM) VALUES (@newID,@NAME,@strNTLogin,getDATE())
		SELECT @newID = OBJECT_ID FROM A_PRODUCTS_HISTORY WHERE ID = @newID
	end
else
	begin
		print 'The ID is not null so we are just updating product object ID = ' + @objID
		set @newID = @objID
	end
print 'Now update all the values with the data passed in'
UPDATE A_PRODUCTS_HISTORY SET
PARENT_ID = @PARENT_ID,
SUPPLIER_ID = @SUPPLIER_ID,
NAME = @NAME ,
COMMENTS = @COMMENTS ,
PROCEDURE_ID = @PROCEDURE_ID ,
APP_OBJECT = @APP_OBJECT ,
CUSTOMIZABLE = @CUSTOMIZABLE ,
REQ_FORM = @REQ_FORM,
MGR_TEAM = @MGR_TEAM ,
SALES_TAX = @SALES_TAX ,
CUST_MGR_ROLE = @MGR_ROLE,
OEM  = @OEM,
MODEL = @MODEL,
AREA = @PROCESS_AREA,
CU = @COPPER,
MM = @MM,
modby = @strNTLogin,
DRCM = getDAte(),
TotalSalePrice=@totalSalePrice,
MaterialCost=@materialCost,
CustomerRequirementId = @customerRequirementId
WHERE OBJECT_ID = @newID
if @@ERROR <> 0 goto problem
declare @myID as varchar(50)
SELECT @myID = ID FROM A_PRODUCTS_HISTORY WHERE OBJECT_ID = @newID

DELETE FROM A_PRODUCT_OBJ_USED_ON_LINK WHERE PRODUCT_ID = @myID
if @@ERROR <> 0 goto problem
if @OBJ_USED_ON is not null
	begin
	print 'making a cursor to go through the ref files string'
	CREATE TABLE #TempItems	(IT varchar(50))
	INSERT INTO #TempItems Exec A_SP_Z_SPLIT @OBJ_USED_ON,', '
	Declare @it nvarchar(50)
	Declare @curs Cursor
	set @curs = Cursor For SELECT * FROM #TempItems
	open @curs
	Fetch Next from @curs Into @it
	while (@@fetch_status = 0)
	Begin
		print 'Adding Applicable Object = ' + @it
		INSERT INTO A_PRODUCT_OBJ_USED_ON_LINK (ID,PRODUCT_ID,OBJECT_ID,DRCM,MODBY)
			VALUES(newID(),@myID,ltrim(@it),getDate(),@strNTLogin)
		if @@ERROR <> 0 goto problem
		Fetch Next from @curs Into @it
	End
	close @curs
	Deallocate @curs
	end
fin:
if @@trancount > 0 COMMIT TRANSACTION
return 0
PROBLEM:
if @@trancount > 0 	ROLLBACK TRANSACTION
return 1

