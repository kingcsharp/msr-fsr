







CREATE   PROCEDURE dbo.A_SP_PROPOSALS_DELETE
@id nvarchar(50),
@strNTLogin nvarchar(50)
AS
DELETE FROM A_PROPOSALS_HISTORY WHERE ID = @id









