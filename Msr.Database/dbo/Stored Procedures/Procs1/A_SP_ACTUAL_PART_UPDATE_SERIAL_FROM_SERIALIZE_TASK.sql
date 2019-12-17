





CREATE     PROCEDURE [dbo].[A_SP_ACTUAL_PART_UPDATE_SERIAL_FROM_SERIALIZE_TASK]
@ID varchar(50),
@taskID varchar(50),
@SN varchar(2000),
@strNTLogin varchar(50)
AS
begin Transaction
declare @curSerial varchar(50),@moveFrom varchar(50),@apHistID varchar(50),@newObjID varchar(50),@partID varchar(50),
		@parentID varchar(50), @kitSerial varchar(50),@kitPartID varchar(50)
SELECT @curSerial = SERIAL,@apHistID = HISTORY_REF_ID,@partID = PART_ID,
		@parentID = PARENT_ID
		FROM A_V_ACTUAL_PARTS_QUICK WHERE ID = @ID

select @kitSerial=serial,@kitPartID=part_id from [A_ACTUAL_PARTS_HISTORY] where object_id in
(
select parent_id from [A_ACTUAL_PARTS_HISTORY] where object_id=@ID
)

	IF NOT EXISTS(SELECT ID from PartsTransactionLog WHERE TaskId = @taskID AND PartId=@kitPartID)
			BEGIN
				INSERT INTO PartsTransactionLog(PartId,SerialNumber,CreatedDate,TaskID,PurchaseHistoryId)
				VALUES(@kitPartID,@kitSerial,GETDATE(),@taskID,@apHistID)
			END		

if isNull(@curSerial,'') = isNull(@SN,'')
BEGIN
	IF NOT EXISTS(SELECT ID from PartsTransactionLog WHERE TaskId = @taskID AND PartId=@partID)
			BEGIN
				INSERT INTO PartsTransactionLog(PartId,SerialNumber,CreatedDate,TaskID,PurchaseHistoryId)
				VALUES(@partID,@SN,GETDATE(),@taskID,@apHistID)
			END		
	goto fin
END

print 'The serial number has changed'

if @curSerial is null
	begin
		INSERT INTO PartsTransactionLog(PartId,SerialNumber,CreatedDate,TaskID,PurchaseHistoryId)
		VALUES(@partID,@SN,GETDATE(),@taskID,@apHistID)
	end
else
	begin
		IF EXISTS(SELECT ID from PartsTransactionLog WHERE TaskId = @taskID AND PartId=@partID)
			BEGIN
				UPDATE PartsTransactionLog SET SerialNumber = @SN WHERE TaskId = @taskID and PartId =@partID;
			END
		ELSE
			BEGIN
				INSERT INTO PartsTransactionLog(PartId,SerialNumber,CreatedDate,TaskID,PurchaseHistoryId)
				VALUES(@partID,@SN,GETDATE(),@taskID,@apHistID)
			END
		END	


if @parentID is null
	begin
	print 'This is a root part'
	end

if @curSerial is null
	begin
	print 'The current serial number is null'
	select @moveFrom = OBJECT_ID FROM A_V_ACTUAL_PARTS_APPROVED_DATA WHERE SERIAL = @SN AND PART_ID = @partID
	if @moveFrom is null
		begin
			UPDATE A_ACTUAL_PARTS_HISTORY SET SERIAL = @SN WHERE ID = @apHistID
			goto fin
		end
	else
		begin
			UPDATE A_ACTUAL_PARTS_HISTORY SET 
				PARENT_ID = @parentID,
				AP_STATUS = 'ap_installed',
				LOCATION = (SELECT LOCATION FROM A_V_ACTUAL_PARTS_QUICK WHERE ID = @parentID)
			WHERE OBJECT_ID = @moveFrom
			UPDATE A_ACTUAL_PARTS_HISTORY SET PARENT_ID = NULL, AP_STATUS = 'ap_available' WHERE ID = @apHistID
			exec A_SP_ACTUAL_PART_REPLACE_PART_IN_CALL @taskID,@ID,@moveFrom,@strNTLogin
			exec A_SP_ACTUAL_PARTS_DELETE_A_PART_ENTIRELY @ID,@strNTLogin
			goto fin
		end		
	end
