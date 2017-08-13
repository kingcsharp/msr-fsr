CREATE VIEW [dbo].[Portal_PeopleObjectSearchView]
	AS
SELECT     p.ID as Id,p.NAME as FirstName,p.LAST_NAME as LastName,
p.POSITION_NAME as PositionName,p.BOSS_NAME as BossName,p.COMPANY_NAME as CompanyName,p.LOCATION_NAME as LocationName,
p.PRIMARY_PHONE_NUMBER as PrimaryPhoneNumber,p.SECONDARY_PHONE_NUMBER as SecondaryPhoneNumber,p.WORK_EMAIL_ADDRESS as WorkEmailAddress,
p.SYSTEM_STATUS as SystemStatus,
					  LTRIM(STR(YEAR(p.HIRE_DATE))) + '-' + dbo.leadingZeros(LTRIM(STR(MONTH(p.HIRE_DATE))), 2) 
                      + '-' + dbo.leadingZeros(LTRIM(STR(DAY(p.HIRE_DATE))), 2) AS DateHired,
                       o.STATUS AS Status, o.REV AS Rev,
					   dbo.A_FN_FILES_SHOW_ALL_XML_DATA_FOR_OBJECT(p.OBJ_ID, 
                      'PICTURE') AS PicRecord,
					  dbo.A_V_COMPANIES_APPROVED_DATA.NAME AS RootCoName
FROM         dbo.A_OBJECTS o INNER JOIN
                      dbo.A_PEOPLE_SEARCH_TABLE p ON o.ID = p.OBJ_ID INNER JOIN
                      dbo.A_PEOPLE_HISTORY ON p.ID = dbo.A_PEOPLE_HISTORY.ID LEFT OUTER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA ON dbo.A_PEOPLE_HISTORY.ROOT_COMPANY = dbo.A_V_COMPANIES_APPROVED_DATA.ID