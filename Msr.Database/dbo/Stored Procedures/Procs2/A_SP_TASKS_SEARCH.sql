











/*
STORED PROCEDURE CALL: 
- MODULE: AnswerAlpha\asp/ActualTasks/searchTasks.asp
*/
CREATE                                          PROCEDURE A_SP_TASKS_SEARCH
@fieldList nvarchar(4000),
@alias nvarchar(50),
@strWHERE nvarchar(4000),
@strPurposes varchar(4000),
@strObjects varchar(4000),
@strProjects varchar(4000),
@strSort nvarchar(500),
@strNTLogin nvarchar(50)
as

declare @isStandardSearch int
set @isStandardSearch = 0
if @fieldList is null
	begin
		print 'The field list is so null so we know this a standard search from searchDiscussions.asp'
		set @isStandardSearch =1
	end


--build the sql for the query
declare @sql as nvarchar(4000)
declare @myCo varchar(50)

SELECT @myCo = COMPANY 
FROM A_APPROVED_PEOPLE 
WHERE ID=@strNTLogin


--dbo.A_FN_TASK_GET_STANDARD_SQL_STRING(@fieldList,@alias,@strNTlogin)

if @fieldList = '' or @fieldList is NULL 
begin

set @fieldList = 'DISTINCT ID, 
				SORT_ID, 
                DESCRIPTION, 
                STATUS, 
                REQUESTOR, 
                CHILD_ORDER, 
                CREATED_BY, 
                CREATE_DATE, 
                SYSTEM_TASK, 
                PROCEDURE_ID, 
                REQUESTEE_ID, 
                GROUP_REQUESTEE_ID, 
                ORIG_PLANNED_START_DATE, 
                ORIG_PLANNED_STOP_DATE, 
                CUR_PLANNED_START_DATE, 
                CUR_PLANNED_STOP_DATE, 
                ACTUAL_START_DATE, 
                ACTUAL_STOP_DATE, 
                CUR_PLANNED_COUNTER_START, 
                LATEST_REQUESTEE_NAME, 
                HAS_DISCUSSION, 
                HAS_SURVEY, 
                HAS_CHILD, 
                HAS_REF_PROC, 
                HAS_FILE, 
                ORIG_REQUESTOR_ID, 
                HAS_REF_OBJ, 
                HAS_MONITOR, 
                ORIG_REQUESTOR_NAME, 
                COLOR_CODE,
				LAST_REQUEST_DATE,
				PARENT_ID,
				PARENT_LIST,
				isParent,
				PRIORITY,
				PRIORITY_NAME, 
				COMPANY_NAME,
				CHILD_STATUS,
				PROCEDURE_STEP_ID, 
				LAST_COMMENT_WRITER,
				LAST_COMMENT_DRCM,
				LAST_COMMENT,
				IS_QUOTE,
				IS_QUOTE_ACCEPT,
				IS_FILL,
				CO_ID,
				FILL_ID,
				PURCHASE_ITEM_ID,
				PURCHASE_HIST_ID,
				PURCHASE_ITEM_ROLE,
				ASSIGNEE_NAME,ASSIGNEE_ID,ASSIGNEE_STATUS '
 				

end


set @sql ='SELECT ' + @fieldList + ' FROM A_V_TASK_SEARCH ' + isNull(@alias,'') + ' '
--We need to add some standard security in here
set @sql = @sql + 'WHERE ('
--Requestee can see non creating 
set @sql = @sql + '(REQUESTEE_ID = ''' + @strNTLogin + ''' AND STATUS <> ''CREATING'')'
-- Add myCompany allowed to see and my child companies allowed to see
set @sql = @sql + ' OR (ALLOWED_CO_ID = ''' + @myCo + ''' AND STATUS <> ''CREATING'')' 
set @sql = @sql + ' OR (ALLOWED_CO_ID IN (SELECT CHILD_COMPANY FROM A_COMPANIES_CHILD_LOOKUP_TABLE WHERE COMPANY = ''' + @myCo + ''') AND STATUS <> ''CREATING'')'
-- Add Im specified to see
set @sql = @sql + ' OR (SPECIFIED_PERSON_ID = ''' + @strNTLogin + ''' AND STATUS <> ''CREATING'')' 
-- Add persons subbordinates
set @sql = @sql + ' OR (REQUESTEE_ID IN (SELECT SUBORDINATE FROM A_PEOPLE_SUB_LOOKUP_TABLE WHERE BOSS = ''' + @strNTLogin + ''') AND STATUS <> ''CREATING'')'
--Now check to see if our role is the one assigned..  we should see those
set @sql = @sql + ' OR (GROUP_REQUESTEE_ID IN (SELECT ROLE_ID FROM A_PERSON_ROLES WHERE PERSON_ID = ''' + @strNTLogin + ''')  AND STATUS <> ''CREATING'') '
--Now check to see if we Created this one
set @sql = @sql + ' OR (CREATED_BY = ''' + @strNTLogin + ''')'
set @sql = @sql + ' OR (ORIG_REQUESTOR_ID = ''' + @strNTLogin + ''') '
set @sql = @sql + ' OR (ASSIGNEE_ID IN (SELECT SUBORDINATE FROM A_PEOPLE_SUB_LOOKUP_TABLE WHERE BOSS = ''' + @strNTLogin + ''') AND STATUS <> ''CREATING'')'
--We still need more security here about invited companies and security clearance levels
--end the Security 
set @sql = @sql + ')'


set @sql = @sql + ' AND ' +  @strWhere


if @isStandardSearch = 1  
	begin
	exec A_SP_Z_PROJECTS_PURPOSES_AND_OBJECTS_SEARCH
		@strPurposes,'A_TASKS','PURPOSES','S', @sql output, @strNTLogin
	exec A_SP_Z_PROJECTS_PURPOSES_AND_OBJECTS_SEARCH
		@strObjects,'A_TASKS','OBJECT', 'S', @sql output, @strNTLogin
	exec A_SP_Z_PROJECTS_PURPOSES_AND_OBJECTS_SEARCH
		@strProjects,'A_TASKS','PROJECT', 'S', @sql output, @strNTLogin
	end 

if len(@strSort) > 0
	begin
	  set @sql = @sql + @strSort
	end

print 'SQL = ' + @SQL
EXEC(@SQL)















































