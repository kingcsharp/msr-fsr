






CREATE    FUNCTION dbo.A_FN_COMPANY_GET_PATH_NAME(@ID varchar(4000))
RETURNS varchar(4000)
AS
BEGIN
declare @p as varchar(50)
declare @pOld as varchar(50)
declare @t as varchar(50)
declare @pathName as varchar(4000)
SELECT @p = PARENT FROM A_COMPANIES_HISTORY WHERE ID = @ID
SELECT @pOld = ROOT FROM A_OBJECTS WHERE OBJ_ID = @ID AND OBJ_TABLE = 'A_COMPANIES_HISTORY'
SELECT @pathName = NAME FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @pOld
while @p is not null
	begin
	SELECT @pathName = NAME + '/' + @pathName FROM A_v_COMPANIES_APPROVED_DATA WHERE ID = @p
	set @pOld = @p
	set @t = NULL
	SELECT @t = HISTORY_REF_ID FROM A_COMPANIES WHERE ID = @p
--	print ' Got the t = ' +@t
	set @p = NULL
	SELECT @p = PARENT FROM A_COMPANIES_HISTORY WHERE ID = @t
	end
return(@pathName)
END









