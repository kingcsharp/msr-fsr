



CREATE           PROCEDURE dbo.A_SP_FILL_FILL_WITH_PART_AND_SERIAL_NUMBER 
@fillID varchar(50),
@strPartID varchar(50),
@strSN varchar(1000),
@strQty float,
@strLoc varchar(50),
@strOwner varchar(50),
@strNTLogin varchar(50)
AS
declare @prob as varchar(500),@myInfo varchar(50)
begin transaction

declare @partIDCO varchar(50)
SELECT @partIDCO = COMPANY FROM A_V_PARTS_APPROVED_DATA WHERE ID = @strPArtID
	
if isNull(@strQty,0) < 1 
	begin
	set @prob = 'Trying to make a part with a qty less than 1 qty = ' + isNull(convert(nvarchar(50),@strQty),'NULL')
	goto PROBLEM
	end

 if (not exists (SELECT * FROM A_FILLS WHERE ID = @fillID AND FILL_OBJ_ID IS NULL AND FILL_QTY IS NULL))
 	begin
 	set @prob = 'This item was already filled'
 	goto problem
 	end
 declare @actPartRootObjID varchar(50),@myCo varchar(50),@messages varchar(2000)
 SELECT @myCo = ROOT_COMPANY FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @strNTLogin
 if @@ERROR <> 0 
	begin
	set @prob = 'Error Finding my root Company'
	goto problem
	end
 print 'My Root Company is ' + @myCo
if @strOwner is null
	set @strOwner = @myCo

 SELECT top 1 @actPartRootObjID = ID 
 	FROM A_V_ACTUAL_PARTS_APPROVED_DATA_QUICK 
 	WHERE SERIAL = @strSN AND (PART_ID = @strPartID OR PART_ID IN 
		(SELECT INT_PART_ID FROM A_V_PARTS_EXTERNAL_PART_LOOKUP WHERE EXT_PART_ID = @strPartID and INT_PART_ID is not null))
 if @@ERROR <> 0 
	begin
	set @prob = 'There was a problem running the query to get the internal part ID'
	goto problem
	end

 if @actPartRootObjID is null
 	begin
	set @myInfo = 'Created a new part to fill this.'
	if @partIDCO <> @strOwner
		begin
		print 'The companies of the part id do not match the part id = ' + @strPartID
		SELECT @strPartID = INT_PART_ID FROM A_V_PARTS_EXTERNAL_PART_LOOKUP 
			WHERE EXT_PART_ID = @strPartID AND INT_PART_ID IS NOT NULL AND INT_CO = @myCo
		if @@ERROR <> 0 goto problem
		if @strPartID is null
			begin
			print 'We did not find the part so we are exiting'
		 	set @prob = 'Could not find a Part in this company for the part = ' + isNull(@strPartID,'NULL')
			goto problem
			end 
		print 'We are using our internal part id of ' + isnull(@strPartID,'NULL')

		end
	if @@ERROR <> 0 goto problem
	print 'The Actual Part Does Not exist so I am going to make it.'
	exec  A_SP_ACTUAL_PARTS_UPDATE_PART
		@actPartRootObjID OUTPUT,
		@messages OUTPUT,
		null,
		@strPartID,
		@strQty,
		@strSN,
		null,
		@strLoc,
		@strOwner,
		'ap_available',
		null,
		null,
		'CREATE_NEW',
		null,
		@strNTLogin	
	print 'The Actual Part ID is ' + @actPartRootObjID
	UPDATE A_OBJECTS SET STATUS = 'APPROVED',UNLOCKED_BY = @strNTLogin,LOCKED_BY = NULL,LOCKED_BY_NAME = NULL
		WHERE ID = @actPartRootObjID
	exec A_SP_ACTUAL_PARTS_FINISH_WF 	null,@actPartRootObjID,@strNTLogin

    DELETE FROM Portal_AddSubPartQtyCount WHERE Portal_AddSubPartQtyCount.ParentId IN (select ParentId from Portal_AddSubPartQtyCount WHERE dbo.Portal_AddSubPartQtyCount.ParentId=@actPartRootObjID)
	end
else
	begin
	set @myInfo = 'Found a part to fill this'
	end
Print 'Filling a fill with an actual part'
declare @objTable varchar(50),@ID varchar(50),
		@fillQty int,@partQty int,
		@objID varchar(50),@sysID varchar(50)

SELECT @partQty = QTY FROM A_V_ACTUAL_PARTS_APPROVED_DATA_QUICK WHERE ID = @actPartRootObjID
if @@ERROR <> 0 goto problem

if @partQty is null
	goto problem

SELECT @fillQty = PURCHASE_QTY,@sysID = SYS_PROC_ID FROM A_V_FILLS_SEARCH WHERE ID = @fillID
if @@ERROR <> 0 goto problem

 declare @myError int
-- exec @myError = A_SP_FILL_FILL_WITH_ACTUAL_PART @fillID,@actPartRootObjID,@strNTLogin
print 'exec A_SP_FILLS_UPDATE_FILL null,null,''' + @fillID + ''',''' + @actPartRootObjID + ''',''' + @strNTLogin + ''''
exec A_SP_FILLS_UPDATE_FILL null,null,@fillID,@actPartRootObjID,@strNTLogin

if isNull(@myError,0) = 1 goto PROBLEM
fin:
if @@trancount > 0 COMMIT TRANSACTION
print 'Finished A_SP_FILL_FILL_WITH_ACTUAL_PART with no errors'
SELECT @myInfo as ID
return 0
PROBLEM:
if @@trancount > 0 	ROLLBACK TRANSACTION
print ' There was a problem in A_SP_FILL_FILL_WITH_ACTUAL_PART and we will terminate and not finish anything '
if @prob is null set @prob = 'Not Filled because there was a big problem'
select @prob as ID
return 1




















