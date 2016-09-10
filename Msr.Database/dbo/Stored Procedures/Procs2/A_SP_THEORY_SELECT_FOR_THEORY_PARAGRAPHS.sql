






CREATE           PROCEDURE A_SP_THEORY_SELECT_FOR_THEORY_PARAGRAPHS
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

--first let me see all theory I am editing
--set @myWhere = '' + ' ((LOCKED_BY = ''' + @strNTLogin + ''') '
--next if my co wrote it it is approved and I have sec level to see it
--set @myWhere = @myWhere + 'OR (CREATING_CO = ''' + @myCO + ''' AND STATUS LIKE ''APPROVED%'' AND SECURITY_LEVEL <= ' + @mySecurityLevel + ')' 
--If the security level is 5 then show this in the select screen
--set @myWhere = @myWhere + ' OR (SECURITY_LEVEL = 5) ' 
--set @myWhere = @myWhere + ')'

declare @sql nvarchar(4000)

if @strParagraphWhere is NULL
	begin
		print 'Paragraph Where is NULL'
		set @sql = 'SET QUOTED_IDENTIFIER OFF SELECT ID,NAME,CREATING_CO_NAME,ROOT AS SEND_ID,ROOT AS ROOT,STATUS
		FROM A_O_THEORY '
		if len(@strWhere) > 0
			set @sql = @sql + 'WHERE STATUS LIKE ''APPROVED%'' AND CREATING_CO = ''' + @myCo + ''' AND '+ @strWhere + ' '
		if len(@strSort) > 0
			set @sql = @sql + @strSort
		print @sql
	end
else
	begin
		print 'Paragraph Where is not null'
		set @sql = 'SET QUOTED_IDENTIFIER OFF SELECT ID,NAME,CREATING_CO_NAME,ROOT AS SEND_ID,ROOT AS ROOT,STATUS
		FROM A_O_THEORY_WITH_PARAGRAPHS '
		if len(@strWhere) > 0
			set @sql = @sql + 'WHERE STATUS LIKE ''APPROVED%'' AND CREATING_CO = ''' + @myCo + ''' AND '+ @myWhere + ' AND ' + @strWhere + ' '
		if len(@strParagraphWhere) > 0
			set @sql = @sql + ' AND  ' + @strParagraphWhere + ' '
		if len(@strSort) > 0
			set @sql = @sql + @strSort
	end

print @sql
exec (@sql)


















