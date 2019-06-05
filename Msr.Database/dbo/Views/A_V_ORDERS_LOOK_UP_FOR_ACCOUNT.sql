


CREATE VIEW [dbo].[A_V_ORDERS_LOOK_UP_FOR_ACCOUNT]
AS

SELECT     '(' + o.CUSTOMER_ROOT_CO_NAME + ') ' + o.[DESCRIPTION] + ' (R-' + CAST(O.REV AS VARCHAR(50))  + ')' + ' [supplier: ' + supplier.NAME + ']' AS NAME, o.OBJECT_ID AS ORDER_ID, 
                      o.CUSTOMER_CO, o.SUPPLIER_ID, o.PRICE, o.CUSTOMER_ROOT_CO_NAME, supplier.NAME AS SUPPLIER_NAME, supplier.ROOT_CO_ID, 
                      o.PROGRESS, o.EXPIRATION_DATE, O.REV, O.ID, O.[OBJECT_ID]
FROM         dbo.A_O_ORDERS o INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA_QUICK supplier ON o.SUPPLIER_ID = supplier.ID
WHERE     (o.PROGRESS = 'ALL_QUOTES_ACCEPTED')

--SELECT     '(' + o.CUSTOMER_ROOT_CO_NAME + ') ' + o.DESCRIPTION + ' [supplier: ' + supplier.NAME + ']' AS NAME, o.OBJECT_ID AS ORDER_ID, 
--                      o.CUSTOMER_CO, o.SUPPLIER_ID, o.PRICE, o.CUSTOMER_ROOT_CO_NAME, supplier.NAME AS SUPPLIER_NAME, supplier.ROOT_CO_ID, 
--                      o.PROGRESS, o.EXPIRATION_DATE
--FROM         dbo.A_O_ORDERS o INNER JOIN
--                      dbo.A_V_COMPANIES_APPROVED_DATA_QUICK supplier ON o.SUPPLIER_ID = supplier.ID
--WHERE     (o.PROGRESS = 'ALL_QUOTES_ACCEPTED')
