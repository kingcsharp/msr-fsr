-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Portal_SelectStageMemberGroups] 
	-- Add the parameters for the stored procedure here
	@ID nvarchar(50),
	@strNTLogin nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @OutPutTable TABLE(Id varchar(50) NULL, GroupName varchar(500) NULL, CreatingCo varchar(500) NULL, StageName varchar(500) NULL, StageId varchar(500) NULL, Hide varchar(500) NULL, GroupHide varchar(500) NULL)

	INSERT INTO @OutPutTable  EXEC A_SP_POPFILL_APPROVAL_GROUPS_BY_WF_STAGE @ID,@strNTLogin

	select * from @OutPutTable
END