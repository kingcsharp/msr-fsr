




CREATE  FUNCTION dbo.A_FN_COMPANY_GET_TOP_COMPANY(@ID varchar(50))
RETURNS varchar(50)
AS
BEGIN
declare @p as varchar(50)
declare @pOld as varchar(50)
declare @t as varchar(50)

SELECT @p = PARENT FROM A_COMPANIES_HISTORY WHERE ID = @ID
SELECT @pOld = ROOT FROM A_OBJECTS WHERE OBJ_ID = @ID AND OBJ_TABLE = 'A_COMPANIES_HISTORY'
while @p is not null
	begin
--	print 'Currently looking at parent id = ' + @p
	set @pOld = @p
	set @t = NULL
	SELECT @t = HISTORY_REF_ID FROM A_COMPANIES WHERE ID = @p
--	print ' Got the t = ' +@t
	set @p = NULL
	SELECT @p = PARENT FROM A_COMPANIES_HISTORY WHERE ID = @t
	end
SELECT @pOld = ID FROM A_COMPANIES WHERE HISTORY_REF_ID = @pOld
return(@pOld)
END







