CREATE PROCEDURE [dbo].[Portal_HideApprovalWorkflow]
	@Id varchar(50)
AS
UPDATE A_WORKFLOWS SET HIDE = 1 WHERE ID = @Id