


CREATE     procedure A_SP_ACTUAL_PARTS_COPY_SUB_PART
	@ID varchar(50),
	@newParentID varchar(50),
	@newRootID varchar(50),
	@strNTLogin varchar(50)
as
print 'Copying a Child' + @ID
declare @myNewID as varchar(50)
exec sp_GetUniqueID3 @myNewID OUTPUT
INSERT INTO A_ACTUAL_PARTS_SUB_PARTS (
ID,ROOT_ID,PARENT_ID,PART_ID,QTY,SERIAL,CUR_OWNER,ASSEMBLY_WT,AP_STATUS,NICK_NAME,DRCM,MODBY,ROOT_STATUS)
SELECT @myNewID,@newRootID,@newParentID,PART_ID,
QTY,SERIAL,CUR_OWNER,ASSEMBLY_WT,AP_STATUS,NICK_NAME,getDate(),@strNTLogin,ROOT_STATUS
FROM A_ACTUAL_PARTS_SUB_PARTS WHERE ID = @ID

--We need to make a cursor to copy all of the sub parts now.
Declare @oneStep nvarchar(50)
Declare @stepCursor Cursor
set @stepCursor = Cursor
For SELECT ID FROM A_ACTUAL_PARTS_SUB_PARTS WHERE PARENT_ID = @ID
open @stepCursor
Fetch Next from @stepCursor
Into @oneStep
while (@@fetch_status = 0)
	Begin
	print @oneStep
	exec A_SP_ACTUAL_PARTS_COPY_SUB_PART @oneStep,@myNewID,@newRootID,@strNTLogin
	Fetch Next from @stepCursor
	Into @oneStep
	End
close @stepCursor
Deallocate @stepCursor		




