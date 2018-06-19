CREATE procedure [dbo].[Portal_GetTsrPurchaseWorkReport]

@purchaseItemId varchar(50)

AS

DECLARE 
	@purchaseHistoryId VARCHAR(50),
	@accountNum VARCHAR(50), 
	@blanketPONum VARCHAR(50);

	SELECT TOP 1 @purchaseHistoryId = PURCHASE_HIST_ID FROM A_ORDER_ITEMS WHERE ID = @purchaseItemId AND PURCHASE_HIST_ID IS NOT NULL ORDER BY DRCM;
	
	SELECT @accountNum = ID, @blanketPONum = REFERENCE_PO FROM A_V_ACCOUNTS_APPROVED_DATA WHERE ID IN (SELECT ACCOUNT_ID FROM A_ORDER_ITEMS WHERE PARENT = @purchaseHistoryId)

	SELECT f.PURCHASE_ID AS PurchaseId, f.PURCH_ITEM_ID AS PurchaseItemId, f.FILL_OBJ_ID AS ActualPartDbId, 
	f.CUST_LINE_ITEM AS LineItem,ap.QTY AS Quantity,ap.SERIAL, f.SUP_NAME AS SupplierName, f.CUST_NAME AS CustomerName, 
	f.PROD_ID AS ProductId, f.PROD_NAME AS ProductName, f.PROC_ID AS ProcedureId, f.PROC_NAME AS ProcedureName, 
	f.APP_OBJ_DESC AS OwnerPartName, f.FILL_DATE AS FillDate, sq.CUST_PURCH_NUM AS CustomerPurchaseNumber, 
	@accountNum AS AccountNumber, @blanketPONum AS BlanketPoNumber FROM A_V_FILLS_SEARCH f 
	LEFT OUTER JOIN A_V_ACTUAL_PARTS_APPROVED_DATA ap on f.FILL_OBJ_ID = ap.ID 
	LEFT OUTER JOIN A_V_PURCHASES_WITH_SUPPLIER_QUOTES sq on f.PURCHASE_HIST_ID = sq.PURCH_HIST_ID
	WHERE f.PURCH_ITEM_ID IN (SELECT ID FROM A_ORDER_ITEMS WHERE PURCHASE_HIST_ID = @purchaseHistoryId)

	GO