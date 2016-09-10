

CREATE   PROCEDURE dbo.A_SP_ACTUAL_PARTS_GRAB_CHILDREN_PARTS
@ID varchar(50),
@strNTLogin varchar(50)
AS
begin Transaction
print 'Going to grab the parts for ap id = ' + @ID
declare @LOCATION varchar(50), @cPartID varchar(50), @pQty float, @cQty float,
	@CUR_OWNER varchar(50),@apStat varchar(50),@pPartID varchar(50),
	@cObjID varchar(50),@msgs varchar(4000)
SELECT @LOCATION = LOCATION, @pQty = Qty,@CUR_OWNER = CUR_OWNER,@pPartID = PART_ID
	FROM A_ACTUAL_PARTS_HISTORY 
	WHERE OBJECT_ID = @ID
if @@ERROR <> 0 goto problem

declare @phPartID varchar(50)
SELECT @phPartID = PARTS_HISTORY_ID FROM A_PARTS WHERE ID = @pPartID
print 'My Location is ' + @LOCATION
print 'parent Quantity = ' + convert(varchar(50),@pQty)
print 'Cur Owner = ' + @CUR_OWNER
print 'Part ID = ' + @phPartID
if @@ERROR <> 0 goto problem


Declare @curs Cursor,@cNickName nvarchar(200),
	@searchPartQty float,@runningQty float,@cont tinyInt,@split1 float,@split2 float,
	@splitReturnID varchar(50),@splitObjID varchar(50),@partCollectorCurs cursor,
	@cnt int,@pcID varchar(50),@pcQty float,@strSplitList varchar(50),@splitReturnObjID varchar(50),
	@errMsg varchar(5000),@pcObjID varchar(50),@mergeIntoPart varchar(50)

