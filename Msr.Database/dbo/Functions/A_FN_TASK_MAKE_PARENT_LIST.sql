

CREATE   FUNCTION [dbo].[A_FN_TASK_MAKE_PARENT_LIST] (@ID varchar(50))
RETURNS nvarchar(3000)
as
BEGIN
declare @parentID varchar(100)
declare @res varchar(3000)
SELECT @parentID = parent_id from A_TASKS WHERE ID = @ID
set @res = ''
while @parentID is not null
	begin
	set @res = @parentID + @res
	SELECT @parentID = parent_id from A_TASKS WHERE ID = @parentID
	end

return(@res) 
END


