
CREATE  PROCEDURE DBO.A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING
@so1 varchar(8000) OUTPUT,
@so2 varchar(8000) OUTPUT,
@so3 varchar(8000) OUTPUT,
@input varchar(8000)
AS
set @input = isNull(@input,'')
set @so1 = isNull(@so1,'')
set @so2 = isNull(@so2,'')
set @so3 = isNull(@so3,'')
if (len(@so1) + len(@input)) < 8000 and len(@so2) = 0
	begin
	set @so1 = @so1 + @input
	goto fin
	end
if (len(@so2) + len(@input)) < 8000 and len(@so3) = 0
	begin
	set @so2 = @so2 + @input
	goto fin
	end
if (len(@so3) + len(@input)) < 8000
	begin
	set @so1 = @so1 + @input
	goto fin
	end
print 'The warnings has exceeded the longest it can be'
goto problem
fin:
return(0)
problem:
return(1)






