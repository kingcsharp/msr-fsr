CREATE VIEW [dbo].[Portal_WorkOrders]

AS
SELECT DISTINCT
NewId() AS Id,
t.LATEST_REQUESTEE_NAME AS RequesteeName,
t.ID AS TaskId,
supp.NAME AS SupplierName,
purch.ID AS PurchaseId,
toi.PURCHASE_HIST_ID AS PurchaseHistId,
toi.PURCHASE_ITEM_ID AS PurchaseItemId,
customer.NAME + '-' + toi.PURCHASE_ITEM_ID  AS WoItem,
customer.NAME AS CustomerName,
ISNULL(t.ACTUAL_STOP_DATE, purchItem.DUE_DATE) AS DueDate,
purchItem.ORIG_DUE_DATE AS OrigDueDate,
t.PROCEDURE_ID AS ProcId,
customer.ID AS CustId, 
dbo.A_FN_DATE_TIME_ADD_USING_UNITS(purchItem.PROD_TIME_UNIT, purchItem.DUE_DATE, - purchItem.PROD_TIME) AS StartDate,
t.STATUS AS Status, 
t.REQUESTEE_ID AS RequesteeId,
t.GROUP_REQUESTEE_ID AS GroupRequesteeId,
Product.NAME AS ProductName,
purch.CUST_PURCH_NUM AS CustPurchNum,
Account.REFERENCE_PO AS ReferencePo, 
[PROC].NAME AS ProcName,
purchItem.QTY AS Qty,
dbo.A_V_ACTUAL_PARTS_QUICK.NICK_NAME AS NickName,
dbo.A_V_ACTUAL_PARTS_QUICK.SERIAL AS Serial,
dbo.A_V_ACTUAL_PARTS_QUICK.ID AS ActualPartId,
t.CUR_PLANNED_START_DATE AS StDate, 
ISNULL(t.ACTUAL_STOP_DATE, purchItem.DUE_DATE) AS ActualStopDate,
t.ACTUAL_START_DATE  AS ActualStartDate,
purchItem.MT_NUM AS MtNum, 
toi.FILL_ITEM_ID AS FillItemId,
dbo.A_FILLS.BATCH_PARENT AS BatchParent,
dbo.A_FILLS.BATCHED AS Batched,
dbo.A_FILLS.BATCH_FILL AS BatchEdFill,
dbo.A_FILLS.ID AS FillId,
dbo.A_FILLS.FILL_QTY AS FillQty,
dbo.A_TASK_COMPLETION_STATS.PERC_COMPLETE AS PercComplete,
ISNULL(dbo.A_TASK_COMPLETION_STATS.TIME_COMPLETE,0) AS TimeComplete,
dbo.A_TASK_COMPLETION_STATS.MY_TOT_HOURS  AS MyTotHours,
 dbo.A_TASK_COMPLETION_STATS.MY_COMP_HOURS AS MyCompHours,
REPLACE(REPLACE((CASE WHEN CHARINDEX('<<nl/>>', A_TASK_COMPLETION_STATS.CUR_STEP_TEXT) > 0 THEN SUBSTRING(A_TASK_COMPLETION_STATS.CUR_STEP_TEXT, 0, CHARINDEX('<<nl/>>', A_TASK_COMPLETION_STATS.CUR_STEP_TEXT )) 
ELSE A_TASK_COMPLETION_STATS.CUR_STEP_TEXT END),'<<bb>>',''),'<</bb>>','')  AS CurStepText,
dbo.A_V_ACTUAL_PARTS_QUICK.OBJECT_ID AS ActPartObjId,
CASE WHEN (          

SELECT count(*)
 FROM A_DOCUMENTS WHERE ID IN
(
SELECT file_id FROM A_V_ACTUAL_PARTS_RELATED_FILES
WHERE 
ACTUAL_PART_ID = dbo.A_V_ACTUAL_PARTS_QUICK.ID
AND (FILE_NAME LIKE '%%' OR FILE_NAME is NULL ) 
AND  (FILE_DESCRIPTION LIKE '%%' OR FILE_DESCRIPTION is NULL ) 
AND STATUS = 'ACTIVE'
)) > 0THEN 1 ELSE 0 END 
AS HasFile,
supp.ID as SupplierId,
isnull('['+STUFF((    SELECT ',' + '{"Date":"'+  FORMAT ( n.CreatedDate, 'MM/dd/yyyy hh:mm') +'","Name":"'+ u.FirstName + ' '+ u.LastName + '","Message":"' +n.message  +'"}'
                        FROM [Portal_Note] n
						INNER JOIN AspNetUsers u ON u.Id = n.CreatedBy
                        WHERE n.EntityId=dbo.A_FILLS.ID
						ORDER BY n.CreatedDate DESC
                        FOR XML PATH('')), 1, 1, '' ) +']'

						,'') AS Notes
