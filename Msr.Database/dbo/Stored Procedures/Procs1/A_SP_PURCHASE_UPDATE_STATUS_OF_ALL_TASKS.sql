

CREATE   PROCEDURE dbo.A_SP_PURCHASE_UPDATE_STATUS_OF_ALL_TASKS 
@phID varchar(50),
@strNTLogin varchar(50)
AS

Declare @taskID nvarchar(50), @curs Cursor
set @curs = Cursor For 
	SELECT ID FROM A_TASKS 
		WHERE ID IN (SELECT TASK_ID FROM A_TASK_ORDER_INFORMATION WHERE PURCHASE_HIST_ID = @phID)

open @curs
Fetch Next from @curs Into @taskID
while (@@fetch_status = 0)
Begin
	print 'looking at task ID = ' + @taskID
	exec A_SP_TASK_UPDATE_BASED_ON_PURCHASE_INFO @taskID,@strNTLogin
	Fetch Next from @curs Into @taskID
End
close @curs
Deallocate @curs






