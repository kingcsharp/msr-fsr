








CREATE       PROCEDURE A_SP_NEEDS_SEARCH
	@strWhere nvarchar(4000),
	@strSort nvarchar(1000),
	@strNTLogin nvarchar(50)
AS
--find out my Company
declare @myCO as nvarchar(50)
exec A_SP_GET_PERSON_COMPANY @strNTLogin,@myCO OUTPUT

declare @sql nvarchar(4000)
set @sql = 'SET QUOTED_IDENTIFIER OFF SELECT *
	FROM A_O_NEEDS n '

if len(@strWhere) > 0
	begin
	  set @sql = @sql + ' WHERE ' + @strWhere + ' '
	end
--We need to add some standard security in here
set @sql = @sql + 'AND ('
--User can always see if he is the creator or creator's boss
set @sql = @sql + '(CREATED_BY = ''' + @strNTLogin + ''') OR (CREATED_BY IN (SELECT SUBORDINATE FROM A_PEOPLE_SUB_LOOKUP_TABLE WHERE BOSS = ''' + @strNTLogin + '''))'
set @sql = @sql + 'OR ('
--Other Users can sometimes see if approved & within ad period
set @sql = @sql + 'STATUS IN (''APPROVED'',''APPROVED_BUT_REVISING'',''APPROVED_BUT_DELETING'')' 
set @sql = @sql + 'AND (GETDATE() BETWEEN ADVERTISING_START_DATE AND ADVERTISING_STOP_DATE OR (ADVERTISING_START_DATE IS NULL AND ADVERTISING_STOP_DATE > GETDATE()) OR (ADVERTISING_STOP_DATE IS NULL AND ADVERTISING_START_DATE < GETDATE()))'
set @sql = @sql + 'AND ('
--User can see if his root company is the creating company(which is always the root company of the creator)
set @sql = @sql + '(CREATING_CO = ''' + @myCO + ''')'
--User can see if his root company is the customer company or the customer company is a child of his root company
set @sql = @sql + ' OR (CUSTOMER_CO = ''' + @myCO + ''' OR CUSTOMER_CO IN (SELECT CHILD_COMPANY FROM A_COMPANIES_CHILD_LOOKUP_TABLE WHERE COMPANY = ''' + @myCO + '''))'
--User can see if his root company is in Companies Allowed or if his root company is an ancestor
--of any of the companies allowed.
set @sql = @sql + ' OR (
	(''' + @myCO + ''' IN (SELECT CO_ID FROM A_NEEDS_COMPANIES_ALLOWED WHERE NEED_ID = n.ID)) OR
	(''' + @myCO + ''' IN 
		(SELECT COMPANY FROM A_COMPANIES_CHILD_LOOKUP_TABLE 
			WHERE CHILD_COMPANY IN 
				(SELECT CO_ID FROM A_NEEDS_COMPANIES_ALLOWED WHERE NEED_ID = n.ID)
		)
	)
)'
--User can see if he is in People Allowed to View or one of his subordinates are in the 
--people allowed to view
set @sql = @sql + ' OR 
(
	(''' + @strNTLogin + ''' IN (SELECT PERSON_ID FROM A_NEEDS_PEOPLE_ALLOWED WHERE NEED_ID = n.ID))
	OR
	(
	''' + @strNTLogin + ''' IN (SELECT BOSS FROM A_PEOPLE_SUB_LOOKUP_TABLE WHERE SUBORDINATE IN
						(SELECT PERSON_ID FROM A_NEEDS_PEOPLE_ALLOWED WHERE NEED_ID = n.ID))
	)

)'
--end the Security 
set @sql = @sql + ')))'




if len(@strSort) > 0
	begin
	  set @sql = @sql + @strSort
	  print 'SQL = ' + @sql
	end

print 'SQL = ' + @SQL
EXEC(@SQL)