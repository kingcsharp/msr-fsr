

/*

STORED PROCEDURE CALL: 

- MODULE: \asp\description\searchDescription.asp

*/

CREATE               PROCEDURE A_SP_DESCRIPTION_SEARCH
@strWhere nvarchar(100),
@strSort nvarchar(50),
@searchMethod nvarchar(100),
--@searchID nvarchar(50),
@strNTLogin nvarchar(50)
AS
declare @sql as nvarchar(4000)
if @searchMethod = 'myOfficialRole'  
	begin
		declare @myRole as nvarchar(50)
		exec A_SP_GET_PRIMARY_POSITION_BY_NT_LOGIN @strNTLogin,@myRole OUTPUT
		set @sql = 'SELECT * FROM A_V_DESCRIPTION_SEARCH WHERE ROLE_ID = '+ @myRole + ' '
		print 'my role is' + @myRole + ''
		--set @sql = 'SELECT * FROM A_V_DESCRIPTION_SEARCH WHERE' + @strWhere +' ' 
		print @sql
	end
else
	set @sql = 'SELECT * FROM A_V_DESCRIPTION_SEARCH WHERE' + @strWhere +' '  
	+ @strSort +'' 
	print @sql

--if @searchMethod = 'mySubRole'  
--begin
--set @sql = 'SELECT * FROM A_V_DESCRIPTION_SEARCH WHERE ROLE_ID !='  + @searchID + ' '
--end
--else
--begin
--set @sql = 'SELECT * FROM A_V_DESCRIPTION_SEARCH '
--end

EXEC(@SQL)

















