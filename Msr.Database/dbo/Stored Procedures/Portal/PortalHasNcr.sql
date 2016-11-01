alter    PROCEDURE [dbo].[PortalHasNcr]
@partID varchar(50)
as

declare @sql as nvarchar(4000)
declare @myCo varchar(50)
declare @strWHERE nvarchar(4000)
declare @fieldList nvarchar(4000)


if @fieldList = '' or @fieldList is NULL 
begin

set @fieldList = 'DISTINCT ID '
				
end

set @sql ='SELECT ' + @fieldList + ' FROM A_V_TASK_SEARCH '

set @sql = @sql + 'WHERE PROCEDURE_ID in (SELECT DISTINCT p.ROOT FROM A_O_PROCEDURES p WHERE p.VERB_NAME = ''NCR'') 
AND PROCEDURE_STEP_ID is null   AND  (LATEST_REQUESTEE_NAME LIKE ''%%'' OR LATEST_REQUESTEE_NAME is NULL ) AND  (ORIG_REQUESTOR_NAME LIKE ''%%'' OR ORIG_REQUESTOR_NAME is NULL ) AND  (DESCRIPTION LIKE ''%%'' OR DESCRIPTION is NULL ) AND  (COMPANY_NAME LIKE ''%%'' OR COMPANY_NAME is NULL ) AND  (ID LIKE ''%%'' OR ID is NULL ) AND  (LAST_COMMENT LIKE ''%%'' OR LAST_COMMENT is NULL ) AND  (ASSIGNEE_NAME LIKE ''%%'' OR ASSIGNEE_NAME is NULL )'

print 'SQL = ' + @SQL
declare @myTable TABLE ( ID varchar(50))
INSERT INTO @myTable EXEC(@SQL)

SELECT  COUNT(*)
 FROM @myTable I
	 inner JOIN [A_TASKS] at on I.ID = at.ID
	 inner JOIN [A_V_TASK_PURCHASE_INFORMATION] info on I.ID = info.ID
	 inner JOIN [A_TASK_OBJECT_LINK] ol on at.ID = ol.TASK_ID
	  INNER JOIN A_V_ACTUAL_PARTS_APPROVED_DATA p on p.OBJECT_ID = ol.OBJECT_ID
	  WHERE p.OBJECT_ID LIKE isnull(@partID,'%')