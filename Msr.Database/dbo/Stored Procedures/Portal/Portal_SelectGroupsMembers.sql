-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Portal_SelectGroupsMembers] 
	-- Add the parameters for the stored procedure here
	@ID nvarchar(50),
	@strNTLogin nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @OutPutTable TABLE(GroupName varchar(500) NULL, Id varchar(50) NULL , ObjectId varchar(500) NULL, PersonName varchar(500) NULL, MiddleName varchar(500) NULL, PersonId varchar(500) NULL)

	INSERT INTO @OutPutTable  EXEC A_SP_POPFILL_APPROVAL_PEOPLE_BY_WF_GROUP @ID,@strNTLogin

	select PersonId from @OutPutTable
END