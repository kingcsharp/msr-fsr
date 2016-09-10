
CREATE     PROCEDURE DBO.A_SP_ACTUAL_PARTS_MERGE_PART
@newID varchar(50) OUTPUT,
@msg varchar(8000) OUTPUT,
@ID varchar(50),
@strNTLogin varchar(50)
AS
BEGIN TRANSACTION
declare @rootChildList as varchar(8000),@testChildList as varchar(8000),@myPartID varchar(50),
	@myLocation varchar(50),@myOwner varchar(50)
declare @rootID varchar(50)
SELECT @rootID = ROOT FROM A_OBJECTS WHERE ID = @ID

SELECT @myLocation = LOCATION,@myPartID = PART_ID,@myOwner = CUR_OWNER FROM A_V_ACTUAL_PARTS_APPROVED_DATA WHERE ID = @rootID
exec A_SP_ACTUAL_PARTS_CREATE_CHILD_LIST @rootChildList OUTPUT,@rootID,0,NULL,@strNTLogin
if @@ERROR <> 0 goto problem
print 'Got the merging Part Child list = ' + isNull(@rootChildList,'NULL')

print 'SELECT ID FROM A_V_ACTUAL_PARTS_APPROVED_DATA 
	WHERE PARENT_ID is NULL AND PART_ID = ''' + isNull(@myPartID,'NULL') + ''' AND LOCATION = ''' +isNull(@myLocation,'NULL') + ''' AND 
		ID <> ''' + isNull(@rootID,'NULL') + ''' AND CUR_OWNER = '''+isNull(@myOwner,'NULL')+''''
declare @sql nvarchar(4000)
print @sql
Declare @it as nvarchar(50)
Declare @curs Cursor
set @curs = Cursor For 
	SELECT ID FROM A_V_ACTUAL_PARTS_APPROVED_DATA 
	WHERE PARENT_ID is NULL AND PART_ID = @myPartID AND LOCATION = @myLocation AND ID <> @rootID AND CUR_OWNER = @myOwner 
	AND (SERIAL IS NULL OR SERIAL ='') AND NICK_NAME IS NULL
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	print 'Checking found possible part = ' + @it
	set @testChildList = NULL
	exec A_SP_ACTUAL_PARTS_CREATE_CHILD_LIST @testChildList OUTPUT,@it,0,null,@strNTLogin
	if @@ERROR <> 0 goto problem
	print 'testChildList = ' + @testChildList
	print 'rootChildList = ' + @rootChildList
	if @testChildList = @rootChildList
		begin
			print 'We could Mege these 2'
			exec A_SP_ACTUAL_PARTS_MERGE_FIRST_INTO_SECOND @it,@rootID,@strNTLogin
			if @@ERROR <> 0 goto problem
		end
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs 


fin:
if @@trancount > 0 COMMIT TRANSACTION
print 'Finished A_SP_ACTUAL_PARTS_GET_MERGE_PART_LIST with no errors'
return 0

PROBLEM:
if @@trancount > 0 	ROLLBACK TRANSACTION
print ' There was a problem in A_SP_ACTUAL_PARTS_GET_MERGE_PART_LIST and we will terminate and not finish anything '
return 1








