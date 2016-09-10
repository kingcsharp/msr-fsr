








CREATE           PROCEDURE [dbo].[A_SP_ADMIN_SQL_TO_RUN_EXECUTE]
AS

DELETE FROM A_ADMIN_SQL_TO_RUN WHERE CODE LIKE '%A_SP_TASK_CHECK_RECURSION_STATUS%'
DELETE FROM A_ADMIN_SQL_TO_RUN WHERE CODE = 'exec A_SP_PEOPLE_COMPLETELY_UPDATE_SUBORDINATES_TABLE'

--if (not(exists(SELECT ID FROM A_ADMIN_CONFIGURATION WHERE NAME = 'QUE_ON' AND VAL = '1')))
--	goto fin
declare @runID varchar(50),@successCount int,@failCount int
set @successCount = 0
set @failCount = 0
SELECT @runID = newID()
INSERT INTO A_ADMIN_SQL_TO_RUN_STATS (ID,START_TIME)
	values(@runID,getDate())
declare @exCnt int
SELECT @exCnt = COUNT(ID) FROM A_ADMIN_SQL_TO_RUN WHERE STATUS = 'EXECUTING'
declare @runCnt int
SELECT @runCnt = COUNT(ID) FROM A_ADMIN_SQL_TO_RUN WHERE STATUS = 'RUNNING'
if (@runCnt = 0 and @exCnt > 0)
	UPDATE A_ADMIN_SQL_TO_RUN SET STATUS = 'WAITING' WHERE STATUS = 'EXECUTING'
SELECT @exCnt = COUNT(ID) FROM A_ADMIN_SQL_TO_RUN WHERE STATUS = 'EXECUTING'

if @exCnt > 0
	begin
	print 'Not Running.. Too many running'
	UPDATE A_ADMIN_SQL_TO_RUN SET STATUS = 'RAN_2_LONG' WHERE 
		STATUS = 'RUNNING' AND 
		(
		RUN_TIME < dateAdd(n,-3,getDate())
		)
	declare @lastRunTime datetime,@runGroup varchar(50)
	SELECT top 1 @lastRunTime = RUN_TIME,@runGroup = RUN FROM A_ADMIN_SQL_TO_RUN WHERE STATUS = 'RUNNING'
	print 'Been running for'
	print (dateDiff(n,@lastRunTime,getDate()))
	if dateDiff(n,@lastRunTime,getDate()) > 1
		begin
		UPDATE A_ADMIN_SQL_TO_RUN SET STATUS = 'PBRAN_2_LONG' WHERE RUN = @runGroup AND STATUS in ('RUNNING') AND ATTEMPT > 2
		UPDATE A_ADMIN_SQL_TO_RUN SET STATUS = 'WAITING' WHERE RUN = @runGroup AND STATUS in ('EXECUTING','RUNNING')
		end
	goto fin
	end
print 'Getting some stuff to run'
create table #tempTable (ID varchar(50))
INSERT INTO #tempTable SELECT TOP 30 ID FROM A_ADMIN_SQL_TO_RUN WHERE STATUS = 'WAITING' AND (RUN_AT_DATE IS NULL or RUN_AT_DATE <= getDate())
UPDATE A_ADMIN_SQL_TO_RUN SET STATUS = 'EXECUTING',RUN = @runID,RUN_TIME=getDate() WHERE ID IN
	(SELECT ID FROM #tempTable)
declare @curs CURSOR,@it varchar(50),@sql varchar(7500),@ErrorMsgID int,@stat varchar(50)
set @curs = 
	CURSOR FOR 
		SELECT ID,CODE 
		FROM A_ADMIN_SQL_TO_RUN 
		WHERE ID IN (SELECT ID FROM #tempTable) 
		ORDER BY DRCM

open @curs
fetch next from @curs into @it,@sql
while @@fetch_status = 0
	begin
	print 'Running SQL = ' + isNull(@sql,'null')
	if @sql is not null
		begin
		UPDATE A_ADMIN_SQL_TO_RUN SET 
			STATUS='RUNNING',RUN_TIME=getDate(), ATTEMPT = isNULL(ATTEMPT,0) + 1
			WHERE ID = @it
		exec(@sql)
		SET @ErrorMsgID =@@ERROR
		IF @ErrorMsgID <>0
			BEGIN
			set @failCount = @failCount + 1
			INSERT INTO A_ADMIN_SQL_TO_RUN_ERRORS (ID,ERROR_INFO)
				values(@it,@ErrorMsgID)
			set @stat = 'FAILED'
			END
		else
			begin
			set @stat = 'SUCCESS'
			set @successCount = @successCount + 1
			end
		UPDATE A_ADMIN_SQL_TO_RUN SET 
		STATUS=@stat,
		FINISH_TIME = getDate()
		WHERE ID = @it
		end
	fetch next from @curs into @it,@sql
	end



UPDATE A_ADMIN_SQL_TO_RUN_STATS SET 
SUCCESSES = @successCount,
FAILURES = @failCount,
FINISH_TIME = getDate() WHERE ID = @runID



fin:







