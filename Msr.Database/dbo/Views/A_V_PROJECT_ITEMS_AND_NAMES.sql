


CREATE VIEW dbo.A_V_PROJECT_ITEMS_AND_NAMES
AS
SELECT     pil.PROJECT_ID, pil.ITEM_TYPE, pil.ITEM_ID, ISNULL(s.SUBJECT, N'') + ISNULL(m.MEETING_NAME, N'') + ISNULL(p.NAME, N'') + ISNULL(d.SUBJECT, 
                      N'') + ISNULL(t.DESCRIPTION, N'') + ISNULL(mes.MESSAGE, N'') AS ITEM_NAME, ISNULL(s.STATUS, '') + ISNULL(m.STATUS, '') + ISNULL(p.STATUS, '') 
                      + ISNULL(d.STATUS, '') + ISNULL(t.STATUS, '') AS ITEM_STATUS
FROM         dbo.A_PROJECT_ITEM_LINK pil LEFT OUTER JOIN
                      dbo.A_TASKS t ON pil.ITEM_ID = t.ID LEFT OUTER JOIN
                      dbo.A_DISCUSSIONS d ON pil.ITEM_ID = d.ID LEFT OUTER JOIN
                      dbo.A_MEETINGS m ON pil.ITEM_ID = m.ID LEFT OUTER JOIN
                      dbo.A_PROJECTS p ON pil.ITEM_ID = p.ID LEFT OUTER JOIN
                      dbo.A_SURVEYS s ON pil.ITEM_ID = s.ID LEFT OUTER JOIN
                      dbo.A_MESSAGES mes ON pil.ITEM_ID = mes.ID



