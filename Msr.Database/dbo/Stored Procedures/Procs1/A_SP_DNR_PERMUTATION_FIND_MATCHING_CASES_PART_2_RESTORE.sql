




CREATE PROCEDURE dbo.A_SP_DNR_PERMUTATION_FIND_MATCHING_CASES_PART_2_RESTORE
@ID varchar(50)
AS

CREATE TABLE #allTests 
	(PROC_ID varchar(50),ROLL_UP_ID varchar(50),MY_ANSWER varchar(2000),IS_PASSING tinyint,
	ACTUAL_STOP_DATE dateTime,USE_RESULT tinyint,MONITOR_TYPE varchar(50)
	)

 INSERT INTO #allTests 
 	SELECT PROCEDURE_ID,ROLL_UP_ID,MY_ANSWER,IS_PASSING,ACTUAL_STOP_DATE,USE_RESULT,MONITOR_TYPE
 	FROM
 	A_V_DNR_TASKS_WITH_TEST_INFO
 	WHERE
 	DNR_ID = @ID
 	AND ROLL_UP_ID NOT IN (SELECT ROLL_UP_ID FROM #procsToIgnore)

if dbo.md() = 1 
	begin
print 'here'
		SELECT PROC_ID  from #allTests
print 'here'
	end
CREATE TABLE #DNRcases (CASE_ID varchar(50))

--Get all the DNR's that contain all the tests we have done on this case
INSERT INTO #DNRcases
	SELECT ID FROM A_TASKS DNR 
		WHERE STATUS IN ('FINISHED','CLOSED','COMPLETED') AND ID <> @ID AND SYSTEM_TASK = 'SYS_DNR' AND EXISTS 
		(
		SELECT ID FROM A_TASKS t WHERE t.PARENT_ID = DNR.ID 
			AND t.PROCEDURE_ID IN 
			(
			SELECT PROC_ID FROM #allTests
			)
		)

if dbo.md() = 1 SELECT 'All Matching Cases',* from #DNRCases

declare @curs1 as cursor,@pid varchar(50),@rid varchar(50),@myAns varchar(50),
		@isPassing tinyInt,@actStopDate dateTime,@useRes tinyInt,@monType varchar(50)
set @curs1 = 
		CURSOR FOR 
			SELECT PROC_ID,ROLL_UP_ID,MY_ANSWER,IS_PASSING,ACTUAL_STOP_DATE,USE_RESULT,MONITOR_TYPE
			FROM #allTests 
			ORDER BY ACTUAL_STOP_DATE



open @curs1
fetch next from @curs1 into @pid,@rid,@myAns,@isPassing,@actStopDate,@useRes,@monType
while (@@fetch_status = 0)
	Begin
	if dbo.md() = 1 print '@pid = ' + @pid + ' rid = ' + @rid + ' isPassing = ' + convert(varchar(50),@isPassing) + ' myAnswer = ' + isNull(@myAns,'NULL')
	if @useRes = 0
		begin
		print 'We are not using the result to delete some Cases isPassing = ' + convert(nvarchar(10),@isPassing)
		if @isPassing = 1
			begin
			if dbo.md() = 1 
				begin
				SELECT 'CASES REMOVED BECAUSE TEST IS PASSING',*
				FROM #DNRcases WHERE 
				Exists
				(
				SELECT d1.* FROM A_V_DNR_TASKS_WITH_TEST_INFO d1
					WHERE 
						d1.PROCEDURE_ID = @pid AND
						d1.ROLL_UP_ID = @rid AND 
						d1.PARENT_TASK_ID = CASE_ID AND
						d1.IS_PASSING = 0
				)
				end
			DELETE FROM #DNRcases WHERE 
			Exists
			(
				SELECT d1.* FROM A_V_DNR_TASKS_WITH_TEST_INFO d1
					WHERE 
						d1.PROCEDURE_ID = @pid AND
						d1.ROLL_UP_ID = @rid AND 
						d1.PARENT_TASK_ID = CASE_ID AND
						d1.IS_PASSING = 0
			)
			end
		else
			begin
			DELETE FROM #DNRcases WHERE 
				not Exists
				(
				SELECT * FROM A_V_DNR_TASKS_WITH_TEST_INFO 
				WHERE 
					PROCEDURE_ID = @pid AND
					ROLL_UP_ID = @rid AND 
					PARENT_TASK_ID = CASE_ID AND
					IS_PASSING = @isPassing
				)
			end
		end
	else
		begin
		print 'We are using the result to delete some Cases'
		if @monType = 'TEXT'
			begin
			DELETE FROM #DNRcases WHERE 
			not Exists
			(
			SELECT * FROM A_V_DNR_TASKS_WITH_TEST_INFO 
			WHERE 
				PARENT_TASK_ID = CASE_ID AND
				PROCEDURE_ID = @pid AND
				ROLL_UP_ID = @rid AND 
				IS_PASSING = @isPassing AND
				MY_ANSWER in 
					(SELECT SIMILAR_TEXT FROM A_MONITOR_TEXT_THESAURUS 
					WHERE MAIN_TEXT =  @myAns AND MON_ROLL_UP_ID = @rid)
			)			
			end
		else
			begin
			print 'My Answer is  ' + @myAns
			DELETE FROM #DNRcases WHERE 
			not Exists
			(
			SELECT * FROM A_V_DNR_TASKS_WITH_TEST_INFO 
			WHERE 
				PARENT_TASK_ID = CASE_ID AND
				PROCEDURE_ID = @pid AND
				ROLL_UP_ID = @rid AND 
				IS_PASSING = @isPassing AND
				MY_ANSWER = @myAns
			)
			end
		end
	fetch next from @curs1 into @pid,@rid,@myAns,@isPassing,@actStopDate,@useRes,@monType
	End
close @curs1
Deallocate @curs1




SELECT CASE_ID AS [ID] FROM #DNRcases
