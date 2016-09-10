





CREATE  FUNCTION dbo.dateToVarchar(@d datetime)
RETURNS varchar(50)
AS
BEGIN
declare @op as varchar(50)
set @op = 
	convert(varchar(50),month(@d)) + '/' +
	convert(varchar(50),day(@d)) + '/' +
	convert(varchar(50),year(@d))


return(@op)
end







