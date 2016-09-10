
CREATE  PROCEDURE dbo.A_SP_ACTUAL_PARTS_CREATE_BATCH_PART 
@batchActualPartID varchar(50) OUTPUT,
@retVal integer OUTPUT,
@batchPartID varchar(50),
@custCo varchar(50),
@strNTLogin varchar(50)
AS
declare @myCo varchar(50)
SELECT @myCO = ROOT_COMPANY FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @strNTLogin
exec A_SP_ACTUAL_PARTS_UPDATE_PART
@batchActualPartID OUTPUT,
null,
null,
@batchPartID, --@PART_ID varchar(50),
'1', --@QTY varchar(50),
null, --@SERIAL nvarchar(1000),
null, --@NICK_NAME nvarchar(1000),
null, --@LOCATION_ID varchar(50),
@custCo, --@CUR_OWNER varchar(50),
'ap_in_fill', --@AP_STATUS varchar(50),
null, --@PRODUCTS varchar(8000),
null, --@PARENT_ID varchar(50),
null, --@subpartAction varchar(10),
null, --@responsiblePerson varchar(50),
@strNTLogin --varchar(50)
UPDATE A_ACTUAL_PARTS_HISTORY SET SERIAL = 'BATCH-' + OBJECT_ID, NICK_NAME = 'BATCH-' + OBJECT_ID WHERE OBJECT_ID = @batchActualPartID
exec A_SP_OBJECTS_QUICK_APPROVE @batchActualPartID,@strNTLogin
exec A_SP_ACTUAL_PARTS_FINISH_WF null,@batchActualPartID,@strNTLogin
set @retVal = 0








