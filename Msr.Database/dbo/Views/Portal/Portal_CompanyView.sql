CREATE VIEW [dbo].Portal_CompanyView
AS

SELECT        
ID AS Id, 
EXTERNAL_ID AS ExternalId, 
NAME AS Name, 
CO_TYPE AS CoType, 
OBJECT_ID AS ObjectId, 
STATUS AS Status, 
LOCKED_BY AS LockedBy, 
UNLOCKED_BY AS UnlockedBy, 
CREATED_BY AS CreatedBy, 
CREATING_CO AS CreatingCo, 
REV AS Rev, 
WFS_ID AS WfsID, 
LOCKED_BY_NAME AS LockedByName, 
ROOT AS Root, 
CHILDREN_COUNT AS ChildrenCount, 
TOP_COMPANY AS TopCompany, 
picRecord AS PicRecord, 
ROOT_CO_NAME AS RootCoName, 
PARENT_NAME AS ParentName,
PARENT as Parent,
PHONE AS Phone,
LOCATION AS Location,
LOCATION_NAME AS LocationName,
DRCM 
FROM dbo.A_O_COMPANIES A_V_COMPANIES_APPROVED_DATA

GO