,0 AS HasMonitor
,1 AS HasNcr
,[PROC].Threshold

FROM         dbo.A_V_COMPANIES_APPROVED_DATA_QUICK AS customer INNER JOIN
                      dbo.A_V_PURCHASES_APPROVED_DATA AS purch ON customer.ID = purch.CUSTOMER_CO RIGHT OUTER JOIN
                      dbo.A_TASK_COMPLETION_STATS RIGHT OUTER JOIN
                      dbo.A_TASK_OBJECT_LINK AS T_OBJ INNER JOIN
                      dbo.A_V_PROCEDURES_DATA_QUICK AS [PROC] INNER JOIN
                      dbo.A_TASK_ORDER_INFORMATION AS toi INNER JOIN
                      dbo.A_TASKS AS t ON toi.TASK_ID = t.ID ON [PROC].ID = t.PROCEDURE_ID ON T_OBJ.TASK_ID = t.ID INNER JOIN
                      dbo.A_FILLS ON toi.FILL_ITEM_ID = dbo.A_FILLS.ID INNER JOIN
                      dbo.A_V_PRODUCTS_APPROVED_DATA AS Product INNER JOIN
                      dbo.A_ORDER_ITEMS AS purchItem ON Product.ID = purchItem.PRODUCT_ID ON dbo.A_FILLS.PURCH_ITEM_ID = purchItem.ID ON 
                      dbo.A_TASK_COMPLETION_STATS.TASK_ID = t.ID LEFT OUTER JOIN
                      dbo.A_V_ACCOUNTS_APPROVED_DATA_QUICK AS Account ON purchItem.ACCOUNT_ID = Account.ID ON 
                      purch.HISTORY_REF_ID = toi.PURCHASE_HIST_ID LEFT OUTER JOIN
                      dbo.A_V_ACTUAL_PARTS_QUICK ON T_OBJ.OBJECT_ID = dbo.A_V_ACTUAL_PARTS_QUICK.ID
					  RIGHT JOIN dbo.A_V_COMPANIES_APPROVED_DATA_QUICK supp on supp.ID = Account.SUPPLIER_CO
WHERE     (t.STATUS IN ('REQUESTED', 'ACCEPTED', 'CLOSED', 'FINISHED')) AND (toi.PURCHASE_ITEM_ID IS NOT NULL)

--WHERE (CUST_ID = '2' OR (  GROUP_REQUESTEE_ID IN ('','1502','1490','1498','1506','1510','1494') 
--OR  REQUESTEE_ID = '110332' OR  exists( 	  SELECT ID FROM A_TASKS ts 	
--  WHERE ts.PARENT_ID = t.ID and       (ts.REQUESTEE_ID = '110332' or ts.GROUP_REQUESTEE_ID IN ('','1502','1490','1498','1506','1510','1494') )	  )  ) )
--  AND (STATUS IN ('REQUESTED','ACCEPTED')) AND  (SERIAL LIKE '%%' OR SERIAL is NULL ) AND  (CUST_PURCH_NUM LIKE '%%' OR CUST_PURCH_NUM is NULL )
--   AND  (PROC_ID LIKE '%%' OR PROC_ID is NULL ) AND  (PRODUCT_NAME LIKE '%%' OR PRODUCT_NAME is NULL ) AND  (PURCHASE_ITEM_ID LIKE '%%' OR PURCHASE_ITEM_ID is NULL ) 
--   ORDER BY ST_DATE

GO