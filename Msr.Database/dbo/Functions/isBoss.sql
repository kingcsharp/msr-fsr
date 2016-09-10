
CREATE   FUNCTION isBoss(@ID varchar(50))
RETURNS smallInt
AS
BEGIN
	DECLARE @tester varchar(50),
	@ret as smallint
	SELECT top 1 @tester = ID FROM 
	A_V_PEOPLE_APPROVED_DATA p WHERE p.BOSS = @ID
	if @tester is null
		begin
		set @ret =  0
		end
	else
		begin
		set @ret = 1
		end
return(@ret)
END

