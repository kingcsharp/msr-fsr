


CREATE procedure A_SP_PHONE_NUMBERS_DELETE_BY_ID
	@ID nvarchar(50)
as
print 'Deleting the phone number with ID = ' + @ID
DELETE FROM A_PHONE_NUMBERS WHERE ID = @ID





