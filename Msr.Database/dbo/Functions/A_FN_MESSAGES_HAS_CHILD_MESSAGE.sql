



create       FUNCTION A_FN_MESSAGES_HAS_CHILD_MESSAGE (@ID varchar(50))
RETURNS smallInt
AS
BEGIN
	DECLARE @tester varchar(50),
	@ret as smallint
	SELECT top 1 @tester = ID FROM 
	A_MESSAGES l WHERE l.PARENT_ID = @ID AND STATUS ='SENT'
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




