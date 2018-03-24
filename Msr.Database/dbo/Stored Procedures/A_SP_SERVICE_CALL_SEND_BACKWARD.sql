



/*
STORED PROCEDURE CALLED IN serviceCalls/acceptServiceCall.asp
States = WORKER,BOSS,CUSTOMER REVIEW GROUP,CUSTOMER AP GROUP,AR GROUP,CLOSED
*/
CREATE     PROCEDURE dbo.A_SP_SERVICE_CALL_SEND_BACKWARD
@weeklyID varchar(50),
@reason varchar(4000),
@strNTlogin varchar(50)
AS
declare @currentState varchar(50),@workTypeID varchar(50),@skipBoss tinyInt,
	@canSend tinyInt,@isWorker int,@isBoss int,@isApproverRole int,
	@isPayerRole int,@isRecievableRole int,@newState varchar(50)

SELECT @workTypeID = WORK_TYPE, @currentState = STATUS FROM A_SERVICE_CALLS_WEEKLY_REPORTS WHERE ID = @weeklyID
exec A_SP_SERVICE_CALL_GET_MY_SECURITY_DATA
		@isWorker OUTPUT,@isBoss OUTPUT,@isApproverRole OUTPUT,@isPayerRole OUTPUT,
		@isRecievableRole OUTPUT,@weeklyID,@strNTLogin

set @newState = null
if @currentState = 'BOSS' and (@isBoss = 1 OR @isWorker = 1)  set @newState = 'WORKER'	
if @currentState = 'CUSTOMER_REVIEW_GROUP' and (@isApproverRole = 1 OR @isBoss = 1 OR @isWorker = 1)	set @newState = 'WORKER'	
if @currentState = 'CUSTOMER_AP_GROUP' and (@isApproverRole = 1 OR @isBoss = 1 OR @isWorker = 1)	set @newState = 'WORKER'	
if @currentState = 'AR_GROUP' and (@isPayerRole = 1 OR @isBoss = 1 OR @isWorker = 1)	set @newState = 'WORKER'	
if @currentState = 'CLOSED' and (@isApproverRole = 1 or @isRecievableRole = 1 OR @isBoss = 1 OR @isWorker = 1)	set @newState = 'WORKER'	

if @newState is null goto fin

UPDATE A_SERVICE_CALLS_WEEKLY_REPORTS
	SET STATUS =@newState,REASON = @reason,REASON_TYPE = 'BACKWARD'
	WHERE ID = @weeklyID
INSERT INTO A_SERVICE_CALLS_STATUS_HISTORY
	(ID, WEEKLY_ID, STATUS_CHANGED_TO, CHANGED_BY, REASON,REASON_TYPE, DATE_CHANGED, DRCM, MODBY)
	VALUES
	(newid(), @weeklyID, @newState, @strNTlogin, @reason, 'BACKWARD', getdate(), getdate(), @strNTlogin) 

exec A_SP_SERVICE_CALL_SEND_EMAIL_UPDATE @weeklyID,'sentBackward',@strNTLogin

fin: