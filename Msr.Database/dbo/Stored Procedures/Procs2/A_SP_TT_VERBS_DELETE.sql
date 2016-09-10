






CREATE   PROCEDURE A_SP_TT_VERBS_DELETE
@id nvarchar(50),
@strNTLogin nvarchar(50)
AS
DELETE FROM A_TT_VERBS_HISTORY WHERE ID = @id





