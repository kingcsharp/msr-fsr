


/*
STORED PROCEDURE CALLED IN projects/processProject.asp

*/
CREATE    PROCEDURE A_SP_PROJECT_COPY_ONE_PROJECT
@newID varchar(50) OUTPUT,
@msg varchar(1000) OUTPUT,
@projectID varchar(50),
@strNTLogin varchar(50)
AS
declare @ID varchar(50)
declare @DATE_CREATED varchar(50)
declare @INITIATOR varchar(50)
declare @STATUS varchar(50)
declare @NAME varchar(50)
declare @PRIORITY_LEVEL varchar(50)
declare @OBJECTIVE varchar(50)
declare @LEADER varchar(50)
declare @SECURITY_LEVEL varchar(50)
declare @ORIGINAL_PLANNED_STOP_DATE varchar(50)
declare @CURRENT_PLANNED_STOP_DATE varchar(50)
declare @ACTUAL_STOP_DATE varchar(50)
declare @MONTH_GOALS varchar(50)
declare @ISSUES varchar(50)
declare @SUMMARY varchar(50)
declare @sql varchar(4000)
declare @sponsorList varchar(4000)
declare @memberList varchar(4000)
declare @peopleList varchar(4000)
declare @companyList varchar(4000)
declare @roleList varchar(4000)


print 'getting my regular project data'

SELECT 
@NAME = [NAME],
@PRIORITY_LEVEL = PRIORITY_LEVEL,
@OBJECTIVE = OBJECTIVE,
@SECURITY_LEVEL = SECURITY_LEVEL,
@ORIGINAL_PLANNED_STOP_DATE = ORIGINAL_PLANNED_STOP_DATE,
@CURRENT_PLANNED_STOP_DATE = CURRENT_PLANNED_STOP_DATE,
@ACTUAL_STOP_DATE = ACTUAL_STOP_DATE,
@MONTH_GOALS = MONTH_GOALS,
@ISSUES = ISSUES,
@SUMMARY = SUMMARY,
@LEADER = LEADER,
@SUMMARY = SUMMARY
FROM A_PROJECTS
WHERE ID = @projectID

set @NAME = 'Copy of ' + @NAME



print 'getting my delimited sponsor list'
print @sql
set @sql = 'SELECT PEOPLE_ID AS ID FROM A_PROJECT_SPONSORS 
			WHERE PROJECT_ID = ''' + @projectID +''''
exec A_SP_Z_UTIL_GET_COMMA_ID_LIST @sql,@sponsorList OUTPUT 

print 'getting my delimited member list'
set @sql = 'SELECT PEOPLE_ID AS ID FROM A_PROJECT_MEMBERS 
			WHERE PROJECT_ID = ''' + @projectID +''''
exec A_SP_Z_UTIL_GET_COMMA_ID_LIST @sql,@memberList OUTPUT 

print 'getting my delimited people allowed to view list'
set @sql = 'SELECT PEOPLE_ID AS ID FROM A_PROJECT_ALLOWED_PEOPLE 
			WHERE PROJECT_ID = ''' + @projectID +''''
exec A_SP_Z_UTIL_GET_COMMA_ID_LIST @sql,@peopleList OUTPUT 

print 'getting my delimited companie allowed to view list'
set @sql = 'SELECT COMPANY_ID AS ID FROM A_PROJECT_ALLOWED_COMPANIES 
			WHERE PROJECT_ID = ''' + @projectID +''''
exec A_SP_Z_UTIL_GET_COMMA_ID_LIST @sql,@companyList OUTPUT 

print 'getting my delimited roles allowed to view list'
set @sql = 'SELECT ROLE_ID AS ID FROM A_PROJECT_ALLOWED_ROLES 
			WHERE PROJECT_ID = ''' + @projectID +''''
exec A_SP_Z_UTIL_GET_COMMA_ID_LIST @sql,@roleList OUTPUT 

exec A_SP_PROJECT_UPDATE_ONE_PROJECT @newID OUTPUT,@msg OUTPUT,
NULL, -- ID
@NAME,
@PRIORITY_LEVEL,
@OBJECTIVE,
@sponsorList, --@SPONSORS
@leader, --@LEADER 
@memberList, --@MEMBERS
@companyList, --@COMPANIES_TO_VIEW 
@roleList, --@ROLES_TO_VIEW
@peopleList, --@PEOPLE_TO_VIEW
@SECURITY_LEVEL,
@ORIGINAL_PLANNED_STOP_DATE,
@CURRENT_PLANNED_STOP_DATE,
@ACTUAL_STOP_DATE,
@MONTH_GOALS,
@ISSUES,
@SUMMARY,
@strNTLogin

print 'Purposes'
INSERT INTO A_BUSINESS_PURPOSES_ITEM_LINK(ID,ITEM_ID,ITEM_TYPE,BUSINESS_PURPOSE_ID,DRCM,MODBY)
SELECT newID(),@newID,ITEM_TYPE,BUSINESS_PURPOSE_ID,getDate(),@strNTLogin 
FROM A_BUSINESS_PURPOSES_ITEM_LINK 
WHERE ITEM_ID = @projectID AND ITEM_TYPE= 'A_PROJECTS'

print 'Objects'
INSERT INTO A_OBJECT_ITEM_LINK (ID,ITEM_ID,ITEM_TYPE,OBJECT_ID,DRCM,MODBY)
SELECT newID(),@newID,ITEM_TYPE,OBJECT_ID,getDate(),@strNTLogin FROM A_OBJECT_ITEM_LINK 
WHERE ITEM_ID = @projectID AND ITEM_TYPE='A_PROJECTS' 

print 'Projects'
INSERT INTO A_PROJECT_ITEM_LINK (ID,ITEM_ID,ITEM_TYPE,PROJECT_ID,DRCM,MODBY)
SELECT newID(),@newID,ITEM_TYPE,PROJECT_ID,getDate(),@strNTLogin  FROM A_PROJECT_ITEM_LINK 
WHERE ITEM_ID = @projectID AND ITEM_TYPE='A_PROJECTS' 






