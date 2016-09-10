







CREATE  FUNCTION dbo.A_FN_MAKE_ZERO_NULL (@cnt float)
RETURNS float
as
BEGIN
declare @res float
if @cnt <> 0
	set @res = @cnt
return(@res)

END













