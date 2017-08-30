-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Portal_GetProcedureStepLabors 
	-- Add the parameters for the stored procedure here
	@ID varchar(50),
    @strNTLogin varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT ID AS Id, ROLE_NAME AS RoleName, PROCEDURE_ID AS ProcedureId,STEP_ID AS StepId,ROLE_ID AS RoleId,RELATIONSHIP AS RelationShip,LABOR_ROLE AS LaborRole,QTY AS Qty,QTY_TYPE AS QtyType,OBJ_REF_ID AS ObjRefId,OBJ_ID AS ObjId,OBJ_DESC AS ObjDesc FROM A_V_PROCEDURE_STEP_LABOR WHERE STEP_ID = @ID
END