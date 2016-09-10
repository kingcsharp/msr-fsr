

create     PROCEDURE A_SP_POPFILL_TASK_BY_ID
@taskId varchar(50),
@strNTLogin varchar(50)
AS
SELECT ID, DESCRIPTION 
FROM A_TASKS
WHERE ID = @taskID


