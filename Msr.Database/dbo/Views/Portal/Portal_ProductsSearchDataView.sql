Create View Portal_ProductsSearchDataView
AS

SELECT     
o.Id,
'(' + orders.CUSTOMER_ROOT_CO_NAME + ') ' + ph.NAME + ' [supplier: ' + ad.NAME + ']' AS NAME,
ph.SUPPLIER_ID AS SupplierId,
pqp.CUST_ID AS CustomerId,
pqp.Order_Id AS OrderId,
o.STATUS AS Status
FROM dbo.A_OBJECTS o
INNER JOIN dbo.A_PRODUCTS_HISTORY ph ON o.ID = ph.OBJECT_ID
INNER JOIN dbo.A_V_COMPANIES_APPROVED_DATA ad ON ph.SUPPLIER_ID = ad.ID 
INNER JOIN dbo.A_V_PROCEDURES_APPROVED_DATA pad ON ph.PROCEDURE_ID = pad.ID 
INNER JOIN  dbo.A_PRODUCTS_QUICK_PRICE pqp ON ph.ID = PQP.PROD_HIST_ID
INNER JOIN [A_O_ORDERS] orders ON orders.OBJECT_ID = pqp.Order_Id