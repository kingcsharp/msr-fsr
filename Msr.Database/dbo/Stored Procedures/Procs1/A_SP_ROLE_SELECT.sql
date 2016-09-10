

CREATE     PROCEDURE A_SP_ROLE_SELECT 
	@ROLE_NAME nvarchar(255),
	@ROLE_NAME_EXACT nvarchar(255),
  	@PERSON_NAME nvarchar(255),
  	@PERSON_NAME_EXACT nvarchar(255),
	@strNTLogin nvarchar(50),
	@strSort nvarchar(200)
AS
print @strSort
declare @sql varchar(4000)
print 'Get my company'
declare @myCo as nvarchar(50)
exec A_SP_GET_PERSON_COMPANY @strNTLogin,@myCo OUTPUT
print 'The persons company is ' + @myCo

print 'if we dont have people then just do a simple role search and save time'
if @PERSON_NAME is null
	begin
	print 'doing simple role search'	
		if @ROLE_NAME is null
			begin
			print '@ROLE_NAME is null'
			set @sql = 'SELECT  ID,
								NAME,
								OBJECT_ID 
						FROM A_APPROVED_ROLES 
						WHERE CREATING_CO ='''+@myCo+''''
			end
		 else
			begin
			print '@ROLE_NAME is NOT null'
			set @sql= 'SELECT ID,
							  NAME,
						   	  OBJECT_ID 
						FROM A_APPROVED_ROLES 
						WHERE CREATING_CO = '''+@myCo+''' and NAME like ''%'+@ROLE_NAME+'%''' 
			end 
	end
else
	begin
	print 'We need to search roles with the assingees'
		if @ROLE_NAME is NULL
			begin
			print '@ROLE_NAME is null'
			set @sql = 'SELECT 	DISTINCT ID,
								NAME,
								OBJECT_ID 
								FROM A_APPROVED_ROLES_WITH_ASSIGNEES 
						WHERE CREATING_CO = '''+@myCo+'''
						AND MEMBER_NAME LIKE ''%'+@PERSON_NAME+'%''' 
			end
		else
			begin
			print '@ROLE_NAME is null'
			set @sql = 'SELECT DISTINCT ID,
								NAME,
								OBJECT_ID 
						FROM A_APPROVED_ROLES_WITH_ASSIGNEES 
						WHERE CREATING_CO = ''' + @myCo + ''' 
						AND	MEMBER_NAME LIKE %'+@PERSON_NAME+'% 
						AND NAME like ''%'+@ROLE_NAME+'%'''
			end
	end	
runSQL:
print 'Adding the sort'
if @strSort is not null
	begin
	set @sql = @sql + ' ' +  @strSort 
	end
executeSQL:
print 'SQL = ' + @SQL
EXEC(@SQL)
