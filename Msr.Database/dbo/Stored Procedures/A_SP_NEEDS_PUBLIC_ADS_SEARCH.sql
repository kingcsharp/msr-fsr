






CREATE         PROCEDURE A_SP_NEEDS_PUBLIC_ADS_SEARCH
	@strWhere nvarchar(4000),
	@strSort nvarchar(1000)
AS


declare @sql nvarchar(4000)
set @sql = 'SET QUOTED_IDENTIFIER OFF SELECT *
	FROM A_O_NEEDS n '

if len(@strWhere) > 0
	begin
	  set @sql = @sql + ' WHERE ' + @strWhere + ' '
	end
--We need to add some standard security in here.  Public can see if...
set @sql = @sql + 'AND '
--approved & within ad period
set @sql = @sql + 'STATUS IN 
	(''APPROVED'',''APPROVED_BUT_REVISING'',''APPROVED_BUT_DELETING'')' 
set @sql = @sql + 'AND 
	(GETDATE() BETWEEN ADVERTISING_START_DATE AND ADVERTISING_STOP_DATE OR 
		(ADVERTISING_START_DATE IS NULL AND ADVERTISING_STOP_DATE > GETDATE()) 
		OR 
		(ADVERTISING_STOP_DATE IS NULL AND ADVERTISING_START_DATE < GETDATE())
	)'
set @sql = @sql + 'AND ('
--adv publicly = yes
set @sql = @sql + '(ADVERSTISE_PUBLICLY = ''NEED_YES'')'
--end the Security 
set @sql = @sql + ')'



if len(@strSort) > 0
	begin
	  set @sql = @sql + @strSort
	  print 'SQL = ' + @sql
	end


print 'SQL = ' + @SQL
EXEC(@SQL)