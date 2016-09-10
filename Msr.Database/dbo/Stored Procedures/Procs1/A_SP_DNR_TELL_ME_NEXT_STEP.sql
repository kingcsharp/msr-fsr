







CREATE          PROCEDURE dbo.A_SP_DNR_TELL_ME_NEXT_STEP
@ID varchar(50)
AS
exec pr 'DNR What to do next'
declare @mType varchar(50),@taskID varchar(50),@ynA varchar(50),
	@resID varchar(50),@ynRes varchar(50)
create TABLE #tempFailingTests (ID varchar(50),TASK_ID varchar(50),RETEST_NOW tinyInt,ROLL_UP_ID varchar(50),STOP_TIME dateTime)
declare @so varchar(8000)
Declare @mID nvarchar(50),@mDesc varchar(4000),@mIsPassing tinyint,@tID varchar(50)
Declare @curs Cursor,@tType varchar(50)

--SELECT * FROM A_TASKS t WHERE t.PARENT_ID = @ID
--	ORDER BY ACTUAL_STOP_DATE,LAST_REQUEST_DATE,ORIG_PLANNED_START_DATE

UPDATE A_DNR_TASK_INFO SET DNR_STATUS = NULL WHERE DNR_ID = @ID

declare @testWasFailing tinyint,@repairTime dateTime
declare @numRepairsDoneAtATime int,@DNR_ID varchar(50),@stopTime datetime
set @curs = Cursor For SELECT t.ID,d.TASK_TYPE,d.DNR_ID,t.ACTUAL_STOP_DATE FROM A_TASKS t,A_DNR_TASK_INFO d WHERE t.Status = 'CLOSED' AND t.PARENT_ID = @ID AND t.ID = d.TASK_ID
	ORDER BY ACTUAL_STOP_DATE,LAST_REQUEST_DATE,ORIG_PLANNED_START_DATE
open @curs
Fetch Next from @curs Into @tID,@tType,@DNR_ID,@stopTime
while (@@fetch_status = 0)
Begin
	
	if dbo.md() = 1
		begin
		print 'Looking at task = ' + @tID + ' which has Type = ' + @tType
		--SELECT t.*,d.* FROM A_TASKS t,A_DNR_TASK_INFO d WHERE t.ID = @tID AND t.ID = d.TASK_ID
		end
	
	if @tType = 'Corrective Action'
		begin
		UPDATE #tempFailingTests SET RETEST_NOW = 1
		UPDATE A_DNR_TASK_INFO SET DNR_STATUS = 'UNKNOWN' WHERE TASK_ID = @tID
		SELECT @numRepairsDoneAtATime = count(ID) FROM A_DNR_TASK_INFO WHERE DNR_ID = @DNR_ID AND TASK_TYPE = 'Corrective Action' AND DNR_STATUS = 'UNKNOWN'
		if dbo.MD() = 1 
			begin
			print 'Num repairs = ' + convert(varchar(50),@numRepairsDoneAtATIME)
			SELECT * FROM A_DNR_TASK_INFO WHERE DNR_ID = @DNR_ID 
--AND TASK_TYPE = 'Corrective Action' 
--AND DNR_STATUS = 'UNKNOWN'

			end
		IF @numRepairsDoneAtATime > 1
			UPDATE A_DNR_TASK_INFO SET DNR_STATUS = 'SHOT_GUN' WHERE DNR_ID = @DNR_ID AND TASK_TYPE = 'Corrective Action' AND DNR_STATUS = 'UNKNOWN'
		end
	
	if @tType = 'test'
		begin
		select @repairTime = ACTUAL_STOP_DATE FROM A_TASKS t,A_DNR_TASK_INFO d 
			WHERE DNR_ID = @DNR_ID AND d.TASK_ID = t.ID AND d.DNR_STATUS = 'UNKNOWN' AND d.TASK_TYPE = 'Corrective Action'
		if dbo.md() = 1 
			begin
			print 'select @repairTime = ACTUAL_STOP_DATE FROM A_TASKS t,A_DNR_TASK_INFO d 
			WHERE DNR_ID = ''' + @DNR_ID + ''' AND d.TASK_ID = t.ID AND d.DNR_STATUS = =''UNKNOWN'' AND d.TASK_TYPE = ''Corrective Action'''
			print 'Last repair Stop Time = ' + isNull(convert(varchar(50),@repairTime),'NULL')
			end
		if @repairTime is not null AND exists(SELECT * FROM #tempFailingTests 
			WHERE STOP_TIME < @repairTime AND 
				ROLL_UP_ID IN (SELECT ROLL_UP_ID FROM A_MONITOR_TEMPLATES WHERE TASK_ID = @tID AND IS_PASSING = 1))
			begin
				if dbo.md() = 1 print 'We have a repair that fixed something '
				UPDATE A_DNR_TASK_INFO SET DNR_STATUS = 'GOOD_REPAIR' WHERE DNR_ID = @DNR_ID AND TASK_TYPE = 'Corrective Action' AND DNR_STATUS = 'UNKNOWN'
			end

		if @repairTime is not null AND exists(SELECT * FROM #tempFailingTests 
			WHERE STOP_TIME < @repairTime AND 
				ROLL_UP_ID IN (SELECT ROLL_UP_ID FROM A_MONITOR_TEMPLATES WHERE TASK_ID = @tID AND IS_PASSING = 0))
			begin
				if dbo.md() = 1 print 'We have a repair that fixed nothing'
				UPDATE A_DNR_TASK_INFO SET DNR_STATUS = 'FAILED_REPAIR' WHERE DNR_ID = @DNR_ID AND TASK_TYPE = 'Corrective Action' AND DNR_STATUS = 'UNKNOWN'
			end


		INSERT INTO #tempFailingTests 
			SELECT ID,TASK_ID,0,ROLL_UP_ID,@stopTime FROM A_MONITOR_TEMPLATES t 
			WHERE TASK_ID = @tID AND IS_PASSING = 0 AND NOT
				(EXISTS(SELECT ID FROM #tempFailingTests f2 WHERE f2.ROLL_UP_ID = t.ROLL_UP_ID))
 		DELETE FROM #tempFailingTests 
			WHERE ROLL_UP_ID IN (SELECT ROLL_UP_ID FROM A_MONITOR_TEMPLATES WHERE TASK_ID = @tID AND IS_PASSING = 1)
		



		end


	Fetch Next from @curs Into @tID,@tType,@DNR_ID,@stopTime
End
close @curs
Deallocate @curs

SELECT f.*,p.HISTORY_REF_ID AS PROC_HIST_ID,p.ID AS PROC_ID,m.DESCRIPTION,m.MONITOR_TYPE,m.MY_ANSWER,m.ID AS MONITOR_ID FROM #tempFailingTests f,A_MONITOR_TEMPLATES m,A_TASKS t,A_PROCEDURES p WHERE
	f.ID = m.ID AND t.ID = f.TASK_ID AND p.ID = t.PROCEDURE_ID












