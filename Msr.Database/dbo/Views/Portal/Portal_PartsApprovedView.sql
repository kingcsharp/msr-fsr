CREATE VIEW dbo.Portal_PartsApprovedView
AS
SELECT        ID AS Id, PARTS_HISTORY_ID AS PartsHistoryId, UNIT AS Unit, COMPANY_PART_NUMBER AS ComapnyPartNumber, NAME AS Name, PART_TYPE AS PartType, SPARE AS Spare, CONSUMABLE AS Consumable, 
                         TRACK_FROM_START AS TrackFromStart, DRCM AS Drcm, MODBY AS ModBy, COMPANY AS Comapny, OBJECT_ID AS ObjectId, PART_TYPE_NAME AS PartTypeName, UNIT_SHIPPING_WEIGHT AS UnitShippingWeight, 
                         CREATING_CO AS CreatingCo, WEIGHT_TYPE AS WeightType, CUSTOMER_SEE_AVAILABILITY AS CustomerSeeAvailability, SUPPLIER_SEE_AVAILABILITY AS SupplierSeeAvailability, 
                         SUPPLIER_SEE_INSTALL_BASE AS SupplierSeeInstallBase, ROOT_CO_NAME AS RootCoName, COMPANY_NAME AS ComapnyName, WT_TYPE_NAME AS WtTypeName, STATUS AS Status, OBJECT_ID AS ObjId
FROM            dbo.A_V_PART_DATA_BY_APPROVED_DATA
GO