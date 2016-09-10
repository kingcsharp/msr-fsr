





CREATE      PROCEDURE A_SP_TASK_UPDATE_ALL_DATES
@taskID varchar(50),
@strNTLogin varchar(50)
AS
declare @o1 as dateTime
declare @o2 as dateTime
declare @p1 as dateTime
declare @p2 as dateTime
declare @a1 as dateTime
declare @a2 as dateTime
SELECT @o1 = THE_DATE FROM A_TASK_DATES WHERE TASK_ID = @taskID AND DATE_TYPE = 'ORIG_PLANNED_START' AND IS_CURRENT = 1
SELECT @o2 = THE_DATE FROM A_TASK_DATES WHERE TASK_ID = @taskID AND DATE_TYPE = 'ORIG_PLANNED_STOP'  AND IS_CURRENT = 1
SELECT @p1 = THE_DATE FROM A_TASK_DATES WHERE TASK_ID = @taskID AND DATE_TYPE = 'CUR_PLANNED_START'  AND IS_CURRENT = 1
SELECT @p2 = THE_DATE FROM A_TASK_DATES WHERE TASK_ID = @taskID AND DATE_TYPE = 'CUR_PLANNED_STOP'  AND IS_CURRENT = 1
SELECT @a1 = THE_DATE FROM A_TASK_DATES WHERE TASK_ID = @taskID AND DATE_TYPE = 'ACTUAL_START'  AND IS_CURRENT = 1
SELECT @a2 = THE_DATE FROM A_TASK_DATES WHERE TASK_ID = @taskID AND DATE_TYPE = 'ACTUAL_STOP'  AND IS_CURRENT = 1
-- print 'Updating Task ' + isNULL(@taskID,'NULL') + ' with the following dates'
-- print 'OPS = ' + convert(varchar(50),@o1)
-- print 'OPSt = ' + convert(varchar(50),@o2)
-- print 'CPS = ' + convert(varchar(50),@p1)
-- print 'CPSt = ' + convert(varchar(50),@p2)
-- print 'AS = ' + convert(varchar(50),@a1)
-- print 'ASt = ' + convert(varchar(50),@a2)
 
UPDATE A_TASKS SET
ORIG_PLANNED_START_DATE = @o1,
ORIG_PLANNED_STOP_DATE = @o2,
CUR_PLANNED_START_DATE = @p1,
CUR_PLANNED_STOP_DATE = @p2,
ACTUAL_START_DATE = @a1,
ACTUAL_STOP_DATE = @a2
WHERE ID = @taskID








