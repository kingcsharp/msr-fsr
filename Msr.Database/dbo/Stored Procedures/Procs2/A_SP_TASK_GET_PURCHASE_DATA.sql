CREATE PROCEDURE dbo.A_SP_TASK_GET_PURCHASE_DATA
@taskID varchar(50)
AS
declare @purchID varchar(50)
exec A_SP_TASK_GET_CALL_ROOT_TASK_ID @purchID OUTPUT,@taskID
print 'The purchase task ID = ' + @purchID
SELECT * FROM A_V_TASK_PURCHASE_INFORMATION WHERE ID = @purchID
