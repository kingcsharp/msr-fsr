-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Portal_SelectListedFiles
	-- Add the parameters for the stored procedure here
	@ObjId varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select 
	A_V_FILES_SEARCH_BY_SUBORDINATE.NAME AS Name,
	A_DOCUMENT_LINK.LINKED_DOC_ID AS LinkedDocId

	from 

	A_DOCUMENT_LINK inner join A_V_FILES_SEARCH_BY_SUBORDINATE on A_DOCUMENT_LINK.LINKED_DOC_ID = A_V_FILES_SEARCH_BY_SUBORDINATE.ID 

	where A_DOCUMENT_LINK.OBJECT_ID =@ObjId
END