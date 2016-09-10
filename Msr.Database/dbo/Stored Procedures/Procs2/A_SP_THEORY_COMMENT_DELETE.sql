



CREATE          PROCEDURE A_SP_THEORY_COMMENT_DELETE 
@ID varchar(50),
@THEORY_OBJ_ID varchar(50),
@strNTLogin varchar(50)
AS
print 'Get the value of the Theory ID for this Theory Object ID'
declare @tID as nvarchar(50)
SELECT @tID = ID FROM A_THEORY_HISTORY WHERE OBJECT_ID = @THEORY_OBJ_ID

DELETE FROM A_THEORY_COMMENTS WHERE 
ID = @ID

exec A_SP_THEORY_REORDER_COMMENTS @tID





