

CREATE      PROCEDURE A_SP_THEORY_COMMENT_UPDATE_ONE_COMMENT
@newID nvarchar(50) OUTPUT,
@messages nvarchar(500) OUTPUT,
@ID nvarchar(50),
@DATA nvarchar(4000),
@THEORY_OBJ_ID nvarchar(50),
@PRINT_ORDER nvarchar(10),
@LOC varchar(50),
@strNTLogin nvarchar(50)
AS
print 'Starting procedure A_SP_THEORY_COMMENT_UPDATE_ONE_COMMENT'
print 'Get the value of the Theory ID for this Theory Object ID'
declare @tID as nvarchar(50)
SELECT @tID = ID FROM A_THEORY_HISTORY WHERE OBJECT_ID = @THEORY_OBJ_ID
if @ID is null
	begin
		print 'ID is Null do we need to create this comment'
		exec sp_GetUniqueID3 @newID OUTPUT
		print 'Got a new ID = ' + @newID
		INSERT INTO A_THEORY_COMMENTS (ID) VALUES (@newID)
	end
else
	begin
		print 'The ID is not null so we are just updating step ID = ' + @ID
		set @newID = @ID
	end
print 'Now update all the values with the data passed in'
UPDATE A_THEORY_COMMENTS SET
DATA = @DATA,
PRINT_ORDER = @PRINT_ORDER,
THEORY_HIST_ID = @tID,
LOC = @LOC,
DRCM = getDate(),
MODBY = @strNTLogin
WHERE ID = @newID

exec A_SP_THEORY_REORDER_COMMENTS @tID


