CREATE PROCEDURE dbo.A_SP_WF_GROUP_RESTART_ALL_MY_CURRENT_GROUPS_THAT_ARE_STARTED
@groupID varchar(50)
AS
print 'Restarting all groups which are number ' + @groupID
declare @curs as CURSOR,@it varchar(50),@starter varchar(50)
set @curs = CURSOR FOR SELECT ID,MODBY FROM A_WORKFLOW_GROUP_STARTED WHERE WF_GROUP_ID = @groupID AND ISNULL(FINISHED,0) <> 1
open @curs
fetch next from @curs into @it,@starter
while @@fetch_status = 0
	begin
	print 'working on gs = ' + @it
	exec A_SP_WF_CREATE_WORKFLOW_GROUP_DATA @it,@starter	
	fetch next from @curs into @it,@starter
	end

