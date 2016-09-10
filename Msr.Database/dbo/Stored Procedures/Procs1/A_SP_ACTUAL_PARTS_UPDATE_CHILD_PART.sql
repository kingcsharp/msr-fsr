








CREATE        PROCEDURE A_SP_ACTUAL_PARTS_UPDATE_CHILD_PART
@newID nvarchar(50) OUTPUT,
@messages nvarchar(2000) OUTPUT,
@ID nvarchar(50),
@PARENT_ID varchar(50),
@PART_ID varchar(50),
@QTY varchar(50),
@SERIAL varchar(50),
@NICK_NAME varchar(50),
@CUR_OWNER varchar(50),
@AP_STATUS varchar(50),
@PRODUCTS varchar(8000),
@strNTLogin varchar(50)
AS
declare @ROOT_ID as varchar(50)
declare @intN as smallint
set @intN = 0
print 'Updating an Actual PArt Child'
if @ID is null
	begin
		print 'ID is Null  we need to create this Actual Part'
		print 'first get the root pdata from the parent id'
		SELECT @ROOT_ID = ROOT_ID FROM A_ACTUAL_PARTS_SUB_PARTS WHERE ID = @PARENT_ID
		exec sp_GetUniqueID3 @newID OUTPUT
		INSERT INTO A_ACTUAL_PARTS_SUB_PARTS (ID,PARENT_ID,ROOT_ID,MODBY,DRCM) 
		VALUES (@newID,@PARENT_ID,@ROOT_ID,@strNTLogin,getDATE())
		set @intN = 1
	end
else
	begin
		set @newID = @ID
	end

UPDATE A_ACTUAL_PARTS_SUB_PARTS SET
PARENT_ID = @PARENT_ID,
PART_ID = @PART_ID,
QTY = @QTY,
SERIAL = @SERIAL,
CUR_OWNER = @CUR_OWNER,
AP_STATUS = @AP_STATUS,
NICK_NAME = @NICK_NAME,
DRCM = getDAte(),
MODBY = @strNTLogin
WHERE ID = @newID


if (@intN = 1 and @PART_ID is not null)
	begin
	print 'This is a new part so add all the childs automatically'
	exec A_SP_ACTUAL_PART_SUB_PARTS_AUTO_CREATE_CHIL_PT_4_PARENT @newID,@strNTLogin
	end

print 'Updating Products List for the Part'
print 'First delete all the ones we used to have'
DELETE FROM A_ACTUAL_PART_PRODUCTS_INSIDE_LINK WHERE ACTUAL_PART_ID = @newID
print 'making a cursor to go through the string'
CREATE TABLE #TempItems	(IT varchar(50))
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @PRODUCTS,', '
Declare @it nvarchar(50)
Declare @curs Cursor
set @curs = Cursor For SELECT * FROM #TempItems
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	print 'Adding Reference Product = ' + @it
	INSERT INTO A_ACTUAL_PART_PRODUCTS_INSIDE_LINK(ID,ACTUAL_PART_ID,PRODUCT_ID,DRCM,MODBY)
	VALUES (newID(),@newID,ltrim(@it),getDate(),@strNTLogin)
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs






