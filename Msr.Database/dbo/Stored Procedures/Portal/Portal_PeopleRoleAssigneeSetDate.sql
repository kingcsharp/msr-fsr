CREATE PROCEDURE Portal_AssignCertificationRoles
	@StartDate datetime,
	@EndDate datetime,
	@Person varchar(50),
	@Role varchar(50)
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE dbo.A_ROLE_ASSIGNEE SET StartDate = @StartDate,Enddate =@EndDate where PERSON =@Person and ROLE =@Role
END
