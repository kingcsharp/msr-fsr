







/*
STORED PROCEDURE CALLED IN administration/saveRatingProcedure.asp

*/

CREATE              PROCEDURE A_SP_ADMIN_UPDATE_RATE_PROCEDURE2
@PROCEDURE_ID varchar(50),
@RATE_TYPE varchar(50),
@strNTLogin varchar(50)
AS


declare @myCO as nvarchar(50)
exec A_SP_GET_PERSON_COMPANY @strNTLogin,@myCO OUTPUT

	DELETE FROM A_ADMIN_RATE_PROCEDURES  
	WHERE RATE_TYPE= @RATE_TYPE AND COMPANY = @myCO
	

if @PROCEDURE_ID IS NOT NULL 
	begin
		declare @newID as varchar(50)
		exec sp_GetUniqueID3 @newID OUTPUT
		print 'we are updating a product rating'
		INSERT INTO A_ADMIN_RATE_PROCEDURES ([ID],[COMPANY],[PROCEDURE_ID],[RATE_TYPE],[DRCM],[MODBY])
		VALUES
		(@newID, @myCo, @PROCEDURE_ID, @RATE_TYPE ,getDate(),@strNTLogin)
	 end 







