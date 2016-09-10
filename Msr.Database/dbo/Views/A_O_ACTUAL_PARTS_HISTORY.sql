CREATE VIEW dbo.A_O_ACTUAL_PARTS_HISTORY
AS
SELECT     partHistory.ID, partHistory.NICK_NAME, partHistory.SERIAL, PAP.ID + ' ' + ISNULL(PAP.NICK_NAME + N'-', N'') + ISNULL('(' + PAP.SERIAL + ')', '') 
                      AS PARENT_NAME, partHistory.LOCATION, partHistory.OBJECT_ID, partHistory.MERGABLE, partHistory.PARENT_ID, partHistory.PART_ID, 
                      partHistory.QTY, partHistory.CUR_OWNER, partHistory.ASSEMBLY_WT, partHistory.AP_STATUS, partHistory.ROOT_ID, partHistory.ROOT_STATUS, 
                      partInfo.PART_TYPE, partInfo.PART_TYPE_NAME, partInfo.NAME AS PART_DESC, partInfo.UNIT, partInfo.SUPPLIER_SEE_INSTALL_BASE, 
                      partInfo.SUPPLIER_SEE_AVAILABILITY, partInfo.CUSTOMER_SEE_AVAILABILITY, 
                      dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK.OBJECT_ID AS LOCATION_OBJECT_ID, partInfo.COMPANY_PART_NUMBER, 
                      dbo.A_OBJECTS.LOCKED_BY, dbo.A_OBJECTS.UNLOCKED_BY, dbo.A_OBJECTS.CREATED_BY, dbo.A_OBJECTS.CREATE_DATE, 
                      dbo.A_OBJECTS.ROOT, dbo.A_OBJECTS.REV_INFO, dbo.A_OBJECTS.CREATING_CO, dbo.A_OBJECTS.STATUS, dbo.A_OBJECTS.REV, 
                      dbo.A_OBJECTS.WFS_ID, dbo.A_OBJECTS.LOCKED_BY_NAME, dbo.A_OBJECTS.CREATING_CO_NAME, partHistory.DRCM, partHistory.MODBY, 
                      dbo.A_OBJECTS.APPROVAL_ACTIVITY, dbo.A_OBJECTS.APPROVAL_DATE, 
                      dbo.A_V_COMPANIES_APPROVED_DATA_QUICK.NAME AS CURRENT_OWNER_NAME, partHistory.HAS_CHILD, partHistory.RESPONSIBLE_PERSON, 
                      dbo.A_V_PEOPLE_DATA_QUICK.FULL_NAME AS RESP_PERSON_FULL_NAME, 
                      dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK.NAME AS LOCATION_NAME
FROM         dbo.A_ACTUAL_PARTS_HISTORY partHistory INNER JOIN
                      dbo.A_PARTS ON partHistory.PART_ID = dbo.A_PARTS.ID INNER JOIN
                      dbo.A_PARTS_HISTORY partInfo ON dbo.A_PARTS.PARTS_HISTORY_ID = partInfo.ID INNER JOIN
                      dbo.A_OBJECTS ON partHistory.OBJECT_ID = dbo.A_OBJECTS.ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA_QUICK ON partHistory.CUR_OWNER = dbo.A_V_COMPANIES_APPROVED_DATA_QUICK.ID LEFT OUTER JOIN
                      dbo.A_V_PEOPLE_DATA_QUICK ON partHistory.RESPONSIBLE_PERSON = dbo.A_V_PEOPLE_DATA_QUICK.ID LEFT OUTER JOIN
                      dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK ON partHistory.LOCATION = dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK.ID LEFT OUTER JOIN
                      dbo.A_V_ACTUAL_PARTS_APPROVED_DATA_QUICK PAP ON partHistory.PARENT_ID = PAP.ID
