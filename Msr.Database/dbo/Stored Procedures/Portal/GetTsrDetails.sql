ALTER procedure [dbo].[GetTsrDetails]
@fileId int
AS

DECLARE @objectid VARCHAR(50);

SELECT  @objectid = FILL_OBJ_ID FROM A_V_FILLS_SEARCH with (noLock)  WHERE ID COLLATE DATABASE_DEFAULT = @fileId
EXEC A_SP_TASKS_FIND_FOR_FILL_ID @fileId,''
