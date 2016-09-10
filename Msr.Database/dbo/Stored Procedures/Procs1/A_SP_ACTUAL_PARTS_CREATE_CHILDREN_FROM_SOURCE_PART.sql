


CREATE    PROCEDURE DBO.A_SP_ACTUAL_PARTS_CREATE_CHILDREN_FROM_SOURCE_PART 
@myRoot varchar(50),
@strNTLogin varchar(50)
AS
begin transaction
declare @LOCATION varchar(50), @cPartID varchar(50), @pQty float, @cQty float,
	@CUR_OWNER varchar(50),@apStat varchar(50),@pPartID varchar(50),
	@cObjID varchar(50),@msgs varchar(4000)

SELECT @LOCATION = LOCATION, @pQty = Qty,@CUR_OWNER = CUR_OWNER,
	@pPartID = PART_ID
	FROM A_ACTUAL_PARTS_HISTORY 
	WHERE OBJECT_ID = @myRoot
if @@ERROR <> 0 goto problem
declare @phPartID varchar(50)
SELECT @phPartID = PARTS_HISTORY_ID FROM A_PARTS WHERE ID = @pPartID
print 'My Location is ' + @LOCATION
print 'parent Quantity = ' + convert(varchar(50),@pQty)
print 'Cur Owner = ' + @CUR_OWNER
print 'Part ID = ' + @phPartID
if @@ERROR <> 0 goto problem


Declare @curs Cursor,@cNickName nvarchar(200)
set @curs = Cursor For SELECT PART_ID,QTY,NICK_NAME FROM A_PARTS_SUB_PARTS WHERE PARENT = @phPartID
if @@ERROR <> 0 goto problem
open @curs
if @@ERROR <> 0 goto problem
Fetch Next from @curs Into @cPartID,@cQty,@cNickName
while (@@fetch_status = 0)
Begin
	print 'Adding Child Part = ' + @cPartID
	set @cQty = @cQty * @pQty
	if @@ERROR <> 0 goto problem
	exec A_SP_ACTUAL_PARTS_UPDATE_PART
	@cObjID OUTPUT,
	@msgs OUTPUT,
	NULL,
	@cPartID,
	@cQty,
	NULL,
	@cNickName,
	@LOCATION,
	@CUR_OWNER,
	'ap_installed',
	NULL,
	@myRoot,
	null,
	null,
	@strNTLogin
	if @@ERROR <> 0 goto problem
	print 'Added the child part and got an object ID back of ' + @cObjID
	print 'going to go ahead and set the status to approved and then call function to finish approval WF'
	UPDATE A_OBJECTS SET STATUS = 'APPROVED',LOCKED_BY = NULL WHERE ID = @cObjID
	if @@ERROR <> 0 goto problem
	exec A_SP_ACTUAL_PARTS_FINISH_WF null,@cObjID,@strNTLogin
	if @@ERROR <> 0 goto problem
	Fetch Next from @curs Into @cPartID,@cQty,@cNickName
End
close @curs
Deallocate @curs






fin:
if @@trancount > 0 COMMIT TRANSACTION
print 'Finished A_SP_ACTUAL_PARTS_CREATE_CHILDREN_FROM_SOURCE_PART with no errors'
return 0

PROBLEM:
if @@trancount > 0 	ROLLBACK TRANSACTION
print ' There was a problem in A_SP_ACTUAL_PARTS_CREATE_CHILDREN_FROM_SOURCE_PART and we will terminate and not finish anything '
return 1











