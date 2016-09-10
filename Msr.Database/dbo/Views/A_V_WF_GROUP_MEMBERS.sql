




CREATE VIEW dbo.A_V_WF_GROUP_MEMBERS
AS
SELECT     g.NAME AS GROUP_NAME, g.ID AS GROUP_ID, g.OBJECT_ID, p.NAME + ' ' + p.LAST_NAME AS PERSON_NAME, p.MIDDLE_NAME, 
                      pl.USER_ID AS PERSON_ID
FROM         dbo.A_WF_GROUPS g INNER JOIN
                      dbo.A_WF_GROUP_PEOPLE_LINK pl ON g.ID = pl.WF_GROUP_ID INNER JOIN
                      dbo.A_APPROVED_PEOPLE p ON pl.USER_ID = p.ID





