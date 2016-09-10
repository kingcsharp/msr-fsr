


CREATE         PROCEDURE dbo.A_SP_ADMIN_GET_PEOPLE_PAGE_HIT_DATA
	@strWhere nvarchar(4000),
	@strSort nvarchar(1000),
	@searchType varchar(50),
	@strNTLogin nvarchar(50)
AS
--find out my Company
declare @myCO as nvarchar(50)
exec A_SP_GET_PERSON_COMPANY @strNTLogin,@myCO OUTPUT
declare @sql nvarchar(4000)
declare @select nvarchar(2000)
declare @groupBy nvarchar(2000)
declare @strNewWhere nvarchar(2000)

if @searchType is null
begin
	set @select = 'SELECT COUNT(ID) AS NUM_HITS, PAGE, USER_ID, MO,YR,FULL_NAME,ROOT_COMPANY, 
                    CO_NAME, DEPT_NAME
					FROM A_V_PEOPLE_PAGE_HITS_RAW_DATA '
	set @strNewWhere = ' WHERE ROOT_COMPANY is not null '
--<> ''' + @myCO + ''' '
	set @groupBy = 'GROUP BY PAGE, USER_ID, MO, YR, FULL_NAME, ROOT_COMPANY, 
					CO_NAME, DEPT_NAME'
	goto makeSQL
end

if @searchType = "PperM"
begin
	set @select = 'SELECT COUNT(ID) AS NUM_HITS, USER_ID, MO,YR,FULL_NAME,ROOT_COMPANY, 
                    CO_NAME, DEPT_NAME
					FROM A_V_PEOPLE_PAGE_HITS_RAW_DATA '
	set @strNewWhere = ' WHERE ROOT_COMPANY is not null '
--<> ''' + @myCO + ''' '
	set @groupBy = 'GROUP BY USER_ID, MO, YR, FULL_NAME, ROOT_COMPANY, 
					CO_NAME, DEPT_NAME'
	goto makeSQL
end

if @searchType = "CperM"
begin
	set @select = 'SELECT COUNT(ID) AS NUM_HITS, MO,YR,ROOT_COMPANY, 
                    CO_NAME
					FROM A_V_PEOPLE_PAGE_HITS_RAW_DATA '
	set @strNewWhere = ' WHERE ROOT_COMPANY is not null '
--<> ''' + @myCO + ''' '
	set @groupBy = 'GROUP BY MO, YR, ROOT_COMPANY, CO_NAME '
	goto makeSQL
end

if @searchType = "PperD"
begin
	set @select = 'SELECT COUNT(ID) AS NUM_HITS, USER_ID, DA,MO,YR,FULL_NAME,ROOT_COMPANY, 
                    CO_NAME, DEPT_NAME
					FROM A_V_PEOPLE_PAGE_HITS_RAW_DATA '
	set @strNewWhere = ' WHERE ROOT_COMPANY is not null '
--<> ''' + @myCO + ''' '
	set @groupBy = 'GROUP BY USER_ID, DA,MO, YR, FULL_NAME, ROOT_COMPANY, 
					CO_NAME, DEPT_NAME'
	goto makeSQL
end
if @searchType = "CperD"
begin
	set @select = 'SELECT COUNT(ID) AS NUM_HITS, DA,MO,YR,ROOT_COMPANY, 
                    CO_NAME	FROM A_V_PEOPLE_PAGE_HITS_RAW_DATA '
	set @strNewWhere = ' WHERE ROOT_COMPANY is not null '
--<> ''' + @myCO + ''' '
	set @groupBy = 'GROUP BY DA,MO, YR,ROOT_COMPANY, 
					CO_NAME '
	goto makeSQL
end
if @searchType = "DperD"
begin
	set @select = 'SELECT COUNT(ID) AS NUM_HITS, DA,MO,YR,ROOT_COMPANY, 
                    CO_NAME, DEPT_NAME
					FROM A_V_PEOPLE_PAGE_HITS_RAW_DATA '
	set @strNewWhere = ' WHERE ROOT_COMPANY is not null '
--<> ''' + @myCO + ''' '
	set @groupBy = 'GROUP BY DA,MO, YR,ROOT_COMPANY, 
					CO_NAME, DEPT_NAME'
	goto makeSQL
end
if @searchType = "DperM"
begin
	set @select = 'SELECT COUNT(ID) AS NUM_HITS, MO,YR,ROOT_COMPANY, 
                    CO_NAME, DEPT_NAME
					FROM A_V_PEOPLE_PAGE_HITS_RAW_DATA '
	set @strNewWhere = ' WHERE ROOT_COMPANY is not null '
--<> ''' + @myCO + ''' '
	set @groupBy = 'GROUP BY MO, YR,ROOT_COMPANY, 
					CO_NAME, DEPT_NAME'
	goto makeSQL
end
if @searchType = "PperPperD"
begin
	set @select = 'SELECT COUNT(ID) AS NUM_HITS, PAGE, USER_ID, MO,DA,YR,FULL_NAME,ROOT_COMPANY, 
                    CO_NAME, DEPT_NAME
					FROM A_V_PEOPLE_PAGE_HITS_RAW_DATA '
	set @strNewWhere = ' WHERE ROOT_COMPANY is not null '
--<> ''' + @myCO + ''' '
	set @groupBy = 'GROUP BY PAGE, USER_ID, MO, DA, YR, FULL_NAME, ROOT_COMPANY, 
					CO_NAME, DEPT_NAME'
	goto makeSQL
end
if @searchType = "PperP"
begin
	set @select = 'SELECT COUNT(ID) AS NUM_HITS, PAGE, USER_ID, FULL_NAME,ROOT_COMPANY, 
                    CO_NAME, DEPT_NAME
					FROM A_V_PEOPLE_PAGE_HITS_RAW_DATA '
	set @strNewWhere = ' WHERE ROOT_COMPANY is not null '
--<> ''' + @myCO + ''' '
	set @groupBy = 'GROUP BY PAGE, USER_ID, FULL_NAME, ROOT_COMPANY, 
					CO_NAME, DEPT_NAME'
	goto makeSQL
end


makeSQL:
if @strSort = ' ORDER BY ' set @strSort = NULL

if @strWhere is not null
begin
	if len(@strNewWhere) > 0 set @strNewWhere = @strNewWhere + ' AND ' + @strWhere + ' '
	else set @strNewWhere = ' WHERE ' + @strWhere + ' '
end	
set @sql = @select + isNull(@strNewWhere,'') + isNull(@groupBy,'') + isNull(@strSort,'')

fin:
print @sql
exec (@sql)



