CREATE VIEW Portal_TrainingView

AS 
SELECT 
RA.ID,
RA.StartDate,
RA.EndDate,
PST.FULL_NAME AS FullName,
rv.RoleName AS PositionName,
CASE
    WHEN
      RA.EndDate >=  CAST(GetDate() AS DATE)
        THEN
          'Active'
    WHEN
      RA.EndDate <= CAST(GetDate() AS DATE)
        THEN
          'Deactive'
    ELSE
      'NA'
  END AS Status
FROM dbo.A_ROLE_ASSIGNEE AS RA
INNER JOIN dbo.A_PEOPLE_SEARCH_TABLE AS PST ON RA.PERSON = PST.OBJ_ID 
INNER JOIN Portal_RolesView rv ON rv.Id = ra.ROLE
WHERE RA.STATUS ='ACTIVE' 
AND RA.EndDate IS NOT NULL AND RA.StartDate IS NOT NULL