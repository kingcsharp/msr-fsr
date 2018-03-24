








/*
STORED PROCEDURE CALLED IN worktypes/editServiceCalls.asp

*/
CREATE                               PROCEDURE A_SP_SERVICE_CALL_UPDATE_ONE_WEEKLY_REPORT
@newID varchar(50) OUTPUT,
@msg varchar(1000) OUTPUT,
@weeklyID varchar(50),
@startDate datetime,
@machineName varchar(1000),
@workType varchar(50),
@expensesDescription nvarchar(4000),
@expensesAmount nvarchar(50),
@orderNumber varchar(50),
@comment varchar(4000),
@refFiles varchar(4000),
@allNormalHours varchar (1000),
@allOverHours varchar (1000),
@empID varchar(50),
@strNTLogin varchar(50)
AS
print 'inside update A_SP_SERVICE_CALL_UPDATE_ONE_WEEKLY_REPORT'

-- if not(exists(SELECT * FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @empID AND (ID = @strNTLogin or BOSS = @strNTLogin)))
-- 	begin
-- 	print 'Error You are not allowed to submit a card for this person'
-- 	goto fin
-- 	end



declare @startDay int,@sql varchar(8000), @startMonth int, @startYear int,@exAmount money,@workerID varchar(50),
	@hourRate money, @otRate money

set @startDay = DAY(@startDate)
set @startMonth = MONTH(@startDate)
set @startYear = YEAR(@startDate)
set @exAmount = convert(money, @expensesAmount,2)

if @weeklyID is null
begin
	print 'Grabbing normal rate and ot rate to make them fixed. ONLY FOR NEW WORKTYPES.'
	SELECT @hourRate = HOUR_RATE FROM A_SERVICE_CALLS_WORK_TYPES WHERE ID = @workType
	SELECT @otRate = OT_RATE FROM A_SERVICE_CALLS_WORK_TYPES WHERE ID = @workType
	set @workerID = @empID
 	print 'we are making a New weekly report'
 	exec SP_GETUNIQUEID3 @newID OUTPUT 
	INSERT INTO A_SERVICE_CALLS_WEEKLY_REPORTS 
	([ID], STATUS , HOUR_RATE_FIXED, OT_RATE_FIXED, WORKER_ID, REASON_TYPE)
	VALUES(@newID, 'WORKER', @hourRate, @otRate, @workerID, 'FORWARD')
	set @weeklyID=@newID
	print 'Created a new serviceCall with the id of ' + @weeklyID
end 
else set @newID = @weeklyID

print 'get the current worker'
SELECT @workerID = WORKER_ID FROM A_SERVICE_CALLS_WEEKLY_REPORTS WHERE ID = @weeklyID
print 'we are updating the serviceCall with serviceCall id of ' + @weeklyID
UPDATE A_SERVICE_CALLS_WEEKLY_REPORTS 
	set 
	WORKER_ID = @workerID ,
	MACHINE_NAME = @machineName,
	COMMENTS = @comment,
	START_DAY = @startDay,
	START_MONTH = @startMonth,
	START_YEAR= @startYear,
	WORK_TYPE = @workType,
	EXPENSES_DESCRIPTION = @expensesDescription,
	EXPENSES_AMOUNT = @exAmount,
	ORDER_NUMBER = @orderNumber,
	DRCM = getDate(),
	MODBY = @strNTLogin,
	ACTUAL_START_DATE = @startDate
	WHERE ID = @weeklyID 

CREATE TABLE #TempItems (IT varchar(50))
print 'Deleting then adding reference files'
DELETE FROM A_SERVICE_CALLS_ATTACHMENTS WHERE WEEKLY_ID = @weeklyID
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @refFiles,','
INSERT INTO A_SERVICE_CALLS_ATTACHMENTS (ID,WEEKLY_ID,DOC_ID,DRCM,MODBY)
	SELECT newID(),@weeklyID,ltrim(IT),getDate(),@strNTlogin FROM #TempItems
DELETE FROM #TempItems


