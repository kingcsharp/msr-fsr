




CREATE    FUNCTION leadingSpaces(@val varchar(50),@len int)
RETURNS varchar(50)
AS
BEGIN
	
	DECLARE @myLen int,
		@so varchar(50),
		@cnt int
	set @myLen = len(@val)
	if @len > @myLen
		begin
		set @cnt = 1
		while @cnt < (@len - @myLen)
			begin
			set @so = isnull(@so,'') + ' '
			set @cnt = @cnt + 1
			end
		end
	return(isNull(@so,'') + @val)

END






