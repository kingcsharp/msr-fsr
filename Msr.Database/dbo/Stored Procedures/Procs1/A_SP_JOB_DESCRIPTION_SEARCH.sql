

/*

STORED PROCEDURE CALL: jobDescription\searchjobDescription.asp

*/

CREATE                 PROCEDURE A_SP_JOB_DESCRIPTION_SEARCH
@strWhere nvarchar(4000),
@strSort nvarchar(1000),
@strNTLogin nvarchar(50)
AS
declare @fieldList varchar(1000)
declare @myWhere as nvarchar(2000)
--find out my Company
declare @myCO as nvarchar(50)
exec A_SP_GET_PERSON_COMPANY @strNTLogin,@myCO OUTPUT

declare @mySecurityLevel as nvarchar(50)
exec A_SP_SECURITY_LEVEL_GET_FOR_PERSON @mySecurityLevel OUTPUT,@strNTlogin
print 'Searchers Sec Level is ' + @mySecurityLevel

--first let me see all procedures I am editing
set @myWhere = '' + ' ((LOCKED_BY = ''' + @strNTLogin + ''') '
--next if my co wrote it it is approved and I have sec level to see it
set @myWhere = @myWhere + 'OR (CREATING_CO = ''' + @myCO + ''' AND STATUS LIKE ''APPROVED%'' AND SECURITY_LEVEL <= ' + @mySecurityLevel + ')' 
--If the security level is 5 then never show this in the search only in the select screen
set @myWhere = @myWhere + ' AND (SECURITY_LEVEL <> 5) ' 
set @myWhere = @myWhere + ')'


declare @sql nvarchar(4000)

print 'Doing default search'
set @sql = 'SELECT * FROM A_V_JOB_DESCRIPTION_SEARCH_DATA ' 			

runSQL:
if len(@strWhere) > 0
	set @sql = @sql + 'WHERE '+ @myWhere + ' AND '  + @strWhere + ' '

if len(@strSort) > 0
		set @sql = @sql + @strSort

print @sql
exec (@sql)































