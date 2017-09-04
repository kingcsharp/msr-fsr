CREATE PROCEDURE [dbo].[Portal_HideApprovalStage]
	@Id varchar(50)
AS
UPDATE A_WF_STAGES SET HIDE = 1 WHERE ID = @Id