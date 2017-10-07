CREATE VIEW dbo.Portal_ApprovalGroupsView
AS
SELECT        OBJ_TABLE AS ObjectTable, OBJ_ID AS ObjId, ID AS Id, OBJ_DESC AS ObjDesc, DRCM AS Drcm, MODBY AS ModBy, CO_PART_NUM AS CoPartNum, PART_TYPE AS PartType, PART_CO AS PartCo, LOCKED_BY AS LockedBy, 
                         UNLOCKED_BY AS UnLockedBy, CREATED_BY AS CreatedBy, CREATE_DATE AS CreateDate, ROOT AS Root, REV_INFO AS RevInfo, CREATING_CO AS CreatingCo, STATUS AS Status, REV AS Rev, WFS_ID AS WfsId, 
                         NAME AS Name, HIDE AS Hide
FROM            dbo.A_V_WF_GROUPS
GO