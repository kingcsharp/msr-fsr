
CREATE  PROCEDURE dbo.A_SP_COMPANIES_ROOT_ENCRYPT_PASSWORD
@coObjID varchar(50),
@password nvarchar(200)
AS
if @password is null 
	goto fin

declare @coID as varchar(50)
SELECT @coID = ID FROM A_COMPANIES_HISTORY WHERE OBJECT_ID = @coObjID
print 'COID = ' + @coID
UPDATE A_COMPANIES_NEW_PERSON_DATA SET PASSWORD = @password WHERE CO_ID = @coID



fin:

