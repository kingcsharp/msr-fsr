




CREATE FUNCTION [dbo].[findFillForTask](@taskID varchar(50))
RETURNS nvarchar(4000)
AS
BEGIN
declare @pID varchar(50),@fillID varchar(50), @cnt int
SELECT @pID = PARENT_ID FROM A_TASKS WHERE ID = @taskID
set @cnt = 0
while @pID is not null and @fillID is null and @cnt < 20
begin
set @cnt = @cnt + 1
SELECT @fillID = FILL_ITEM_ID FROM A_TASK_ORDER_INFORMATION WHERE TASK_ID = @pID
SELECT @pID = PARENT_ID FROM A_TASKS WHERE ID = @pID
end
return(@fillID)
END







