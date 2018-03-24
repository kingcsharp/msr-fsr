CREATE VIEW dbo.Portal_PartTypesApprovedVIew
AS
SELECT        ID AS Id, HISTORY_REF_ID AS HistoryRefId, NAME AS Name, SPARE AS Spare, CONSUMABLE AS Consumable, OBJECT_ID AS ObjectId, UNIT AS Unit, UNIT_SHIPPING_WEIGHT AS UnitShippingWeight, 
                         LOCKED_BY AS LockedBy, UNLOCKED_BY AS UnLockedBy, CREATED_BY AS CreatedBy, CREATE_DATE AS CreateDate, ROOT AS Root, REV_INFO AS RevInfo, CREATING_CO AS CreatingCo, STATUS AS Status, REV AS Rev, 
                         WFS_ID AS WfsId, LOCKED_BY_NAME AS LockedByName, CREATING_CO_NAME AS CreatingCoName, APPROVAL_ACTIVITY AS ApprovalActivity
FROM            dbo.A_APPROVED_PART_TYPES
GO