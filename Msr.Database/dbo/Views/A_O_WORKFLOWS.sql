



CREATE VIEW dbo.A_O_WORKFLOWS
AS
SELECT     w.NAME, w.OBJECT_ID, w.ID, w.HIDE, o.LOCKED_BY, o.UNLOCKED_BY, o.CREATED_BY, o.CREATE_DATE, o.ROOT, o.REV_INFO, o.STATUS, 
                      o.CREATING_CO, o.REV, o.WFS_ID, w.STAMP_NAME, w.STAMP_ID
FROM         dbo.A_OBJECTS o INNER JOIN
                      dbo.A_WORKFLOWS w ON o.ID = w.OBJECT_ID




