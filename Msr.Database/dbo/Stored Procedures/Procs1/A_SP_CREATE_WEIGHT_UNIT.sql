CREATE PROCEDURE DBO.A_SP_CREATE_WEIGHT_UNIT 
@ID varchar(50),
@NAME varchar(500),
@kilos float
AS
if not exists(SELECT * FROM A_WEIGHT_UNITS WHERE NAME = @NAME)
	begin
	print 'Creating a weight unit named ' + @NAME
	INSERT INTO A_WEIGHT_UNITS (ID,NAME,KG)
		VALUES (@ID,@NAME,@kilos)
	end
else print 'This one already exists'
