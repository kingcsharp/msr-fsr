
CREATE PROCEDURE dbo.A_SP_ACTUAL_PARTS_SEARCH_PARTS_FROM_CALL
@taskID varchar(50),
@strWhere nvarchar(200),
@strSort varchar(1000),
@strNTLogin varchar(50)
AS
declare @callRootTaskID varchar(50),@purchHistID varchar(50),@sql nvarchar(4000)
exec A_SP_TASK_GET_CALL_ROOT_TASK_ID @callRootTaskID OUTPUT,@taskID
print 'The root task ID = ' + @callRootTaskID
SELECT @purchHistID = PURCHASE_HIST_ID FROM A_TASK_ORDER_INFORMATION WHERE TASK_ID = @callRootTaskID

set @sql = 'SELECT * FROM A_V_ACTUAL_PARTS_CALL_DATA_COMPLETE WHERE ROOT_TASK = ''' + @callRootTaskID + ''' '
set @sql = @sql + isNull(' AND ' + @strWhere,'')
set @sql = @sql + isNull(@strSort,'')

exec(@sql)



