CREATE PROCEDURE [dbo].[Portal_ProcedureStepGetRefFilesDialog]
	-- Add the parameters for the stored procedure here
	@procStepID varchar(50),
    @strNTLogin varchar(50)

AS
BEGIN

	SET NOCOUNT ON;

		
       SELECT NAME AS Show, DOC_ID AS Value,SERVER_PATH as ServerPath FROM A_V_PROCEDURE_STEP_DOCUMENT_DATA WHERE STEP_ID = @procStepID
END