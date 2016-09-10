

create   PROCEDURE A_SP_TASK_COMMENT_DELETE_ONE_COMMENT
@commentID nvarchar(50),
@strNTLogin nvarchar(50)
AS
print 'getting the writer'
declare @writerID varchar(50)
SELECT @writerID = WRITER
FROM A_TASK_COMMENT
WHERE ID = @commentID

if @writerID = @strNTLogin
	begin
	DELETE FROM A_TASK_COMMENT WHERE ID = @commentID
	end
