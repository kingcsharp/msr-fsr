



/*
STORED PROCEDURE CALLED IN serviceCalls/acceptServiceCall.asp

*/

CREATE              PROCEDURE A_SP_SERVICE_CALL_GET_VALID_SUBMIT_DATA
@weeklyIDs varchar(4000),
@submitAction varchar(50),
@strNTlogin varchar(50)
AS
declare @myFirstSubmitType varchar(50)
declare @hardCodeWorkerStatus varchar(50)
declare @hardCodeBossStatus varchar(50)
declare @hardCodeApproverStatus varchar(50)
declare @hardCodePayerStatus varchar(50)
declare @hardCodeRecievableStatus varchar(50)
declare @hardCodeClosedStatus varchar(50)
declare @workType varchar(100)
declare @workerName varchar(100)
declare @customerName varchar(100)
declare @machineName varchar(100)
declare @supplierName varchar(100)
declare @currentStatus varchar(50)	
declare @payerRole varchar(50)	
declare @reasonType varchar(50)	
declare @startDate varchar(50)	
declare @reason varchar(50)	
declare @total money
declare @poNumber varchar(50)	
declare @it nvarchar(50)
declare @curs Cursor
declare @workerID varchar(50)
declare @isWorker int
declare @isBoss int
declare @isApproverRole int
declare @isPayerRole int
declare @isRecievableRole int
declare @thisOneSubmitType varchar(50)
declare @hasPONumber varchar(50)
set @hardCodeWorkerStatus = 'WORKER'
set @hardCodeBossStatus = 'BOSS'
set @hardCodeApproverStatus = 'CUSTOMER_REVIEW_GROUP'
set @hardCodePayerStatus = 'CUSTOMER_AP_GROUP'
set @hardCodeRecievableStatus = 'AR_GROUP'
set @hardCodeClosedStatus = 'CLOSED'

CREATE TABLE #TmyData 
(ID varchar(50),
WORK_TYPE varchar(1000),
WORKER_ID varchar(50),
WORKER_NAME varchar(100),
SUPPLIER_NAME varchar(100),
CUSTOMER_NAME varchar(100),
MACHINE_NAME varchar(100),
STATUS varchar(50),
REASON varchar(50),
REASON_TYPE varchar(50),
START_DATE varchar(50),
PAYER_ROLE varchar(50),
TOTAL money,
HAS_PO_NUMBER varchar(50))

