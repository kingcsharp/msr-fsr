








CREATE   PROCEDURE dbo.A_SP_FILL_FILL_WITH_PERSON
@fillID varchar(50),
@fillRootID varchar(50),
@strNTLogin varchar(50)
AS
begin transaction
declare @fillObjID as varchar(50)
declare @mySQL varchar(7500)

SELECT @fillObjID = (SELECT OBJECT_ID FROM A_PEOPLE_HISTORY h,A_PEOPLE a WHERE h.ID = a.HISTORY_REF_ID AND a.ID = @fillRootID)
Print 'Filling a fill with a person'
declare @objTable varchar(50),@ID varchar(50),@fillQty int,@partQty int,@objID varchar(50),@sysID varchar(50)
SELECT @objTable = OBJ_TABLE,@ID = OBJ_ID,@objID = OBJ_REF_ID FROM A_V_APPROVED_OBJECTS WHERE ID = @fillRootID
print 'The table is ' + isnull(@objTable,'Null and object Root ID = ' + isNull(@fillRootID,'NULL'))
print 'The ID is = ' + @ID

UPDATE A_FILLS SET FILL_OBJ_ID = @fillRootID, FILL_QTY = 1, 
	FILLER = @strNTLogin,MODBY = @strNTLogin,DRCM = getDate() 
WHERE ID = @fillID

print 'now we should go ahead and fill our child items that are waiting on us.'
declare @purchItemID varchar(50)
SELECT @purchItemID = PURCH_ITEM_ID FROM A_FILLS WHERE ID = @fillID
declare @curs as CURSOR,@childFill as varchar(50)
set @curs = CURSOR FOR SELECT ID FROM A_FILLS WHERE FILL_BY = 'PARENT_FILL_ITEM' AND 
		PURCH_ITEM_ID IN (SELECT ID FROM A_ORDER_ITEMS WHERE PARENT = @purchItemID)
open @curs
fetch next FROM @curs into @childFill
while @@fetch_status = 0
	begin
	set @mySQL =  'exec A_SP_FILLS_UPDATE_FILL null,null,'
			+ isnull('''' + @childFill + '''','NULL') + ','
			+ isnull('''' + @fillRootID + '''','NULL') + ','
			+ isnull('''' + @strNTLogin + '''','NULL') + ''
	exec A_SP_ADMIN_SQL_TO_RUN_QUE_UP @mySQL,@strNTLogin
	if @@ERROR <> 0 goto problem
	fetch next FROM @curs into @childFill
	end
close @curs
deallocate @curs
if @@ERROR <> 0 goto problem
print 'We finished all the children fills'

declare @myTaskID varchar(50),@taskPerson varchar(50)
SELECT @myTaskID = TASK_ID FROM A_FILLS WHERE ID = @fillID
SELECT @taskPerson = REQUESTEE_ID FROM A_TASKS WHERE ID = @myTaskID
declare @RET_STATUS varchar(50)
declare @MSGS varchar(500)
if @myTaskID is not null exec A_SP_TASK_FINISH @myTaskID,@taskPerson
declare @partID varchar(50),@newPartID varchar(50)
SELECT @partID = PART_ID FROM A_V_ACTUAL_PARTS_APPROVED_DATA WHERE ID = @fillRootID
	
set @mySQL =  'exec A_SP_FILL_START_PROCEDURE_IF_POSSIBLE ' 
	+ isnull('''' + @fillID + '''','NULL') + ','
	+ isnull('''' + @strNTLogin + '''','NULL') + ''
print @mySQL
exec(@mySQL)
--exec A_SP_ADMIN_SQL_TO_RUN_QUE_UP @mySQL,@strNTLogin
		

fin:
if @@trancount > 0 COMMIT TRANSACTION
print 'Finished A_SP_FILL_FILL_WITH_PERSON with no errors'
return 0
PROBLEM:
if @@trancount > 0 	ROLLBACK TRANSACTION
print ' There was a problem in A_SP_FILL_FILL_WITH_PERSON and we will terminate and not finish anything '
return 1









