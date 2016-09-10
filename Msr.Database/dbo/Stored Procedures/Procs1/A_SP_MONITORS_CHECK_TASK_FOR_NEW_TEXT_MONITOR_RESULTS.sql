




CREATE      PROCEDURE dbo.A_SP_MONITORS_CHECK_TASK_FOR_NEW_TEXT_MONITOR_RESULTS
@retVal varchar(50) OUTPUT,
@retMSG varchar(50) OUTPUT,
@TASK_ID varchar(50),
@strNTLogin varchar(50)
AS
if dbo.md() = 1 
	begin
	print 'Checking the monitors on Task = ' + @TASK_ID + ' for new text'
	SELECT * FROM A_MONITOR_TEMPLATES wHERE TASK_ID = @TASK_ID
	end

if not exists(SELECT * FROM A_MONITOR_TEMPLATES WHERE TASK_ID = @TASK_ID AND MONITOR_TYPE = 'TEXT' AND USE_RESULT = 1 AND MY_ANSWER <> '' AND MY_ANSWER is not null)
	begin
	if dbo.md() = 1 print 'There are no text monitors on this call'
	goto fin
	end

if dbo.md() = 1 print 'We have a text monitor here time to check them all'
Declare @it nvarchar(50),@rollUpID varchar(50),@myTXT varchar(200)
Declare @curs Cursor
set @curs = Cursor For SELECT ID,ROLL_UP_ID,MY_ANSWER FROM A_MONITOR_TEMPLATES WHERE TASK_ID = @TASK_ID AND MONITOR_TYPE = 'TEXT'
open @curs
Fetch Next from @curs Into @it,@rollUpID,@myTXT
while (@@fetch_status = 0)
Begin
	if dbo.md() = 1 print 'Looking at monitor = ' + @it
	if not exists(SELECT * FROM A_MONITOR_TEMPLATES WHERE ROLL_UP_ID = @rollUpID and MY_ANSWER = @myTXT and ID <> @it)
		set @retVal = 'NEW_TEXT'
	
	Fetch Next from @curs Into @it,@rollUpID,@myTXT
End
close @curs
Deallocate @curs


	




fin:

