--   exec Portal_GetNcrReport '109815'

create procedure Portal_GetNcrReport

@FileId nvarchar(50)
AS

DECLARE @PurchItemId nvarchar(50),@SupName nvarchar(50), @FillObjDesc nvarchar(250),@CustName nvarchar(50), @Technician nvarchar(50), @FillObjId nvarchar(50),
@Serial nvarchar(50),@PartDesc nvarchar(50)

SELECT 
@PurchItemId = PURCH_ITEM_ID,
@SupName = SUP_NAME,
@FillObjDesc = FILL_OBJ_DESC,
@CustName = CUST_NAME,
@FillObjId = FILL_OBJ_ID
FROM A_V_FILLS_SEARCH WHERE ID = @FileId

SELECT TOP 1 @Technician=LATEST_REQUESTEE_NAME FROM A_TASKS WHERE PARENT_ID = (SELECT TASK_ID FROM A_TASK_ORDER_INFORMATION with (noLock) WHERE FILL_ITEM_ID = @FileId)

SELECT @Serial=SERIAL, @PartDesc= PART_DESC FROM A_V_ACTUAL_PARTS_APPROVED_DATA WHERE ID = @FillObjId

SELECT @PurchItemId AS PurchItemId,@SupName AS SupName, @FillObjDesc AS FillObjDesc, @CustName As CustName, @Technician AS Technician, @Serial AS Serial, @PartDesc AS PartDesc, @FillObjId AS FillObjId




