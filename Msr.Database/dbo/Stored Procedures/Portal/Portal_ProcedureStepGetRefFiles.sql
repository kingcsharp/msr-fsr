-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Portal_ProcedureStepGetRefFiles
	-- Add the parameters for the stored procedure here
	@procStepID varchar(50),
    @strNTLogin varchar(50)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--real store procedure was A_SP_PROCEDURE_STEP_GET_REFERENCE_FILES

    -- Insert statements for procedure here
		
       SELECT NAME AS Name, DOC_ID AS Id FROM A_V_PROCEDURE_STEP_DOCUMENT_DATA WHERE STEP_ID = @procStepID
END