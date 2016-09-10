
CREATE           PROCEDURE A_SP_ACTUAL_PARTS_SPLIT_PART
	@newID varchar(50) OUTPUT,
	@msg varchar(4000) OUTPUT,
	@objID varchar(50),
	@DISTS varchar(200),
	@strNTLogin varchar(50)
AS
begin transaction
print 'Splitting part obj =  ' + @objID
declare @myQTY as numeric
SELECT @myQTY = QTY FROM A_ACTUAL_PARTS_HISTORY WHERE OBJECT_ID = @objID
if @@ERROR <> 0 goto problem
print 'The max QTY - '
print @myQTY

print 'Updating The QTYS'
CREATE TABLE #TempItems	(IT varchar(50))
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @DISTS,'_____'
print 'First Lets Check the QTYS to make sure they are ok'
declare @splitQTYS as numeric
declare @nID as varchar(50),@msg2 nvarchar(4000)
declare @newObjID as varchar(50)
declare @newRootPart as varchar(50)
declare @tot as int
SELECT @tot = Count(IT) FROM #TempItems
print 'making a cursor to go through the splits and make the divisions'
Declare @it nvarchar(50)
Declare @curs Cursor
declare @cnt smallint
set @cnt = 1
set @curs = Cursor For SELECT * FROM #TempItems
open @curs
Fetch Next from @curs Into @it
print 'The first Qty = ' + @it
if @@ERROR <> 0 goto problem
while (@@fetch_status = 0) and (@cnt < @tot)
	Begin
	print 'Creating a new Actual Part with QTY = ' + @it
	exec A_SP_OBJECT_COPY @newObjID OUTPUT,@msg2 OUTPUT,@objID,'',@strNTLogin
	if @@ERROR <> 0 goto problem
	print 'Copied it now getting its ID'
	SELECT @nID = ID FROM A_ACTUAL_PARTS_HISTORY WHERE OBJECT_ID = @newObjID
	set @newID = @newObjID
	if @@ERROR <> 0 goto problem
	print 'Got its ID = ' + @nID
	UPDATE A_OBJECTS SET LOCKED_BY = NULL,STATUS='APPROVED'WHERE ID = @newObjID
	if @@ERROR <> 0 goto problem
	print 'Unlocked it and set status to approved'
	UPDATE A_ACTUAL_PARTS_HISTORY SET QTY = @it WHERE OBJECT_ID = @newObjID
	if @@ERROR <> 0 goto problem
	print 'Updates its quanitity'
	exec A_SP_ACTUAL_PARTS_FINISH_WF @nID,@newObjID,@strNTLogin
	if @@ERROR <> 0 goto problem
	set @msg = isNull(@msg + ',','') + @nID
	Fetch Next from @curs Into @it
	if @@ERROR <> 0 goto problem
	set @cnt = @cnt + 1
	End
close @curs
Deallocate @curs
declare @oldQty float,@mult float,@ap_ID varchar(50)
SELECT @oldQty = QTY FROM A_ACTUAL_PARTS_HISTORY WHERE OBJECT_ID = @objID
SET @mult = @it / @oldQty
UPDATE A_ACTUAL_PARTS_HISTORY
	SET QTY = @it
	WHERE OBJECT_ID = @objID
SELECT @ap_ID = ROOT FROM A_OBJECTS WHERE ID=@objID
exec A_SP_ACTUAL_PARTS_CASCADE_QTYS_FOR_A_PART_BY_ID @mult,@ap_ID
if @@ERROR <> 0 goto problem

fin:
if @@trancount > 0 COMMIT TRANSACTION
print 'Finished A_SP_ACTUAL_PARTS_SPLIT_PART with no errors'
return 0
PROBLEM:
if @@trancount > 0 	ROLLBACK TRANSACTION
print ' There was a problem in A_SP_ACTUAL_PARTS_SPLIT_PART and we will terminate and not finish anything '
return 1









