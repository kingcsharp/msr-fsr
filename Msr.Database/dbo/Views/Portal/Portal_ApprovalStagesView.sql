CREATE VIEW dbo.Portal_ApprovalStagesView
AS
SELECT        WF_GROUP_ID AS WfGroupId, GROUP_NAME AS GroupName, CREATING_CO AS CreatingCo, STAGE_NAME AS StageName, STAGE_ID AS Id, HIDE AS Hide, GROUP_HIDE AS GroupHide
FROM            dbo.A_V_WF_STAGES_WITH_GROUPS
GO
