




CREATE       PROCEDURE A_SP_OBJECT_GET_REV_DATA
	@strID nvarchar(50),
	@strNTLogin nvarchar(50)
as
declare @root as nvarchar(50)
SELECT @root = ROOT FROM A_OBJECTS WHERE ID = @strID
SELECT * FROM A_V_OBJECT_REVISION_DATA WHERE root = @root ORDER by REV





