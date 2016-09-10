


CREATE VIEW dbo.A_V_MESSAGES_SEARCH_DATA
AS
SELECT     m.ID, m.MESSAGE, m.SENDER, m.PARENT_ID, m.DATE_CREATED, m.STATUS, parent_message.MESSAGE AS PARENT_MESSAGE, m.DATE_SENT, 
                      dbo.A_FN_MESSAGES_HAS_CHILD_MESSAGE(m.ID) AS hasChild, sender_name.P_NAME AS SENDER_NAME, m.HIDE_MESSAGE, m.IMPORTANCE, 
                      m.TO_COUNT, m.TO_READ_COUNT, m.CC_COUNT, m.CC_READ_COUNT, m.TO_COUNT + ISNULL(m.CC_COUNT, 0) AS TOTAL_COUNT, 
                      m.TO_READ_COUNT + ISNULL(m.CC_READ_COUNT, 0) AS TOTAL_READ_COUNT, parent_message.SENDER AS PARENT_SENDER
FROM         dbo.A_MESSAGES parent_message RIGHT OUTER JOIN
                      dbo.A_MESSAGES m INNER JOIN
                      dbo.A_V_PEOPLE_BY_NTLOGIN sender_name ON m.SENDER = sender_name.P_ID ON parent_message.ID = m.PARENT_ID



