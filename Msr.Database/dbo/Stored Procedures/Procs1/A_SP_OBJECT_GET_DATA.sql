


CREATE    PROCEDURE A_SP_OBJECT_GET_DATA
	@strID nvarchar(50),
	@strNTLogin nvarchar(50)
as
SELECT * FROM A_OBJECTS WHERE ID = @strID



