CREATE VIEW dbo.Portal_ProductsActualPart
AS
SELECT DISTINCT 
                         ORDER_ID AS Id, ORDER_HIST_REF_ID AS OrderHistId, PRODUCT_ID AS ProductId, SUPPLIER_ID AS SupplierId, APP_OBJECT AS AppObject, DESCRIPTION, CUSTOMER_CO AS CustomerCo, 
                         CUSTOMER_PERSON AS CustomerPerson, BUDGETARY_ONLY AS BudgetaryOnly, EXPIRATION_DATE AS ExpirationDate, PRODUCT_NAME AS ProductName, CUSTOMER_NAME AS CustomerName, 
                         SUPPLIER_NAME AS SupplierName, PROGRESS, PROC_SYS_ID AS ProcSysId, SOURCE_ID AS SourceId, ADD_COST_ID AS AddCostId, PARENT, STATUS
FROM            dbo.A_V_ORDERS_WITH_ITEMS_AND_APPLICABLE_OBJECTS
GO

