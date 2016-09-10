




CREATE VIEW dbo.A_APPROVED_PART_TYPES
AS
SELECT     pt.ID, pt.HISTORY_REF_ID, pth.NAME, pth.SPARE, pth.CONSUMABLE, pth.OBJECT_ID, pth.UNIT, pth.UNIT_SHIPPING_WEIGHT, o.LOCKED_BY, 
                      o.UNLOCKED_BY, o.CREATED_BY, o.CREATE_DATE, o.ROOT, o.REV_INFO, o.CREATING_CO, o.STATUS, o.REV, o.WFS_ID, o.LOCKED_BY_NAME, 
                      o.CREATING_CO_NAME, o.APPROVAL_ACTIVITY
FROM         dbo.A_OBJECTS o INNER JOIN
                      dbo.A_PART_TYPES_HISTORY pth ON o.ID = pth.OBJECT_ID INNER JOIN
                      dbo.A_PART_TYPES pt ON pth.ID = pt.HISTORY_REF_ID





