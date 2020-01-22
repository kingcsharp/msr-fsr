






















CREATE                  PROCEDURE [dbo].[A_SP_FILLS_UPDATE_FILL]
	@newID varchar(50) OUTPUT,
	@msg nvarchar(4000) OUTPUT,
	@fillID varchar(50),
	@fillObjID varchar(50),
	@strNTLogin varchar(50),
	@strLocation varchar(50)
as
print 'Inside A_SP_FILLS_UPDATE_FILL FILL ID = ' + isNull(@fillID,'NULL') + 
	' Fill OBJ = ' + @fillObjID + ' strNTLogin = ' + @strNTLogin + ' strLocation = ' + @strLocation

declare @fillObject varchar(50),@objTable varchar(50)
declare @fObjID varchar(50),@qty float
SELECT @fillObject = OBJ_PROD_APPLIES_TO FROM A_V_FILLS_SEARCH WHERE ID = @fillID
SELECT @objTable = OBJ_TABLE FROM A_V_APPROVED_OBJECTS WHERE ID = @fillObject
declare @purchaseItemID varchar(50),@purchHistID varchar(50)
SELECT @purchaseItemID = PURCH_ITEM_ID FROM A_FILLS WHERE ID = @fillID
SELECT @purchHistID = PURCHASE_HIST_ID FROM A_ORDER_ITEMS WHERE ID = @purchaseItemID
exec A_SP_ORDER_ITEM_UPDATE_QTYS @purchaseItemID

if @fillObjID is not null
	begin
	SELECT @objTable = OBJ_TABLE FROM A_OBJECTS WHERE ROOT = @fillObjID
	print 'The object Table is ' + @objTable
	end


if (isNull(@objTable,'') = 'A_ACTUAL_PARTS_HISTORY')
	begin
	print 'Calling Fill with Actual Part'
	exec A_SP_FILL_FILL_WITH_ACTUAL_PART @fillID,@fillObjID,@strNTLogin
	UPDATE A_FILLS SET LOCATION_ID = @strLocation WHERE ID = @fillID
	end
if (isNull(@objTable,'') = 'A_PARTS_HISTORY')
	begin
	print 'Calling Fill with Actual Part'
	exec A_SP_FILL_FILL_WITH_ACTUAL_PART @fillID,@fillObjID,@strNTLogin
	end

if (isNull(@objTable,'') = 'A_PART_TYPES_HISTORY')
	begin
	print 'Calling Fill with Actual Part'
	exec A_SP_FILL_FILL_WITH_ACTUAL_PART @fillID,@fillObjID,@strNTLogin
	end

if (isNull(@objTable,'') = 'A_PEOPLE_HISTORY')
	begin
	print 'Calling Fill with Person'
	exec A_SP_FILL_FILL_WITH_PERSON @fillID,@fillObjID,@strNTLogin
	end


if (@objTable is null)
	begin
	print 'This fill must be filled by other means'
	if (SELECT FILL_BY FROM A_FILLS WHERE ID = @fillID) = 'PARENT_FILL_ITEM'
		begin
		print 'This is going to be filled with its parent item'
		
		SELECT @fObjID = FILL_OBJ_ID, @qty = FILL_QTY FROM A_FILLS
			WHERE PURCH_ITEM_ID = 
				(SELECT PARENT FROM A_ORDER_ITEMS WHERE ID = @purchaseItemID)

		print 'We got the fill object from our parent and it is ' + isNull(@fObjID,'NULL')
		UPDATE A_FILLS SET FILL_OBJ_ID = @fObjID,FILL_QTY = @qty,FILLER = @strNTLogin,LOCATION_ID = @strLocation WHERE ID = @fillID
		end
	end

print '#$#$#$#$#$#$#$#$#$Fixing to fill all child ITems of  ' + @purchaseItemID
declare @curs as CURSOR,@childFill as varchar(50)
set @curs = CURSOR FOR SELECT ID FROM A_FILLS WHERE 
		FILL_BY = 'PARENT_FILL_ITEM' AND FILL_OBJ_ID IS NULL AND 
		PURCH_ITEM_ID IN (SELECT ID FROM A_ORDER_ITEMS WHERE PARENT = @purchaseItemID)
open @curs
fetch next FROM @curs into @childFill
while @@fetch_status = 0
	begin
	exec A_SP_FILLS_UPDATE_FILL null,null,@childFill,@fillObjID,@strNTLogin,@strLocation
	fetch next FROM @curs into @childFill
	end
close @curs
deallocate @curs
print '#$#$#$#$#$#$#$#$#$We finished all the children fills'


















