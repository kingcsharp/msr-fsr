CREATE VIEW [dbo].[Portal_PeopleObjectSearchView]
	AS
SELECT        p.ID, p.NAME AS FirstName, p.LAST_NAME AS LastName, p.POSITION_NAME AS PositionName, p.BOSS_NAME AS BossName, p.COMPANY_NAME AS CompanyName, p.LOCATION_NAME AS LocationName, 
                         p.PRIMARY_PHONE_NUMBER AS PrimaryPhoneNumber, p.SECONDARY_PHONE_NUMBER AS SecondaryPhoneNumber, p.WORK_EMAIL_ADDRESS AS WorkEmailAddress, p.SYSTEM_STATUS AS SystemStatus, 
                         LTRIM(STR(YEAR(p.HIRE_DATE))) + '-' + dbo.leadingZeros(LTRIM(STR(MONTH(p.HIRE_DATE))), 2) + '-' + dbo.leadingZeros(LTRIM(STR(DAY(p.HIRE_DATE))), 2) AS DateHired, o.STATUS, o.REV, 
                         dbo.A_FN_FILES_SHOW_ALL_XML_DATA_FOR_OBJECT(p.OBJ_ID, 'PICTURE') AS PicRecord, dbo.A_V_COMPANIES_APPROVED_DATA.NAME AS RootCoName, p.OBJ_ID AS ObjectId, 
                         dbo.A_PEOPLE_HISTORY.SCREEN_TYPE AS ScreenType, dbo.A_PEOPLE_HISTORY.LANG AS LanguageId, dbo.A_PEOPLE_HISTORY.IS_HEAD AS IsHead, dbo.A_PEOPLE_HISTORY.TIME_ZONE AS TimeZone
FROM            dbo.A_OBJECTS AS o INNER JOIN
                         dbo.A_PEOPLE_SEARCH_TABLE AS p ON o.ID = p.OBJ_ID INNER JOIN
                         dbo.A_PEOPLE_HISTORY ON p.ID = dbo.A_PEOPLE_HISTORY.ID LEFT OUTER JOIN
                         dbo.A_V_COMPANIES_APPROVED_DATA ON dbo.A_PEOPLE_HISTORY.ROOT_COMPANY = dbo.A_V_COMPANIES_APPROVED_DATA.ID