set @curs = Cursor For SELECT PART_ID,QTY,NICK_NAME FROM A_PARTS_SUB_PARTS WHERE PARENT = @phPartID
if @@ERROR <> 0 goto problem
open @curs
if @@ERROR <> 0 goto problem
Fetch Next from @curs Into @cPartID,@cQty,@cNickName
while (@@fetch_status = 0)
Begin
	set @strSplitList = ''
	set @pcQty = 0
	set @runningQty = 0
	print 'Searching for Child PArt = ' + @cPartID
	set @cQty = @cQty * @pQty
	if @@ERROR <> 0 goto problem
	
	SELECT @searchPartQty = isNull(sum(QTY),0)
		FROM A_V_ACTUAL_PARTS_APPROVED_DATA 
		WHERE PART_ID = @cPartID
		AND AP_STATUS = 'ap_available'
		AND (
			LOCATION = @LOCATION OR 
			LOCATION IN (SELECT CHILD_LOC FROM A_LOCATIONS_CHILD_LOOKUP WHERE LOCATION = @LOCATION)
			)

	print 'I found ' + isnull(convert(varchar(50),@searchPartQty),'NULL') + ' parts at this location'
	print 'SELECT isNull(sum(QTY),0)
		FROM A_V_ACTUAL_PARTS_APPROVED_DATA 
		WHERE PART_ID = ' + isNull(@cPartID,'NULL') + 
		'AND AP_STATUS = ''ap_available''
		AND (
			LOCATION = ''' + isnull(@LOCATION,'NULL') + ''' OR 
			LOCATION IN (SELECT CHILD_LOC FROM A_LOCATIONS_CHILD_LOOKUP WHERE LOCATION = ''' + isNull(@LOCATION,'NULL') + ')
			)'



	if @searchPartQty < @cQty
		begin
		print 'There is no way I can gather enough parts to make this part at this location'
		print 'I am going to just make a new part cause that is all I can do.'
		exec A_SP_ACTUAL_PARTS_UPDATE_PART
			@cObjID OUTPUT,
			@msgs OUTPUT,
			NULL,
			@cPartID,
			@cQty,
			NULL,
			@cNickName,
			@LOCATION,
			@CUR_OWNER,
			'ap_installed',
			NULL,
			@ID,
			null,
			null,
			@strNTLogin
			if @@ERROR <> 0 goto problem
			print 'Added the child part and got an object ID back of ' + @cObjID
			print 'going to go ahead and set the status to approved and then call function to finish approval WF'
			UPDATE A_OBJECTS SET STATUS = 'APPROVED',LOCKED_BY = NULL WHERE ID = @cObjID
			if @@ERROR <> 0 goto problem
			exec A_SP_ACTUAL_PARTS_FINISH_WF null,@cObjID,@strNTLogin
			INSERT INTO A_ACTUAL_PARTS_PICK_LIST 
				(ID,PARENT_ID,CHILD_ID,CHILD_LOCATION,CHILD_QTY,DATE_PICKED,IS_BRAND_NEW)
				SELECT newID(),@ID,ID,LOCATION,QTY,getDate(),1
				FROM A_V_ACTUAL_PARTS_APPROVED_DATA WHERE ID = @cObjID
		end
	else
		begin
		print 'There is definitely enough parts at this location or at its child locations to make this actual part'
		print 'since I dont really care where they come from within this group of parts I am going to start with the smallest break downa nd work my way up.'
		set @partCollectorCurs = CURSOR FOR 
			SELECT ID,QTY,OBJECT_ID
				FROM A_V_ACTUAL_PARTS_APPROVED_DATA 
				WHERE PART_ID = @cPartID 
				AND AP_STATUS = 'ap_available'
				AND (
					LOCATION = @LOCATION OR 
					LOCATION IN (SELECT CHILD_LOC FROM A_LOCATIONS_CHILD_LOOKUP WHERE LOCATION = @LOCATION)
					)
				ORDER BY QTY
		OPEN @partCollectorCurs
		fetch next from @partCollectorCurs into @pcID,@pcQty,@pcObjID
		set @runningQty = 0
		set @cnt = 0
		set @cont = 1
		set @mergeIntoPart = null
		while @@fetch_status = 0 and @cont = 1
			begin
			print 'We are looking at actual part ID = ' + @pcID
			if @cnt = 0
				begin
				set @mergeIntoPart = @pcObjID
				end

			set @runningQty = @runningQty + @pcQty
			print 'The merge PArt ID = ' + @mergeIntoPart
		
			if @runningQty = @cQty
				begin
				print 'The running qty = cQty so we need to add this one and stop pcObjID = ' + @pcObjID
				set @cont = 0
				exec A_SP_ACTUAL_PART_ADD_PART_TO_PICK_LIST @ID,@mergeIntoPart,@pcObjID,@cPartID,@cNickName,@strNTLogin				
				end
			else
				if @runningQty > @cQty
					begin
					print 'We have more than we need of this actual part so we need to split it'
					set @split1 = @runningQty - @cQty
					set @split2 = @pcQty - @split1
					set @strSplitList = convert(varchar(50),@split2) + '_____' + convert(varchar(50),@split1)
					print 'The Splits look like ' + @strSplitList
					SELECT @splitObjID = OBJECT_ID FROM A_V_ACTUAL_PARTS_APPROVED_DATA WHERE ID = @pcID
					exec A_SP_ACTUAL_PARTS_SPLIT_PART
						@splitReturnObjID OUTPUT,@errMsg OUTPUT,@splitObjID,@strSplitList,@strNTLogin			
					if @cnt = 0
						set @mergeIntoPart = @splitReturnObjID  --we dont want to merge since we split it
					exec A_SP_ACTUAL_PART_ADD_PART_TO_PICK_LIST @ID,@mergeIntoPart,@splitReturnObjID,@cPartID,@cNickName,@strNTLogin
					set @cont = 0
					end
				else
					begin
					print 'This is just the first of many more we need so add it to pick list and keep adding.'
					print 'pc Obj ID = ' + @pcObjID
					exec A_SP_ACTUAL_PART_ADD_PART_TO_PICK_LIST @ID,@mergeIntoPart,@pcObjID,@cPartID,@cNickName,@strNTLogin
					end
			print '--------------Finished adding one child part = ' + @pcObjID + ' Now my cont = ' + convert(varchar(50),@cont)
			set @cnt = @cnt + 1
			fetch next from @partCollectorCurs into @pcID,@pcQty,@pcObjID
			end
		end
	if @@ERROR <> 0 goto problem
	Fetch Next from @curs Into @cPartID,@cQty,@cNickName
End
close @curs
Deallocate @curs


fin:
if @@trancount > 0 COMMIT TRANSACTION
print 'Finished A_SP_ACTUAL_PARTS_GRAB_CHILDREN_PARTS with no errors'
return 0

PROBLEM:
if @@trancount > 0 	ROLLBACK TRANSACTION
print ' There was a problem in A_SP_ACTUAL_PARTS_CREATE_CHILDREN_FROM_SOURCE_PART and we will terminate and not finish anything '
return 1







