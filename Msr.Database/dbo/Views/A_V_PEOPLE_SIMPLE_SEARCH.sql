


CREATE VIEW dbo.A_V_PEOPLE_SIMPLE_SEARCH
AS
SELECT     p.*, c.NAME AS COMPANY_NAME, ISNULL(boss.NAME, '') + ' ' + ISNULL(boss.LAST_NAME, '') AS BOSS_NAME
FROM         dbo.A_PEOPLE_HISTORY boss RIGHT OUTER JOIN
                      dbo.A_O_PEOPLE p ON boss.ID = p.BOSS LEFT OUTER JOIN
                      dbo.A_O_COMPANIES c INNER JOIN
                      dbo.A_ROLE_ASSIGNEE ca ON c.ID = ca.ROLE ON p.ID = ca.PERSON



