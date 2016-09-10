CREATE  procedure dbo.A_SP_ACTUAL_PARTS_DELETE_A_PART_ENTIRELY
@ID varchar(50),
@apID varchar(50)
AS
if exists(SELECT TASK_ID FROM A_TASK_OBJECT_LINK WHERE OBJECT_ID = @ID)
	begin
		print 'This part still has task history and can not be deleted'
		goto PROBLEM
	end
if exists(SELECT ID FROM A_OBJECTS WHERE ROOT = @ID AND REV <> 1)
	begin
		print 'This part has been revved once we can not delete it here'
		goto PROBLEM
	end


print 'This task has no task history.  Lets try to delete it'
declare @success tinyint
declare @curs cursor,@childID varchar(50)
set @curs = CURSOR for SELECT OBJECT_ID FROM A_ACTUAL_PARTS_HISTORY WHERE PARENT_ID = @ID
open @curs
fetch next from @curs into @childID
while @@fetch_status = 0
	begin
	print 'Deleting child' + @childID
	exec A_SP_ACTUAL_PARTS_DELETE_A_PART_ENTIRELY @childID,@apID
	if @success = 1 goto PROBLEM
	fetch next from @curs into @childID
	end 
close @curs
deallocate @curs
DELETE FROM A_ACTUAL_PARTS_HISTORY WHERE OBJECT_ID = @ID


FIN: return 0
PROBLEM:
begin
	print 'There was an error deleting ID = ' + @ID


 return 1
end


