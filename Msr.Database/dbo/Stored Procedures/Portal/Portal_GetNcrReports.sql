


CREATE procedure [dbo].[Portal_GetNcrReports]
	@FileId nvarchar(50)
AS

DECLARE 
@PurchItemId nvarchar(50),
@SupName nvarchar(50),
@FillObjDesc nvarchar(250),
@CustName nvarchar(50), 
@Technician nvarchar(50),
@FillObjId nvarchar(50),
@Serial nvarchar(50),
@PartNumber nvarchar(50),
@PartDesc nvarchar(50),
@Comments nvarchar(MAX),
@NickName nvarchar(50),
@DateComplete datetime

SELECT 
@PurchItemId = PURCH_ITEM_ID,
@SupName = SUP_NAME,
@FillObjDesc = FILL_OBJ_DESC,
@CustName = CUST_NAME,
@FillObjId = FILL_OBJ_ID
FROM A_V_FILLS_SEARCH WHERE ID = @FileId

SELECT @Technician=LATEST_REQUESTEE_NAME FROM A_TASKS WHERE PARENT_ID = (SELECT TASK_ID FROM A_TASK_ORDER_INFORMATION with (noLock) WHERE FILL_ITEM_ID = @FileId)

SELECT @Serial=SERIAL, @PartNumber = COMPANY_PART_NUMBER, @PartDesc= PART_DESC, @NickName = NICK_NAME  FROM A_V_ACTUAL_PARTS_APPROVED_DATA WHERE ID = @FillObjId

SELECT @DateComplete = max(ACTUAL_STOP_DATE) FROM A_TASKS WHERE ID IN (SELECT TASK_ID FROM A_TASK_ORDER_INFORMATION WHERE FILL_ITEM_ID = @FileId)

SELECT @Comments =ISNULL(STUFF((
            SELECT '|' + COMMENT
			FROM A_TASK_COMMENT WHERE TASK_ID IN  (SELECT TASK_ID FROM A_TASK_ORDER_INFORMATION WHERE FILL_ITEM_ID = @FileId)
            FOR XML PATH('')
            ), 1, 1, ''),'')

SELECT @PurchItemId AS PurchItemId,
@SupName AS SupName,
 @FillObjDesc AS FillObjDesc, @CustName As CustName, @Technician AS Technician,
 @Serial AS Serial,
 @PartNumber AS PartNumber,
  @PartDesc AS PartDesc,
@NickName As NickName,
 @FillObjId AS FillObjId
,@Comments AS Comments
,@DateComplete AS DateComplete