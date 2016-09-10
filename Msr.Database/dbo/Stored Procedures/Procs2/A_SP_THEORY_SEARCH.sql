









CREATE               PROCEDURE A_SP_THEORY_SEARCH
	@strWhere nvarchar(2000),
	@strParagraphWhere nvarchar(2000),
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

--first let me see all theory I or my subordinates are created
set @myWhere = '' + '( (LOCKED_BY = ''' + @strNTLogin + ''') OR (CREATED_BY = ''' + @strNTLogin + ''') OR (CREATED_BY IN (SELECT SUBORDINATE FROM A_PEOPLE_SUB_LOOKUP_TABLE WHERE BOSS = ''' + @strNTLogin + '''))'
--next if my co wrote it and it is approved and I have sec level to see it
set @myWhere = @myWhere + 'OR (CREATING_CO = ''' + @myCO + ''' AND STATUS LIKE ''APPROVED%'' AND SECURITY_LEVEL <= ' + @mySecurityLevel + ')' 
--If the security level is 5 then never show this in the search only in the select screen
set @myWhere = @myWhere + ' AND (SECURITY_LEVEL <> 5) '
--put the roles in too
set @myWhere = @myWhere + ' OR  '
set @myWhere = @myWhere + ' exists ( SELECT ID FROM A_THEORY_ROLES_ALLOWED tra WHERE tra.THEORY_ID = A_V_THEORY_HISTORY_SEARCH.ID AND  '
set @myWhere = @myWhere + ' tra.ROLE_ID IN (SELECT ROLE_ID FROM A_PERSON_ROLES WHERE PERSON_ID = ''' + @strNTLogin + ''' )) '



set @myWhere = @myWhere + ')'

declare @sql nvarchar(4000)

if @strParagraphWhere is NULL
	begin
		set @sql = 'SET QUOTED_IDENTIFIER OFF SELECT *
		FROM A_V_THEORY_HISTORY_SEARCH '
		if len(@strWhere) > 0
			set @sql = @sql + 'WHERE '+ @myWhere + ' AND '  + @strWhere + ' '
		if len(@strSort) > 0
			set @sql = @sql + @strSort
	end
else
	begin
		set @sql = 'SET QUOTED_IDENTIFIER OFF SELECT DISTINCT SPECIAL_ROOT,SPECIAL_ID,ID,OBJECT_ID,SECURITY_LEVEL,
		CREATING_DEPT,OBJ_ID,LOCKED_BY,CREATED_BY,ROOT,CREATING_CO,
		NAME,CREATING_CO_NAME,DEPT_NAME,REV,STATUS,LOCKED_BY_NAME,DEPT_NAME,SECURITY_NAME,APPROVAL_DATE
		FROM A_O_THEORY_WITH_PARAGRAPHS A_V_THEORY_HISTORY_SEARCH '
		if len(@strWhere) > 0
			set @sql = @sql + 'WHERE '+ @myWhere + ' AND ' + @strWhere + ' '
		if len(@strParagraphWhere) > 0
			set @sql = @sql + ' AND  ' + @strParagraphWhere + ' '
		if len(@strSort) > 0
			set @sql = @sql + @strSort
	end

print @sql
exec (@sql)










