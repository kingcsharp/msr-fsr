



CREATE   FUNCTION getUniqueID()
RETURNS nvarchar(50)
AS
BEGIN
	DECLARE @myVal nvarchar(50)
	exec sp_getUniqueID3 @myVal OUTPUT
	RETURN @myVal
END






