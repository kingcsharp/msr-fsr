

CREATE   PROCEDURE A_SP_PROCEDURES_GET_ROLES_TO_VIEW
	@strID nvarchar(50),
	@strNTLogin nvarchar(50)
AS
--find out my Company
declare @myCO as nvarchar(50)
exec A_SP_GET_PERSON_COMPANY @strNTLogin,@myCO OUTPUT

SELECT * FROM A_V_PROCEDURE_ROLES_TO_VIEW WHERE PROCEDURE_ID = @strID


