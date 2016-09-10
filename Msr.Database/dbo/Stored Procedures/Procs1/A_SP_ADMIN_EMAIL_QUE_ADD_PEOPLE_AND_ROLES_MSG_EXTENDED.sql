




CREATE     PROCEDURE DBO.A_SP_ADMIN_EMAIL_QUE_ADD_PEOPLE_AND_ROLES_MSG_EXTENDED
	@roleList varchar(8000),
	@peopleList varchar(8000),
	@linkPath nvarchar(1000),
	@emailSubject nvarchar(500),
	@bodyText varchar(8000),
	@strNTLogin nvarchar(50)
as
begin transaction
-- print '--------Sending An Email!!'
-- print 'RoleList = ' + isnull(@roleList,'NULL') 
-- print '@peopleList = ' + isnull(@peopleList,'NULL')
-- print '@linkPath = ' + isnull(@linkPath,'NULL')
-- print '@emailSubject = ' + isnull(@emailSubject,'NULL')
-- print '@bodyText = ' + isnull(@bodyText,'NULL')
-- print '@strNTLogin = ' + isnull(@strNTLogin,'NULL')

CREATE TABLE #TempPeopleList (IT varchar(50))
-- print 'Splitting the role list first'
CREATE TABLE #TempItems	(IT varchar(50))
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @roleList,','
INSERT INTO #TempPeopleList SELECT DISTINCT PERSON FROM A_V_ROLES_APPROVED_WITH_PEOPLE_IDS 
	WHERE ROLE_ID IN (SELECT IT FROM #TempItems)

-- SELECT 'After Roles' AS Note, IT FROM #TempPeopleList

DELETE FROM #TempItems
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @peopleList,','
INSERT INTO #TempPeopleList SELECT DISTINCT(IT) FROM #TempItems WHERE IT NOT IN (SELECT IT FROM #TempPeopleList)

-- SELECT 'After People' AS Note, IT FROM #TempPeopleList

CREATE TABLE #FinalItems	(IT varchar(50))
INSERT INTO #FinalItems SELECT DISTINCT(IT) FROM #TempPeopleList

-- SELECT 'Final' AS Note, IT FROM #FinalItems

declare @html varchar(8000)
set @html = isNull(@bodyText,'') + '
			<obj type="text"><attribute name="value" value="ANSWER Update Email" /></obj>
			<obj type="link">
				<attribute name="url" value="' +  isNull(dbo.getEmailURL(),'NULLEMAILURL') + '/' + isNull(@linkPath,'NULLPATH') + '" />
				<obj type="text"><attribute name="value" value="Link to information" /></obj>
			</obj>'

-- print '@html = ' + isnull(@html,'NULL')
--print 'Inserting now'
-- SELECT * FROM #FinalItems
-- SELECT newID() AS ID, ps.WORK_EMAIL_ADDRESS,@html,@emailSubject,getDate(),@strNTLogin,p.ID
-- 	FROM #FinalItems l LEFT JOIN A_PEOPLE p on p.ID = l.IT
-- 		 LEFT JOIN A_PEOPLE_SEARCH_TABLE ps on p.HISTORY_REF_ID = ps.ID 
-- 	WHERE ps.WORK_EMAIL_ADDRESS is not NULL

INSERT INTO A_ADMIN_EMAIL_QUE (ID,EMAIL_ADDRESS,BODY,SUBJECT,DRCM,MODBY,PERSON_ID)
	SELECT newID() AS ID, ps.WORK_EMAIL_ADDRESS,@html,@emailSubject,getDate(),@strNTLogin,p.ID
	FROM #FinalItems l LEFT JOIN A_PEOPLE p on p.ID = l.IT 
		LEFT JOIN A_PEOPLE_SEARCH_TABLE ps on p.HISTORY_REF_ID = ps.ID 
	WHERE ps.WORK_EMAIL_ADDRESS is not NULL
--print 'Done inserting'
fin:
if @@trancount > 0 	commit transaction
-- print '----Successfully added an email to the que!!'
return 0
problem:
if @@trancount > 0 	ROLLBACK TRANSACTION
print '-----There was a problem in adding and email to the que and we will terminate and not finish anything '
return 1









