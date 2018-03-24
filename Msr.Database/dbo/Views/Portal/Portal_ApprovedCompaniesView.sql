CREATE VIEW dbo.Portal_ApprovedCompaniesView
AS
SELECT DISTINCT 
                         ID AS Id, HISTORY_REF_ID AS HistoryRefId, NAME AS Name, CO_TYPE AS CoType, PARENT AS Parent, PHONE AS Phone, LOCATION AS Location, LOCATION_NAME AS LocationName, DRCM AS Drcm, MODBY AS ModBy, 
                         OBJECT_ID AS ObjectId, LOCKED_BY AS LockedBy, UNLOCKED_BY AS UnLockedBy, CREATED_BY AS CreatedBy, CREATE_DATE AS CreateDate, REV_INFO AS RevInfo, CREATING_CO AS CreatingCo, STATUS AS Status, 
                         REV AS Rev, WFS_ID AS WfsId, LOCKED_BY_NAME AS LockedByName, CREATING_CO_NAME AS CreatingCoName, APPROVAL_ACTIVITY AS ApprovalActivity, GENERAL_STATUS AS GeneralStatus, 
                         PARENT_NAME AS ParentName, ROOT_CO_NAME AS RootCoName, ROOT_CO_ID AS RootCoId
FROM            dbo.A_APPROVED_COMPANIES
GO