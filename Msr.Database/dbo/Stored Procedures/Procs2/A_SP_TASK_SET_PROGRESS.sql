CREATE     PROCEDURE dbo.A_SP_TASK_SET_PROGRESS
@newID nvarchar(50) OUTPUT,
@messages nvarchar(2000) OUTPUT,
@ID nvarchar(50),
@ROOT_ID varchar(50),
@TASK_DONE_LIST varchar(8000),
@strNTLogin varchar(50)
AS
if @TASK_DONE_LIST is null
	goto fin
declare @retData as varchar(8000),@retID as varchar(50)
CREATE TABLE #TempItems	(IT varchar(50))
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @TASK_DONE_LIST,'---'

if (exists(SELECT * FROM #TempItems WHERE IT = @ID) or (@ROOT_ID = @ID))
	begin
	print 'This one should be accepted and completed ' + @ID
 	if exists(SELECT * FROM A_TASKS WHERE ID = @ID and STATUS = 'REQUESTED')
 		exec A_SP_TASK_ACCEPT @retData OUTPUT,@retID OUTPUT,@ID,@strNTLogin
 	if not(exists(SELECT * FROM A_TASKS WHERE PARENT_ID = @ID))
 		exec A_SP_TASK_QUICK_CLOSE @retData OUTPUT,@retID OUTPUT,@ID,@strNTLogin
	end
declare @curs as cursor,@curID as varchar(50)
set @curs = CURSOR FOR SELECT ID FROM A_TASKS WHERE PARENT_ID = @ID ORDER BY dbo.leadingSpaces(ID,50)
open @curs
fetch next from @curs into @curID
while @@fetch_Status = 0
	begin
	exec A_SP_TASK_SET_PROGRESS @newID OUTPUT,@messages OUTPUT,
		@curID,@ROOT_ID,@TASK_DONE_LIST,@strNTLogin

	fetch next from @curs into @curID
	end 


fin:

