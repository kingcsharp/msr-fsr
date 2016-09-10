



CREATE  PROCEDURE A_SP_POPFILL_WF_LOCKED_BY_PEOPLE_ASSIGNED
	@strWhere nvarchar(200),
	@strNTLogin nvarchar(50)
AS
declare @sql as nvarchar(200)
set @sql = 'SELECT p.FULL_NAME as SHOW, p.ID AS VALUE FROM A_APPROVED_PEOPLE p WHERE  ' + @strWhere
exec(@sql)





