CREATE VIEW dbo.Portal_PartTypesView
AS
SELECT        LOCKED_BY AS LockedBy, UNLOCKED_BY AS UnlockedBy, CREATED_BY AS CreatedBy, CREATE_DATE AS CreatedDate, ROOT AS Root, REV_INFO AS RevlInfo, CREATING_CO AS CreatingCo, STATUS AS Status,
                          REV AS Rev, WFS_ID AS WfsId, LOCKED_BY_NAME AS LockedByName, CREATING_CO_NAME AS CreatingCoName, APPROVAL_ACTIVITY AS ApprovalActivity, ID AS Id, NAME AS Name, DRCM AS Drcm, 
                         MODBY AS ModBy, OBJECT_ID AS ObjectId, UNIT AS Unit, UNIT_SHIPPING_WEIGHT AS UnitShippingWeight, SPARE AS Spare, CONSUMABLE AS Consumable
FROM            dbo.A_O_PART_TYPES_HISTORY
GO

GO
