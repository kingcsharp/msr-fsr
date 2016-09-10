




CREATE    FUNCTION dbo.isParentTask(@ID varchar(50))
RETURNS smallInt
AS
BEGIN
	DECLARE @tester varchar(50),
	@ret as smallint
	SELECT top 1 @tester = ID FROM 
	A_TASKS t WHERE t.PARENT_ID = @ID
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







