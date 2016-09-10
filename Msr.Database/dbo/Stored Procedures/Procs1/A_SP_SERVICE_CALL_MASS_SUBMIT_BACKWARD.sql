






/*
STORED PROCEDURE CALLED IN serviceCalls/submitServiceCall.asp

*/

CREATE                        PROCEDURE A_SP_SERVICE_CALL_MASS_SUBMIT_BACKWARD
@newID varchar(50) OUTPUT,
@msg varchar(1000) OUTPUT,
@massSubmitWeeklyIDs varchar(4000),
@statusType varchar(50),
@reason varchar(4000),
@strNTlogin varchar(50)
AS
declare @hardCodeStatus varchar(50)
set @hardCodeStatus = 'BACKWARD'

	CREATE TABLE #TempItems	(IT varchar(50))
	INSERT INTO #TempItems Exec A_SP_Z_SPLIT @massSubmitWeeklyIDs,';'
	Declare @it nvarchar(50)
	Declare @curs Cursor
	Declare @currentStatus varchar(50)

	set @curs = Cursor For SELECT * FROM #TempItems
	open @curs
	Fetch Next from @curs Into @it
	while (@@fetch_status = 0)
	Begin
		print 'getting current status'
		SELECT @currentStatus = STATUS
			FROM A_SERVICE_CALLS_WEEKLY_REPORTS
		WHERE ID = @it
		print 'The current status is' + @currentStatus
		if @currentStatus = 'WORKER' 
		begin
			print 'deleting the service call with id of ' +@it
			DELETE FROM A_SERVICE_CALLS_WEEKLY_REPORTS
			WHERE ID = @it
		end

		if @currentStatus = 'BOSS' 
		begin
			print 'submiting a service call and setting status BACK to WORKER'
			UPDATE A_SERVICE_CALLS_WEEKLY_REPORTS
			SET STATUS ='WORKER',
			REASON = @reason, 
			REASON_TYPE= @hardCodeStatus
			WHERE ID = @it

			INSERT INTO A_SERVICE_CALLS_STATUS_HISTORY
			(ID, WEEKLY_ID, STATUS_CHANGED_TO, CHANGED_BY, REASON,REASON_TYPE, DATE_CHANGED, DRCM, MODBY)
			VALUES(newid(), @it, 'WORKER', @strNTlogin, @reason, @hardCodeStatus, getdate(), getdate(), @strNTlogin) 
		end
		if @currentStatus ='CUSTOMER_REVIEW_GROUP' 
		begin
			print 'submiting a service call and setting status to BACK TO BOSS'
			UPDATE A_SERVICE_CALLS_WEEKLY_REPORTS
			SET STATUS ='BOSS',
				REASON = @reason, 
				REASON_TYPE= @hardCodeStatus,
				INVOICE_DATE = getDate()
			WHERE ID = @it

			INSERT INTO A_SERVICE_CALLS_STATUS_HISTORY
			(ID, WEEKLY_ID, STATUS_CHANGED_TO, CHANGED_BY, REASON,REASON_TYPE, DATE_CHANGED, DRCM, MODBY)
			VALUES(newid(), @it, 'BOSS', @strNTlogin, @reason, @hardCodeStatus, getdate(), getdate(), @strNTlogin) 
		end
	    if @currentStatus ='CUSTOMER_AP_GROUP' 
		begin
		print 'submiting a service call and setting status to AR_GROUP'
			UPDATE A_SERVICE_CALLS_WEEKLY_REPORTS
			SET STATUS ='CUSTOMER_REVIEW_GROUP',
				REASON = @reason, 
				REASON_TYPE= @hardCodeStatus
			WHERE ID = @it

			INSERT INTO A_SERVICE_CALLS_STATUS_HISTORY
				(ID, WEEKLY_ID, STATUS_CHANGED_TO, CHANGED_BY, REASON,REASON_TYPE, DATE_CHANGED, DRCM, MODBY)
			VALUES(newid(), @it, 'CUSTOMER_REVIEWG_ROUP', @strNTlogin, @reason, @hardCodeStatus, getdate(), getdate(), @strNTlogin) 
		end
		if @currentStatus ='AR_GROUP' 
		begin
			print 'submiting a service call and setting status to CLOSED'
			UPDATE A_SERVICE_CALLS_WEEKLY_REPORTS
			SET STATUS ='CUSTOMER_AP_GROUP',
				REASON = @reason,
				REASON_TYPE= @hardCodeStatus
			WHERE ID = @it

			INSERT INTO A_SERVICE_CALLS_STATUS_HISTORY
					(ID, WEEKLY_ID, STATUS_CHANGED_TO, CHANGED_BY, REASON,REASON_TYPE, DATE_CHANGED, DRCM, MODBY)
					VALUES(newid(), @it, 'CUSTOMER_AP_GROUP', @strNTlogin, @reason, @hardCodeStatus, getdate(), getdate(), @strNTlogin) 
		end
		if @currentStatus ='CLOSED' 
		begin
			print 'submiting a service call and setting status to CLOSED'
			UPDATE A_SERVICE_CALLS_WEEKLY_REPORTS
			SET STATUS ='AR_GROUP',
				REASON = @reason,
				REASON_TYPE= @hardCodeStatus
			WHERE ID = @it

			INSERT INTO A_SERVICE_CALLS_STATUS_HISTORY
					(ID, WEEKLY_ID, STATUS_CHANGED_TO, CHANGED_BY, REASON,REASON_TYPE, DATE_CHANGED, DRCM, MODBY)
					VALUES(newid(), @it, 'AR_GROUP', @strNTlogin, @reason, @hardCodeStatus, getdate(), getdate(), @strNTlogin) 
		end

		Fetch Next from @curs Into @it
	End
	close @curs
	Deallocate @curs





