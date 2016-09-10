

CREATE  PROCEDURE A_SP_TASK_GET_DATA
@ID nvarchar(50),
@strNTLogin nvarchar(50)
AS
declare @hasUnfinishedChildren tinyint,@hasChildren tinyInt
if exists(SELECT ID FROM A_TASKS WHERE PARENT_ID = @ID AND STATUS NOT IN ('FINISHED','COMPLETED','CLOSED','COMPLETE'))
	set @hasUnfinishedChildren = 1
else
	set @hasUnfinishedChildren = 0

if exists(SELECT ID FROM A_TASKS WHERE PARENT_ID = @ID)
	set @hasChildren = 1
else
	set @hasChildren = 0


SELECT *,@hasUnfinishedChildren as UNFINISHED_CHILDREN,@hasChildren as HAS_CHILDREN FROM A_V_TASK_EDIT_DATA WHERE ID = @ID


