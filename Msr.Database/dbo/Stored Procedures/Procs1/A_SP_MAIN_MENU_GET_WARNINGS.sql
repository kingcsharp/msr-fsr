


























CREATE                     PROCEDURE A_SP_MAIN_MENU_GET_WARNINGS
@retVal varchar(8000) OUTPUT,
@retVal2 varchar(8000) OUTPUT,
@retVal3 varchar(8000) OUTPUT,
@strNTLogin varchar(50)
AS
declare @myPurchases as numeric
declare @ordersICanAcceptQuotesFor as numeric
declare @myFills as numeric
declare @myActions as numeric
declare @myRoleActions as numeric
declare @myUnAssignedActions as numeric
declare @myAcceptedActions as numeric
declare @myDisIHaveNotViewed as numeric
declare @myDisIinitiatedWithNewResponses as numeric
declare @myDisIwroteWithNewResponses as numeric
declare @myDisIwantToBeNotified as numeric
declare @myProcedures as numeric
declare @myPendingApprovals as numeric
declare @myParts as numeric
declare @myPartTypes as numeric
declare @myActualParts as numeric
declare @myProcedureTypes as numeric
declare @myPeople as numeric
declare @myRoles as numeric
declare @myCompanies as numeric
declare @myLocations as numeric
declare @myRegions as numeric
declare @myProducts as numeric
declare @myProdPrices as numeric
declare @myCustNeeds as numeric
declare @myProcTheoryHier as numeric
declare @myPrepop as numeric
declare @myAccounts as numeric
declare @myAct2Review as numeric
declare @myForecasts as numeric
declare @myMeetings as numeric
declare @myMeetingsWithin24Hours as numeric
declare @myMeetingsHappeningNow as numeric
declare @meetingsIHaveNotEmailed as numeric
declare @meetings24HoursFromNow as numeric
declare @mySurveysWithNoReply as numeric
declare @mySurveysIinitiatedWithNewResponses as numeric
declare @myProdReqFormsSubmittedToMyRole as numeric
declare @myProdReqFormsAcceptedByMyRole as numeric
declare @myProdReqFormsIAmMaking as numeric
declare @myOrdersIAmMaking as numeric
declare @myOrdersThatNeedQuotes as numeric
declare @quotesIAmCreating as numeric
declare @tasksAcceptedUrgentPastDue as numeric
declare @tasksRequestedYouUrgentPastDue as numeric
declare @tasksRequestedRoleUrgentPastDue as numeric
declare @tasksAcceptedUrgentDueSoon as numeric
declare @tasksRequestedYouUrgentDueSoon as numeric
declare @tasksRequestedRoleUrgentDueSoon as numeric
declare @tasksFromMeUrgentPastDue as numeric
declare @tasksFromMeUrgentDueSoon as numeric
declare @tasksFromMe as numeric
declare @tasksCompletedAndOver7 as numeric
declare @messagesImportantToYouAndNotRead as numeric
declare @messagesToYouAndNotRead as numeric
declare @messagesImportantToYouCCAndNotRead as numeric
declare @messagesToYouCCAndNotRead as numeric
declare @needsCreating as numeric
declare @needsCompanySpecified as numeric
declare @needsYourSpecified as numeric
declare @myTheory as numeric
declare @myExecutingPurchases as numeric
declare @myActualPartsWithTasks as numeric
declare @myInvoicesToInvoice as numeric

-- get my root company
declare @myRootCo as nvarchar(50)
SELECT @myRootCo = ROOT_COMPANY FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @strNTLogin
--exec A_SP_GET_PERSON_COMPANY @strNTLogin,@myRootCo OUTPUT
-- get myCompany
declare @myCo as varchar(50)
SELECT @myCo = COMPANY 
FROM A_APPROVED_PEOPLE 
WHERE ID=@strNTLogin

--get My Roles list
print 'Getting my Role List'
CREATE TABLE #myRoles	(ID varchar(50))
INSERT INTO #myRoles SELECT ROLE_ID FROM A_PERSON_ROLES WHERE PERSON_ID = @strNTLogin
--Make a list of all my roles to pass to the various pages we will visit
print 'Making a list of roles'
Declare @it nvarchar(50), @curs Cursor, @myRoleList varchar(4000)
set @curs = Cursor For SELECT * FROM #myRoles
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	print 'Adding Role = ' + @it
	set @myRoleList = isNULL(@myRoleList + ', ','') + @it
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs
--print 'MyRoleList is ' + @myRoleList
--SELECT @myInvoicesToInvoice = count(INVOICE_ID) FROM A_V_INVOICES_WITH_ACCT_INFORMATION WHERE (CREATING_CO = @myRootCo) AND INVOICE_BALANCE <> 0 AND ACCT_TYPE = 'PURCHASING_ACCOUNT'

