CREATE procedure [dbo].[Portal_GetPartsAndKitsLabels]
@fileId int
AS

DECLARE  @parent_id int; 
DECLARE @history_id int;


SELECT @parent_id = FILL_OBJ_ID, @history_id= PURCHASE_HIST_ID FROM A_V_FILLS_SEARCH with (noLock)  WHERE ID = @fileId
print @parent_id;

SELECT * FROM(

SELECT t1.id, t1.PROC_NAME, t1.SERIAL, T1.COMPANY_PART_NUMBER, t1.PART_DESC, t1.ACTUAL_PART_ID,
 (SELECT COUNT(SerialNumber) FROM PartsTransactionLog L WHERE L.PartId = t1.ACTUAL_PART_ID AND L.SerialNumber = t1.SERIAL) AS CYCLE_COUNT,
  t2.LocationName AS SITE_NAME, t2.DueDate AS DUE_DATE, t2.WOItem AS WO_ITEM_NUMBER, t2.ReferencePO AS PO_NUMBER, t1.OEM_PART_NUMBER AS OEM_PART_NUMBER,
  -- ADD FIRST PART IMAGE ID (IF ANY) HERE
(SELECT TOP (1) DL.LINKED_DOC_ID 
	FROM A_V_DOCUMENTS_WITH_LINKED_ITEM AS DL 
	WHERE DL.OBJECT_ID = PH.OBJECT_ID AND DL.CONTENTTYPE LIKE 'image/%') AS PART_IMAGE_ID

FROM A_V_ACTUAL_PARTS_APPROVED_DATA_FOR_EACH_PART t1 
INNER JOIN Portal_WorkOrders t2 ON t2.FillId = @fileId 
INNER JOIN A_PARTS_HISTORY PH ON t1.PART_HIST_ID = PH.ID

WHERE (t1.PARENT_ID = @parent_id  or t1.id =@parent_id)
and
t1.HId in (select id from [A_ACTUAL_PARTS_HISTORY] where parent_id =@parent_id or object_id=@parent_id)

) AS Parts ORDER BY id ASC


GO