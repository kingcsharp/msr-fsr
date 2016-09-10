






CREATE    PROCEDURE DBO.A_SP_ADMIN_EMAIL_QUE_ADD_PEOPLE_AND_ROLES_MSG
	@roleList varchar(8000),
	@peopleList varchar(8000),
	@linkPath nvarchar(1000),
	@emailSubject nvarchar(500),
	@strNTLogin nvarchar(50)
as
begin transaction
print 'In here'
CREATE TABLE #TempPeopleList (IT varchar(50))
--splitting the role list first
CREATE TABLE #TempItems	(IT varchar(50))
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @roleList,','
Declare @it nvarchar(50)
Declare @curs Cursor
set @curs = Cursor For SELECT * FROM #TempItems
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	INSERT INTO #TempPeopleList SELECT DISTINCT PERSON FROM A_V_ROLES_APPROVED_WITH_PEOPLE_IDS 
		WHERE ROLE_ID IN (SELECT IT FROM #TempItems)
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs

DELETE FROM #TempItems
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @peopleList,','
INSERT INTO #TempPeopleList SELECT DISTINCT(IT) FROM #TempItems WHERE IT NOT IN (SELECT IT FROM #TempPeopleList)

CREATE TABLE #FinalItems	(IT varchar(50))
INSERT INTO #FinalItems SELECT DISTINCT(IT) FROM #TempPeopleList

declare @html varchar(4000)
set @html = '
			<obj type="text"><attribute name="value" value="ANSWER Update Email" /></obj>
			<obj type="link">
				<attribute name="url" value="' +  dbo.getEmailURL() + '/' + @linkPath + '" />
				<obj type="text"><attribute name="value" value="Link to information" /></obj>
			</obj>'

print 'Inserting email'
INSERT INTO A_ADMIN_EMAIL_QUE (ID,EMAIL_ADDRESS,BODY,SUBJECT,DRCM,MODBY,PERSON_ID)
	SELECT newID() AS ID, WORK_EMAIL_ADDRESS,@html,@emailSubject,getDate(),@strNTLogin,ID
	FROM #FinalItems l,A_V_APPROVED_PEOPLE_SIMPLE_SEARCH p WHERE
	l.IT = p.ID AND p.WORK_EMAIL_ADDRESS is not NULL
print 'Done inserting an email'


fin:
if @@trancount > 0 	commit transaction
return 0
problem:
if @@trancount > 0 	ROLLBACK TRANSACTION
print ' There was a problem in A_SP_ORDERS_FINISH_WF and we will terminate and not finish anything '
return 1






