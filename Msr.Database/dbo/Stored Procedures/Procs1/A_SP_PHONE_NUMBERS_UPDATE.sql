



CREATE  procedure A_SP_PHONE_NUMBERS_UPDATE
	@NUMBER nvarchar(50),
	@phoneType nvarchar(50),
	@PHONE_EXTENSION nvarchar(50),
	@PHONE_PIN nvarchar(50),
	@ID nvarchar(50),
	@strNTLogin  nvarchar(50)
as
print 'Updating the phone number with ID = ' + @ID
UPDATE A_PHONE_NUMBERS SET
PHONE_NUMBER = @NUMBER,
PHONE_TYPE = @phoneType,
PHONE_PIN = @PHONE_PIN,
PHONE_EXTENSION = @PHONE_EXTENSION,
MODBY = @strNTLogin,
DRCM = getDate() WHERE ID = @ID






