

CREATE  FUNCTION dbo.A_FN_EE_GET_OVERALL_SHOW (@o smallint)
RETURNS varchar(50)
as
BEGIN
declare @ret as varchar(50)
if @o = 0 set @ret = 'EE_OVERALL_NONE'
if @o >= 1 and @o <=5 set @ret = 'EE_OVERALL_LOW'
if @o >= 6 and @o <=10 set @ret = 'EE_OVERALL_MED'
if @o >= 11 and @o <=15 set @ret = 'EE_OVERALL_HIGH'
return(@ret) 
END







