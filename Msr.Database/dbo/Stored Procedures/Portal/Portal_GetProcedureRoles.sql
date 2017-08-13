-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Portal_GetProcedureRoles
	-- Add the parameters for the stored procedure here
	@strID nvarchar(50),
	@strNTLogin nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @OutPutTable TABLE(ProcedureId varchar(50) NULL,RoleName varchar(500) NULL,ProcedureObjectId varchar(50) NULL,RoleId varchar(50) NULL,Relationship varchar(50) NULL)

	INSERT INTO @OutPutTable EXEC A_SP_PROCEDURES_GET_ROLES_TO_VIEW @strID,@strNTLogin
	
	SELECT RoleId from @OutPutTable
END