--SELECT @myActualPartsWithTasks = count(DISTINCT ACTUAL_PART_ID) FROM A_V_ACTUAL_PARTS_WITH_RECENT_TASKS p
--	WHERE ORIG_REQUESTOR_ID = @strNTLogin AND
--		(REQUESTEE_ID = @strNTLogin or GROUP_REQUESTEE_ID IN (SELECT ID FROM #myRoles))

--SELECT @myExecutingPurchases = count(ID) FROM A_PURCHASES_HISTORY ph WHERE PURCHASE_STATUS = 'ALL_FILLED' AND
--	exists (SELECT ID FROM A_TASKS t,A_TASK_ORDER_INFORMATION oi 
--				WHERE oi.PURCHASE_HIST_ID = ph.ID
--				AND  t.ID = oi.TASK_ID
--				AND PURCHASE_HIST_ID IS NOT NULL
--				AND t.STATUS in ('REQUESTED','ACCEPTED') AND 
--				(t.REQUESTEE_ID = @strNTLogin or t.GROUP_REQUESTEE_ID IN (SELECT ID FROM #myRoles)
--			)
--
--		)
SELECT @myPurchases = count(ID) FROM A_OBJECTS WHERE STATUS = 'CREATING' AND LOCKED_BY = @strNTLogin AND OBJ_TABLE = 'A_PURCHASES_HISTORY' 
SELECT @quotesIAmCreating = count(ID) FROM A_OBJECTS WHERE STATUS = 'CREATING' AND LOCKED_BY = @strNTLogin AND OBJ_TABLE = 'A_QUOTES_HISTORY' 
SELECT @ordersICanAcceptQuotesFor = count(ID) FROM A_O_ORDERS WHERE PROGRESS = 'ALL_QUOTED_WAIT_ACCEPTANCE' AND (CUSTOMER_CO = @myCo or CUSTOMER_PERSON = @strNTLogin)
--SELECT @myOrdersThatNeedQuotes =  count(DISTINCT QOL_ID) FROM A_V_QUOTE_ORDER_LINK_WITH_RESPONSIBLE_ROLE WHERE ROLE_ID IN (SELECT ID FROM #myRoles) AND QUOTE_ID is NULL
SELECT @myOrdersIAmMaking = count(ID) FROM A_OBJECTS WHERE STATUS = 'CREATING' AND LOCKED_BY = @strNTLogin AND OBJ_TABLE = 'A_ORDERS_HISTORY'
SELECT @myProdReqFormsSubmittedToMyRole = count(ID) FROM A_V_PROD_REQUEST_FORMS_WITH_ALL_DATA WHERE PROGRESS = 'PRQ_SUBMITTED' 
	AND CUST_MGR_ROLE IN (SELECT ID FROM #myRoles)
SELECT @myProdReqFormsAcceptedByMyRole = count(ID) FROM A_V_PROD_REQUEST_FORMS_WITH_ALL_DATA WHERE PROGRESS = 'PRQ_ACCEPTED' 
	AND CUST_MGR_ROLE IN (SELECT ID FROM #myRoles)
SELECT @myProdReqFormsIAmMaking = count(ID) FROM A_V_PROD_REQUEST_FORMS_WITH_ALL_DATA WHERE PROGRESS = 'PRF_NEW_STATUS' AND UPLOADER = @strNTLogin

SELECT @myAct2Review = count(ID) FROM A_TASKS WHERE STATUS = 'FINISHED' AND ORIG_REQUESTOR_ID = @strNTLogin
SELECT @myActions = count(ID) FROM A_TASKS WHERE STATUS = 'REQUESTED' AND REQUESTEE_ID = @strNTLogin
SELECT @myAcceptedActions = count(ID) FROM A_TASKS WHERE STATUS = 'ACCEPTED' AND REQUESTEE_ID = @strNTLogin
SELECT @myRoleActions = count(ID) FROM A_TASKS WHERE STATUS = 'REQUESTED' AND GROUP_REQUESTEE_ID in (SELECT ID FROM #myRoles)
SELECT @myUnAssignedActions = count(ID) FROM A_TASKS WHERE STATUS = 'CREATING' AND CREATED_BY = @strNTLogin
SELECT @tasksFromMe = count(ID) FROM A_TASKS WHERE ORIG_REQUESTOR_ID = @strNTLogin
SELECT @tasksCompletedAndOver7 = count(ID) FROM A_TASKS WHERE STATUS = 'FINISHED' AND REQUESTEE_ID = @strNTLogin AND ACTUAL_STOP_DATE < (DATEADD(dd,-7,getDate()))
SELECT @tasksAcceptedUrgentPastDue = count(ID) FROM A_TASKS WHERE STATUS = 'ACCEPTED' AND REQUESTEE_ID = @strNTLogin AND CUR_PLANNED_STOP_DATE < (getDate()) AND (PRIORITY = 1 or PRIORITY = 2)
SELECT @tasksRequestedYouUrgentPastDue = count(ID) FROM A_TASKS WHERE STATUS = 'REQUESTED' AND REQUESTEE_ID = @strNTLogin AND CUR_PLANNED_STOP_DATE < (getDate()) AND (PRIORITY = 1 or PRIORITY = 2)
SELECT @tasksRequestedRoleUrgentPastDue = count(ID) FROM A_TASKS WHERE STATUS = 'REQUESTED' AND GROUP_REQUESTEE_ID in (SELECT ID FROM #myRoles) AND CUR_PLANNED_STOP_DATE < (getDate()) AND (PRIORITY = 1 or PRIORITY = 2)
SELECT @tasksFromMeUrgentPastDue = count(ID) FROM A_TASKS WHERE (STATUS = 'REQUESTED' or STATUS = 'ACCEPTED') AND ORIG_REQUESTOR_ID = @strNTLogin AND CUR_PLANNED_STOP_DATE < (getDate()) AND (PRIORITY = 1 or PRIORITY = 2)
SELECT @tasksAcceptedUrgentDueSoon = count(ID) FROM A_TASKS WHERE STATUS = 'ACCEPTED' AND REQUESTEE_ID = @strNTLogin AND CUR_PLANNED_STOP_DATE < (getDate() + 7) AND CUR_PLANNED_STOP_DATE >= getDate() AND (PRIORITY = 1 or PRIORITY = 2)
SELECT @tasksRequestedYouUrgentDueSoon = count(ID) FROM A_TASKS WHERE STATUS = 'REQUESTED' AND REQUESTEE_ID = @strNTLogin AND CUR_PLANNED_STOP_DATE < (getDate() + 7) AND CUR_PLANNED_STOP_DATE >= getDate() AND (PRIORITY = 1 or PRIORITY = 2)
SELECT @tasksRequestedRoleUrgentDueSoon = count(ID) FROM A_TASKS WHERE STATUS = 'REQUESTED' AND GROUP_REQUESTEE_ID in (SELECT ID FROM #myRoles) AND CUR_PLANNED_STOP_DATE < (getDate() + 7) AND CUR_PLANNED_STOP_DATE >= getDate() AND (PRIORITY = 1 or PRIORITY = 2)
SELECT @tasksFromMeUrgentDueSoon = count(ID) FROM A_TASKS WHERE (STATUS = 'REQUESTED' or STATUS = 'ACCEPTED') AND ORIG_REQUESTOR_ID = @strNTLogin AND CUR_PLANNED_STOP_DATE < (getDate() + 7) AND CUR_PLANNED_STOP_DATE >= getDate() AND (PRIORITY = 1 or PRIORITY = 2)

SELECT @myProcedures = count(ID) FROM A_OBJECTS WHERE (STATUS = 'CREATING' OR STATUS = 'DENIED') AND LOCKED_BY = @strNTLogin AND OBJ_TABLE = 'A_PROCEDURES_HISTORY'
SELECT @myParts = count(ID) FROM A_OBJECTS WHERE (STATUS = 'CREATING' OR STATUS = 'DENIED') AND LOCKED_BY = @strNTLogin AND OBJ_TABLE = 'A_PARTS_HISTORY'
SELECT @myPartTypes = count(ID) FROM A_OBJECTS WHERE (STATUS = 'CREATING' OR STATUS = 'DENIED') AND LOCKED_BY = @strNTLogin AND OBJ_TABLE = 'A_PART_TYPES_HISTORY'
SELECT @myActualParts = count(ID) FROM A_OBJECTS WHERE (STATUS = 'CREATING' OR STATUS = 'DENIED') AND LOCKED_BY = @strNTLogin AND OBJ_TABLE = 'A_ACTUAL_PARTS_HISTORY'
SELECT @myProcedureTypes = count(ID) FROM A_OBJECTS WHERE (STATUS = 'CREATING' OR STATUS = 'DENIED') AND LOCKED_BY = @strNTLogin AND OBJ_TABLE = 'A_TT_VERBS_HISTORY'
SELECT @myProcTheoryHier = count(ID) FROM A_OBJECTS WHERE (STATUS = 'CREATING' OR STATUS = 'DENIED') AND LOCKED_BY = @strNTLogin AND OBJ_TABLE = 'A_NOUN_HIERARCHIES_HISTORY'
SELECT @myPeople = count(ID) FROM A_OBJECTS WHERE (STATUS = 'CREATING' OR STATUS = 'DENIED') AND LOCKED_BY = @strNTLogin AND OBJ_TABLE = 'A_PEOPLE_HISTORY'
SELECT @myRoles = count(ID) FROM A_OBJECTS WHERE (STATUS = 'CREATING' OR STATUS = 'DENIED') AND LOCKED_BY = @strNTLogin AND OBJ_TABLE = 'A_ROLES_HISTORY'
SELECT @myCompanies = count(ID) FROM A_OBJECTS WHERE (STATUS = 'CREATING' OR STATUS = 'DENIED') AND LOCKED_BY = @strNTLogin AND OBJ_TABLE = 'A_COMPANIES_HISTORY'
SELECT @myLocations = count(ID) FROM A_OBJECTS WHERE (STATUS = 'CREATING' OR STATUS = 'DENIED') AND LOCKED_BY = @strNTLogin AND OBJ_TABLE = 'A_LOCATIONS_HISTORY'
SELECT @myRegions = count(ID) FROM A_OBJECTS WHERE (STATUS = 'CREATING' OR STATUS = 'DENIED') AND LOCKED_BY = @strNTLogin AND OBJ_TABLE = 'A_REGIONS_HISTORY'
SELECT @myProducts = count(ID) FROM A_OBJECTS WHERE (STATUS = 'CREATING' OR STATUS = 'DENIED') AND LOCKED_BY = @strNTLogin AND OBJ_TABLE = 'A_PRODUCTS_HISTORY'
SELECT @myProdPrices = count(ID) FROM A_OBJECTS WHERE (STATUS = 'CREATING' OR STATUS = 'DENIED') AND LOCKED_BY = @strNTLogin AND OBJ_TABLE = 'A_PROD_PRICE_LIST_HISTORY'
SELECT @myCustNeeds = count(ID) FROM A_OBJECTS WHERE (STATUS = 'CREATING' OR STATUS = 'DENIED') AND LOCKED_BY = @strNTLogin AND OBJ_TABLE = 'A_CUSTOMER_NEEDS_HISTORY'
SELECT @myPendingApprovals = count(PERSON_ID) FROM A_V_APPROVALS_PENDING WHERE PERSON_ID = @strNTlogin
SELECT @myPrepop = count(ID) FROM A_OBJECTS WHERE (STATUS = 'CREATING' OR STATUS = 'DENIED') AND LOCKED_BY = @strNTLogin AND OBJ_TABLE = 'A_PREPOP_HISTORY'
SELECT @myAccounts = count(ID) FROM A_OBJECTS WHERE (STATUS = 'CREATING' OR STATUS = 'DENIED') AND LOCKED_BY = @strNTLogin AND OBJ_TABLE = 'A_ACCOUNTS_HISTORY'
SELECT @myForecasts = count(ID) FROM A_OBJECTS WHERE (STATUS = 'CREATING' OR STATUS = 'DENIED') AND LOCKED_BY = @strNTLogin AND OBJ_TABLE = 'A_FORECASTS_HISTORY'
SELECT @needsCreating = count(ID) FROM A_OBJECTS WHERE (STATUS = 'CREATING' OR STATUS = 'DENIED') AND LOCKED_BY = @strNTLogin AND OBJ_TABLE = 'A_NEEDS_HISTORY'
SELECT @myTheory = count(ID) FROM A_OBJECTS WHERE OBJ_TABLE = 'A_THEORY_HISTORY' AND (STATUS = 'CREATING' OR STATUS = 'DENIED') AND LOCKED_BY = @strNTLogin

SELECT DISTINCT @myMeetings = count(ID) 
FROM A_V_MEETING_SEARCH  
WHERE (SCRIBE= @strNTLogin OR OWNER= @strNTLogin OR TIME_KEEP= @strNTLogin 
	OR ID IN (SELECT MEETING_ID FROM A_MEETING_INV_PEOPLE WHERE PEOPLE_ID=@strNTLogin)
	OR ID IN (SELECT MEETING_ID FROM A_MEETING_REALLY_INIVTED_COMPANY WHERE COMPANY_ID=@myCo) 
	OR ID IN (SELECT MEETING_ID FROM A_MEETING_INV_ROLE WHERE ROLE_ID IN (SELECT ID FROM #myRoles)))
	AND	(START_DATE >= getDate() OR getDate() BETWEEN START_DATE AND STOP_DATE)
	AND (ID NOT IN (SELECT MEETING_ID FROM A_MEETING_RESPONSES WHERE PERSON_ID = @strNTlogin))

SELECT DISTINCT @myMeetingsHappeningNow = count(ID)   
FROM A_V_MEETING_SEARCH  
WHERE (SCRIBE= @strNTLogin OR OWNER= @strNTLogin OR TIME_KEEP= @strNTLogin 
	OR ID IN (SELECT MEETING_ID FROM A_MEETING_INV_PEOPLE WHERE PEOPLE_ID=@strNTLogin)
	OR ID IN (SELECT MEETING_ID FROM A_MEETING_REALLY_INIVTED_COMPANY WHERE COMPANY_ID=@myCo) 
	OR ID IN (SELECT MEETING_ID FROM A_MEETING_INV_ROLE WHERE ROLE_ID IN (SELECT ID FROM #myRoles)))
	AND	(getDate() BETWEEN START_DATE AND STOP_DATE 
		 AND ID IN (SELECT MEETING_ID FROM A_MEETING_RESPONSES WHERE PERSON_ID = @strNTlogin AND RESPONSE = 'ACCEPT'))
	 

SELECT DISTINCT @meetings24HoursFromNow = count(ID)   
FROM A_V_MEETING_SEARCH  
WHERE (SCRIBE= @strNTLogin OR OWNER= @strNTLogin OR TIME_KEEP= @strNTLogin 
	OR ID IN (SELECT MEETING_ID FROM A_MEETING_INV_PEOPLE WHERE PEOPLE_ID=@strNTLogin)
	OR ID IN (SELECT MEETING_ID FROM A_MEETING_REALLY_INIVTED_COMPANY WHERE COMPANY_ID=@myCo) 
	OR ID IN (SELECT MEETING_ID FROM A_MEETING_INV_ROLE WHERE ROLE_ID IN (SELECT ID FROM #myRoles)))
	AND	(DATEDIFF(hour, getDate(),START_DATE) <= 24 AND START_DATE > getDate()
	AND ID IN (SELECT MEETING_ID FROM A_MEETING_RESPONSES WHERE PERSON_ID = @strNTlogin AND RESPONSE = 'ACCEPT'))

SELECT DISTINCT @meetingsIHaveNotEmailed = count(ID)   
FROM A_V_MEETING_SEARCH  
WHERE OWNER= @strNTLogin AND DATE_EMAIL_SENT IS NULL AND START_DATE >= GETDATE() 

SELECT @myDisIHaveNotViewed= count(DISTINCT ID) FROM A_V_DISCUSSION_SEARCH
WHERE ID NOT IN (select DISCUSSION_ID from A_DISCUSSION_VIEWED_BY_PEOPLE WHERE PERSON_ID = @strNTlogin) 
AND (I_ID =@strNTLogin 
     OR ID IN (SELECT DISCUSSION_ID FROM A_DISCUSSION_INV_PEOPLE WHERE PEOPLE_ID = @strNTLogin )
	 OR ID IN (SELECT DISCUSSION_ID FROM A_DISCUSSION_INV_COMPANY WHERE COMPANY_ID =  @myCo )
     OR ID IN (SELECT DISCUSSION_ID FROM A_DISCUSSION_INV_ROLE WHERE ROLE_ID IN  (SELECT ID FROM #myRoles))
	 )

SELECT @myDisIinitiatedWithNewResponses= count(DISTINCT DISCUSSION_ID) FROM A_DISCUSSION_RESPONSE_ALERTS WHERE R_TYPE ='INITIATOR' AND ALERTEE_ID = @strNTlogin
SELECT @myDisIwroteWithNewResponses= count(DISTINCT DISCUSSION_ID) FROM A_DISCUSSION_RESPONSE_ALERTS WHERE R_TYPE ='WRITER' AND ALERTEE_ID = @strNTlogin
SELECT @myDisIwantToBeNotified = count(DISTINCT DISCUSSION_ID) FROM A_DISCUSSION_RESPONSE_ALERTS WHERE R_TYPE ='NOTIFY' AND ALERTEE_ID = @strNTlogin

SELECT @mySurveysWithNoReply= count(distinct ID) FROM A_V_SURVEY_BY_ID s
WHERE  (ID IN (SELECT SURVEY_ID FROM A_SURVEY_INV_PEOPLE WHERE SURVEY_ID = ID AND PEOPLE_ID=@strNTLogin)
		OR ID IN (SELECT SURVEY_ID FROM A_SURVEY_REALLY_INIVTED_COMPANY WHERE COMPANY_ID=@myCo) 
		OR ID IN (SELECT SURVEY_ID FROM A_SURVEY_INV_ROLE WHERE ROLE_ID IN (SELECT ID FROM #myRoles))) 
		AND ((STATUS='OPEN' AND OWNER <> @strNTLogin) AND NOT EXISTS (SELECT * FROM A_SURVEY_REPLIES WHERE ROOT = s.ID AND AUTHOR = @strNTLogin))

SELECT @mySurveysIinitiatedWithNewResponses= count(DISTINCT ID) FROM A_SURVEY_RESPONSE_ALERTS WHERE ALERTEE_ID = @strNTlogin

create table #serviceCallWeeks(dt datetime)
INSERT INTO #serviceCallWeeks SELECT DISTINCT ACTUAL_START_DATE FROM A_V_SERVICE_CALLS_WEEKLY_DATA WHERE 
	(WORKER_ID = @strNTlogin AND STATUS ='WORKER') OR
	(WORKER_ID IN (SELECT ID FROM A_V_PEOPLE_SELF_BOSS_IF_NULL WHERE BOSS = @strNTLogin) AND STATUS='BOSS') OR
	(APPROVER_ROLE IN (SELECT ID FROM #myRoles) AND STATUS ='CUSTOMER_REVIEW_GROUP') OR
	(PAYER_ROLE IN (SELECT ID FROM #myRoles) AND STATUS ='CUSTOMER_AP_GROUP') OR
	(RECEIVABLE_ROLE IN (SELECT ID FROM #myRoles) AND STATUS ='AR_GROUP')

SELECT @messagesImportantToYouAndNotRead = count(DISTINCT ID) FROM A_V_MESSAGES_SEARCH_DATA WHERE IMPORTANCE = 'HIGH' AND STATUS = 'SENT' AND ID IN (SELECT MESSAGE_ID FROM A_MESSAGES_PEOPLE_LINK WHERE PERSON_ID = @strNTlogin AND IS_CC_MESSAGE = 0 AND IS_READ = 0)
SELECT @messagesToYouAndNotRead = count(DISTINCT ID) FROM A_V_MESSAGES_SEARCH_DATA WHERE IMPORTANCE = 'NORMAL' AND STATUS = 'SENT' AND  ID IN (SELECT MESSAGE_ID FROM A_MESSAGES_PEOPLE_LINK WHERE PERSON_ID = @strNTlogin AND IS_CC_MESSAGE = 0 AND IS_READ = 0)
SELECT @messagesImportantToYouCCAndNotRead = count(DISTINCT ID) FROM A_V_MESSAGES_SEARCH_DATA WHERE IMPORTANCE = 'HIGH' AND STATUS = 'SENT' AND  ID IN (SELECT MESSAGE_ID FROM A_MESSAGES_PEOPLE_LINK WHERE PERSON_ID = @strNTlogin AND IS_CC_MESSAGE = 1 AND IS_READ = 0)
SELECT @messagesToYouCCAndNotRead = count(DISTINCT ID) FROM A_V_MESSAGES_SEARCH_DATA WHERE IMPORTANCE = 'NORMAL' AND STATUS = 'SENT' AND  ID IN (SELECT MESSAGE_ID FROM A_MESSAGES_PEOPLE_LINK WHERE PERSON_ID = @strNTlogin AND IS_CC_MESSAGE = 1 AND IS_READ = 0)
SELECT @needsCompanySpecified = count(DISTINCT ID) 
	FROM A_V_NEEDS_APPROVED_DATA 
	WHERE 
		(
		GETDATE() BETWEEN ADVERTISING_START_DATE AND ADVERTISING_STOP_DATE 
		OR 
			(ADVERTISING_START_DATE IS NULL AND ADVERTISING_STOP_DATE > GETDATE())
			OR (ADVERTISING_STOP_DATE IS NULL AND ADVERTISING_START_DATE < GETDATE())
		) 		AND 
		(
		HISTORY_REF_ID IN 
			(SELECT NEED_ID FROM A_NEEDS_COMPANIES_ALLOWED 
				WHERE CO_ID = @myCo 
					OR CO_ID IN 
					(SELECT COMPANY FROM A_COMPANIES_CHILD_LOOKUP_TABLE WHERE CHILD_COMPANY = @myCo)
					OR CO_ID IN 
					(SELECT CHILD_COMPANY FROM A_COMPANIES_CHILD_LOOKUP_TABLE WHERE COMPANY = @myCo)
			)
		)  

SELECT @needsYourSpecified= count(DISTINCT ID) 
	FROM A_V_NEEDS_APPROVED_DATA 
	WHERE 
	(
	GETDATE() BETWEEN ADVERTISING_START_DATE AND ADVERTISING_STOP_DATE 
	OR 
		(ADVERTISING_START_DATE IS NULL AND ADVERTISING_STOP_DATE > GETDATE()) 
		OR 
		(ADVERTISING_STOP_DATE IS NULL AND ADVERTISING_START_DATE < GETDATE())
	) 
	AND HISTORY_REF_ID IN 
		(SELECT NEED_ID FROM A_NEEDS_PEOPLE_ALLOWED 
			WHERE PERSON_ID = @strNTlogin
		)  


print 'fixing to get my data'
print isnull((@myActions),'0')
declare @so as varchar(8000)
set @so = '<w>'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so

set @so = '<i n="myPurchases" 
d="Quotes_4_1" 
v="' + convert(varchar(10),(SELECT case when (@myPurchases = 0) then null else @myPurchases end)) + '" 
h="asp/purchases/search.asp?emp=' + @strNTLogin + '&amp;STATUS=CREATING%2C+DENIED"
c="mm" />'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so

set @so = '<i n="myExecutingPurchases" 
d="Quotes_4_2" 
v="' + convert(varchar(10),(SELECT case when (@myExecutingPurchases = 0) then null else @myExecutingPurchases end)) + '" 
h="asp/purchases/search.asp?PURCHASE_STATUS=ALL_FILLED&amp;executing=true"
c="mm" />'
if @so is not null
	exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so


set @so = '<i n="myFills" 
d="Quotes_5_1" 
v="' + convert(varchar(10),(SELECT case when (@myFills = 0) then null else @myFills end)) + '" 
h="asp/fills/search.asp?STATUS=NOT_FILLED" 
c="mm" />'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so

--set @so = '<i n="ordersICanAcceptQuotesFor" 
--d="Quotes_2_3" 
--v="' + convert(varchar(10),(SELECT case when (@ordersICanAcceptQuotesFor = 0) then null else @ordersICanAcceptQuotesFor end)) + '" 
--h="asp/orders/search.asp?PROGRESS=ALL_QUOTED_WAIT_ACCEPTANCE" 
--c="mm" />'
--exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so


set @so = '<i n="Quotes_IMCreating"  
d="Quotes_3_1" 
v="' + convert(varchar(10),(SELECT case when (@quotesIAmCreating = 0) then null else @quotesIAmCreating end)) + '" 
h="asp/quotes/search.asp?emp=' + @strNTLogin + '&amp;STATUS=CREATING%2C+DENIED" c="mm" />'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so

set @so = '<i n="OrdersICanQuote" 
d="Quotes_3_2" 
v="' + convert (varchar(50),(select case when 
(@myOrdersThatNeedQuotes = 0) then NULL else 
@myOrdersThatNeedQuotes end)) + '" 
h="asp/quoteOrderLink/searchOrdersThatNeedQuotes.asp?emp=' + @strNTLogin + '&amp;STATUS=NEW" 
c="mm" />'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so


set @so = '<i n="Orders_IMCreating" 
d="Quotes_2" 
v="' + convert (varchar(50),(select case when 
(@myOrdersIAmMaking = 0) then NULL else 
@myOrdersIAmMaking end)) + '" 
h="asp/orders/search.asp?emp=' + @strNTLogin + '&amp;STATUS=CREATING%2C+DENIED" 
c="mm" />'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so

--Escalations
set @so = '<i n="Task_AccByMe_Past" 
d="Projects_5_6" 
v="' + convert (varchar(50),(select case when 
(@tasksAcceptedUrgentPastDue = 0) then NULL else 
@tasksAcceptedUrgentPastDue end)) + '" 
h="asp/ActualTasks/searchTasks.asp?people=' + @strNTLogin + '&amp;searchMethod=Mine&amp;STATUS=ACCEPTED&amp;PRIORITY=1,2&amp;DUE_DATE=PAST_DUE"
c="escalation" />'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="Task_Req2Me_Past" 
d="Projects_5_7" 
v="' + convert (varchar(50),(select case when 
(@tasksRequestedYouUrgentPastDue = 0) then NULL else 
@tasksRequestedYouUrgentPastDue end)) + '" 
h="asp/ActualTasks/searchTasks.asp?people=' + @strNTLogin + '&amp;searchMethod=Mine&amp;STATUS=REQUESTED&amp;PRIORITY=1,2&amp;DUE_DATE=PAST_DUE"
c="escalation" />'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="Task_Req2MeRole_Past" 
d="Projects_5_8" 
v="' + convert (varchar(50),(select case when 
(@tasksRequestedRoleUrgentPastDue = 0) then NULL else 
@tasksRequestedRoleUrgentPastDue end)) + '" 
h="asp/ActualTasks/searchTasks.asp?searchMethod=MyRole&amp;STATUS=REQUESTED&amp;PRIORITY=1,2&amp;DUE_DATE=PAST_DUE" 
c="escalation" />'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="Task_FromMe_Past" 
d="Projects_5_12" 
v="' + convert (varchar(50),(select case when 
(@tasksFromMeUrgentPastDue = 0) then NULL else 
@tasksFromMeUrgentPastDue end)) + '" 
h="asp/ActualTasks/searchTasks.asp?people=' + @strNTLogin + '&amp;searchMethod=IMRequestor&amp;STATUS=REQUESTED,ACCEPTED&amp;PRIORITY=1,2&amp;DUE_DATE=PAST_DUE"
c="escalation" />'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
--Due Soon
set @so = '<i n="Task_AccByMe_Dues" 
d="Projects_5_9" 
v="' + convert (varchar(50),(select case when 
(@tasksAcceptedUrgentDueSoon = 0) then NULL else 
@tasksAcceptedUrgentDueSoon end)) + '" 
h="asp/ActualTasks/searchTasks.asp?people=' + @strNTLogin + '&amp;searchMethod=Mine&amp;STATUS=ACCEPTED&amp;PRIORITY=1,2&amp;DUE_DATE=DUE_SOON"
c="warning" />'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="Task_Req2Me_Dues" 
d="Projects_5_10" 
v="' + convert (varchar(50),(select case when 
(@tasksRequestedYouUrgentDueSoon = 0) then NULL else 
@tasksRequestedYouUrgentDueSoon end)) + '" 
h="asp/ActualTasks/searchTasks.asp?people=' + @strNTLogin + '&amp;searchMethod=Mine&amp;STATUS=REQUESTED&amp;PRIORITY=1,2&amp;DUE_DATE=DUE_SOON"
c="warning" />'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="Task_Req2MeRole_Dues" 
d="Projects_5_11" 
v="' + convert (varchar(50),(select case when 
(@tasksRequestedRoleUrgentDueSoon = 0) then NULL else 
@tasksRequestedRoleUrgentDueSoon end)) + '" 
h="asp/ActualTasks/searchTasks.asp?searchMethod=MyRole&amp;STATUS=REQUESTED&amp;PRIORITY=1,2&amp;DUE_DATE=DUE_SOON" 
c="warning" />'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="Task_FromMe_Dues" 
d="Projects_5_13" 
v="' + convert (varchar(50),(select case when 
(@tasksFromMeUrgentDueSoon = 0) then NULL else 
@tasksFromMeUrgentDueSoon end)) + '" 
h="asp/ActualTasks/searchTasks.asp?people=' + @strNTLogin + '&amp;searchMethod=IMRequestor&amp;STATUS=REQUESTED,ACCEPTED&amp;PRIORITY=1,2&amp;DUE_DATE=DUE_SOON"
c="warning" />'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="Task_I_ACCEPTED" 
d="Projects_5_5" 
v="' + convert (varchar(50),(select case when 
(@myAcceptedActions = 0) then NULL else 
@myAcceptedActions end)) + '" 
h="asp/ActualTasks/searchTasks.asp?people=' + @strNTLogin + '&amp;searchMethod=Mine&amp;STATUS=ACCEPTED" 
c="mm" />'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="Task_Req2Me" 
d="Projects_5_1" 
v="' + convert (varchar(50),(select case when 
(@myActions = 0) then NULL else 
@myActions end)) + '" 
h="asp/ActualTasks/searchTasks.asp?people=' + @strNTLogin + '&amp;searchMethod=Mine&amp;STATUS=REQUESTED" 
c="mm" />'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="Task_Req2MeRole" 
d="Projects_5_2" 
v="' + convert (varchar(50),(select case when 
(@myRoleActions = 0) then NULL else 
@myRoleActions end)) + '" 
h="asp/ActualTasks/searchTasks.asp?people=' + @strNTLogin + '&amp;searchMethod=MyRole&amp;STATUS=REQUESTED" 
c="mm"  />'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="Task_IMCreating" 
d="Projects_5_3" 
v="' + convert (varchar(50),(select case when 
(@myUnAssignedActions = 0) then NULL else 
@myUnAssignedActions end)) + '" 
h="asp/ActualTasks/searchTasks.asp?people=' + @strNTLogin + '&amp;searchMethod=IMCreating&amp;STATUS=CREATING" 
c="mm"  />'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
--set @so = '<i n="Task_FromMe" 
--d="Projects_5_14" 
--v="' + ltrim(isnull(str(@tasksFromMe),'0')) + '" 
--h="asp/ActualTasks/searchTasks.asp?people=' + @strNTLogin + '&amp;searchMethod=IMRequestor" 
--c="mm"  />'
--exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="Task_4Review" 
d="Projects_5_4" 
v="' + convert (varchar(50),(select case when 
(@myAct2Review = 0) then NULL else 
@myAct2Review end)) + '" 
h="asp/ActualTasks/searchTasks.asp?people=' + @strNTLogin + '&amp;searchMethod=IMRequestor&amp;STATUS=FINISHED" 
c="mm"  />'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="Task_I_COMPLETED_Past7" 
d="Projects_5_13" 
v="' + convert (varchar(50),(select case when 
(@tasksCompletedAndOver7 = 0) then NULL else 
@tasksCompletedAndOver7 end)) + '" 
h="asp/ActualTasks/searchTasks.asp?people=' + @strNTLogin + '&amp;searchMethod=Mine&amp;STATUS=FINISHED&amp;DUE_DATE=COMPLETE_7"
c="mm" />'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="Procs_IMCreating" 
d="Procedures_1" 
v="' + convert (varchar(50),(select case when 
(@myProcedures = 0) then NULL else 
@myProcedures end)) + '" 
h="asp/Procedures/searchProcedures.asp?emp=' + @strNTLogin + '&amp;STATUS=CREATING%2C+DENIED" 
c="mm"/>'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="ProcTypes_IMCreating" 
d="Procedures_2" 
v="' + convert (varchar(50),(select case when 
(@myProcedureTypes = 0) then NULL else 
@myProcedureTypes end)) + '" 
h="asp/procedureVerbs/searchVerbs.asp?emp=' + @strNTLogin + '&amp;STATUS=CREATING%2C+DENIED" 
c="mm"/>'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="ProcTheory_IMCreating" 
d="Procedures_3" 
v="' + convert (varchar(50),(select case when 
(@myProcTheoryHier = 0) then NULL else 
@myProcTheoryHier end)) + '" 
h="asp/Procedures/searchProcedures.asp?emp=' + @strNTLogin + '&amp;STATUS=CREATING%2C+DENIED" 
c="mm"/>'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="Prepop_IMCreating" 
d="Procedures_4" 
v="' + convert (varchar(50),(select case when 
(@myPrepop = 0) then NULL else 
@myPrepop end)) + '" 
h="asp/ProcedurePrepop/searchPrepop.asp?emp=' + @strNTLogin + '&amp;STATUS=CREATING%2C+DENIED" 
c="mm"/>'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="Parts_IMCreating" 
d="Parts_1" 
v="' + convert (varchar(50),(select case when 
(@myParts = 0) then NULL else 
@myParts end)) + '" 
h="asp/Parts/searchParts.asp?emp=' + @strNTLogin + '&amp;STATUS=CREATING%2C+DENIED" 
c="mm" />'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="PartTypes_IMCreating" 
d="Parts_2" 
v="' + convert (varchar(50),(select case when 
(@myPartTypes = 0) then NULL else 
@myPartTypes end)) + '" 
h="asp/PartTypes/searchPartTypes.asp?emp=' + @strNTLogin + '&amp;STATUS=CREATING%2C+DENIED" 
c="mm"/>'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="ActualParts_IMCreating" 
d="Parts_3" 
v="' + convert (varchar(50),(select case when 
(@myActualParts = 0) then NULL else 
@myActualParts end)) + '" 
h="asp/actualParts/searchActualParts.asp?emp=' + @strNTLogin + '&amp;STATUS=CREATING%2C+DENIED&amp;SEARCH_METHOD=SHOW_CHILDREN_AND_PARENTS" 
c="mm"/>'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="ActualParts_ICanWorkOn" 
d="Parts_3" 
v="' + convert (varchar(50),(select case when 
(@myActualPartsWithTasks = 0) then NULL else 
@myActualPartsWithTasks end)) + '" 
h="asp/actualParts/searchActualParts.asp?showTasks=yes&amp;specSearch=PartsWithWork&amp;SEARCH_METHOD=SHOW_CHILDREN_AND_PARENTS&amp;STATUS=APPROVED, APPROVED_BUT_REVISING, APPROVED_BUT_DELETING" 
c="mm"/>'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="People_IMCreating" 
d="People_1" 
v="' + convert (varchar(50),(select case when 
(@myPeople = 0) then NULL else 
@myPeople end)) + '" 
h="asp/People/searchPeople.asp?emp=' + @strNTLogin + '&amp;STATUS=CREATING%2C+DENIED" 
c="mm"/>'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="Roles_IMCreating" 
d="People_2" 
v="' + convert (varchar(50),(select case when 
(@myRoles = 0) then NULL else 
@myRoles end)) + '" 
h="asp/Roles/searchRoles.asp?emp=' + @strNTLogin + '&amp;STATUS=CREATING%2C+DENIED" 
c="mm"/>'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="Companies_IMCreating" 
d="People_3" 
v="' + convert (varchar(50),(select case when 
(@myCompanies = 0) then NULL else 
@myCompanies end)) + '" 
h="asp/Companies/searchCompanies.asp?emp=' + @strNTLogin + '&amp;STATUS=CREATING%2C+DENIED" 
c="mm"/>'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="Location_IMCreating" 
d="Locations_1" 
v="' + convert (varchar(50),(select case when 
(@myLocations = 0) then NULL else 
@myLocations end)) + '" 
h="asp/Locations/searchLocations.asp?emp=' + @strNTLogin + '&amp;STATUS=CREATING%2C+DENIED" 
c="mm"/>'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="Region_IMCreating" 
d="Locations_2" 
v="' + convert (varchar(50),(select case when 
(@myRegions = 0) then NULL else 
@myRegions end)) + '" 
h="asp/ActualTasks/searchTasks.asp?emp=' + @strNTLogin + '&amp;STATUS=CREATING%2C+DENIED" 
c="mm"/>'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="Products_IMCreating" 
d="Products_1" 
v="' + convert (varchar(50),(select case when 
(@myProducts = 0) then NULL else 
@myProducts end)) + '" 
h="asp/Products/searchProducts.asp?emp=' + @strNTLogin + '&amp;STATUS=CREATING%2C+DENIED" 
c="mm"/>'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="ProdPrices_IMCreating" 
d="Products_2" 
v="' + convert (varchar(50),(select case when 
(@myProdPrices = 0) then NULL else 
@myProdPrices end)) + '" 
h="asp/prodListPrice/searchPriceList.asp?emp=' + @strNTLogin + '&amp;STATUS=CREATING%2C+DENIED" 
c="mm"/>'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="PRF_ToMyRoles" 
d="Products_3_1" 
v="' + convert (varchar(50),(select case when 
(@myProdReqFormsSubmittedToMyRole = 0) then NULL else 
@myProdReqFormsSubmittedToMyRole end)) + '" 
h="asp/prodReqForm/searchReqForm.asp?MGR_ROLE=' + isNull(@myRoleList,'') + '&amp;PROGRESS=PRQ_SUBMITTED"
c="mm"/>'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="PRF_AccptByMyRoles" 
d="Products_3_2" 
v="' + convert (varchar(50),(select case when 
(@myProdReqFormsAcceptedByMyRole = 0) then NULL else 
@myProdReqFormsAcceptedByMyRole end)) + '" 
h="asp/prodReqForm/searchReqForm.asp?MGR_ROLE=' + isNull(@myRoleList,'') + '&amp;PROGRESS=PRQ_ACCEPTED"
c="mm"/>'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="PRF_IMCreating" 
d="Products_3_3" 
v="' + convert (varchar(50),(select case when 
(@myProdReqFormsIAmMaking = 0) then NULL else 
@myProdReqFormsIAmMaking end)) + '" 
h="asp/prodReqForm/searchReqForm.asp?UPLOADER=' + isNull(@strNTLogin,'') + '&amp;STATUS=PRF_NEW_STATUS"
c="mm"/>'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="Accounts_IMCreating" 
d="Forecast_1" 
v="' + convert (varchar(50),(select case when 
(@myAccounts = 0) then NULL else 
@myAccounts end)) + '" 
h="asp/Accounts/search.asp?emp=' + @strNTLogin + '&amp;STATUS=CREATING%2C+DENIED" 
c="mm"/>'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
-- set @so = '<i n="OutstandingInvoices" 
-- d="Forecast_2" 
-- v="' + convert (varchar(50),(select case when 
-- (@myInvoicesToInvoice = 0) then NULL else 
-- @myInvoicesToInvoice end)) + '" 
-- h="asp/Accounts/invoiceSearch.asp?emp=' + @strNTLogin + '&amp;BALANCE=NOT_ZERO" 
-- c="mm"/>'
-- exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="Forecasts_IMCreating" 
d="Forecast_2" 
v="' + convert (varchar(50),(select case when 
(@myForecasts = 0) then NULL else 
@myForecasts end)) + '" 
h="asp/Forecasts/search.asp?emp=' + @strNTLogin + '&amp;STATUS=CREATING%2C+DENIED" 
c="mm"/>'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="Approval_Pending4Me" 
d="Approvals_1" 
v="' + convert (varchar(50),(select case when 
(@myPendingApprovals = 0) then NULL else 
@myPendingApprovals end)) + '" 
h="asp/Approvals/approvalSearch.asp" 
c="mm" />'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="MeetingsHappeningNow" 
d="Projects_4" 
v="' + convert (varchar(50),(select case when 
(@myMeetingsHappeningNow = 0) then NULL else 
@myMeetingsHappeningNow end)) + '" 
h="asp/meetings/searchMeeting.asp?STATUS=inPROGRESS&amp;RESPONSE=ACCEPT" 
c="escalation" />'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="Meetings24HoursFromNow" 
d="Projects_4" 
v="' + convert (varchar(50),(select case when 
(@meetings24HoursFromNow = 0) then NULL else 
@meetings24HoursFromNow end)) + '" 
h="asp/meetings/searchMeeting.asp?STATUS=UPCOMING&amp;RESPONSE=ACCEPT" 
c="escalation" />'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="MeetingsIhaveNotEmailed" 
d="Projects_4" 
v="' + convert (varchar(50),(select case when 
(@meetingsIHaveNotEmailed = 0) then NULL else 
@meetingsIHaveNotEmailed end)) + '" 
h="asp/meetings/searchMeeting.asp?EMAILED=NOT_EMAILED&amp;STATUS=UPCOMING&amp;searchMethod=iInitiated&amp;" 
c="escalation" />' exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="MeetingsIHaveNotRespondedTo" 
d="Projects_4" 
v="' + convert (varchar(50),(select case when 
(@myMeetings = 0) then NULL else 
@myMeetings end)) + '" 
h="asp/meetings/searchMeeting.asp?RESPONSE=NOT_REPLIED&amp;STATUS=upComingAndInProgress" 
c="mm" />'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="DiscussionsIinitiatedWithNewResponses" 
d="Projects_2" 
v="' + convert (varchar(50),(select case when 
(@myDisIinitiatedWithNewResponses = 0) then NULL else 
@myDisIinitiatedWithNewResponses end)) + '" 
h="asp/discussions/searchDiscussion.asp?searchMethod=iInitiated&amp;NEW_RESPONSES=1&amp;RESPONSE_TYPE=1&amp;STATUS=ALL" 
c="mm" />'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="DiscussionsWhereIpostedWithNewResponseToMe" 
d="Projects_2" 
v="' + convert (varchar(50),(select case when 
(@myDisIwroteWithNewResponses = 0) then NULL else 
@myDisIwroteWithNewResponses end)) + '" 
h="asp/discussions/searchDiscussion.asp?searchMethod=iWrote&amp;NEW_RESPONSES=1&amp;RESPONSE_TYPE=0&amp;STATUS=ALL" 
c="mm" />'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="Discussion_IHaveNotViewd" 
d="Projects_2" 
v="' + convert (varchar(50),(select case when 
(@myDisIHaveNotViewed = 0) then NULL else 
@myDisIHaveNotViewed end)) + '" 
h="asp/discussions/searchDiscussion.asp?IS_VIEWED=0&amp;STATUS=ALL" 
c="mm" />'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="Discussion_IwantToBeNotified" 
d="Projects_2" 
v="' + convert (varchar(50),(select case when 
(@myDisIwantToBeNotified = 0) then NULL else 
@myDisIwantToBeNotified end)) + '" 
h="asp/discussions/searchDiscussion.asp?NEW_RESPONSES=1&amp;RESPONSE_TYPE=2&amp;ALERT_ME=1&amp;STATUS=ALL" 
c="mm" />'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="SurveysIHaveNotRepliedTo" 
d="Projects_3" 
v="' + convert (varchar(50),(select case when 
(@mySurveysWithNoReply = 0) then NULL else 
@mySurveysWithNoReply end)) + '" 
h="asp/surveys/searchSurvey.asp?REPLIED=NOT_REPLIED&amp;STATUS=OPEN" 
c="mm" />'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="surveysIinitiatedWithNewResponses" 
d="Projects_3" 
v="' + convert (varchar(50),(select case when 
(@mysurveysIinitiatedWithNewResponses = 0) then NULL else 
@mysurveysIinitiatedWithNewResponses end)) + '" 
h="asp/surveys/searchSurvey.asp?searchMethod=iInitiated&amp;NEW_RESPONSES=1&amp;" 
c="mm" />'

