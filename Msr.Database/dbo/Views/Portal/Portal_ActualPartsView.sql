CREATE VIEW dbo.Portal_ActualPartsView
AS
SELECT        PH.ID, PH.NICK_NAME AS NickName, AP.SYS_NAME AS SysName, PH.SERIAL, PH.PARENT_NAME AS ParentName, PH.LOCATION, PH.OBJECT_ID AS ObjectId, PH.MERGABLE, PH.PARENT_ID AS ParentId, 
                         PH.PART_ID AS PartId, PH.QTY, PH.CUR_OWNER AS CurOwner, PH.ASSEMBLY_WT AS AssemblyWT, PH.AP_STATUS AS ApStatus, PH.ROOT_ID AS RootId, PH.ROOT_STATUS AS RootStatus, PH.PART_TYPE AS PartType, 
                         PH.PART_TYPE_NAME AS PartTypeName, PH.PART_DESC AS PartDesc, PH.UNIT, PH.SUPPLIER_SEE_INSTALL_BASE AS SupplierSeeInstallBase, PH.SUPPLIER_SEE_AVAILABILITY AS SupplierSeeAvaliability, 
                         PH.CUSTOMER_SEE_AVAILABILITY AS CustomerSeeAvailability, PH.LOCATION_OBJECT_ID AS LocationObjectId, PH.COMPANY_PART_NUMBER AS CompanyPartNumber, PH.LOCKED_BY AS LockedBy, 
                         PH.UNLOCKED_BY AS UnlockedBy, PH.CREATED_BY AS CreatedBy, PH.CREATE_DATE AS CreateDate, PH.ROOT, PH.REV_INFO AS RevInfo, PH.CREATING_CO AS Creating, PH.STATUS, PH.REV, PH.WFS_ID AS WfsId, 
                         PH.LOCKED_BY_NAME AS LockedByName, PH.CREATING_CO_NAME AS CreatingCoName, PH.DRCM, PH.MODBY, PH.APPROVAL_ACTIVITY AS ApprovalActivity, PH.APPROVAL_DATE AS ApprovalDate, 
                         PH.CURRENT_OWNER_NAME AS CurrentOwnerName, PH.HAS_CHILD AS HasChild, PH.RESPONSIBLE_PERSON AS ResponsibleName, PH.RESP_PERSON_FULL_NAME AS RespPersonFullName, 
                         PH.LOCATION_NAME AS LocationName, PH.OBJECT_ID AS ObjId
FROM            dbo.A_O_ACTUAL_PARTS_HISTORY AS PH LEFT OUTER JOIN
                         dbo.A_V_ACTUAL_PARTS_APPROVED_DATA_QUICK AS AP ON PH.ID = AP.HISTORY_REF_ID
GO