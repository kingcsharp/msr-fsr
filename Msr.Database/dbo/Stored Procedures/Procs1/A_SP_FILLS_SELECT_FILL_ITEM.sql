





CREATE       PROCEDURE DBO.A_SP_FILLS_SELECT_FILL_ITEM
@strWhere nvarchar(4000),
@objTable varchar(50),
@strOrder varchar(2000),
@strNTLogin varchar(50)
AS
declare @sql nvarchar(4000),@myCo varchar(50)
SELECT @myCo = ROOT_COMPANY FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @strNTLogin
set @sql = 'SELECT * '
if @objTable = 'A_ACTUAL_PARTS_HISTORY'
	begin
	set @sql = @sql + ' , isNull(NAME,'''') + ''['' + convert(varchar(50),isNull(QTY,1)) + '']'' AS DISPLAY_NAME FROM A_V_FILLS_ACTUAL_PART_SEARCH WHERE'
--	set @sql = @sql + ' WHERE CREATING_CO = ''' + @myCo + ''' AND AP_STATUS = ''ap_available'' AND ' + @strWhere

	set @sql = @sql + '(CUR_OWNER = ''' + @myCo + ''' OR CUR_OWNER IN ( SELECT CHILD_COMPANY 
						FROM A_COMPANIES_CHILD_LOOKUP_TABLE WHERE COMPANY = ''' + @myCo + ''' ))'
	set @sql = @sql + ' AND AP_STATUS = ''ap_available'' AND ' + @strWhere 
	end
else
	begin
	set @sql = @sql + ' , isNull(NAME,'''') + ''['' + convert(varchar(50),isNull(QTY,1)) + '']'' AS DISPLAY_NAME FROM A_V_ACTUAL_PARTS_APPROVED_DATA '
	--set @sql = @sql + ' WHERE AP_STATUS = ''ap_available'' AND ' + @strWhere
	end

print @sql
exec(@sql)








