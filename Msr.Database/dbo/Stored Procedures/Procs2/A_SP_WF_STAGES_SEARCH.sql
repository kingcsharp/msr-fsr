





CREATE
 PROCEDURE dbo.A_SP_WF_STAGES_SEARCH
	@STAGE_NAME nvarchar(255),
	@GROUP_NAME nvarchar(255),
	@strNTLogin nvarchar(50)
AS
--find out my Company
declare @myCO as nvarchar(50)
SELECT @myCO = CO FROM A_V_PEOPLE_WITH_COMPANIES p WHERE PERSON = @strNTLogin
declare @strWhere nvarchar(500)
declare @sql nvarchar(500)

set @sql = 'SET QUOTED_IDENTIFIER OFF SELECT DISTINCT STAGE_ID, STAGE_NAME '
set @sql = @sql + ' FROM A_V_WF_STAGES_WITH_GROUPS WHERE  (HIDE IS NULL or HIDE <> 1) AND '
set @sql = @sql + ' CREATING_CO = ''' + @myCO + ''' AND '
set @sql = @sql + @STAGE_NAME + ' AND ' + @GROUP_NAME

print @sql
exec (@sql)







