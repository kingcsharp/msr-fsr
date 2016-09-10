
CREATE    FUNCTION isChildLocation(@ID varchar(50))
RETURNS smallInt
AS
BEGIN
	DECLARE @tester varchar(50),
	@ret as smallint
	SELECT top 1 @tester = ID FROM 
	A_APPROVED_LOCATIONS l WHERE l.PARENT_LOCATION = @ID
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

