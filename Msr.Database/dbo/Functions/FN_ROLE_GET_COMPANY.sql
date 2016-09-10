
CREATE       FUNCTION dbo.FN_ROLE_GET_COMPANY(@roleID varchar(50))
RETURNS varchar(50)
AS
BEGIN
	declare @ret varchar(50)
	SELECT @ret = CREATING_CO FROM A_APPROVED_ROLES WHERE ID = @roleID
	return(@ret)
END

