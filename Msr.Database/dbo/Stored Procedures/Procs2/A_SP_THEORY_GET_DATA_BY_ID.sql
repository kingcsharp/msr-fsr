

CREATE     PROCEDURE A_SP_THEORY_GET_DATA_BY_ID
	@TID nvarchar(50),
	@strNTLogin nvarchar(50)
AS
--find out my Company
declare @myCO as nvarchar(50)
exec A_SP_GET_PERSON_COMPANY @strNTLogin,@myCO OUTPUT

SELECT * FROM A_THEORY_HISTORY WHERE ID = @TID




