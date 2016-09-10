








CREATE                      PROCEDURE A_SP_ACTUAL_PARTS_UPDATE_PART
@newID nvarchar(50) OUTPUT,
@messages nvarchar(2000) OUTPUT,
@objID nvarchar(50),
@PART_ID varchar(50),
@QTY varchar(50),
@SERIAL nvarchar(1000),
@NICK_NAME nvarchar(1000),
@LOCATION_ID varchar(50),
@CUR_OWNER varchar(50),
@AP_STATUS varchar(50),
@PRODUCTS varchar(8000),
@PARENT_ID varchar(50),
@subpartAction varchar(10),
@responsiblePerson varchar(50),
@strNTLogin varchar(50)
AS
begin Transaction
if @QTY is null	set @QTY = 1
if @QTY = 0 goto PROBLEM

declare @newAPID as varchar(50)
print 'Updating an Actual Part'
declare @intN as smallint
set @intN = 0
if @objID is null
	begin
		print 'ID is Null  we need to create this Actual Part'
		exec sp_GetUniqueID3 @newID OUTPUT
		INSERT INTO A_ACTUAL_PARTS_HISTORY (ID,NICK_NAME,SERIAL,MODBY,DRCM,PARENT_ID) 
			VALUES (@newID,@NICK_NAME,@SERIAL,@strNTLogin,getDATE(),@PARENT_ID)
		if @@ERROR <> 0 goto problem
		SELECT @newID = OBJECT_ID FROM A_ACTUAL_PARTS_HISTORY WHERE ID = @newID
		print 'Done Creating now continueing'
	end
else
	begin
		print 'The ID is not null so we are just updating product object ID = ' + @objID
		set @newID = @objID
	end

SELECT @newAPID = ID FROM A_ACTUAL_PARTS_HISTORY WHERE OBJECT_ID = @newID
if @newAPID is null
	begin
		print 'Error Can not find this Part'
		goto problem
	end


print 'We need to do a little work here if the parent is not null'
if @PARENT_ID is not null
	begin
		print 'The Parent ID = ' + @PARENT_ID
		set @AP_STATUS = 'ap_installed'
		if @@ERROR <> 0 goto problem
		SELECT @LOCATION_ID = LOCATION FROM A_V_ACTUAL_PARTS_APPROVED_DATA WHERE ID = @PARENT_ID
		if @@ERROR <> 0 goto problem
	end

print 'Updating the ap history data ' + @AP_STATUS
UPDATE A_ACTUAL_PARTS_HISTORY SET
PARENT_ID = @PARENT_ID,
PART_ID = @PART_ID,
QTY = @QTY,
SERIAL = @SERIAL,
NICK_NAME = @NICK_NAME,
LOCATION = @LOCATION_ID,
CUR_OWNER = @CUR_OWNER,
AP_STATUS = @AP_STATUS,
MODBY = @strNTLogin,
SUB_PART_ACTION = @subPartAction,
RESPONSIBLE_PERSON = @responsiblePerson,
DRCM = getDate()
WHERE OBJECT_ID = @newID
if @@ERROR <> 0 goto problem

DELETE FROM A_ACTUAL_PART_PRODUCTS_INSIDE_LINK WHERE ACTUAL_PART_ID = @newAPID
if @@ERROR <> 0 goto problem
CREATE TABLE #TempItems(IT varchar(50))
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @PRODUCTS,','
if @@ERROR <> 0 goto problem
CREATE TABLE #TempItemsII(IT varchar(50))
if @@ERROR <> 0 goto problem
INSERT INTO #TempItemsII SELECT DISTINCT IT FROM #TempItems
if @@ERROR <> 0 goto problem

INSERT INTO A_ACTUAL_PART_PRODUCTS_INSIDE_LINK (ID,ACTUAL_PART_ID,PRODUCT_ID,DRCM,MODBY)
	SELECT newID(),@newAPID,ltrim(IT),getDate(),@strNTLogin FROM #TempItemsII
if @@ERROR <> 0 goto problem

fin:
if @@trancount > 0 COMMIT TRANSACTION
print 'Finished A_SP_ACTUAL_PARTS_UPDATE_PART with no errors'
return 0

PROBLEM:
if @@trancount > 0 	ROLLBACK TRANSACTION
print ' There was a problem in A_SP_ACTUAL_PARTS_UPDATE_PART and we will terminate and not finish anything '
return 1
raiserror('Problem creating an actual part',16,1)













