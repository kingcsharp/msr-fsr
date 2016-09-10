CREATE VIEW dbo.A_V_COMPANY_CONTACTS
AS
SELECT     co.NAME AS CO_NAME, cc.FIRST_NAME, cc.LAST_NAME, cc.PHONE, cc.COMPANY_ID, cc.CREATING_CO, cc.FULL_NAME, cc.ID, 
                      ISNULL('(' + co.NAME + ') ', '(No Co) ') + ISNULL(cc.FULL_NAME, 'No Name ') + ISNULL(' [' + cc.PHONE + ']', '') AS DISPLAY_DATA
FROM         dbo.A_COMPANY_CONTACTS cc INNER JOIN
                      dbo.A_COMPANIES co ON cc.COMPANY_ID = co.ID
