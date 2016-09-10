


/*
STORED PROCEDURE CALLED IN serviceCalls/rejectServiceCall.asp

*/
CREATE                    PROCEDURE A_SP_SERVICE_CALL_MOVE_BACKWARD
@newID varchar(50) OUTPUT,
@msg varchar(1000) OUTPUT,
@weeklyID varchar(50),
@statusType varchar(50),
@reason varchar(4000),
@strNTlogin varchar(50)
AS

declare @hardCodeStatus varchar(50)
declare @isWorker int
declare @isBoss int
declare @isApproverRole int
declare @isPayerRole int
declare @isRecievableRole int


exec A_SP_SERVICE_CALL_GET_MY_SECURITY_DATA
		@isWorker OUTPUT,
		@isBoss OUTPUT,
		@isApproverRole OUTPUT,
		@isPayerRole OUTPUT,
		@isRecievableRole OUTPUT,
		@weeklyID,
		@strNTLogin

set @hardCodeStatus = 'BACKWARD'

if @statusType ='backwardsToWorker' and (@isBoss =1 or @isWorker =1) 
begin
print 'submiting a service call and setting status to WORKER '
UPDATE A_SERVICE_CALLS_WEEKLY_REPORTS
SET STATUS ='WORKER',
REASON = @reason,
REASON_TYPE = @hardCodeStatus
WHERE ID = @weeklyID
INSERT INTO A_SERVICE_CALLS_STATUS_HISTORY
(ID, WEEKLY_ID, STATUS_CHANGED_TO, CHANGED_BY, REASON,REASON_TYPE, DATE_CHANGED, DRCM, MODBY)
VALUES(newid(), @weeklyID, 'WORKER', @strNTlogin, @reason, @hardCodeStatus, getdate(), getdate(), @strNTlogin) 
end


if @statusType ='backwardsToBoss' and (@isApproverRole =1 or @isBoss =1)
	begin
		print 'submiting a service call and setting status to BOSS'
		UPDATE A_SERVICE_CALLS_WEEKLY_REPORTS
		SET STATUS ='BOSS',
		REASON = @reason,
		REASON_TYPE =@hardCodeStatus
		WHERE ID = @weeklyID
		INSERT INTO A_SERVICE_CALLS_STATUS_HISTORY
		(ID, WEEKLY_ID, STATUS_CHANGED_TO, CHANGED_BY, REASON,REASON_TYPE, DATE_CHANGED, DRCM, MODBY)
		VALUES(newid(), @weeklyID, 'BOSS', @strNTlogin, @reason, @hardCodeStatus, getdate(), getdate(), @strNTlogin) 
	end

if @statusType ='backwardsToApprover' and (@isPayerRole =1 or @isApproverRole =1)
	begin
		print 'submiting a service call and setting status to CUSTOMER_REVIEW_GROUP'
		UPDATE A_SERVICE_CALLS_WEEKLY_REPORTS
		SET STATUS ='CUSTOMER_REVIEW_GROUP',
		REASON = @reason,
		REASON_TYPE =@hardCodeStatus
		WHERE ID = @weeklyID
	
		INSERT INTO A_SERVICE_CALLS_STATUS_HISTORY
		(ID, WEEKLY_ID, STATUS_CHANGED_TO, CHANGED_BY, REASON,REASON_TYPE, DATE_CHANGED, DRCM, MODBY)
		VALUES(newid(), @weeklyID, 'CUSTOMER_REVIEW_GROUP', @strNTlogin, @reason, @hardCodeStatus, getdate(), getdate(), @strNTlogin) 
	end
	
if @statusType ='backwardsToPayer' and (@isRecievableRole =1 or @isPayerRole =1)
	begin
		print 'submiting a service call and setting status to CUSTOMER_AP_GROUP' 	
		UPDATE A_SERVICE_CALLS_WEEKLY_REPORTS
		SET STATUS ='CUSTOMER_AP_GROUP',
		REASON = @reason,
		REASON_TYPE =@hardCodeStatus
		WHERE ID = @weeklyID
		INSERT INTO A_SERVICE_CALLS_STATUS_HISTORY
		(ID, WEEKLY_ID, STATUS_CHANGED_TO, CHANGED_BY, REASON,REASON_TYPE, DATE_CHANGED, DRCM, MODBY)
		VALUES(newid(), @weeklyID, 'CUSTOMER_AP_GROUP', @strNTlogin, @reason, @hardCodeStatus, getdate(), getdate(), @strNTlogin) 
	end

if @statusType ='backwardsToRecievable' and @isRecievableRole =1
	begin
		print 'Accounts Recievable taking back- submiting a service call and setting status to AR_GROUP' 	
		UPDATE A_SERVICE_CALLS_WEEKLY_REPORTS
		SET STATUS ='AR_GROUP',
		REASON = @reason,
		REASON_TYPE =@hardCodeStatus
		WHERE ID = @weeklyID
		INSERT INTO A_SERVICE_CALLS_STATUS_HISTORY
		(ID, WEEKLY_ID, STATUS_CHANGED_TO, CHANGED_BY, REASON,REASON_TYPE, DATE_CHANGED, DRCM, MODBY)
		VALUES(newid(), @weeklyID, 'AR_GROUP', @strNTlogin, @reason, @hardCodeStatus, getdate(), getdate(), @strNTlogin) 
	end



