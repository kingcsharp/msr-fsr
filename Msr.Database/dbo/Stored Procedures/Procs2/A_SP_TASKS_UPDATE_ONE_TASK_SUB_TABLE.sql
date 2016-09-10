


CREATE   PROCEDURE dbo.A_SP_TASKS_UPDATE_ONE_TASK_SUB_TABLE
@ID varchar(50),
@lev smallint
AS
--Delete all places where I am the child
DELETE FROM A_TASKS_RELATION_TABLE WHERE CHILD_ID = @ID
--Add me as my own subordinate
INSERT INTO A_TASKS_RELATION_TABLE (ID,PARENT_ID,CHILD_ID,LEV)
	VALUES (newID(),@ID,@ID,@lev)
--run through the list and add all the parents to the top
declare @curParent as varchar(50)
select @curParent = PARENT_ID FROM A_TASKS WHERE ID = @ID
while @curParent is not null
	begin
	set @lev = @lev + 1
	INSERT INTO A_TASKS_RELATION_TABLE (ID,PARENT_ID,CHILD_ID,LEV)
		VALUES (newID(),@curParent,@ID,@lev)
	exec A_SP_TASKS_UPDATE_ONE_TASK_SUB_TABLE @curParent,0
	select @curParent = PARENT_ID FROM A_TASKS WHERE ID = @curParent
	end





