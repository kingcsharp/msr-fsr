
CREATE  PROCEDURE dbo.A_SP_DNR_TASK_UPDATE_CHILDREN
@DNR_ID varchar(50),
@TASK_ID varchar(50)
AS
DELETE FROM A_DNR_TASK_INFO WHERE TASK_ID = @TASK_ID
declare @TASK_TYPE varchar(50)
--if exists(SELECT * FROM A_MONITOR_TEMPLATES WHERE TASK_ID = @TASK_ID)
--	set @TASK_TYPE = 'test'
--else
--	set @TASK_TYPE = 'Corrective Action'

declare @DNR_STAT varchar(50)

SELECT @DNR_STAT = STATUS FROM A_TASKS WHERE ID = @DNR_ID

INSERT INTO A_DNR_TASK_INFO
	(ID,TASK_ID,DNR_ID,DNR_STATUS,TASK_TYPE)
VALUES(newID(),@TASK_ID,@DNR_ID,@DNR_STAT,@TASK_TYPE)


Declare @it nvarchar(50)
Declare @curs Cursor
set @curs = Cursor For SELECT ID FROM A_TASKS WHERE PARENT_ID = @TASK_ID
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	print 'Looking at DNR = ' + @it
	exec A_SP_DNR_TASK_UPDATE_CHILDREN @DNR_ID,@it
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs

