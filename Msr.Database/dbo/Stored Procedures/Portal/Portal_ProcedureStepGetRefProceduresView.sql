CREATE PROCEDURE Portal_ProcedureStepGetRefProceduresView
	-- Add the parameters for the stored procedure here
	@ID varchar(50),
    @strNTLogin varchar(50)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
		DECLARE @OutPutTable TABLE(StepId varchar(50) NULL,ProcedureLink varchar(50) NULL,ProcName varchar(50) NULL,ProcObjId varchar(50) NULL)

	INSERT INTO @OutPutTable EXEC A_SP_PROCEDURE_STEP_GET_REF_PROCEDURES @ID, @strNTLogin
	
	SELECT ProcedureLink AS Value,ProcName AS Show from @OutPutTable where StepId=@ID
END