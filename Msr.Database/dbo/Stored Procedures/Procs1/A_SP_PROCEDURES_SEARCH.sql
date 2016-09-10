










CREATE          PROCEDURE A_SP_PROCEDURES_SEARCH
	@strWhere nvarchar(2000),
	@strStepWhere nvarchar(2000),
	@strSort nvarchar(1000),
	@strNTLogin nvarchar(50)
AS
declare @myWhere as nvarchar(2000)
--find out my Company
declare @myCO as nvarchar(50)
exec A_SP_GET_PERSON_COMPANY @strNTLogin,@myCO OUTPUT

declare @mySecurityLevel as nvarchar(50)
exec A_SP_SECURITY_LEVEL_GET_FOR_PERSON @mySecurityLevel OUTPUT,@strNTlogin
print 'Searchers Sec Level is ' + @mySecurityLevel

--first let me see all procedures I or my subordinates have created
set @myWhere = '' + ' ((CREATED_BY = ''' + @strNTLogin + ''') OR (CREATED_BY IN (SELECT SUBORDINATE FROM A_PEOPLE_SUB_LOOKUP_TABLE WHERE BOSS = ''' + @strNTLogin + '''))'
--next if my co wrote itand it is approved and I have sec level to see it
set @myWhere = @myWhere + 'OR (CREATING_CO = ''' + @myCO + ''' AND (STATUS LIKE ''APPROVED%'' OR STATUS = ''OLD'') AND SECURITY_LEVEL <= ' + @mySecurityLevel + ')' 
--If the security level is 5 then never show this in the search only in the select screen
set @myWhere = @myWhere + ' AND (SECURITY_LEVEL <> 5) ' 
set @myWhere = @myWhere + ')'

declare @sql nvarchar(4000)

if @strStepWhere is NULL
	begin
		set @sql = 'SET QUOTED_IDENTIFIER OFF SELECT *
		FROM A_V_PROCEDURE_HISTORY_SEARCH '
		if len(@strWhere) > 0
			set @sql = @sql + 'WHERE '+ @myWhere + ' AND '  + @strWhere + ' '
		if len(@strSort) > 0
			set @sql = @sql + @strSort
	end
else
	begin
		set @sql = 'SET QUOTED_IDENTIFIER OFF SELECT *
		FROM A_O_PROCEDURES_WITH_STEPS '
		if len(@strWhere) > 0
			set @sql = @sql + 'WHERE '+ @myWhere + ' AND ' + @strWhere + ' '
		if len(@strStepWhere) > 0
			set @sql = @sql + ' AND  ' + @strStepWhere + ' '
		if len(@strSort) > 0
			set @sql = @sql + @strSort
	end

print @sql
exec (@sql)
















