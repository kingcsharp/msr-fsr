





CREATE  FUNCTION dbo.getEmailURL()
RETURNS varchar(200)
AS
BEGIN
	DECLARE @myVal nvarchar(50)
	SELECT @myVal = VAL FROM A_ADMIN_CONFIGURATION WHERE NAME = 'EMAIL_URL'
	return (@myVal)
END








