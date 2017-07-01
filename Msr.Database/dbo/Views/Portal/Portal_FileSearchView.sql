create view Portal_FileSearchView

AS
SELECT 
Id,
SUP_NAME AS SupName,
FILL_OBJ_DESC As FillObjDesc,
PURCH_ITEM_ID AS PurchItemId
FROM A_V_FILLS_SEARCH

GO