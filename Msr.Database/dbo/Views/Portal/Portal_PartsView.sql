Create View [dbo].[Portal_PartsView]

AS
SELECT        
ID, 
OBJ_ID AS ObjId, 
NAME, 
COMPANY_PART_NUMBER AS CompanyPartNumber, 
COMPANY_NAME AS CompanyName, 
REV AS Revision, 
STATUS AS Status, 
CREATING_CO AS CreatingCo, 
CONSUMABLE AS Consumable, 
SPARE AS Spare, 
UNIT AS Unit
FROM            dbo.A_O_PARTS_HISTORY

GO
