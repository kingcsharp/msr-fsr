











CREATE      PROCEDURE A_SP_TASKS_ACCEPTED_PERSON_UPDATE_TASK
@newID nvarchar(50) OUTPUT,
@messages nvarchar(2000) OUTPUT,
@ID nvarchar(50),
@COMMENT nvarchar(4000),
@DESCRIPTION nvarchar(4000),
@COUNTER varchar(50),
@REF_FILE_LIST varchar(8000),
@SYSTEM_PEOPLE_TO_EMAIL varchar(8000),
@NONSYSTEM_PEOPLE_TO_EMAIL varchar(8000),
@PARENT_ID varchar(50),
@PRIORITY varchar(50),
@SYSTEM_TASK varchar(50),
@REF_PROCEDURES varchar(8000),
@DISCUSSIONS varchar(8000),
@SURVEYS varchar(8000),
@MEETINGS varchar(8000),
@OBJECTS varchar(8000),
@COMPANIES varchar(8000),
@SECURITY_LEVEL varchar(50),
@ORIG_PLANNED_START_DATE dateTime,
@ORIG_PLANNED_STOP_DATE dateTime,
@ORIG_PLANNED_COUNTER_START numeric,
@ORIG_PLANNED_COUNTER_STOP numeric,
@CUR_PLANNED_START_DATE dateTime,
@CUR_PLANNED_STOP_DATE dateTime,
@CUR_PLANNED_COUNTER_START numeric,
@CUR_PLANNED_COUNTER_STOP numeric,
@ACTUAL_START_DATE dateTime,
@ACTUAL_STOP_DATE dateTime,
@ACTUAL_COUNTER_START numeric,
@ACTUAL_COUNTER_STOP numeric,
@strNTLogin varchar(50)
AS


set @ORIG_PLANNED_START_DATE = dbo.timeToGrenich(@ORIG_PLANNED_START_DATE,@strNTLogin)
set @ORIG_PLANNED_STOP_DATE = dbo.timeToGrenich(@ORIG_PLANNED_STOP_DATE,@strNTLogin)
set @CUR_PLANNED_START_DATE = dbo.timeToGrenich(@CUR_PLANNED_START_DATE,@strNTLogin)
set @CUR_PLANNED_STOP_DATE = dbo.timeToGrenich(@CUR_PLANNED_STOP_DATE,@strNTLogin)
set @ACTUAL_START_DATE = dbo.timeToGrenich(@ACTUAL_START_DATE,@strNTLogin)
set @ACTUAL_STOP_DATE = dbo.timeToGrenich(@ACTUAL_STOP_DATE,@strNTLogin)

print 'Updating a Task by an assigned person'
set @newID = @ID
--Update the task with the data he put in
UPDATE A_TASKS SET
PARENT_ID = @PARENT_ID,
DESCRIPTION = @DESCRIPTION,
SYSTEM_TASK = @SYSTEM_TASK,
SECURITY_LEVEL = @SECURITY_LEVEL,
PRIORITY = @PRIORITY,
COMMENT = @COMMENT,
MODBY = @strNTLogin,
COUNTER = @COUNTER,
DRCM = getDate()
WHERE ID = @newID

print 'Updating Dates'
exec A_SP_TASK_DATES_UPDATE @newID,'ORIG_PLANNED_START',@ORIG_PLANNED_START_DATE,@strNTLogin
exec A_SP_TASK_DATES_UPDATE @newID,'ORIG_PLANNED_STOP',@ORIG_PLANNED_STOP_DATE,@strNTLogin
exec A_SP_TASK_DATES_UPDATE @newID,'CUR_PLANNED_START',@CUR_PLANNED_START_DATE,@strNTLogin
exec A_SP_TASK_DATES_UPDATE @newID,'CUR_PLANNED_STOP',@CUR_PLANNED_STOP_DATE,@strNTLogin
exec A_SP_TASK_DATES_UPDATE @newID,'ACTUAL_START',@ACTUAL_START_DATE,@strNTLogin
exec A_SP_TASK_DATES_UPDATE @newID,'ACTUAL_STOP',@ACTUAL_STOP_DATE,@strNTLogin

print 'Updating Counters'
exec A_SP_TASK_COUNTER_UPDATE @newID,'ORIG_PLANNED_START',@ORIG_PLANNED_COUNTER_START,@strNTLogin
exec A_SP_TASK_COUNTER_UPDATE @newID,'ORIG_PLANNED_STOP',@ORIG_PLANNED_COUNTER_STOP,@strNTLogin
exec A_SP_TASK_COUNTER_UPDATE @newID,'CUR_PLANNED_START',@CUR_PLANNED_COUNTER_START,@strNTLogin
exec A_SP_TASK_COUNTER_UPDATE @newID,'CUR_PLANNED_STOP',@CUR_PLANNED_COUNTER_STOP,@strNTLogin
exec A_SP_TASK_COUNTER_UPDATE @newID,'ACTUAL_START',@ACTUAL_COUNTER_START,@strNTLogin
exec A_SP_TASK_COUNTER_UPDATE @newID,'ACTUAL_STOP',@ACTUAL_COUNTER_STOP,@strNTLogin

