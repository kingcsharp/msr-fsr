CREATE PROCEDURE [dbo].[Portal_DeleteWorkItemImagesById]
	@fileLinkId nvarchar(50),
	@strNTLogin varchar(50)
AS
BEGIN
	
	SET NOCOUNT ON;

	UPDATE A_TASK_REF_FILES SET  STATUS = 'DELETED',  DRCM = getDate(),  DATE_DELETED = getDate(),  DELETED_BY = @strNTLogin, DELETED_BY_NAME = (SELECT FULL_NAME FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @strNTLogin), MODBY = @strNTLogin WHERE ID = @fileLinkId
END

GO