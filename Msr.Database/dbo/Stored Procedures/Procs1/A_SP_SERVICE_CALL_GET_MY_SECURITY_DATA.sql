
CREATE  PROCEDURE A_SP_SERVICE_CALL_GET_MY_SECURITY_DATA
@isWorker varchar(50) OUTPUT,
@isBoss varchar(50) OUTPUT,
@isApproverRole varchar(50) OUTPUT,
@isPayerRole varchar(50) OUTPUT,
@isRecievableRole varchar(50) OUTPUT,
@weeklyID varchar(50),
@strNTlogin varchar(50)
AS

print 'Process service call with an id of ' +@weeklyID
print 'Grabbing sercurity parameters' 
print 'Security for worker'
declare @workerID varchar(50)
SELECT @workerID = WORKER_ID 
FROM A_V_SERVICE_CALL_SEARCH_DATA
WHERE ID = @weeklyID
print 'Getting the worker ID of this service call' + @workerID
if @workerID = @strNTlogin 
	begin
	print 'I am the WORKER'
	set @isWorker=1
	end

print 'Security for boss'
declare @bossID varchar(50)
SELECT @bossID = BOSS 
FROM A_V_SERVICE_CALL_SEARCH_DATA
WHERE ID = @weeklyID

print 'Getting the BOSS ID of this service call' + @bossID
if @bossID = @strNTlogin 
	begin
	print 'I am the BOSS'
	set @isBoss=1
	end

print 'Getting the role for customer approver'
declare @approverRole varchar(50)
SELECT @approverRole = APPROVER_ROLE 
FROM A_V_SERVICE_CALL_SEARCH_DATA
WHERE ID = @weeklyID 
AND APPROVER_ROLE IN (SELECT ROLE_ID FROM A_APPROVED_ROLE_ASSIGNEES WHERE PERSON = @strNTLogin) 
if @approverRole is not null
begin
	print 'I am the Approver'
	set @isApproverRole =1
end

print 'Getting the role for customer payer'
declare @payerRole varchar(50)
SELECT @payerRole = PAYER_ROLE 
FROM A_V_SERVICE_CALL_SEARCH_DATA
WHERE ID = @weeklyID 
AND PAYER_ROLE IN (SELECT ROLE_ID FROM A_APPROVED_ROLE_ASSIGNEES WHERE PERSON = @strNTLogin) 
if @payerRole is not null
begin
	print 'I am the Payer'
	set @isPayerRole =1
end

print 'Getting the role for accounts recievable'
declare @receiveableRole varchar(50)
SELECT @receiveableRole = RECIEVABLE_ROLE 
FROM A_V_SERVICE_CALL_SEARCH_DATA
WHERE ID = @weeklyID 
AND RECIEVABLE_ROLE IN (SELECT ROLE_ID FROM A_APPROVED_ROLE_ASSIGNEES WHERE PERSON = @strNTLogin) 
if @receiveableRole is not null
begin
	print 'I am Accounts Recievable'
	set @isRecievableRole =1
end