CREATE TABLE #TempItems	(IT varchar(50))
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @weeklyIDs,';'
print 'opening my cursor' 
set @curs = Cursor For SELECT * FROM #TempItems
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
	Begin
	print 'setting permissions to nothing for the next id'
	set @isWorker = NULL
	set @isBoss = NULL
	set @isApproverRole = NULL
	set @isPayerRole =NULL
	set @isRecievableRole = NULL

	print 'Grabbing my sercurity'
	exec A_SP_SERVICE_CALL_GET_MY_SECURITY_DATA
		@isWorker OUTPUT,
		@isBoss OUTPUT,
		@isApproverRole OUTPUT,
		@isPayerRole OUTPUT,
		@isRecievableRole OUTPUT,
		@it,
		@strNTLogin

	print 'getting my info for this id of ' +@it
	SELECT  @workType = WORK_TYPE,
		@workerID = WORKER_ID, 
		@workerName = WORKER_NAME,
		@customerName = CUSTOMER_NAME,
		@machineName = MACHINE_NAME,
		@supplierName = SUPPLIER_NAME,
		@currentStatus = STATUS,
		@payerRole = PAYER_ROLE,
		@startDate = START_DATE_STRING + ' (' + convert(nvarchar(2),datepart(ww,START_DATE_STRING)) +')', 
		@reason = REASON,
		@reasonType = REASON_TYPE,
		@poNumber = ORDER_NUMBER
	FROM A_V_SERVICE_CALL_SEARCH_DATA
	WHERE ID = @it
	if @isPayerRole = 1
		begin
		print 'Getting total for payer'
		SELECT @total = convert(money,TOTAL) FROM A_V_SERVICE_CALL_INVOICE_DATA WHERE ID = @it
		print 'set @total = '
		print convert(varchar(50),@total,1)
		end
	set @thisOneSubmitType = null
	if @isWorker = 1 and @currentStatus = @hardCodeWorkerStatus  
		begin
		print 'The first valid ID i am allowed to submit is a WORKER'
		set @thisOneSubmitType =  'massWorker' 
		end
	if @isBoss = 1 and @currentStatus = @hardCodeBossStatus
		begin
		print 'The first valid ID i am allowed to submit is a BOSS'
		set @thisOneSubmitType =  'massBoss' 		
		end
	if @isApproverRole = 1 and @currentStatus = @hardCodeApproverStatus
		begin
		print 'The first valid ID i am allowed to submit is a APPROVER'
		set @thisOneSubmitType =  'massApprover' 
		end
	if @isPayerRole = 1 and @currentStatus = @hardCodePayerStatus
		begin
		print 'The first valid ID i am allowed to submit is a PAYER'	
		set @thisOneSubmitType =  'massPayer' 
		end
	if @isRecievableRole = 1 and @currentStatus = @hardCodeRecievableStatus
		begin
		print 'The first valid ID i am allowed to submit is a ACCOUNTS RECIEVABLE'
		set @thisOneSubmitType =  'massRecievable' 	
		end
	print 'isWorker is******' + convert(varchar(50), @isWorker)
	print 'currentSTatus is*******' + @currentStatus
	if @submitAction = 'massBackward'
		begin
		if @isWorker = 1 and @currentStatus = @hardCodeBossStatus
			begin
			print 'The first valid ID is taking back from a Boss to a Worker'
			set @thisOneSubmitType =  'massWorkerTakeBack' 		
			end
		if @isBoss = 1 and @currentStatus = @hardCodeApproverStatus
			begin
			print 'The first valid ID is taking back from a Approver to a Boss'
			set @thisOneSubmitType =  'massBossTakeBack' 		
			end
		if @isApproverRole = 1 and @currentStatus = @hardCodePayerStatus
			begin
			print 'The first valid ID is taking back from a Payer to a Approver'
			set @thisOneSubmitType =  'massApproverTakeBack' 		
			end
		if @isPayerRole = 1 and @currentStatus = @hardCodeRecievableStatus
			begin
			print 'The first valid ID is taking back from a Recievable to a Payer'
			set @thisOneSubmitType =  'massPayerTakeBack' 		
			end
		if @isRecievableRole = 1 and @currentStatus = @hardCodeClosedStatus
			begin
			print 'The first valid ID is taking back from a Recievable to a Payer'
			set @thisOneSubmitType =  'massRecievableTakeBack' 		
			end
		end 

	print 'This ones submit type = ' + isNULL(@thisOneSubmitType,'NULL')
	print 'My First submit type = ' + isNULL(@myFirstSubmitType,'NULL')
	if @myFirstSubmitType is NULL set @myFirstSubmitType = @thisOneSubmitType
	if (@thisOneSubmitType is not null) and (isNull(@myFirstSubmitType,'') = @thisOneSubmitType)
		begin
		print ' This one matches with the first one so put it in'
		print 'PO NUMBER IS' +@poNumber
		if @poNumber is NULL 
			begin
			print 'Has no PO Number'
			set @hasPONumber = 0
			end
		else
			begin
			set @hasPONumber = 1	
			print 'Has no PO Number'
			end
		INSERT INTO #TmyData 
		(ID, WORK_TYPE, WORKER_NAME,SUPPLIER_NAME ,CUSTOMER_NAME, MACHINE_NAME, STATUS, PAYER_ROLE, REASON, REASON_TYPE, START_DATE, TOTAL, HAS_PO_NUMBER)
		VALUES
		(@it , @workType, @workerName, @customerName, @machineName, @supplierName, @currentStatus,@payerRole, @reason, @reasonType, @startDate, @total, @hasPONumber)
		end
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs

print 'Finished getting all records'
declare @grandTotal money
SELECT @grandTotal = SUM(TOTAL)FROM #TmyData
print 'Got a grand Total of '
print convert(varchar(50),@grandTotal,1)

print 'Returning the records now'
SELECT *
,'$' + convert(varchar(50),TOTAL,1) AS DOLLAR_TOTAL
,'$' + convert(varchar(50),@grandTotal,1) AS GRAND_TOTAL
FROM #TmyData

SET QUOTED_IDENTIFIER OFF 




