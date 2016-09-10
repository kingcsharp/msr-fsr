
create        FUNCTION A_FN_MESSAGES_IS_RECIPIENT (@messageID varchar(50),@strNTlogin varchar(50))
RETURNS smallInt
AS
BEGIN
	DECLARE @tester varchar(50),
	@ret as smallint
	SELECT @tester = PERSON_ID FROM 
	A_MESSAGES_PEOPLE_LINK  
	WHERE MESSAGE_ID = @messageID
	AND PERSON_ID = @strNTlogin
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

