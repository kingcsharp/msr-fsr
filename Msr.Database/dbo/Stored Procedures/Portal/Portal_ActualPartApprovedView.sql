CREATE VIEW dbo.Portal_ActualPartApprovedView
AS
SELECT        ID AS Id, HISTORY_REF_ID AS HistoryRefId, OBJECT_ID AS ObjectId, LOCATION AS Location, NICK_NAME AS NickName, MERGABLE AS Mergable, PARENT_ID AS ParentId, PART_ID AS PartId, QTY AS Qty, SERIAL AS Serial, 
                         CUR_OWNER AS CurOwner, ASSEMBLY_WT AS AssemblyWt, AP_STATUS AS ApStatus, ROOT_ID AS RootId, ROOT_STATUS AS RootStatus, MODIFIED AS Modified, SYS_NAME AS SysName, PREV_OWNER AS PrevOwner, 
                         SUB_PART_ACTION AS SubPartAction, CURRENT_OWNER_NAME AS CurrentOwnerName, PART_DESC AS PartDesc, PART_TYPE_NAME AS PartTypeName, LOCATION_NAME AS LocationName, NAME AS Name, 
                         STATUS AS Status, COMPANY_PART_NUMBER AS ComapnyPartNumber, HAS_CHILD AS HasChild, RESPONSIBLE_PERSON AS ResponsiblePerson
FROM            dbo.A_V_ACTUAL_PARTS_APPROVED_DATA
GO