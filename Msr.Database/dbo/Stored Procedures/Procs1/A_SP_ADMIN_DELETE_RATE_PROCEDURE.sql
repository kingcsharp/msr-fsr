







/*
STORED PROCEDURE CALLED IN administration/saveRatingProcedure.asp

*/
create             PROCEDURE A_SP_ADMIN_DELETE_RATE_PROCEDURE
@RATE_TYPE varchar(50),
@strNTLogin varchar(50)
AS

declare @myCO as nvarchar(50)
exec A_SP_GET_PERSON_COMPANY @strNTLogin,@myCO OUTPUT

	DELETE FROM A_ADMIN_RATE_PROCEDURES  
	WHERE RATE_TYPE= @RATE_TYPE AND COMPANY = @myCO
	






