





/*
STORED PROCEDURE CALLED IN serviceCalls/acceptServiceCall.asp

*/
CREATE                     PROCEDURE A_SP_SERVICE_CALL_MOVE_FORWARD
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
set @hardCodeStatus = 'FORWARD'
declare @workTypeID varchar(50)
SELECT @workTypeID = WORK_TYPE FROM A_SERVICE_CALLS_WEEKLY_REPORTS WHERE ID = @weeklyID
declare @skipBoss tinyInt
SELECT @skipBoss = isnull(SKIP_BOSS,0) FROM A_SERVICE_CALLS_WORK_TYPES WHERE ID = @workTypeID
if @skipBoss = 1 and @statusType = 'workerForwardsToBoss'
	begin
	set @statusType = 'bossForwardsToApprover'
	set @isBoss = 1
	end




if @statusType ='workerForwardsToBoss' and @isWorker =1
begin
print 'submiting a service call and setting status to BOSS '
UPDATE A_SERVICE_CALLS_WEEKLY_REPORTS
SET STATUS ='BOSS',
REASON = @reason,
REASON_TYPE = @hardCodeStatus
WHERE ID = @weeklyID
INSERT INTO A_SERVICE_CALLS_STATUS_HISTORY
(ID, WEEKLY_ID, STATUS_CHANGED_TO, CHANGED_BY, REASON,REASON_TYPE, DATE_CHANGED, DRCM, MODBY)
VALUES(newid(), @weeklyID, 'BOSS', @strNTlogin, @reason, @hardCodeStatus, getdate(), getdate(), @strNTlogin) 
end
	else if @statusType ='bossForwardsToApprover' and @isBoss =1
	begin
		print 'submiting a service call and setting status to CUSTOMER_REVIEW_GROUP'
		UPDATE A_SERVICE_CALLS_WEEKLY_REPORTS
		SET STATUS ='CUSTOMER_REVIEW_GROUP',
			REASON = @reason, 
			REASON_TYPE= @hardCodeStatus
		WHERE ID = @weeklyID

		INSERT INTO A_SERVICE_CALLS_STATUS_HISTORY
		(ID, WEEKLY_ID, STATUS_CHANGED_TO, CHANGED_BY, REASON,REASON_TYPE, DATE_CHANGED, DRCM, MODBY)
		VALUES(newid(), @weeklyID, 'CUSTOMER_REVIEW_GROUP', @strNTlogin, @reason, @hardCodeStatus, getdate(), getdate(), @strNTlogin) 
	end
		else if @statusType ='approverForwardsToPayer' and @isApproverRole =1
		begin
			print 'submiting a service call and setting status to CUSTOMER APPROVED - WAITING PAYMENT'
			UPDATE A_SERVICE_CALLS_WEEKLY_REPORTS
			SET STATUS ='CUSTOMER_AP_GROUP',
				REASON = @reason, 
				REASON_TYPE= @hardCodeStatus,
				INVOICE_DATE = getDate()
			WHERE ID = @weeklyID

			INSERT INTO A_SERVICE_CALLS_STATUS_HISTORY
			(ID, WEEKLY_ID, STATUS_CHANGED_TO, CHANGED_BY, REASON,REASON_TYPE, DATE_CHANGED, DRCM, MODBY)
			VALUES(newid(), @weeklyID, 'CUSTOMER_AP_GROUP', @strNTlogin, @reason, @hardCodeStatus, getdate(), getdate(), @strNTlogin) 
		end
			else if @statusType ='payerForwardsToAccRecievable' and @isPayerRole =1
			begin
				print 'submiting a service call and setting status to AR_GROUP'
				UPDATE A_SERVICE_CALLS_WEEKLY_REPORTS
				SET STATUS ='AR_GROUP',
					REASON = @reason, 
					REASON_TYPE= @hardCodeStatus
				WHERE ID = @weeklyID

				INSERT INTO A_SERVICE_CALLS_STATUS_HISTORY
				(ID, WEEKLY_ID, STATUS_CHANGED_TO, CHANGED_BY, REASON,REASON_TYPE, DATE_CHANGED, DRCM, MODBY)
				VALUES(newid(), @weeklyID, 'AR_GROUP', @strNTlogin, @reason, @hardCodeStatus, getdate(), getdate(), @strNTlogin) 
			end
				else if @statusType ='recievableCloseServiceCall' and @isRecievableRole =1
				begin
					print 'submiting a service call and setting status to CLOSED'
					UPDATE A_SERVICE_CALLS_WEEKLY_REPORTS
					SET STATUS ='CLOSED',
					REASON = @reason, 
					REASON_TYPE= @hardCodeStatus
					WHERE ID = @weeklyID

					INSERT INTO A_SERVICE_CALLS_STATUS_HISTORY
					(ID, WEEKLY_ID, STATUS_CHANGED_TO, CHANGED_BY, REASON,REASON_TYPE, DATE_CHANGED, DRCM, MODBY)
					VALUES(newid(), @weeklyID, 'CLOSED', @strNTlogin, @reason, @hardCodeStatus, getdate(), getdate(), @strNTlogin) 
				end