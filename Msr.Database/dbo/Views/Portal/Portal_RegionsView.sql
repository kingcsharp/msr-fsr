CREATE VIEW [dbo].[Portal_RegionsView]
AS
SELECT        ID AS Id, NAME AS Name, MODBY AS ModBy, DRCM AS Drcm, OBJECT_ID AS ObjectId, OBJ_ID AS ObjId, LOCKED_BY AS LockedBy, UNLOCKED_BY AS UnlockedBy, CREATED_BY AS CreatedBy, 
                         CREATE_DATE AS CreatedDate, ROOT AS Root, REV_INFO AS RevInfo, CREATING_CO AS CreatedCo, STATUS AS Status, REV AS Rev, WFS_ID AS WfsId, LOCKED_BY_NAME AS LockedByName, 
                         CREATING_CO_NAME AS CreatingCoName
FROM            dbo.A_O_REGIONS
