CREATE procedure [dbo].[Portal_GetPartsAndKitsLabels]
@fileId int
AS

DECLARE  @parent_id int; 
DECLARE @history_id int;


SELECT @parent_id = FILL_OBJ_ID, @history_id= PURCHASE_HIST_ID FROM A_V_FILLS_SEARCH with (noLock)  WHERE ID = @fileId
print @parent_id;

SELECT * FROM(

SELECT * FROM A_V_ACTUAL_PARTS_APPROVED_DATA_FOR_EACH_PART 
WHERE (PARENT_ID = @parent_id  or id =@parent_id)
and
HId in (select id from [A_ACTUAL_PARTS_HISTORY] where parent_id =@parent_id or object_id=@parent_id)

 ) AS Parts ORDER BY Id ASC


GO