CREATE VIEW [dbo].[Portal_PartTypesView]
AS

SELECT     pth.Id, pth.NAME, pth.SPARE, pth.CONSUMABLE,  pth.UNIT, o.REV,o.STATUS,
                      o.LOCKED_BY_NAME as LockedByName
FROM         dbo.A_PART_TYPES_HISTORY pth INNER JOIN
                      dbo.A_OBJECTS o ON pth.OBJECT_ID = o.ID
GO
