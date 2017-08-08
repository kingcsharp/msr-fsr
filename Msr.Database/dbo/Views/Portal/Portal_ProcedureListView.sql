CREATE  VIEW [dbo].[Portal_ProcedureListView]
AS

SELECT  
phs.SPECIAL_ROOT as RootCompany,
phs.ID as Id,
phs.NAME as ProcedureName,
phs.SECURITY_LEVEL as SecurityClearanceLevel,
phs.CREATED_BY as CreatedBy,
phs.CREATING_CO_NAME as CreatingCompany,
phs.STATUS as ApprovalStatus,
phs.REV as Revision                         
FROM dbo.A_V_PROCEDURE_HISTORY_SEARCH AS phs 
