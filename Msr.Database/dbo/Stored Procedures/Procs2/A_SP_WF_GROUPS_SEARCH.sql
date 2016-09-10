





CREATE      PROCEDURE A_SP_WF_GROUPS_SEARCH
@NAME nvarchar(100),
@MEMBER nvarchar(100),
@MEMBER_ROLES nvarchar(100),
@strNTLogin nvarchar(50)
as
declare @sql as varchar(8000)
declare @myCo as nvarchar(50)
exec A_SP_GET_PERSON_COMPANY @strNTLogin,@myCo OUTPUT
print 'company = ' + @myCo
--IF there is no member search criteria then just search the main view
if ((@MEMBER is null) and (@MEMBER_ROLES is null))
 	begin
		set @sql = 'SELECT * FROM A_V_WF_GROUPS 
			WHERE (HIDE IS NULL or HIDE <> 1) AND CREATING_CO = ''' + @myCo + ''' AND ' + 
		@NAME
	end 
print @sql
exec(@sql)