else
	begin
	if @parentID is not null
		begin
		print 'The current serial number is not null'
		select @moveFrom = HISTORY_REF_ID FROM A_V_ACTUAL_PARTS_APPROVED_DATA WHERE SERIAL = @SN AND PART_ID = @partID
		if @moveFrom is null
			begin
				print 'We have to create a new part for this serial number ' + @SN
				exec A_SP_ACTUAL_PARTS_COPY_ONE @newObjID OUTPUT,@apHistID,@strNTLogin,null
				UPDATE A_OBJECTS SET STATUS = 'APPROVED',LOCKED_BY = NULL,LOCKED_BY_NAME = null WHERE ID = @newObjID
				UPDATE A_ACTUAL_PARTS_HISTORY SET SERIAL = @SN WHERE OBJECT_ID = @newObjID
				exec A_SP_ACTUAL_PARTS_FINISH_WF null,@newObjID,@strNTLogin
				UPDATE A_ACTUAL_PARTS_HISTORY SET PARENT_ID = NULL, AP_STATUS = 'ap_available' WHERE ID = @apHistID
				goto fin
			end
		else
			begin
				UPDATE A_ACTUAL_PARTS_HISTORY SET PARENT_ID = (SELECT PARENT_ID FROM A_V_ACTUAL_PARTS_QUICK WHERE ID = @ID) WHERE ID = @moveFrom
				UPDATE A_ACTUAL_PARTS_HISTORY SET PARENT_ID = NULL WHERE ID = @apHistID
				goto fin
			end		
		end
	else
		begin
		print 'The current serial number is not null and this is the root part'
		select @moveFrom = OBJECT_ID FROM A_V_ACTUAL_PARTS_APPROVED_DATA WHERE SERIAL = @SN AND PART_ID = @partID
		if @moveFrom is null
			begin
				print 'We have to create a new part for this serial number ' + @SN
				exec A_SP_ACTUAL_PARTS_COPY_ONE @newObjID OUTPUT,@apHistID,@strNTLogin,null
				UPDATE A_OBJECTS SET STATUS = 'APPROVED',LOCKED_BY = NULL,LOCKED_BY_NAME = null WHERE ID = @newObjID
				UPDATE A_ACTUAL_PARTS_HISTORY SET SERIAL = @SN WHERE OBJECT_ID = @newObjID
				exec A_SP_ACTUAL_PARTS_FINISH_WF null,@newObjID,@strNTLogin
				print 'Now we need to make this part ' + @newObjID + ' a part of this call'
				exec A_SP_ACTUAL_PART_REPLACE_PART_IN_CALL @taskID,@ID,@newObjID,@strNTLogin
				
			end
		else
			begin
				print 'Move the part ' + @moveFrom + ' into this call.'
				declare @oldPartStat varchar(50),@oldPartLoc varchar(50)
				SELECT @oldPartStat = AP_STATUS,@oldPartLoc = LOCATION FROM A_ACTUAL_PARTS_HISTORY WHERE ID = @apHistID
				UPDATE A_ACTUAL_PARTS_HISTORY 
					SET AP_STATUS = @oldPartStat,
						LOCATION = @oldPartLoc
					WHERE OBJECT_ID = @newObjID
				exec A_SP_ACTUAL_PART_REPLACE_PART_IN_CALL @taskID,@ID,@moveFrom,@strNTLogin
			end		
			UPDATE A_ACTUAL_PARTS_HISTORY 
				SET AP_STATUS = 'ap_available',LOCATION = NULL
				WHERE ID = @apHistID
			print 'Now we need to check and see if we should delete the part because it was a temp part and this is the only place it was used at.'
			exec A_SP_ACTUAL_PARTS_DELETE_A_PART_ENTIRELY @ID,@strNTLogin
		end

	end
	





fin:
if @@trancount > 0 COMMIT TRANSACTION
print 'Finished A_SP_ACTUAL_PART_UPDATE_SERIAL_FROM_SERIALIZE_TASK with no errors'
return 0

PROBLEM:
if @@trancount > 0 	ROLLBACK TRANSACTION
print ' There was a problem in A_SP_ACTUAL_PART_UPDATE_SERIAL_FROM_SERIALIZE_TASK and we will terminate and not finish anything '
return 1

GO