declare @isStatusID varchar(50)
SELECT @isStatusID = ID FROM A_SERVICE_CALLS_STATUS_HISTORY WHERE WEEKLY_ID = @weeklyID
IF @isStatusID is null 
	begin
	INSERT INTO A_SERVICE_CALLS_STATUS_HISTORY
	(ID, WEEKLY_ID, STATUS_CHANGED_TO, CHANGED_BY, REASON,REASON_TYPE, DATE_CHANGED, DRCM, MODBY)
	VALUES(newid(), @weeklyID, 'WORKER', @strNTlogin, NULL, 'FORWARD', getdate(), getdate(), @strNTlogin) 
	end

declare @normalHours real, @overHours real, @totalHours real, @nt0 real, @nt1 real, @nt2 real,
	@nt3 real, @nt4 real, @nt5 real, @nt6 real, @ot0 real, @ot1 real, @ot2 real, @ot3 real,
	@ot4 real, @ot5 real, @ot6 real

print 'we are now deleteing the normal and over time hours with the id of' + @weeklyID
DELETE FROM A_SERVICE_CALL_WORK_TIME WHERE WEEKLY_ID = @weeklyID
print 'making a cursor to go through the string'
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @allNormalHours,';'
Declare @it varchar(50), @curs Cursor, @ctr int, @currentDate varchar(50), @currentDay int, @currentMonth int, @currentYear int
set @ctr = 0
set @curs = Cursor For SELECT IT FROM #TempItems
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	set @currentDate = DATEADD(d, @ctr,@startDate) 
	INSERT INTO A_SERVICE_CALL_WORK_TIME
	(ID, WEEKLY_ID, D, MO, YR, HOURS, HOUR_TYPE, DRCM,MODBY)
	VALUES (newID(),@weeklyID,DAY(@currentDate), MONTH(@currentDate), YEAR(@currentDate),ltrim(@it),'NORMAL',getDate(), @strNTLogin)	
	set @sql = isNull(@sql + ',','') + ' NT_' + convert(nvarchar(40),@ctr) + ' = ' + convert(varchar(10),isNull(@it,0))
	set @normalHours = isNull(@normalHours,0) + isNull(@it,0)
	set @ctr = @ctr +1 
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs

DELETE FROM #TempItems
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @allOverHours,';'
set @ctr = 0
set @curs = Cursor For SELECT * FROM #TempItems
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	set @currentDate = DATEADD(d, @ctr,@startDate) 
	print 'Adding over hour = ' + convert(varchar(50), @it)
	INSERT INTO A_SERVICE_CALL_WORK_TIME
	(ID, WEEKLY_ID, D, MO, YR, HOURS, HOUR_TYPE, DRCM,MODBY)
	VALUES (newID(),@weeklyID,DAY(@currentDate), MONTH(@currentDate), YEAR(@currentDate),ltrim(@it),'OVER',getDate(), @strNTLogin)	
	set @sql = isNull(@sql + ',','') + ' OT_' + convert(nvarchar(10),@ctr) + ' = ' + convert(nvarchar(10),isNull(@it,0))
	set @overHours = isNull(@overHours,0) + isNull(@it,0)
	set @ctr = @ctr +1 
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs


print 'We are inserting the totals from A_V_SERVICE_CALLS_TOTALS to A_SERVICE_CALLS_TOTALS to make things run faster.'
print 'Delete my old records in A_SERVICE_CALL_TOTAL_HOURS'

DELETE FROM A_SERVICE_CALLS_TOTAL_HOURS
WHERE WEEKLY_ID = @weeklyID

declare @totSQL varchar(4000),@totID varchar(50)
select @totID = newID()

INSERT INTO A_SERVICE_CALLS_TOTAL_HOURS
(ID,WEEKLY_ID, NORMAL_HOURS, OT_HOURS, TOTAL_HOURS)
VALUES
(@totID,@weeklyID,@normalHours,@overHours,@normalHours+@overHours)

set @totSQL = 'UPDATE A_SERVICE_CALLS_TOTAL_HOURS 
SET ' + @sql + ' WHERE ID = ''' + @totID + ''''
print 'Tot SQL = '
print @totSQL
exec(@totSQL)









fin: