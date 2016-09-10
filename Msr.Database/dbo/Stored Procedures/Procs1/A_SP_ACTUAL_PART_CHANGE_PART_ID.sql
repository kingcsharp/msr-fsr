


CREATE    PROCEDURE dbo.A_SP_ACTUAL_PART_CHANGE_PART_ID 
@ID varchar(50),
@customerCo varchar(50)
AS
print 'We need to go through this whole actual part and all its children and change their IDs'
Declare @it nvarchar(50), @curs Cursor
set @curs = Cursor For SELECT ID FROM A_V_ACTUAL_PARTS_APPROVED_DATA WHERE PARENT_ID = @ID
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	print 'Fixing the child ' + @it
	exec A_SP_ACTUAL_PART_CHANGE_PART_ID @it,@customerCo
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs

print 'Change this part' +  @ID
declare @myPartID varchar(50),@myHistID varchar(50),@LOCAL_PART_ID varchar(50)
SELECT @myPartID = PART_ID,@myHistID = HISTORY_REF_ID FROM A_V_ACTUAL_PARTS_APPROVED_DATA WHERE ID = @ID
print '########### SELECT  * FROM A_V_PARTS_WITH_RELATED_EXTERNAL_CO_INFO 
	WHERE EXTERNAL_PART_ID = ''' + @myPartID + ''' AND LOCAL_CO = ''' + @customerCo + ''''


SELECT @LOCAL_PART_ID = LOCAL_PART_ID FROM A_V_PARTS_WITH_RELATED_EXTERNAL_CO_INFO 
	WHERE EXTERNAL_PART_ID = @myPartID AND LOCAL_CO = @customerCo
if @LOCAL_PART_ID is null 
	begin
	print 'The local part is null this bad'
	goto PROBLEM
	end
print 'Updating the part id to the local part id'
UPDATE A_ACTUAL_PARTS_HISTORY SET PART_ID = @LOCAL_PART_ID WHERE ID = @myHistID
print '%%%%%%%% About to update the Creating co of  ' + @ID
UPDATE A_OBJECTS SET CREATING_CO = @customerCo WHERE ROOT = @ID

fin:
return 0

PROBLEM:
return 1



