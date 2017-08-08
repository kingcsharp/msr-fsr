create VIEW [dbo].[Portal_PeopleView]
AS

SELECT
ID As Id,
OBJ_ID AS ObjectId,
NAME AS FirstName,
LAST_NAME AS LastName,
FULL_NAME AS FullName,
TIME_ZONE AS TimeZone,
PRIMARY_PHONE_NUMBER AS PrimaryPhone,
WORK_EMAIL_ADDRESS AS Email,
LOGIN AS Login,
STATUS AS Status,
COMPANY_NAME AS CompanyName,
POSITION_NAME AS TItle,
'ClientAdmin' AS RoleName,
Create_date AS CreatedDate
FROM A_V_PEOPLE_OBJECT_SEARCH