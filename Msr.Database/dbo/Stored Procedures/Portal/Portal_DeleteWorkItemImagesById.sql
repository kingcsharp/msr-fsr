CREATE PROCEDURE [dbo].[Portal_DeleteWorkItemImagesById]
	-- Add the parameters for the stored procedure here
	@fileLinkId nvarchar(50),
	@strNTLogin varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE A_ACTUAL_PARTS_RELATED_FILES SET  STATUS = 'DELETED',  DRCM = getDate(),  DATE_DELETED = getDate(),  DELETED_BY = @strNTLogin, MODBY = @strNTLogin WHERE ID = @fileLinkId
END

GO