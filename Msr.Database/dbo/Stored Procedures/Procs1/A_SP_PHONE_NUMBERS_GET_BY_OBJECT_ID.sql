


CREATE procedure A_SP_PHONE_NUMBERS_GET_BY_OBJECT_ID
	@objID nvarchar(50)
	
as
print 'Getting all phone numbers for ' + @objID
SELECT * FROM A_PHONE_NUMBERS WHERE OBJECT_ID = @objID



