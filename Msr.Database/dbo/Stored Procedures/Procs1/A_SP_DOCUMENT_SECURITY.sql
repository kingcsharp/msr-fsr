



CREATE  PROCEDURE A_SP_DOCUMENT_SECURITY
	@ID varchar(50),
	@strNTLogin varchar(50)
as
declare @myCo as varchar(50)
exec A_SP_GET_PERSON_COMPANY @strNTLogin,@myCo OUTPUT

print 'My Company = ' + @myCo
SELECT * FROM A_O_DOCUMENTS WHERE ID = @ID and CREATING_CO = @myCo




