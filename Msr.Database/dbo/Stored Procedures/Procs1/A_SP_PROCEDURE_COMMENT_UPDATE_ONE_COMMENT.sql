

CREATE      PROCEDURE A_SP_PROCEDURE_COMMENT_UPDATE_ONE_COMMENT
@newID nvarchar(50) OUTPUT,
@messages nvarchar(500) OUTPUT,
@ID nvarchar(50),
@DATA nvarchar(4000),
@PROC_OBJ_ID nvarchar(50),
@PRINT_ORDER nvarchar(10),
@LOC varchar(50),
@strNTLogin nvarchar(50)
AS
print 'Starting procedure A_SP_PROCEDURE_STEP_UPDATE_ONE_STEP'
print 'Get the value of the Procedure ID for this Procedure Object ID'
declare @pID as nvarchar(50)
SELECT @pID = ID FROM A_PROCEDURES_HISTORY WHERE OBJECT_ID = @PROC_OBJ_ID
if @ID is null
	begin
		print 'ID is Null do we need to create this comment'
		exec sp_GetUniqueID3 @newID OUTPUT
		print 'Got a new ID = ' + @newID
		INSERT INTO A_PROCEDURE_COMMENTS (ID) VALUES (@newID)
	end
else
	begin
		print 'The ID is not null so we are just updating step ID = ' + @ID
		set @newID = @ID
	end
print 'Now update all the values with the data passed in'
UPDATE A_PROCEDURE_COMMENTS SET
DATA = @DATA,
PRINT_ORDER = @PRINT_ORDER,
PROC_HIST_ID = @pID,
LOC = @LOC,
DRCM = getDate(),
MODBY = @strNTLogin
WHERE ID = @newID

exec A_SP_PROCEDURE_REORDER_COMMENTS @pID


