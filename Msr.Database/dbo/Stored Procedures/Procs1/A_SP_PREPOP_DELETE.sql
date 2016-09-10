







CREATE    PROCEDURE A_SP_PREPOP_DELETE
@id nvarchar(50),
@strNTLogin nvarchar(50)
AS
DELETE FROM A_PREPOP_HISTORY WHERE ID = @id






