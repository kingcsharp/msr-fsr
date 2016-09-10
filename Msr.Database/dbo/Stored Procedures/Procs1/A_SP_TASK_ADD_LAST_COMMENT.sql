


CREATE         PROCEDURE A_SP_TASK_ADD_LAST_COMMENT
@taskID as nvarchar(50),
@modby as nvarchar(50)

AS
declare @lastComment as nvarchar(4000)
declare @allowedToView as varchar(50)
print 'Inside A_SP_TASK_ADD_LAST_COMMENT. Getting my comment data  with \a task_id of' + isNull(@taskID,'Null')
print 'Modby value is' + isNull(@modby,'Null')


print 'deleting my comments'
DELETE FROM A_TASK_LAST_COMMENT
WHERE TASK_ID = @taskID

print 'inserting my new one'


INSERT INTO  A_TASK_LAST_COMMENT (ID,TASK_ID,DRCM,MODBY)
VALUES (newID(),@taskID,getDate(),@modby )

print 'updating my last comment according to who is allowed to view it.'

print 'Everyone is allowed to view this comment so updating PUBLIC_COMMENT column where task id =' + isNull(@taskID,'Null')
UPDATE A_TASK_LAST_COMMENT 
SET COMMENT_ID =	(SELECT TOP 1 ID FROM A_TASK_COMMENT 
						WHERE TASK_ID = @taskID AND ALLOWED_TO_VIEW = 'ALL'
						ORDER BY DRCM DESC)
WHERE TASK_ID = @taskID


 if @modby is null
 	begin
 	print 'Finiding my modby value because its a delete'
 	UPDATE A_TASK_LAST_COMMENT 
	SET MODBY =	(SELECT TOP 1 MODBY FROM A_TASK_COMMENT 
	WHERE TASK_ID = @taskID AND ALLOWED_TO_VIEW = 'ALL'
	ORDER BY DRCM DESC)
	WHERE TASK_ID = @taskID

	end

