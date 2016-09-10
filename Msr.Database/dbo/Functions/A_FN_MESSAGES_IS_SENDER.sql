
create        FUNCTION A_FN_MESSAGES_IS_SENDER (@messageID varchar(50),@strNTlogin varchar(50))
RETURNS smallInt
AS
BEGIN
	DECLARE @tester varchar(50),
	@ret as smallint
	SELECT @tester = SENDER FROM 
	A_MESSAGES  
	WHERE ID = @messageID
	AND SENDER = @strNTlogin
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

