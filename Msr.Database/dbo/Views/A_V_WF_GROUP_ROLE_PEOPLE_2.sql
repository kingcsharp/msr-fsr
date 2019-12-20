
CREATE  VIEW [dbo].[A_V_WF_GROUP_ROLE_PEOPLE_2]
AS
SELECT     wfgrl.GROUP_ID, wfgrl.ROLE_ID, p.FULL_NAME, r.PERSON_ID AS PERSON
FROM         dbo.A_PERSON_ROLES r INNER JOIN
                      dbo.A_V_WF_GROUP_ROLE_LINK wfgrl ON r.ROLE_ID = wfgrl.ROLE_ID INNER JOIN
                      dbo.A_APPROVED_PEOPLE p ON r.PERSON_ID = p.ID