declare @dt as datetime
set @curs = cursor for SELECT * FROM #serviceCallWeeks ORDER BY DT
OPEN @curs
fetch next from @curs into @dt
while @@fetch_status = 0
	begin

	exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
	set @so = '<i n="serviceCallsInWorkerBox" 
	d="ServiceCalls_1" 
	v="' + convert (varchar(50),@dt,10) + '" 
	h="asp/serviceCalls/searchServiceCall2.asp?startDate=' + convert (varchar(50),@dt,10) + '&amp;lastDate=' + convert (varchar(50),@dt,10) + '"    
	c="mm" />'
	fetch next from @curs into @dt
	end
close @curs
deallocate @curs






exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="messagesImportantToYouAndNotRead" 
d="Projects_6" 
v="' + convert (varchar(50),(select case when 
(@messagesImportantToYouAndNotRead = 0) then NULL else 
@messagesImportantToYouAndNotRead end)) + '" 
h="asp/messages/searchMessages.asp?IMPORTANCE=HIGH&amp;IS_READ=0&amp;IS_CC_MESSAGE=0&amp;searchMethod=searchToCCYou&amp;STATUS=SENT"      
c="escalation" />'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="messagesToYouAndNotRead" 
d="Projects_6" 
v="' + convert (varchar(50),(select case when 
(@messagesToYouAndNotRead = 0) then NULL else 
@messagesToYouAndNotRead end)) + '" 
h="asp/messages/searchMessages.asp?STATUS=SENT&amp;IMPORTANCE=NORMAL&amp;IS_READ=0&amp;IS_CC_MESSAGE=0&amp;searchMethod=searchToCCYou"      
c="escalation"/>'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="messagesImportantToYouCCAndNotRead" 
d="Projects_6" 
v="' + convert (varchar(50),(select case when 
(@messagesImportantToYouCCAndNotRead = 0) then NULL else 
@messagesImportantToYouCCAndNotRead end)) + '" 
h="asp/messages/searchMessages.asp?STATUS=SENT&amp;IMPORTANCE=HIGH&amp;IS_READ=0&amp;IS_CC_MESSAGE=1&amp;searchMethod=searchToCCYou"      
c="escalation" />'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="messagesToYouCCAndNotRead" 
d="Projects_6" 
v="' + convert (varchar(50),(select case when 
(@messagesToYouCCAndNotRead = 0) then NULL else 
@messagesToYouCCAndNotRead end)) + '" 
h="asp/messages/searchMessages.asp?STATUS=SENT&amp;IMPORTANCE=NORMAL&amp;IS_READ=0&amp;IS_CC_MESSAGE=1&amp;searchMethod=searchToCCYou"      
c="escalation" />'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="needsCreating" 
d="Products_4" 
v="' + convert (varchar(50),(select case when 
(@needsCreating = 0) then NULL else 
@needsCreating end)) + '" 
h="asp/needs/search.asp?STATUS=CREATING&amp;emp=' + @strNTLogin +'"      
c="mm" />'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="needsCompanySpecified" 
d="Products_4" 
v="' + convert (varchar(50),(select case when 
(@needsCompanySpecified = 0) then NULL else 
@needsCompanySpecified end)) + '" 
h="asp/needs/search.asp?searchMethod=searchByCompanyVertical&amp;AD_STATUS=NEEDS_CURRENT&amp;STATUS=APPROVED, APPROVED_BUT_REVISING, APPROVED_BUT_DELETING"
c="mm" />'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="needsYourSpecified" 
d="Products_4" 
v="' + convert (varchar(50),(select case when 
(@needsYourSpecified = 0) then NULL else 
@needsYourSpecified end)) + '" 
h="asp/needs/search.asp?searchMethod=searchByPersonAllowed&amp;AD_STATUS=NEEDS_CURRENT&amp;STATUS=APPROVED, APPROVED_BUT_REVISING, APPROVED_BUT_DELETING"      
c="mm" />'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '<i n="Theory_IMCreating" 
d="Theory_1" 
v="' + convert (varchar(50),(select case when 
(@myTheory = 0) then NULL else 
@myTheory end)) + '" 
h="asp/Theory/searchTheory.asp?emp=' + @strNTLogin + '&amp;STATUS=CREATING%2C+DENIED" 
c="mm"/>'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so
set @so = '</w>'
exec A_SP_MAIN_MENU_GET_WARNINGS_APPEND_WARNING @retVal OUTPUT, @retVal2 OUTPUT, @retVal3 OUTPUT,@so 


































































