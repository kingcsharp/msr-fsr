






CREATE      PROCEDURE dbo.A_SP_DNR_FIND_TESTS_FROM_SIMILAR_CASES
@ID varchar(50)
AS


CREATE TABLE #procsToIgnore(ROLL_UP_ID VARCHAR(50))
CREATE TABLE #tempMatchCases(ID VARCHAR(50))
CREATE TABLE #matchCases(ID VARCHAR(50),DROPPED_TESTS int,DROPPED_TEST_ID varchar(50))


declare @procCurs as cursor,@rid varchar(50),@droppedTests int
set @droppedTests = 0
set @procCurs = 
	CURSOR FOR 
		SELECT ROLL_UP_ID
			FROM
			A_V_DNR_TASKS_WITH_TEST_INFO
			WHERE
			DNR_ID = @ID AND IS_PASSING = 0
			ORDER BY ACTUAL_STOP_DATE DESC

open @procCurs
fetch next from @procCurs into @rid
while (@@fetch_status = 0)
	Begin
	set @droppedTests = @droppedTests + 1
	INSERT INTO #procsToIgnore (ROLL_UP_ID) values(@rid)
	INSERT INTO #tempMatchCases exec A_SP_DNR_PERMUTATION_FIND_MATCHING_CASES_PART_2 @ID
	INSERT INTO #matchCases SELECT ID,@droppedTests,@rid FROM #tempMatchCases t 
		WHERE NOT EXISTS (SELECT ID FROM #matchCases WHERE ID = t.ID)
	if dbo.MD() = 1
		begin
		SELECT 'procsToIgnore',i.ROLL_UP_ID,m.DESCRIPTION FROM #procsToIgnore i,A_MONITOR_TEMPLATES m
			WHERE m.ID = i.ROLL_UP_ID
		SELECT 'tempMAtching Cases',* FROM #tempMatchCases
		SELECT 'matching Cases',* FROM #matchCases
		end

	DELETE FROM #tempMatchCases
	fetch next from @procCurs into @rid
	end





create TABLE #CASES (ID varchar(50),DROPPED_TESTS int,DROPPED_TEST_ID varchar(50))
INSERT INTO #CASES SELECT DISTINCT ID,DROPPED_TESTS,DROPPED_TEST_ID FROM #matchCases ORDER BY DROPPED_TESTS


if dbo.MD() = 1 SELECT 'CASES',* FROM #CASES c,A_TASKS t WHERE t.ID = c.ID
declare @totCases int
SELECT @totCases = count(ID) FROM #CASES

--Now we need to find the failing tests that we have not done on all the matching cases.
--Failing Tests will find all the tests on similar cases that were failing at one point during the case
CREATE TABLE #FAILING_TESTS 
	(PARENT_ID varchar(50),TASK_ID varchar(50),P_ID varchar(50),
	P_HiST_ID varchar(50),P_NAME varchar(3000),DROPPED_TEST_ID varchar(50),
	DROPPED_TEST_NAME varchar(1000),NUM_DROPPED_TESTS int)

INSERT INTO #FAILING_TESTS
	(PARENT_ID,TASK_ID,P_ID,P_HiST_ID,P_NAME,DROPPED_TEST_ID,
	DROPPED_TEST_NAME,NUM_DROPPED_TESTS)
	SELECT DISTINCT t.PARENT_TASK_ID,t.TASK_ID,t.PROCEDURE_ID,t.PROC_HIST_ID,
		t.PROC_NAME,c.DROPPED_TEST_ID,m.DESCRIPTION,c.DROPPED_TESTS
	FROM A_V_DNR_TASKS_WITH_TEST_INFO t,#CASES c,A_MONITOR_TEMPLATES m
	WHERE t.IS_PASSING = 0 AND 
	t.PARENT_TASK_ID = c.ID AND
	m.ID = c.DROPPED_TEST_ID and
--	TASK_ID IN (SELECT ID FROM A_TASKS WHERE PARENT_ID IN (SELECT ID FROM #CASES)) AND
	t.ROLL_UP_ID not in (SELECT ROLL_UP_ID FROM A_V_DNR_TASKS_WITH_TEST_INFO WHERE PARENT_TASK_ID = @ID)


if dbo.MD() = 1 SELECT 'Failing Procs',* FROM #FAILING_TESTS


SELECT DISTINCT P_ID,P_HIST_ID,P_NAME,NUM_DROPPED_TESTS,DROPPED_TEST_ID,DROPPED_TEST_NAME FROM #FAILING_TESTS ORDER BY NUM_DROPPED_TESTS









