

CREATE  FUNCTION dbo.A_FN_DATE_TIME_ADD_USING_UNITS 
	(@units varchar(50),@tm datetime,@num float)
RETURNS datetime
as
BEGIN
set @num = isNull(@num,0)
declare @myDate dateTime

	select @myDate = 
	case
		when @units = 'TIME_SYS_SECONDS'
			then dateAdd(ss,@num,@tm)
		when @units = 'TIME_SYS_MINUTES'
			then dateAdd(mi,@num,@tm)
		when @units = 'TIME_SYS_HOURS'
			then dateAdd(hh,@num,@tm)
		when @units = 'TIME_SYS_DAYS'
			then dateAdd(d,@num,@tm)
		when @units = 'TIME_SYS_WEEKS'
			then dateAdd(wk,@num,@tm)
		when @units = 'TIME_SYS_MONTHS'
			then dateAdd(m,@num,@tm)
		when @units = 'TIME_SYS_YEARS'
			then dateAdd(yy,@num,@tm)
		else dateAdd(yy,0,@tm)
	end
				



	return(@myDate)
END


