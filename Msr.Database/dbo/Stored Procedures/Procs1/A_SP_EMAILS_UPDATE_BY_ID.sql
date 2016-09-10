




CREATE   procedure A_SP_EMAILS_UPDATE_BY_ID
	@ADDRESS nvarchar(50),
	@EMAIL_TYPE nvarchar(50),
	@ID nvarchar(50),
	@strNTLogin  nvarchar(50)
as
print 'Updating the Email address with ID = ' + @ID
UPDATE A_EMAILS SET
ADDY = @ADDRESS,
[TYPE] = @EMAIL_TYPE,
MODBY = @strNTLogin,
DRCM = getDate() WHERE ID = @ID