print 'Getting the latest and greatest dates and putting them in the main table'
exec A_SP_TASK_UPDATE_ALL_DATES @newID,@strNTLogin

print 'Getting the latest and greatest counters and putting them in the main table'
exec A_SP_TASK_UPDATE_ALL_COUNTERS @newID,@strNTLogin

Declare @curs Cursor
Declare @it nvarchar(50)
CREATE TABLE #TempItems	(IT varchar(50))


print 'Updating Reference File List'
DELETE FROM A_TASK_REF_FILES WHERE TASK_ID = @newID
print 'making a cursor to go through the string'
DELETE FROM  #TempItems
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @REF_FILE_LIST,', '
set @curs = Cursor For SELECT * FROM #TempItems
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	print 'Adding Reference File = ' + @it
	INSERT INTO A_TASK_REF_FILES(ID,TASK_ID,FILE_ID,DRCM,MODBY)
	VALUES (newID(),@newID,ltrim(@it),getDate(),@strNTLogin)
	Fetch Next from @curs Into @it
End close @curs
Deallocate @curs

print 'Updating People To Email List'
DELETE FROM A_TASK_EMAIL_PEOPLE_LINK WHERE TASK_ID = @newID
print 'making a cursor to go through the string'
DELETE FROM  #TempItems
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @SYSTEM_PEOPLE_TO_EMAIL,', '
set @curs = Cursor For SELECT * FROM #TempItems
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	print 'Adding Link to Object = ' + @it
	INSERT INTO A_TASK_EMAIL_PEOPLE_LINK(ID,TASK_ID,PERSON_ID,DRCM,MODBY)
	VALUES (newID(),@newID,ltrim(@it),getDate(),@strNTLogin)
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs



print 'Updating Disussion List'
print 'First delete all the ones we used to have'
DELETE FROM A_TASK_DISCUSSION_LINK WHERE TASK_ID = @newID
print 'making a cursor to go through the string'
DELETE FROM  #TempItems
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @DISCUSSIONS,', '
set @curs = Cursor For SELECT * FROM #TempItems
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	print 'Adding Reference File = ' + @it
	INSERT INTO A_TASK_DISCUSSION_LINK(ID,TASK_ID,DISCUSSION_ID,DRCM,MODBY)
	VALUES (newID(),@newID,ltrim(@it),getDate(),@strNTLogin)
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs

print 'Updating Survey List'
print 'First delete all the ones we used to have'
DELETE FROM A_TASK_SURVEY_LINK WHERE TASK_ID = @newID
print 'making a cursor to go through the string'
DELETE FROM  #TempItems
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @SURVEYS,', '
set @curs = Cursor For SELECT * FROM #TempItems
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	print 'Adding Link to Survey = ' + @it
	INSERT INTO A_TASK_SURVEY_LINK(ID,TASK_ID,SURVEY_ID,DRCM,MODBY)
	VALUES (newID(),@newID,ltrim(@it),getDate(),@strNTLogin)
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs

print 'Updating Meeting List'
print 'First delete all the ones we used to have'
DELETE FROM A_TASK_MEETING_LINK WHERE TASK_ID = @newID
print 'making a cursor to go through the string'
DELETE FROM  #TempItems
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @MEETINGS,', '
set @curs = Cursor For SELECT * FROM #TempItems
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	print 'Adding Link to Meeting= ' + @it
	INSERT INTO A_TASK_MEETING_LINK(ID,TASK_ID,MEETING_ID,DRCM,MODBY)
	VALUES (newID(),@newID,ltrim(@it),getDate(),@strNTLogin)
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs

print 'Updating Object List'
print 'First delete all the ones we used to have'
DELETE FROM A_TASK_OBJECT_LINK WHERE TASK_ID = @newID
print 'making a cursor to go through the string'
DELETE FROM  #TempItems
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @OBJECTS,', '
set @curs = Cursor For SELECT * FROM #TempItems
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	print 'Adding Link to Object = ' + @it
	INSERT INTO A_TASK_OBJECT_LINK(ID,TASK_ID,OBJECT_ID,DRCM,MODBY)
	VALUES (newID(),@newID,ltrim(@it),getDate(),@strNTLogin)
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs
print 'Updating Companies to View List'
print 'First delete all the ones we used to have'
DELETE FROM A_TASK_COMPANIES_TO_VIEW_LINK WHERE TASK_ID = @newID
print 'making a cursor to go through the string'
DELETE FROM  #TempItems
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @COMPANIES,', '
set @curs = Cursor For SELECT * FROM #TempItems
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	print 'Adding Link to Object = ' + @it
	INSERT INTO A_TASK_COMPANIES_TO_VIEW_LINK(ID,TASK_ID,CO_ID,DRCM,MODBY)
	VALUES (newID(),@newID,ltrim(@it),getDate(),@strNTLogin)
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs



exec A_SP_TASK_UPDATE_PURPOSE_OBJECTS_PROJECTS @newID,@strNTLogin


exec A_SP_TASKS_UPDATE_DATA @ID,@strNTlogin





