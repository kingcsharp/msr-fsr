CREATE PROCEDURE dbo.A_SP_TASK_UPDATE_PURPOSE_OBJECTS_PROJECTS 
@ID varchar(50),
@strNTLogin varchar(50)
AS
Declare @it nvarchar(50)
Declare @curs Cursor
CREATE TABLE #TempItems	(IT varchar(50))

DELETE FROM #TempITems
INSERT INTO #TempItems SELECT DISCUSSION_ID 
	FROM A_TASK_DISCUSSION_LINK WHERE TASK_ID = @ID
set @curs = Cursor For SELECT * FROM #TempItems
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	exec A_SP_PROJECT_COPY_ONE_ITEMS_PROJ_OBJ_PURP_TAGS
	@it,'A_DISCUSSIONS',@ID,'A_TASKS',@strNTLogin
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs

DELETE FROM #TempITems
INSERT INTO #TempItems SELECT SURVEY_ID 
	FROM A_TASK_SURVEY_LINK WHERE TASK_ID = @ID
set @curs = Cursor For SELECT * FROM #TempItems
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	exec A_SP_PROJECT_COPY_ONE_ITEMS_PROJ_OBJ_PURP_TAGS
	@it,'A_SURVEYS',@ID,'A_TASKS',@strNTLogin
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs

DELETE FROM #TempITems
INSERT INTO #TempItems SELECT MEETING_ID 
	FROM A_TASK_MEETING_LINK WHERE TASK_ID = @ID
set @curs = Cursor For SELECT * FROM #TempItems
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	exec A_SP_PROJECT_COPY_ONE_ITEMS_PROJ_OBJ_PURP_TAGS
	@it,'A_MEETINGS',@ID,'A_TASKS',@strNTLogin
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs

exec A_SP_PROJECT_ITEM_CLEAN_UP_ALL_TAGS @ID,'A_TASKS',@strNTLogin




