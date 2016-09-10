





/*
STORED PROCEDURE CALLED IN administration/saveRatingProcedure.asp

*/

CREATE            PROCEDURE A_SP_ADMIN_UPDATE_RATE_PROCEDURE
@newID nvarchar(50) OUTPUT,
@msg nvarchar(1000) OUTPUT,
@PROCEDURE_ID nvarchar(50),
@RATE_TYPE nvarchar(50),
@strNTLogin nvarchar(50)
AS

print 'pid' +@PROCEDURE_ID
print 'RATE' +@RATE_TYPE

declare @myCO as nvarchar(50)
exec A_SP_GET_PERSON_COMPANY @strNTLogin,@myCO OUTPUT

	DELETE FROM A_ADMIN_RATE_PROCEDURES  
	WHERE RATE_TYPE= @RATE_TYPE AND COMPANY = @myCO
	

if @PROCEDURE_ID IS NOT NULL 
print 'we are updating a product rating'
exec sp_GetUniqueID3 @newID OUTPUT

--print 'newid is '+ @newID

     begin
	INSERT INTO A_ADMIN_RATE_PROCEDURES ([ID],[COMPANY],[PROCEDURE_ID],[RATE_TYPE],[DRCM],[MODBY])
	VALUES
	(@newID, @myCo, @PROCEDURE_ID, @RATE_TYPE ,getDate(),@strNTLogin)


 end 





