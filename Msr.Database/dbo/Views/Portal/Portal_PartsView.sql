Create View [dbo].[Portal_PartsView]

AS
SELECT 
Id,
OBJ_ID As ObjId,
Name,
COMPANY_PART_NUMBER AS CompanyPartNumber,
COMPANY_NAME AS CompanyName,
REV AS Revision,
STATUS AS Status,
CREATING_CO As CreatingCo
FROM A_O_PARTS_HISTORY 

GO
