





CREATE     procedure dbo.pr
@s as varchar(200)
AS
	if exists (SELECT * FROM A_ADMIN_CONFIGURATION WHERE NAME = 'SP_DEBUG' AND VAL = 1)
		print @s








