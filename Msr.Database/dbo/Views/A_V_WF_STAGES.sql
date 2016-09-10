




CREATE VIEW dbo.A_V_WF_STAGES
AS
SELECT     s.NAME, s.OBJECT_ID, s.ID, s.HIDE, o.LOCKED_BY, o.UNLOCKED_BY, o.CREATED_BY, o.CREATE_DATE, o.ROOT, o.REV_INFO, o.STATUS, 
                      o.CREATING_CO, o.REV, o.WFS_ID
FROM         dbo.A_OBJECTS o INNER JOIN
                      dbo.A_WF_STAGES s ON o.ID = s.OBJECT_ID





