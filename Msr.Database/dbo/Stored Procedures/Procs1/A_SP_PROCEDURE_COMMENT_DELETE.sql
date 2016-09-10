




CREATE           PROCEDURE A_SP_PROCEDURE_COMMENT_DELETE 
@ID varchar(50),
@PROCEDURE_OBJ_ID varchar(50),
@strNTLogin varchar(50)
AS
print 'Get the value of the Procedure ID for this Procedure Object ID'
declare @pID as nvarchar(50)
SELECT @pID = ID FROM A_PROCEDURES_HISTORY WHERE OBJECT_ID = @PROCEDURE_OBJ_ID

DELETE FROM A_PROCEDURE_COMMENTS WHERE 
ID = @ID

exec A_SP_PROCEDURE_REORDER_COMMENTS @pID






