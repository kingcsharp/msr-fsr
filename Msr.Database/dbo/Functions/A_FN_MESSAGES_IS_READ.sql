
create        FUNCTION A_FN_MESSAGES_IS_READ (@messageID varchar(50),@strNTlogin varchar(50))
RETURNS smallInt
AS
BEGIN
	DECLARE @tester varchar(50),
	@ret as smallint
	SELECT @tester = IS_READ FROM 
	A_MESSAGES_PEOPLE_LINK  
	WHERE MESSAGE_ID = @messageID
	AND PERSON_ID = @strNTlogin
	if @tester = 0
		begin
		set @ret =  0
		end
	else
		begin
		set @ret = 1
		end
return(@ret)
END

