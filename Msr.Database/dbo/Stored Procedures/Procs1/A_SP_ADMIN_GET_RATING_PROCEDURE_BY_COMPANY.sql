







/*
STORED PROCEDURE CALLED IN administration/saveRatingProcedure.asp

*/
CREATE             PROCEDURE A_SP_ADMIN_GET_RATING_PROCEDURE_BY_COMPANY
@strNTLogin varchar(50)
AS

declare @myCO as nvarchar(50)
exec A_SP_GET_PERSON_COMPANY @strNTLogin,@myCO OUTPUT

SELECT * FROM A_V_ADMIN_RATE_TYPE_WITH_PROCEDURE
WHERE CO_ID=@myCO





