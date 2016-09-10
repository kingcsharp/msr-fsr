



CREATE   FUNCTION getCompany(@strNTLogin nvarchar(50))
RETURNS nvarchar(50)
AS
BEGIN
	DECLARE @myCO nvarchar(50)
	SELECT @myCO = ROOT_COMPANY FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @strNTLogin
	RETURN @myCO
END






