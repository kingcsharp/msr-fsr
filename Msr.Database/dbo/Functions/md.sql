




CREATE    FUNCTION dbo.md()
RETURNS tinyInt
AS
BEGIN
	declare @ret tinyInt
	if exists (SELECT * FROM A_ADMIN_CONFIGURATION WHERE NAME = 'SP_DEBUG' AND VAL = 1)
		set @ret = 1
	else
		set @ret = 0
	return @ret
END







