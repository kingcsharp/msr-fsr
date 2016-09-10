



/*
STORED PROCEDURE CALLED IN serviceCalls/acceptServiceCall.asp
States = WORKER,BOSS,CUSTOMER REVIEW GROUP,CUSTOMER AP GROUP,AR GROUP,CLOSED
*/
CREATE     PROCEDURE dbo.A_SP_SERVICE_CALL_SEND_FORWARD
@weeklyID varchar(50),
@reason varchar(4000),
@strNTlogin varchar(50)
AS
declare @currentState varchar(50),@workTypeID varchar(50),@skipBoss tinyInt,
	@canSend tinyInt,@isWorker int,@isBoss int,@isApproverRole int,
	@isPayerRole int,@isRecievableRole int,@newState varchar(50),
	@custGroup varchar(50),@custAPGroup varchar(50),@supplierARGroup varchar(50),
	@supplierID varchar(50)


SELECT @workTypeID = WORK_TYPE, @currentState = STATUS FROM A_SERVICE_CALLS_WEEKLY_REPORTS WHERE ID = @weeklyID
SELECT 	@skipBoss = isnull(SKIP_BOSS,0),
		@custGroup = APPROVER_ROLE,
		@custAPGroup = PAYER_ROLE,
		@supplierID = SUPPLIER_ID
FROM A_SERVICE_CALLS_WORK_TYPES WHERE ID = @workTypeID

SELECT @supplierARGroup = RECIEVABLE_ROLE 
	FROM A_SERVICE_CALLS_ACC_RECIEVABLE_ROLE WHERE 
	COMPANY_ID = @supplierID

exec A_SP_SERVICE_CALL_GET_MY_SECURITY_DATA
		@isWorker OUTPUT,@isBoss OUTPUT,@isApproverRole OUTPUT,@isPayerRole OUTPUT,
		@isRecievableRole OUTPUT,@weeklyID,@strNTLogin


set @newState = null
if @currentState = 'WORKER' and (@isWorker = 1 or @isBoss = 1)
	begin
	if @skipBoss = 1
		set @newState = 'CUSTOMER_REVIEW_GROUP'	
	else
		set @newState = 'BOSS'	
	end

if @currentState = 'BOSS' and @isBoss = 1 set @newState = 'CUSTOMER_REVIEW_GROUP'	
if @currentState = 'CUSTOMER_REVIEW_GROUP' and @isApproverRole = 1	set @newState = 'CUSTOMER_AP_GROUP'	
if @currentState = 'CUSTOMER_AP_GROUP' and @isPayerRole = 1	set @newState = 'AR_GROUP'	
if @currentState = 'AR_GROUP' and @isRecievableRole = 1	set @newState = 'CLOSED'	

if @newState is null goto fin
print 'New State = ' + @newState

if @newState = 'CUSTOMER_REVIEW_GROUP' and @custGroup is null
	set @newState = 'CUSTOMER_AP_GROUP'
if @newState = 'CUSTOMER_AP_GROUP' and @custAPGroup is null
	set @newState = 'AR_GROUP'
if @newState = 'AR_GROUP' and @supplierARGroup is null
	set @newState = 'CLOSED'

print 'Now New State = ' + @newState

UPDATE A_SERVICE_CALLS_WEEKLY_REPORTS
	SET STATUS =@newState,REASON = @reason,REASON_TYPE = 'FORWARD'
	WHERE ID = @weeklyID
INSERT INTO A_SERVICE_CALLS_STATUS_HISTORY
	(ID, WEEKLY_ID, STATUS_CHANGED_TO, CHANGED_BY, REASON,REASON_TYPE, DATE_CHANGED, DRCM, MODBY)
	VALUES
	(newid(), @weeklyID, @newState, @strNTlogin, @reason, 'FORWARD', getdate(), getdate(), @strNTlogin) 

return(0)
fin:
print 'Not allowed to submit this'





