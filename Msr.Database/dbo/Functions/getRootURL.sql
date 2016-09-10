




CREATE FUNCTION dbo.getRootURL()
RETURNS varchar(200)
AS
BEGIN
	DECLARE @myVal nvarchar(50)
	SELECT @myVal = VAL FROM A_ADMIN_CONFIGURATION WHERE NAME = 'ROOT_URL'
	return (@myVal)
END







