CREATE VIEW [dbo].[Portal_LocationsView]
	AS
	SELECT        ID AS Id, NAME AS Name, PARENT_LOCATION AS ParentLocation, PARENT_LOCATION_NAME AS ParentLocationName, ADDRESS_1 AS Address1, ADDRESS_2 AS Address2, FULL_ADDRESS AS FullAddress, 
						 CITY AS City, STATE AS State, COUNTRY AS Country, POSTAL_CODE AS PostalCode, REGION AS Region, REGION_NAME AS RegionName, INTERNAL_ADDRESS AS InternalAddress, OBJECT_ID AS ObjectId, 
						 DRCM AS Drcm, MODBY AS ModBy, PARENT_PATH AS ParentPath, COMPLETE_NAME AS CompleteName, OBJ_ID AS ObjId, LOCKED_BY AS LockedBy, UNLOCKED_BY AS UnlockedBy, 
						 CREATED_BY AS CreatedBy, CREATE_DATE AS CreateDate, ROOT AS Root, REV_INFO AS RevInfo, CREATING_CO AS CreatingCo, STATUS AS Status, isChildLocation AS IsChildLocation, REV AS Revision, 
						 WFS_ID AS WfsId, LOCKED_BY_NAME AS LockedByName, CREATING_CO_NAME AS CreatingCoName
FROM            dbo.A_O_LOCATIONS
