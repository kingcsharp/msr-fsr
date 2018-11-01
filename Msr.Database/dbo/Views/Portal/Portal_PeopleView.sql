CREATE VIEW [dbo].[Portal_PeopleView]
AS
select
pb.ID as Id,
pb.OBJ_ID AS ObjectId,
pb.NAME AS FirstName,
p.PASSWORD as Password,
pb.LAST_NAME AS LastName,
pb.FULL_NAME AS FullName,
pb.TIME_ZONE AS
TimeZone,
pb.PRIMARY_PHONE_NUMBER AS PrimaryPhone,
pb.WORK_EMAIL_ADDRESS AS Email,
pb.LOGIN as Login,
pb.STATUS as Status, 
pb.COMPANY_NAME AS CompanyName,
pb.POSITION_NAME AS TItle,
'ClientAdmin' AS RoleName,
pb.CREATE_DATE AS CreatedDate,
ae.ADDY AS EmailAddress
FROM dbo.A_V_PEOPLE_OBJECT_SEARCH AS pb 
INNER JOIN A_PEOPLE_HISTORY AS p on p.OBJECT_ID=pb.OBJ_ID
INNER JOIN dbo.A_EMAILS ae ON p.OBJECT_ID = ae.OBJECT_ID